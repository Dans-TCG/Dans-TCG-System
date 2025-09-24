Federated Identity Debug Quick Reference
========================================

GitHub Actions OIDC Subject Patterns
------------------------------------
Environment-based (used by current backend deploy job):
  repo:Dans-TCG/Dans-TCG-System:environment:production

Branch-based (alternative):
  repo:Dans-TCG/Dans-TCG-System:ref:refs/heads/main

Tag-based (example):
  repo:Dans-TCG/Dans-TCG-System:ref:refs/tags/v1.0.0

Audience (Azure AD Workload Identity):
  api://AzureADTokenExchange

Issuer:
  https://token.actions.githubusercontent.com

Troubleshooting Steps
---------------------
1. Ensure GitHub workflow job sets `permissions: id-token: write`.
2. Confirm the job uses `environment: production` if you registered an environment-based credential.
3. List existing federated credentials:
   az ad app federated-credential list --id <appObjectId> -o table
4. If missing, create one:
   az ad app federated-credential create --id <appObjectId> --parameters '{"name":"github-env-production","issuer":"https://token.actions.githubusercontent.com","subject":"repo:Dans-TCG/Dans-TCG-System:environment:production","audiences":["api://AzureADTokenExchange"]}'
5. Re-run workflow; Azure login should succeed.

Script Automation
-----------------
Use `scripts/azure/diagnose_federation.sh` for automated validation and (optionally) creation.
