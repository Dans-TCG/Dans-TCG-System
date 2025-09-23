# Copilot Instructions for Dans TCG System

## Project Overview
Dans TCG System is a full-stack platform for managing and selling Magic: The Gathering (MTG) cards. It covers collection tracking, inventory, order fulfilment, POS/invoicing, CRM, finance, and tax compliance. The system integrates with eBay, MTGJSON/Scryfall, AusPost, and uses Azure for hosting and storage.

## Architecture & Structure
- **Backend:** ASP.NET Core (.NET 8, LTS), layered (API → Core → Infrastructure)  
  - `backend/DansTCG.API/` – API controllers (see `HealthController.cs`)  
  - `backend/DansTCG.Core/` – Business logic  
  - `backend/DansTCG.Infrastructure/` – DB and external integrations  
- **Frontend:** React (TypeScript)  
  - `frontend/src/` – Components, pages, services  
- **Databases:**  
  - **Postgres:** Business data (inventory, orders, CRM, finance, tax)  
  - **CosmosDB (Mongo API):** MTG card metadata, prices  
  - **Azure Blob Storage:** Card images, invoices, shipping labels, backups (organized into containers)  
- **Authentication:** Microsoft Entra ID (Azure AD)  
- **CI/CD:** GitHub Actions, deploys to Azure App Service (API, staging slot → production slot) and Azure Static Web Apps (frontend)

## Key Workflows
- **Backend:**  
  - Restore & run: `cd backend && dotnet restore && dotnet run`  
  - Test: `cd backend && dotnet test`  
  - Add migration: `dotnet ef migrations add <name>`  
- **Frontend:**  
  - Install & run: `cd frontend && npm install && npm start`  
  - Test: `cd frontend && npm test`  
- **Environment:**  
  - Local: use `.env` for secrets (see `SETUP.md` for required vars)  
  - Codespaces: secrets injected automatically from GitHub Codespaces config  
  - Production: Azure Key Vault, surfaced via App Service references (`@Microsoft.KeyVault(...)`)  
- **Deployment:**  
  - Push/PR to `main` triggers CI/CD via GitHub Actions  
  - Backend API → Azure App Service staging slot, then swap to production  
  - Frontend → Azure Static Web Apps

## Conventions & Patterns
- **Branching:**  
  - `main` – production branch  
  - `feature/*` – feature branches, merged into main via PRs  
- **Testing:**  
  - Unit tests for business logic  
  - Integration tests for external APIs  
  - E2E tests for critical workflows (card import → order → invoice → tax record)  
- **Code Style:**  
  - Backend: C#/.NET conventions, DI preferred, Swagger for API docs  
  - Frontend: TypeScript, functional components, hooks, ESLint + Prettier  
- **Commit Messages:**  
  - Use `feat:`, `fix:`, `docs:`, etc. (see `CONTRIBUTING.md`)  
- **Docs:**  
  - All docs in `/docs/` (see `README.md`, `SETUP.md`, `RUNBOOK.md`, `DATABASE_SCHEMA.md`)  

## Integration Points
- **eBay API:** Inventory sync, order import  
- **MTGJSON/Scryfall:** Card data  
- **AusPost API:** Shipping labels  
- **Azure Services:** Web App, Postgres, CosmosDB, Blob Storage, Application Insights, Entra ID  

## API & Data Model
- See `API_REFERENCE.md` for endpoint design  
- See `DATABASE_SCHEMA.md` for table/collection structure  

## CI/CD & Ops
- GitHub Actions for build/test/deploy (see `.github/workflows/`)  
- Application Insights for monitoring (alerts for errors, latency, DB failures)  
- Backups: Automated via Azure (see `RUNBOOK.md`)  
- Logs: via Application Insights + `az webapp log tail`  

## Examples
- To add a new API endpoint: implement in `DansTCG.API/Controllers/`, add business logic in `DansTCG.Core/`, update Swagger docs  
- To add a new frontend feature: create a component in `frontend/src/`, follow feature-based folder structure  
- To add a new secret: store in Key Vault with **dash-name** (e.g. `postgres-connection-string`), reference in App Service as `POSTGRES_CONNECTION_STRING`  

## Additional Notes

- See `PROJECT_STRUCTURE.md` for canonical directory layout and file placement.
- `/docs/` is the canonical location for all documentation. Place new docs here.
- **Onboarding:** Review `ONBOARDING.md` for step-by-step setup and contribution guidance before making code changes.
- **Glossary:** See `GLOSSARY.md` for domain-specific terms and acronyms.
- **Architecture Diagram:** See `/docs/architecture-diagram.txt` (replace with PNG/SVG as available) for a high-level system overview.
- **Test Data Seeding:** See `SETUP.md` and `/backend/tests/` for test/demo data scripts or conventions.
- **API Docs:** Available at `http://localhost:8080/swagger` when backend is running.
- **API Versioning:** Not currently implemented, but may be added (see `API_REFERENCE.md`).
- **Error Logging:** All errors and logs are sent to Application Insights; see `RUNBOOK.md` for monitoring and troubleshooting.
- **REST Pattern:** Use REST APIs only for frontend-backend communication (see `API_REFERENCE.md`).
- **Azure Naming:** Follow Azure resource naming conventions as described in `SETUP.md`.
- **PR/CI:** PRs must pass all tests and lint checks before merge (see `CONTRIBUTING.md`).
- **Templates:** Issue and PR templates are provided in `/docs/` for consistency.
- **Example `.env`:**
  ```
  POSTGRES_CONNECTION_STRING=postgres://user:pass@host:5432/db
  COSMOS_CONNECTION_STRING=AccountEndpoint=...;AccountKey=...;
  BLOB_STORAGE_CONNECTION_STRING=DefaultEndpointsProtocol=...;AccountName=...;AccountKey=...;
  AZURE_AD_CLIENT_ID=...
  AZURE_AD_TENANT_ID=...
  AZURE_AD_CLIENT_SECRET=...
  EBAY_API_KEY=...
  AUSPOST_API_KEY=...
  ```

---

For more details, see `README.md`, `SETUP.md`, `CONTRIBUTING.md`, `ONBOARDING.md`, and `/docs/`. Update this file as architecture or workflows evolve.
