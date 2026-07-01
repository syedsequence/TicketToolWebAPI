# 🎫 Ticket Tool Web API

![.NET](https://img.shields.io/badge/.NET-10.0-blueviolet)
![Architecture](https://img.shields.io/badge/Architecture-4--Tier-success)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4)
![EF Core](https://img.shields.io/badge/EF%20Core-ORM-green)
![Mapster](https://img.shields.io/badge/Mapster-Enabled-orange)
![JWT](https://img.shields.io/badge/JWT-Authentication-red)
![Identity](https://img.shields.io/badge/ASP.NET-Identity-blue)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-blue)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D)
![License](https://img.shields.io/badge/License-MIT-lightgrey)
![Status](https://img.shields.io/badge/Status-Active-brightgreen)

---

# 🚀 Ticket Tool Web API

A modern, enterprise-ready **ASP.NET Core 10 Web API** designed for **Ticket Management Systems** across multiple industries including:

- 🏢 Corporate Companies
- 🍽️ Restaurants
- 🏥 Hospitals
- 🏨 Hotels
- 🏭 Manufacturing
- 🏫 Educational Institutions
- 🛒 Retail Stores
- 🏢 Service Providers

Built with a **Clean 4-Tier Architecture**, following best practices for scalability, maintainability, security, and performance.

---

# 📑 Table of Contents

- Features
- Technology Stack
- Architecture
- Project Structure
- Authentication
- User Roles
- Ticket Workflow
- Database
- API Features
- Installation
- Configuration
- Running the Project
- API Documentation
- Future Improvements
- License

---

# ✨ Features

✅ ASP.NET Core 10 Web API

✅ Clean 4-Tier Architecture

✅ Repository Pattern

✅ Dependency Injection

✅ Entity Framework Core

✅ SQL Server

✅ ASP.NET Identity

✅ JWT Authentication

✅ Role Based Authorization

✅ Refresh Token Support

✅ Swagger Documentation

✅ Mapster Object Mapping

✅ Generic Repository

✅ Unit of Work Ready

✅ Pagination

✅ Filtering

✅ Searching

✅ Soft Delete

✅ Audit Fields

✅ File Upload Support

✅ Global Exception Middleware

✅ API Response Wrapper

✅ Validation

✅ Logging Ready

---

# 🛠 Technology Stack

| Technology | Version |
|------------|----------|
| .NET | 10 |
| ASP.NET Core | 10 |
| Entity Framework Core | Latest |
| ASP.NET Identity | Latest |
| SQL Server | Latest |
| JWT | Authentication |
| Swagger | OpenAPI |
| Mapster | Object Mapping |
| LINQ | Querying |
| Dependency Injection | Built-in |

---

# 🏗 Architecture

```
                Client
                  │
                  ▼
        ┌───────────────────┐
        │      API Layer     │
        │ Controllers        │
        │ DTOs               │
        │ Middleware         │
        └─────────┬──────────┘
                  │
                  ▼
        ┌───────────────────┐
        │   Service Layer    │
        │ Business Logic     │
        │ Validation         │
        │ Mapster            │
        └─────────┬──────────┘
                  │
                  ▼
        ┌───────────────────┐
        │ Repository Layer   │
        │ Generic Repository │
        │ Data Access        │
        └─────────┬──────────┘
                  │
                  ▼
        ┌───────────────────┐
        │    Data Layer      │
        │ DbContext          │
        │ EF Core            │
        │ SQL Server         │
        └───────────────────┘
```

---

# 📁 Project Structure

```
TicketTool.API
│
├── Controllers
├── Middlewares
├── Extensions
├── Program.cs
│
TicketTool.Application
│
├── Services
├── Interfaces
├── DTOs
├── Mapping
├── Validators
├── Common
│
TicketTool.Infrastructure
│
├── Repository
├── DbContext
├── Identity
├── Configurations
├── Migrations
│
TicketTool.Domain
│
├── Entities
├── Enums
├── Constants
├── Common
└── Interfaces
```

---

# 🔐 Authentication

The API uses **JWT Bearer Authentication** together with **ASP.NET Identity**.

### Authentication Features

- User Registration
- Login
- JWT Access Token
- Refresh Token
- Role Authorization
- Password Hashing
- Identity Roles
- Secure API Endpoints

---

# 👥 User Roles

The system supports enterprise-level role management.

| Role | Description |
|------|-------------|
| Developer Admin | Full System Access |
| Management | Company Management |
| Branch Manager | Branch Administration |
| Supervisor | Team Supervision |
| User | Create & Track Tickets |
| Technical Manager | Technical Operations |
| Technical Supervisor | Assign & Monitor Technicians |
| Technician | Resolve Assigned Tickets |

---

# 🎫 Ticket Workflow

```
Create Ticket
      │
      ▼
Pending
      │
      ▼
Assigned
      │
      ▼
In Progress
      │
      ▼
Resolved
      │
      ▼
Closed
```

Additional statuses can include:

- Cancelled
- Reopened
- On Hold
- Waiting for Customer
- Escalated

---

# 🗄 Database

Built using **Entity Framework Core** with **SQL Server**.

Supports:

- Code First
- Migrations
- Relationships
- Constraints
- Soft Delete
- Audit Fields

---

# ⚡ API Features

### Authentication

- Register
- Login
- Refresh Token
- Change Password
- Forgot Password
- Reset Password

---

### User Management

- Create Users
- Update Users
- Delete Users
- Assign Roles
- Lock User
- Unlock User

---

### Ticket Management

- Create Ticket
- Update Ticket
- Delete Ticket
- Assign Ticket
- Reassign Ticket
- Close Ticket
- Reopen Ticket

---

### Category Management

- Ticket Categories
- Departments
- Priorities
- Statuses

---

### Dashboard

- Total Tickets
- Open Tickets
- Closed Tickets
- Pending Tickets
- Assigned Tickets
- Technician Performance

---

### Search

- Global Search
- Ticket Search
- User Search

---

### Reports

- Daily Reports
- Monthly Reports
- Branch Reports
- Technician Reports

---

# 🚀 Installation

Clone the repository

```bash
git clone https://github.com/yourusername/TicketTool.git
```

Navigate to the project

```bash
cd TicketTool
```

Restore packages

```bash
dotnet restore
```

Apply migrations

```bash
dotnet ef database update
```

Run the project

```bash
dotnet run
```

---

# ⚙ Configuration

Update **appsettings.json**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  },

  "JWT": {
    "Key": "",
    "Issuer": "",
    "Audience": "",
    "DurationInMinutes": 60
  }
}
```

---

# 🌐 Swagger

After running the project:

```
https://localhost:5001/swagger
```

or

```
https://localhost:7001/swagger
```

---

# 📈 Future Roadmap

- Email Notifications
- SMS Notifications
- Push Notifications
- Multi-Tenant Support
- Organization Management
- Branch Management
- SLA Management
- File Attachments
- Comments
- Activity Timeline
- Audit Logs
- SignalR Real-Time Updates
- Mobile API
- Analytics Dashboard

---

# 🤝 Contributing

Contributions are welcome!

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push your branch
5. Open a Pull Request

---

# 📄 License

This project is licensed under the **MIT License**.

---

# ⭐ Support

If you found this project useful, please consider giving it a ⭐ on GitHub.

---

<div align="center">

### 🚀 Built with ASP.NET Core 10

**Enterprise Ready • Clean Architecture • Secure • Scalable • Maintainable**

Made with ❤️ for Developers

</div>
