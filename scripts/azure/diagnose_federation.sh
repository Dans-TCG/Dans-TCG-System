#!/usr/bin/env bash
set -euo pipefail

# diagnose_federation.sh
# Purpose: Inspect and validate Azure AD (Entra ID) federated identity credentials used for GitHub Actions OIDC login.
# Detects missing or mismatched subject for environment-based deployments (e.g. environment: production) that cause AADSTS700213.

# Requirements:
# - Azure CLI logged in with permission to read the App Registration (Application.Read.All or owner) and create credentials if you choose to.
# - Environment variable AZURE_CLIENT_ID set to the Application (client) ID used in the GitHub workflow (azure/login@v2).
# - jq installed (optional but enhances formatting). The script will fall back gracefully.

# Usage examples:
#   AZURE_CLIENT_ID=<app_client_id> ./diagnose_federation.sh
#   AZURE_CLIENT_ID=<app_client_id> EXPECT_SUBJECT="repo:Dans-TCG/Dans-TCG-System:environment:production" ./diagnose_federation.sh
#   AZURE_CLIENT_ID=<app_client_id> CREATE_IF_MISSING=1 ./diagnose_federation.sh

EXPECT_SUBJECT="${EXPECT_SUBJECT:-repo:Dans-TCG/Dans-TCG-System:environment:production}"
EXPECT_ISSUER="${EXPECT_ISSUER:-https://token.actions.githubusercontent.com}"
AUDIENCE="${AUDIENCE:-api://AzureADTokenExchange}"
CREATE_IF_MISSING="${CREATE_IF_MISSING:-0}"

if ! command -v az >/dev/null 2>&1; then
  echo "[ERROR] Azure CLI (az) not found in PATH" >&2; exit 1;
fi

if [ -z "${AZURE_CLIENT_ID:-}" ]; then
  echo "[ERROR] AZURE_CLIENT_ID env var must be set." >&2
  echo "Usage: export AZURE_CLIENT_ID=00000000-0000-0000-0000-000000000000" >&2
  echo "(Do NOT include < or > characters.)" >&2
  exit 1
fi

# Detect common mistake of literally pasting placeholder with angle brackets which the shell
# interprets as redirection or stores invalid value
if echo "$AZURE_CLIENT_ID" | grep -Eq '[<>]'; then
  echo "[ERROR] AZURE_CLIENT_ID appears to contain angle brackets (< >). Provide the raw GUID only." >&2
  exit 1
fi

# Basic sanity check for GUID format (not exhaustive)
if ! echo "$AZURE_CLIENT_ID" | grep -Eq '^[0-9a-fA-F-]{32,36}$'; then
  echo "[WARN] AZURE_CLIENT_ID does not look like a standard GUID; continuing but it may fail." >&2
fi

echo "== Azure Account Context =="
if ! az account show -o table >/dev/null 2>&1; then
  echo "[ERROR] Not logged into Azure CLI. Run: az login" >&2; exit 1;
fi
az account show -o table || true

echo "\n== Resolving Application Object ID =="
APP_OBJ_ID=$(az ad app show --id "$AZURE_CLIENT_ID" --query id -o tsv 2>/dev/null || true)
if [ -z "$APP_OBJ_ID" ]; then
  echo "[INFO] Could not resolve via 'az ad app show'; trying service principal lookup..."
  APP_OBJ_ID=$(az ad sp show --id "$AZURE_CLIENT_ID" --query appId -o tsv 2>/dev/null || true)
fi
if [ -z "$APP_OBJ_ID" ]; then
  echo "[ERROR] Unable to resolve application object for client id $AZURE_CLIENT_ID" >&2; exit 1;
fi
echo "Application Object ID: $APP_OBJ_ID"

echo "\n== Existing Federated Credentials =="
FED_JSON=$(az ad app federated-credential list --id "$APP_OBJ_ID" -o json || echo '[]')

if command -v jq >/dev/null 2>&1; then
  echo "$FED_JSON" | jq -r '.[] | [ .name, .issuer, .subject, ( .audiences | join(",") ) ] | @tsv' | awk 'BEGIN{print "NAME\tISSUER\tSUBJECT\tAUDIENCES"} {print}'
else
  echo "$FED_JSON"
fi

echo "\n== Validation =="
MATCHED=$(echo "$FED_JSON" | grep -F '"subject":' | grep -F "$EXPECT_SUBJECT" || true)
if [ -n "$MATCHED" ]; then
  echo "[OK] Found credential with expected subject: $EXPECT_SUBJECT"
else
  echo "[FAIL] No credential with subject: $EXPECT_SUBJECT" >&2
  NEED_CREATE=1
fi

if [ -n "$MATCHED" ]; then
  # Further validate issuer & audience if jq available
  if command -v jq >/dev/null 2>&1; then
    ISSUER_OK=$(echo "$FED_JSON" | jq -r --arg s "$EXPECT_SUBJECT" --arg i "$EXPECT_ISSUER" '.[] | select(.subject==$s) | select(.issuer==$i) | .issuer' || true)
    AUD_OK=$(echo "$FED_JSON" | jq -r --arg s "$EXPECT_SUBJECT" --arg a "$AUDIENCE" '.[] | select(.subject==$s) | select(.audiences | index($a)) | .subject' || true)
    if [ -z "$ISSUER_OK" ]; then echo "[WARN] Issuer does not match expected ($EXPECT_ISSUER)"; fi
    if [ -z "$AUD_OK" ]; then echo "[WARN] Audience list does not include $AUDIENCE"; fi
  fi
fi

if [ "${CREATE_IF_MISSING}" = "1" ] && [ "${NEED_CREATE:-0}" = "1" ]; then
  echo "\n== Creating Federated Credential (github-env-production) =="
  TMP_FILE=$(mktemp)
  cat > "$TMP_FILE" <<EOF
{
  "name": "github-env-production",
  "issuer": "${EXPECT_ISSUER}",
  "subject": "${EXPECT_SUBJECT}",
  "audiences": ["${AUDIENCE}"]
}
EOF
  az ad app federated-credential create --id "$APP_OBJ_ID" --parameters @"$TMP_FILE"
  rm -f "$TMP_FILE"
  echo "[INFO] Federated credential created. Re-run script to verify."
fi

echo "\n== Suggested Fix (Manual) =="
echo "az ad app federated-credential create --id $APP_OBJ_ID --parameters '{\n  \"name\": \"github-env-production\",\n  \"issuer\": \"$EXPECT_ISSUER\",\n  \"subject\": \"$EXPECT_SUBJECT\",\n  \"audiences\": [\"$AUDIENCE\"]\n}'"

echo "\nDone."
