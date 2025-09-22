# Dans TCG System – Development Plan

## 📌 Project Overview
Dans TCG System is a **Trading Card Game (MTG) management and sales platform**.  
It covers card data management, collection tracking, inventory, order fulfilment, POS/invoicing, CRM, finance, and tax compliance.  

The system integrates with **eBay, MTGJSON/Scryfall, and AusPost**, with manual support for Trade Magic.  

---

## ⚙️ Architecture & Tech Stack
**Backend:** .NET (ASP.NET Core)  
**Frontend:** React  
**Database:**  
- **Postgres** → business operations (inventory, orders, CRM, finance, tax)  
- **CosmosDB (Mongo API)** → MTG card metadata, prices  
- **Azure Blob Storage** → card images, invoices, shipping labels, backups  

**Authentication:** Microsoft Entra ID (Azure AD)  
**Hosting:** Azure Web App (App Service)  
**Dev Environment:** GitHub Codespaces + GitHub Actions (CI/CD)  

---

## 🏗️ Infrastructure Setup
- **Azure Resources Required:**  
  - Web App (App Service)  
  - Database for PostgreSQL  
  - CosmosDB (Mongo API)  
  - Blob Storage  
  - Application Insights (logging & monitoring)  
  - Microsoft Entra ID tenant + app registrations  

- **CI/CD:**  
  - GitHub Actions pipeline  
  - Deploy to staging slot → manual promotion to production  

- **Secrets/Config:**  
  - Store API keys (eBay, AusPost) in **Azure Key Vault** or GitHub Secrets  
  - Configure environment variables for DB connections  

---

## 🗂️ Data Models
### **Postgres (SQL)**  
- `CollectionEntry` → owned cards (condition, acquisition cost, not-for-sale)  
- `InventoryItem` → items for sale (MTG & non-MTG)  
- `Order`, `OrderLine`, `Shipment` → sales flow  
- `Invoice`, `Subscription` → POS & billing  
- `Customer`, `Supplier` → CRM entities  
- `Transaction`, `BASReport` → finance & tax  

### **CosmosDB (Mongo API)**  
- `cards` → MTG card printings (rules text, variants, prices)  
- `sets` → card sets metadata  
- `price_snapshots` (optional) → time-series price tracking  

### **Blob Storage**  
- Card images  
- Invoice PDFs  
- Shipping labels  
- BAS report exports  
- Backups/logs  

---

## 🔗 API Layer
- **MTG Viewer** → Cosmos (cards, sets, prices) + Blob (images)  
- **Collection Manager** → Postgres (collection entries) + Cosmos refs  
- **Inventory** → Postgres (inventory) + eBay API sync  
- **Orders** → Postgres (orders/shipments), eBay API import, AusPost API for labels  
- **POS & Invoicing** → Postgres (invoices, subscriptions) + Blob (PDFs)  
- **CRM** → Postgres (customers/suppliers) linked to orders/invoices  
- **Finance/Tax** → Postgres (transactions, BAS), Blob for reports  

Auth enforced via **Azure AD (OAuth2 / JWT)**.  

---

## 📅 Roadmap & Phases
- **Phase 1 (11 wks):** MTG Viewer, Collection Manager, Inventory Mgmt  
- **Phase 2 (9 wks):** Orders + CRM + Fulfilment workflow  
- **Phase 3 (6 wks):** POS, Subscriptions, Invoicing  
- **Phase 4 (6 wks):** Finance Tracker, BAS reporting  
- **Phase 5 (9 wks):** Enhancements (analytics, alerts, advanced CRM & finance)  

_Total: ~41 weeks (≈ 10 months, solo dev pace)._  

---

## 🛠️ Development Workflow
- **Branching:**  
  - `main` → stable releases  
  - `develop` → ongoing development  
  - `feature/*` → feature branches  

- **Testing:**  
  - Unit tests for business logic (e.g., GST calc, inventory updates)  
  - Integration tests for external APIs (eBay, AusPost)  
  - E2E tests for order → shipment → finance flow  

- **Code Style:**  
  - Backend: C#/.NET conventions  
  - Frontend: React with TypeScript  

- **Docs:**  
  - API contract (Swagger/OpenAPI generated)  
  - README.md kept up-to-date  
  - Runbooks for ops (DB restore, key rotation, etc.)  

---

## ✅ Next Steps
1. Provision Azure resources (Web App, Postgres, Cosmos, Blob, Entra ID).  
2. Scaffold .NET backend + React frontend, deploy Hello World.  
3. Implement **Phase 1 Sprints**:  
  - Sprint 1 → Infra & CI/CD setup  
  - Sprint 2 → MTG Viewer (Cosmos sync + search)  
  - Sprint 3 → Collection Manager (Postgres + Cosmos link)  
  - Sprint 4 → Inventory Management (Postgres + eBay sync)  

---

## 📋 Onboarding
See `ONBOARDING.md` for a step-by-step checklist for new contributors.

## 🗺️ Architecture Diagram
See `/docs/architecture-diagram.png` (or update with your own diagram) for a high-level overview of system components and data flow.

## 📖 API Documentation
When the backend is running, access Swagger/OpenAPI docs at:  
`http://localhost:5000/swagger`

---

📖 This README serves as the **living project guide**. Update as architecture evolves, new APIs are added, or priorities shift.
