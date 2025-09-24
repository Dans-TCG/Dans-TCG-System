# Dans TCG System – Setup Guide

This guide explains how to set up a development environment for the Dans TCG System project.

---

## 📥 Prerequisites
Make sure you have the following installed locally (if not using GitHub Codespaces):
- [Git](https://git-scm.com/)  
- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- [Node.js (LTS)](https://nodejs.org/) + npm or yarn  
- [PostgreSQL](https://www.postgresql.org/) (local or Azure)  
- [Docker](https://www.docker.com/) (optional for local DBs)  

For cloud resources, you’ll need:
- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)  
- Access to an **Azure subscription**  
- Access to a **Microsoft Entra ID (Azure AD)** tenant  

---

## 🏗️ Cloning the Repo
```bash
git clone https://github.com/danstcg/Dans-TCG-System.git
cd Dans-TCG-System
```

---

## ⚙️ Infrastructure Setup (Azure)
Provision the following resources in Azure:
1. **Azure App Service (Backend API)**  
   - Hosts the .NET backend API  
   
1b. **Azure Static Web Apps (Frontend)**  
   - Hosts the React SPA (production)

2. **Azure Database for PostgreSQL**  
   - Business data (collection, inventory, orders, CRM, finance, tax)  

3. **CosmosDB (Mongo API)**  
   - MTG card metadata and prices  

4. **Azure Blob Storage**  
   - Card images, invoice PDFs, shipping labels, backups  

5. **Application Insights**  
   - Monitoring and error logging  

6. **Microsoft Entra ID (Azure AD)**  
   - Register apps for frontend and backend authentication  

---

## 🔑 Configuration

Store configuration as environment variables or in **Azure Key Vault**:

- `POSTGRES_CONNECTION_STRING`  
- `COSMOS_CONNECTION_STRING`  
- `BLOB_STORAGE_CONNECTION_STRING`  
- `AZURE_AD_CLIENT_ID`  
- `AZURE_AD_TENANT_ID`  
- `AZURE_AD_CLIENT_SECRET`  
- `EBAY_API_KEY`  
- `AUSPOST_API_KEY`  

For local development, copy `.env.sample` to `.env` and adjust values (don’t commit it):
```
POSTGRES_CONNECTION_STRING=Host=localhost;Port=5432;Username=dans;Password=password;Database=dantcg
COSMOS_CONNECTION_STRING=AccountEndpoint=...;AccountKey=...;
BLOB_STORAGE_CONNECTION_STRING=DefaultEndpointsProtocol=...;AccountName=...;AccountKey=...;
AZURE_AD_CLIENT_ID=...
AZURE_AD_TENANT_ID=...
AZURE_AD_CLIENT_SECRET=...
EBAY_API_KEY=...
AUSPOST_API_KEY=...
```

---

## 🧪 Test Data Seeding
If you need test/demo data, see `/backend/tests/` for scripts or conventions. Add your own seeders as needed for local development or CI.

---

## 🏷️ Azure Resource Naming
Follow these conventions for Azure resources (see also `SETUP.md`):

| Resource Type         | Example Name                |
|----------------------|----------------------------|
| Resource Group       | `dans-tcg-rg`              |
| App Service          | `dans-tcg-app`             |
| Postgres DB          | `dans-tcg-pg`              |
| CosmosDB             | `dans-tcg-cosmos`          |
| Blob Storage         | `danstcgstorage`           |
| Key Vault            | `dans-tcg-kv`              |

---

## 📖 API Documentation
When the backend is running, access Swagger/OpenAPI docs at:  
`http://localhost:8080/swagger`

---

## 🖥️ Running the Backend
```bash
cd backend
dotnet restore
dotnet run
```
Backend runs on: `http://localhost:8080`

Swagger API docs: `http://localhost:8080/swagger`

To run with a specific port, set `ASPNETCORE_URLS=http://localhost:8080` in your environment or `.env`.

---

## 🌐 Running the Frontend
```bash
cd frontend
npm install
npm start
```
Frontend runs on: `http://localhost:3000`

For production, the frontend is deployed to Azure Static Web Apps (e.g., `https://danstcg.com` / `https://www.danstcg.com`) and the API base should point to the custom domain on App Service (e.g., `https://api.danstcg.com`).

---

## 🐘 Local PostgreSQL Options

- Recommended: Use the included `docker-compose.yml` service named `postgres` (port 5432). The matching connection string is in `.env.sample`.
- If Docker is not available, install PostgreSQL locally and create a database/user that match the values in `.env.sample` or adjust the connection string accordingly.

### Applying EF Core migrations

If you can't reach a running Postgres instance yet, you can still generate an idempotent SQL script:

1. From `backend/DansTCG.API`, generate the script (already created during setup as `backend/DansTCG.Infrastructure/migrations.sql`).
2. Once Postgres is running, execute that SQL in your DB via your preferred client.

---

## 🧪 Testing
### Backend
```bash
cd backend
dotnet test
```

### Frontend
```bash
cd frontend
npm test
```

---

## 🚀 Deployment
1. Push changes to GitHub → triggers **GitHub Actions** CI/CD.  
2. Backend API deploys automatically to the Azure App Service production slot.  
3. Frontend deploys to Azure Static Web Apps.  

---

## ✅ Next Steps
- Ensure all Azure resources are provisioned.  
- Set environment variables or configure Key Vault secrets.  
- Run the backend and frontend locally to confirm connections.  
- Begin with **Phase 1 Sprint 1 (Infra & CI/CD)** setup.  

