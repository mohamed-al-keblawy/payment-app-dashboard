# payment-app-dashboard
Secure Payment Application with Reporting Dashboard using ASP.NET Core 8 and React 18


# 💳 Payment Application with Reporting Dashboard

This is a secure full-stack Payment Application built using **ASP.NET Core 8**, **SQL Server**, and **React 18**. It supports payment processing, refund handling, reporting APIs, and data visualizations via a React-based dashboard.

---

## 📌 Features

- ✅ Card Validation API
- ✅ Payment Processing (Hold → Confirm)
- ✅ Refund API with code validation
- ✅ Auto-confirmation of payments at 12:00 AM
- ✅ Paginated reports (Payments & Card Balances)
- ✅ React UI for dashboard and reporting
- ✅ Pagination + Filtering
- ✅ Bar Chart for transactions grouped by status
- ✅ CORS-enabled for external POS/clients

---

## 🛠 Technology Stack

| Layer       | Tech                                |
|-------------|-------------------------------------|
| Frontend    | React 18, Axios, Chart.js           |
| Backend     | ASP.NET Core 8 Web API              |
| ORM         | Entity Framework Core               |
| Database    | SQL Server                          |
| Architecture| Clean separation (API, BLL, DAL)    |
| Auth (future)| JWT (optional)                    |

---

## 🚀 Getting Started

### ✅ Prerequisites

- .NET 8 SDK
- Node.js 18+
- SQL Server or LocalDB
- Visual Studio / VS Code

---

### 🧩 Backend Setup

```bash
cd backend
dotnet restore
dotnet ef database update --project PaymentApp.Infrastructure --startup-project PaymentApp.API
dotnet run --project PaymentApp.API




cd frontend
npm install
npm run dev




| Method | Endpoint              | Description                 |
| ------ | --------------------- | --------------------------- |
| GET    | /api/cards/validate   | Validate card               |
| POST   | /api/payment/process  | Hold payment                |
| POST   | /api/payment/refund   | Process refund with code    |
| GET    | /api/reports/payments | Payments report (paginated) |
| GET    | /api/reports/cards    | Card balances report        |








