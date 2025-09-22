# Dans TCG System – API Reference (Draft)


This document outlines the planned API endpoints for each module.  
All endpoints are secured with **Microsoft Entra ID (Azure AD)** using OAuth2 / JWT.

**Note:** API versioning is not currently implemented, but may be introduced in the future. If versioning is added, endpoints will follow the `/api/v{n}/...` pattern.

Base URL (local): `http://localhost:5000/api`  
Base URL (prod): `https://<yourapp>.azurewebsites.net/api`

---

## 🔐 Authentication
- `POST /auth/login` → Obtain JWT token (Azure AD flow)
- `GET /auth/me` → Get current user profile

---

## 🃏 MTG Viewer
- `GET /cards/{id}` → Fetch single card (Cosmos + Blob refs)
- `GET /cards/search?set=LEA&rarity=rare` → Search cards
- `GET /cards/{id}/prices?days=90` → Price history

---

## 📚 Collection Manager
- `POST /collection` → Add card to collection
- `GET /collection` → List all collection entries
- `GET /collection/{id}` → Get single collection entry (with Cosmos metadata)
- `PUT /collection/{id}` → Update (condition, not-for-sale, acquisition cost)
- `POST /collection/import` → Bulk import via CSV/JSON

---

## 📦 Inventory Management
- `POST /inventory` → Add item (MTG or non-MTG)
- `GET /inventory/available` → List items available for sale
- `GET /inventory/{id}` → Get inventory item details
- `PUT /inventory/{id}/status` → Update stock state (available/reserved/sold/damaged)
- `POST /inventory/sync/ebay` → Sync inventory with eBay
- `PUT /inventory/{id}/trade-magic` → Mark as listed on Trade Magic (manual)

---

## 🛒 Orders & Fulfilment
- `POST /orders/import/ebay` → Import orders from eBay
- `POST /orders/manual` → Create manual order
- `GET /orders` → List all orders
- `GET /orders/{id}` → Get order details (lines, customer, status)
- `PUT /orders/{id}/status` → Update order status
- `POST /orders/{id}/ship` → Generate shipment + label (AusPost API)
- `GET /shipments/{id}` → Get shipment details

---

## 🧾 POS & Invoicing
- `POST /pos/sale` → Record POS transaction
- `GET /pos/transactions` → List POS transactions
- `POST /subscriptions` → Create subscription
- `GET /subscriptions/{id}` → Get subscription details
- `GET /invoices` → List invoices
- `GET /invoices/{id}` → Get invoice metadata + Blob link (PDF)

---

## 👥 CRM
- `POST /customers` → Add customer
- `GET /customers` → List customers
- `GET /customers/{id}` → Get customer profile (orders, invoices)
- `PUT /customers/{id}` → Update customer details
- `POST /suppliers` → Add supplier
- `GET /suppliers` → List suppliers

---

## 💰 Finance Tracker
- `POST /finance/expense` → Log expense
- `POST /finance/income` → Log manual income
- `GET /finance/transactions` → List all transactions
- `GET /finance/profit-loss` → Profit & loss summary

---

## 🧾 Tax Tracker
- `POST /tax/bas` → Generate BAS report (monthly)
- `GET /tax/bas?period=2023-08` → Get BAS report for period
- `GET /tax/bas/{id}` → Get BAS report metadata + Blob link (PDF/CSV)

---

## 📌 Cross-Cutting
- `GET /health` → Health check
- `GET /logs/errors` → Retrieve error logs (admin only)

---

📖 This API reference is a **living document**.  
Update as endpoints are implemented, changed, or versioned.
