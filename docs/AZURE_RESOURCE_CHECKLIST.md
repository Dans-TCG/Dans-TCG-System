# Azure Resource Verification Checklist – Dans TCG System

Use this checklist to verify and document your Azure infrastructure setup.

---

## 1. Resource Group & Subscription
- [ ] Resource Group: `danstcg-rg`
- [ ] Subscription: `danstcg-subscription`

## 2. Core Resources
- [ ] App Service: `danstcg-webapp`
- [ ] App Service Plan: `danstcg-plan`
- [ ] Managed Identity: `danstcg-webapp-id-b366`
- [ ] Static Web Apps (Frontend): `danstcg-swa`
- [ ] Azure Database for PostgreSQL: `danstcg-postgres`
- [ ] Cosmos DB for MongoDB: `danstcg-cosmos`
- [ ] Storage Account: `danstcgstorage`
- [ ] Key Vault: `danstcg-kv`
- [ ] Application Insights: `danstcg-ai`
- [ ] Action Group: Application Insights Smart Detection

## 3. Tagging
- [ ] All resources tagged with `env`, `project`, and `owner`

## 4. Access & Identity
- [ ] Managed Identity assigned to App Service
- [ ] Managed Identity has access to Key Vault (Secrets Get/List)
- [ ] Managed Identity has access to Storage Account (Blob Data Reader/Contributor)
- [ ] Managed Identity has access to PostgreSQL (if using AAD auth)
- [ ] Managed Identity has access to Cosmos DB (if using AAD auth)

## 5. Key Vault Secrets
- [ ] `POSTGRES_CONNECTION_STRING`
- [ ] `COSMOS_CONNECTION_STRING`
- [ ] `BLOB_STORAGE_CONNECTION_STRING`
- [ ] `AZURE_AD_CLIENT_ID`
- [ ] `AZURE_AD_TENANT_ID`
- [ ] `AZURE_AD_CLIENT_SECRET`
- [ ] `EBAY_API_KEY`
- [ ] `AUSPOST_API_KEY`

## 6. App Service Configuration
- [ ] App Service references Key Vault secrets using `@Microsoft.KeyVault(...)`
- [ ] Application Insights instrumentation key set
- [ ] CORS allowed origins set via `FRONTEND_URLS` to include `https://danstcg.com,https://www.danstcg.com`
- [ ] Custom domain bound for API (e.g., `api.danstcg.com`) with TLS certificate

## 6b. Static Web Apps Configuration
- [ ] Custom domains added (e.g., `danstcg.com`, `www.danstcg.com`) and SSL certificates issued
- [ ] Routing/security via `staticwebapp.config.json` deployed

## 7. Entra ID (Azure AD) App Registrations
- [ ] Backend app registration created
- [ ] Frontend app registration created
- [ ] Redirect URIs and permissions configured
- [ ] Client IDs/secrets stored in Key Vault

## 8. Monitoring & Alerts
- [ ] Application Insights connected to App Service
- [ ] Action Group configured for error/downtime alerts

---

Update this checklist as you verify each item. Store in `/docs/` for future reference.
