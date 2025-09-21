# Dans TCG System – Setup Guide

This guide explains how to set up a development environment for the Dans TCG System project.

---

## 📥 Prerequisites
Make sure you have the following installed locally (if not using GitHub Codespaces):
- [Git](https://git-scm.com/)  
- [.NET 7 SDK](https://dotnet.microsoft.com/download)  
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
git clone https://github.com/your-org/dans-tcg-system.git
cd dans-tcg-system
```

---

## ⚙️ Infrastructure Setup (Azure)
Provision the following resources in Azure:
1. **Azure Web App (App Service)**  
   - Hosts the .NET backend API and React frontend  

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

For local development, use a `.env` file (don’t commit it):
```
POSTGRES_CONNECTION_STRING=...
COSMOS_CONNECTION_STRING=...
BLOB_STORAGE_CONNECTION_STRING=...
AZURE_AD_CLIENT_ID=...
AZURE_AD_TENANT_ID=...
AZURE_AD_CLIENT_SECRET=...
EBAY_API_KEY=...
AUSPOST_API_KEY=...
```

---

## 🖥️ Running the Backend
```bash
cd backend
dotnet restore
dotnet run
```
Backend runs on: `http://localhost:5000`

Swagger API docs: `http://localhost:5000/swagger`

---

## 🌐 Running the Frontend
```bash
cd frontend
npm install
npm start
```
Frontend runs on: `http://localhost:3000`

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
2. Deploys automatically to **staging slot** in Azure Web App.  
3. Manual promotion from **staging → production**.  

---

## ✅ Next Steps
- Ensure all Azure resources are provisioned.  
- Set environment variables or configure Key Vault secrets.  
- Run the backend and frontend locally to confirm connections.  
- Begin with **Phase 1 Sprint 1 (Infra & CI/CD)** setup.  

