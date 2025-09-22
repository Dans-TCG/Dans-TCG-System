# Azure CLI Resource Export – Dans TCG System

To export your current Azure resource configuration for future automation or disaster recovery, use the following Azure CLI commands as a starting point. (Replace placeholders as needed.)

## Export Resource Group Template
```bash
az group export --name danstcg-rg --output json > danstcg-rg-template.json
```

## Export App Service Configuration
```bash
az webapp config appsettings list --name danstcg-webapp --resource-group danstcg-rg
```

## Export Key Vault Secrets
```bash
az keyvault secret list --vault-name danstcg-kv
```

## Export Storage Account Keys
```bash
az storage account keys list --account-name danstcgstorage --resource-group danstcg-rg
```

## Export Cosmos DB Connection String
```bash
az cosmosdb keys list --name danstcg-cosmos --resource-group danstcg-rg --type connection-strings
```

## Export PostgreSQL Connection String
```bash
az postgres flexible-server show-connection-string --name danstcg-postgres --admin-user <user> --database <db>
```

---

For full Infrastructure as Code, consider using Bicep or Terraform to define and manage your resources. See Azure docs for examples.
