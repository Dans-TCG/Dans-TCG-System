# Dans TCG System – Database Schema

This document describes the database schema across **Postgres**, **CosmosDB**, and **Blob Storage**.

---

## 🗄️ Postgres (Relational, SQL)
### Tables

#### CollectionEntry
- `collection_entry_id (PK)`
- `card_id (FK → CosmosDB)`
- `condition` (NM, EX, etc.)
- `not_for_sale` (bool)
- `location`
- `acquisition_date`
- `acquisition_cost`

#### InventoryItem
- `inventory_id (PK)`
- `collection_entry_id (FK)` (nullable for non-MTG items)
- `name` (for non-MTG items)
- `quantity`
- `status` (available, reserved, sold, damaged)
- `marketplace_listing_id` (eBay/Trade Magic ref)
- `price`
- `created_at`, `updated_at`

#### Order
- `order_id (PK)`
- `customer_id (FK → Customer)`
- `order_source` (eBay, POS, manual, Trade Magic)
- `status` (new, packed, shipped, delivered, cancelled)
- `created_at`, `updated_at`

#### OrderLine
- `order_line_id (PK)`
- `order_id (FK)`
- `inventory_id (FK → InventoryItem)`
- `quantity`
- `sale_price`

#### Shipment
- `shipment_id (PK)`
- `order_id (FK)`
- `carrier` (AusPost, manual)
- `tracking_number`
- `label_blob_id (FK → Blob)`
- `status` (pending, shipped, delivered, returned)

#### Invoice
- `invoice_id (PK)`
- `customer_id (FK)`
- `type` (subscription, service, sale)
- `status` (open, paid, overdue)
- `total_amount`
- `gst_amount`
- `issued_date`
- `due_date`
- `invoice_blob_id (FK → Blob)`

#### Subscription
- `subscription_id (PK)`
- `customer_id (FK)`
- `service_description`
- `monthly_fee`
- `status`
- `start_date`, `end_date`

#### Customer
- `customer_id (PK)`
- `name`
- `email`
- `phone`
- `address`
- `subscription_status`
- `created_at`, `updated_at`

#### Supplier
- `supplier_id (PK)`
- `name`
- `contact_info`
- `created_at`, `updated_at`

#### Transaction
- `transaction_id (PK)`
- `type` (income, expense)
- `source` (order, invoice, manual)
- `amount`
- `gst_amount`
- `customer_id (FK, nullable)`
- `supplier_id (FK, nullable)`
- `linked_order_id (nullable)`
- `linked_invoice_id (nullable)`
- `date`

#### BASReport
- `bas_id (PK)`
- `period_start`
- `period_end`
- `gst_collected`
- `gst_paid`
- `net_gst`
- `status` (draft, submitted)
- `report_blob_id (FK → Blob)`

---

## 📂 CosmosDB (Mongo API)
### Collections

#### cards
- `_id` (scryfall ID or UUID)
- `name`
- `set { code, name, release_date }`
- `collector_number`
- `rarity`
- `mana_cost`
- `type_line`
- `oracle_text`
- `colors`
- `layout`
- `variants { is_foil, is_etched, promo }`
- `prices { current: { ebay, tcgplayer, cardkingdom }, history: [] }`
- `images { normal_blob_id, foil_blob_id }`

#### sets
- `_id` (set code, e.g., "LEA")
- `name`
- `release_date`
- `total_cards`
- `block`

#### price_snapshots (optional)
- `_id`
- `card_id`
- `source`
- `price`
- `currency`
- `timestamp`

---

## ☁️ Blob Storage
### Containers
- `card-images/` → MTG card images
- `invoices/` → invoice PDFs
- `shipping-labels/` → AusPost labels
- `bas-reports/` → exported BAS PDFs/CSVs
- `backups/` → DB backups, logs

