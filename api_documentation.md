# InvoiceSystem API Documentation

This document provides a comprehensive overview of all available API endpoints in the InvoiceSystem Modular Monolith, organized by module.

---

## 🔐 Identity Module
Manages authentication, user profiles, and role-based access control.

| Method | Route | Description |
| :--- | :--- | :--- |
| POST | `/api/auth/register` | Register a new user. |
| POST | `/api/auth/login` | Authenticate and receive JWT tokens. |
| POST | `/api/auth/refresh-token` | Refresh an expired access token. |
| GET | `/api/users` | List all registered users (Admin only). |
| GET | `/api/users/{id}` | Get details of a specific user. |
| PUT | `/api/users/{id}` | Update user profile. |
| DELETE | `/api/users/{id}` | Delete a user (Admin only). |
| GET | `/api/roles` | List all roles and permissions. |
| POST | `/api/roles/{id}/permissions` | Assign permissions to a role. |

---

## 👥 Clients Module
Manages customer and client relationships.

| Method | Route | Description |
| :--- | :--- | :--- |
| GET | `/api/clients` | List all clients. |
| GET | `/api/clients/{id}` | Get specific client details. |
| POST | `/api/clients` | Create a new client record. |
| PUT | `/api/clients/{id}` | Update client information. |
| DELETE | `/api/clients/{id}` | Remove a client. |

---

## 📄 Invoicing Module
Core financial module for managing billing and payments.

| Method | Route | Description |
| :--- | :--- | :--- |
| GET | `/api/invoices` | List all invoices (includes items/payments). |
| GET | `/api/invoices/{id}` | Get specific invoice details. |
| POST | `/api/invoices` | Create a new invoice header. |
| PUT | `/api/invoices/{id}` | Update invoice header details. |
| DELETE | `/api/invoices/{id}` | Remove an invoice and its related data. |
| POST | `/api/invoices/{id}/items` | Add a line item to an invoice. |
| PUT | `/api/invoices/{id}/items/{itemId}` | Update a specific line item. |
| DELETE | `/api/invoices/{id}/items/{itemId}` | Remove a line item. |
| PATCH | `/api/invoices/{id}/status` | Update invoice status (e.g., "Paid"). |
| GET | `/api/invoices/{id}/payments` | List payments for an invoice. |
| POST | `/api/invoices/{id}/payments` | Record a new payment against an invoice. |
| GET | `/api/invoices/{id}/balance` | Get total billed, paid, and balance for an invoice. |
| GET | `/api/invoices/{id}/pdf` | Generate and download invoice as PDF. |

---

## 💸 Expenses Module
Track business spending and expense categories.

| Method | Route | Description |
| :--- | :--- | :--- |
| GET | `/api/expenses` | List all recorded business expenses. |
| GET | `/api/expenses/{id}` | Get specific expense details. |
| POST | `/api/expenses` | Create a new expense record. |
| PUT | `/api/expenses/{id}` | Update an expense record. |
| DELETE | `/api/expenses/{id}` | Delete an expense record. |
| GET | `/api/expense-categories` | List all expense categories. |
| POST | `/api/expense-categories` | Create a new category (e.g., "Travel"). |
| PUT | `/api/expense-categories/{id}` | Update a category name. |
| DELETE | `/api/expense-categories/{id}` | Remove a category. |

---

## 📊 Reporting & Analytics Module
High-level business insights and data aggregation.

| Method | Route | Description |
| :--- | :--- | :--- |
| GET | `/api/reports/financial-summary` | Monthly revenue vs expenses summary. |
| GET | `/api/reports/invoices` | Filterable invoice report (Date, Client, Status). |
| GET | `/api/analytics/revenue-vs-expenses` | Historical trend of revenue and spending. |
| GET | `/api/analytics/top-clients` | List clients by total billed amount. |

---

## 🖥️ Dashboard & Search
Overview and discovery endpoints.

| Method | Route | Description |
| :--- | :--- | :--- |
| GET | `/api/dashboard/kpis` | Real-time Key Performance Indicators. |
| GET | `/api/dashboard/recent-invoices` | List of 5 most recent invoices. |
| GET | `/api/search` | Global search across invoices and clients. |

---

## 📂 Storage Module
Infrastructure for file management.

| Method | Route | Description |
| :--- | :--- | :--- |
| POST | `/api/uploads/receipt` | Upload an image or PDF receipt. |
| GET | `/api/uploads/{fileId}` | Download a previously uploaded file. |
