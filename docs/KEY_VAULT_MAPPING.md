# Azure Key Vault Secret Mapping – Dans TCG System

This table maps application configuration variables to their corresponding Key Vault secret names.

| App Setting Name              | Key Vault Secret Name           | Example Value / Notes                  |
|-------------------------------|---------------------------------|----------------------------------------|
| POSTGRES_CONNECTION_STRING    | postgres-connection-string      | postgres://user:pass@host:5432/db     |
| COSMOS_CONNECTION_STRING      | cosmos-connection-string        | AccountEndpoint=...;AccountKey=...;   |
| BLOB_STORAGE_CONNECTION_STRING| blob-storage-connection-string  | DefaultEndpointsProtocol=...;...      |
| AZURE_AD_CLIENT_ID            | azure-ad-client-id              | Azure AD app registration client ID    |
| AZURE_AD_TENANT_ID            | azure-ad-tenant-id              | Azure AD tenant ID                     |
| AZURE_AD_CLIENT_SECRET        | azure-ad-client-secret          | Azure AD app registration secret       |
| EBAY_API_KEY                  | ebay-api-key                    | eBay API key                           |
| AUSPOST_API_KEY               | auspost-api-key                 | AusPost API key                        |

**Notes:**
- Use dash-case for Key Vault secret names for consistency.
- Reference secrets in App Service config as `@Microsoft.KeyVault(SecretUri=...)`.
- Update this mapping as new secrets are added.
