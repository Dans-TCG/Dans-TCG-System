# Dans TCG System – Roadmap

This roadmap outlines the development phases, sprints, and deliverables for the Dans TCG System.

---

## 📅 Overview
The project is divided into **5 phases** with **14 sprints**.  
Estimated total duration: **~41 weeks (~10 months, solo dev pace).**

---

## Phase 1 – Core Foundation (11 weeks)
**Goal:** Establish backbone with MTG data, collection management, and inventory.  
- **Sprint 1 (2 wks):** Infrastructure setup (Azure, GitHub CI/CD, Entra ID)  
- **Sprint 2 (3 wks):** MTG Viewer (Cosmos sync, Blob images, search API/UI)  
- **Sprint 3 (3 wks):** Collection Manager (Postgres schema, CRUD API, UI, CSV import)  
- **Sprint 4 (3 wks):** Inventory Management (schema, API, UI, eBay sync, Trade Magic manual flag)  

---

## Phase 2 – Orders & CRM (9 weeks)
**Goal:** Enable order management, fulfilment, and CRM.  
- **Sprint 5 (3 wks):** Orders backbone (schema, API import/manual, Orders dashboard)  
- **Sprint 6 (3 wks):** Fulfilment workflow (shipments, picking/packing slips, AusPost API, UI)  
- **Sprint 7 (3 wks):** CRM integration (schema, auto-link from orders, profiles UI)  

---

## Phase 3 – POS & Invoicing (6 weeks)
**Goal:** Direct sales & subscriptions.  
- **Sprint 8 (2–3 wks):** POS sales (schema, GST, UI for cash/EFT sales)  
- **Sprint 9 (3 wks):** Subscriptions & invoicing (schema, auto-invoices, PDFs to Blob, UI)  

---

## Phase 4 – Finance & Tax (6 weeks)
**Goal:** Track transactions and generate BAS reports.  
- **Sprint 10 (3 wks):** Transaction system (schema, ledger, auto-links, dashboard UI)  
- **Sprint 11 (3 wks):** BAS reports (schema, GST aggregation, PDF/CSV exports, UI)  

---

## Phase 5 – Enhancements (9 weeks)
**Goal:** Add automation, analytics, and advanced reporting.  
- **Sprint 12 (3 wks):** Analytics & alerts (price trends, low stock, dashboards)  
- **Sprint 13 (3 wks):** Advanced CRM & orders (notes/tags, privacy, order merging, returns/refunds)  
- **Sprint 14 (3 wks):** Advanced finance (P&L by income stream, export reports)  

---

## 🚀 Milestones
- **End of Phase 1:** Core app live (MTG Viewer, Collection, Inventory)  
- **End of Phase 2:** Orders & CRM fully operational  
- **End of Phase 3:** POS + invoicing live  
- **End of Phase 4:** Finance & tax compliance functional  
- **End of Phase 5:** Advanced features & analytics  

---

📖 This roadmap should be updated as progress is made, priorities shift, or scope changes.
