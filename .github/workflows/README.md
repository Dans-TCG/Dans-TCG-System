# GitHub Workflows – CI/CD Guide

This repo includes separate workflows for backend CI, frontend CI, backend production deploy, backend staging deploy/swap, and frontend deploy.

## Workflows

- backend-ci.yml
	- Purpose: Build/test the backend (no deploy)
	- Triggers: push/PR affecting `backend/**`
	- Artifacts: `backend-publish`

- frontend-ci.yml
	- Purpose: Build/test the frontend (no deploy)
	- Triggers: push/PR affecting `frontend/**`
	- Artifacts: `frontend-dist`

- main_danstcg-webapp.yml (name: deploy-appservice-backend)
	- Purpose: Build and deploy the backend API to Azure App Service (Production slot)
	- Triggers: push to `main` affecting backend or this workflow
	- Parameterized with repo variable: `BACKEND_APP_NAME` (fallback: `danstcg-webapp`)

- backend-deploy-staging.yml
	- Purpose: Build the backend API, deploy to App Service `staging` slot, then swap to production
	- Triggers: push to `main` or manual dispatch
	- Uses Environments: `staging` (deploy), `production` (swap)
	- Parameterized with repo variables: `BACKEND_APP_NAME` (fallback: `danstcg-webapp`), `RESOURCE_GROUP` (fallback: `danstcg-rg`)

- frontend-deploy-swa.yml
	- Purpose: Build and deploy the frontend to Azure Static Web Apps
	- Triggers: push to `main` affecting frontend or this workflow
	- Requires secret `AZURE_STATIC_WEB_APPS_API_TOKEN`
	- Requires repo variables for Vite env injection:
		- `VITE_AZURE_AD_CLIENT_ID`
		- `VITE_AZURE_AD_TENANT_ID`
		- `VITE_AZURE_AD_BACKEND_CLIENT_ID`
		- `VITE_API_BASE` (e.g., `https://api.danstcg.com`)

## Required repository Variables (Settings → Secrets and variables → Actions → Variables)

- `BACKEND_APP_NAME` (e.g., `danstcg-webapp`) – App Service name for backend API
- `RESOURCE_GROUP` (e.g., `danstcg-rg`) – Azure Resource Group containing the App Service

For Frontend (used by frontend-deploy-swa.yml to generate .env.production):
- `VITE_AZURE_AD_CLIENT_ID`
- `VITE_AZURE_AD_TENANT_ID`
- `VITE_AZURE_AD_BACKEND_CLIENT_ID`
- `VITE_API_BASE` (e.g., `https://api.danstcg.com`)

Optional but recommended (CORS):
- `FRONTEND_URL` (e.g., `https://<your-frontend-host>`) for a single origin, or
- `FRONTEND_URLS` (comma-separated) for multiple origins, e.g., `https://www.danstcg.com,https://app.danstcg.com`
	- These are App Service application settings read by the API to allow browser calls from your frontend domains.

## Required repository Secrets (Settings → Secrets and variables → Actions → Secrets)

For App Service deploy (used by azure/login in backend deploy workflows):
- `AZURE_CLIENT_ID` – Client ID of the Entra ID app registration or managed identity you use for OIDC in GitHub Actions
- `AZURE_TENANT_ID` – Entra ID tenant ID
- `AZURE_SUBSCRIPTION_ID` – Azure subscription ID

How to add (GitHub UI):
1) Go to your repository → Settings → Secrets and variables → Actions → New repository secret
2) Create the three secrets: `AZURE_CLIENT_ID`, `AZURE_TENANT_ID`, `AZURE_SUBSCRIPTION_ID`

Where to get values:
- Tenant and Subscription IDs: Azure Portal → Subscriptions → select your subscription
- Client ID:
	- If using a user-assigned managed identity on App Service: Azure Portal → Managed Identities → your identity → Client ID
	- If using an App Registration for OIDC: Azure Portal → Microsoft Entra ID → App registrations → your app → Application (client) ID

For Static Web Apps deploy:
- `AZURE_STATIC_WEB_APPS_API_TOKEN` – Deployment token from your Static Web App (Azure Portal → Static Web Apps → Deployment tokens)

## Environments and approvals (recommended)

Set up two environments in GitHub (Settings → Environments):

- `staging`
	- Optional required reviewers before deploy (backend-deploy-staging deploy job targets this env)

- `production`
	- Add required reviewers to approve the slot swap step (backend-deploy-staging swap job targets this env)

## Notes

- The backend deploy workflows publish only `backend/DansTCG.API` and deploy that artifact.
- The staging flow auto-creates the `staging` slot if it does not exist and warms it up.
- Frontend deploy uses Azure Static Web Apps action; ensure the token secret is added.
- If you prefer deploying frontend to App Service instead of SWA, create an alternate workflow using `azure/webapps-deploy@v3` and deploy the `frontend/dist` folder to the appropriate site.

## Optional: set variables via GitHub CLI (if available)

If you have `gh` authenticated locally, you can set repo variables:

```bash
# Replace values accordingly
gh variable set BACKEND_APP_NAME --body "danstcg-webapp"
gh variable set RESOURCE_GROUP --body "danstcg-rg"

# For secrets (example for SWA token)
gh secret set AZURE_STATIC_WEB_APPS_API_TOKEN --body "<your-swa-deploy-token>"
```

# Placeholder for .github/workflows
# Place GitHub Actions workflow YAML files here.
