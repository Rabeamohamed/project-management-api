# Project Management API

A scalable backend system built with **ASP.NET Core 9** following **Clean Architecture** principles.

---

## 🚀 Features

- Clean Architecture (API / Application / Domain / Infrastructure)
- CQRS Pattern using MediatR
- JWT Authentication & Authorization
- ASP.NET Core Identity
- FluentValidation for request validation
- Global Exception Handling Middleware
- Redis Distributed Caching (Dockerized)
- Entity Framework Core with SQL Server
- User-based data isolation
- Projects & Tasks Management module
- Pagination support
- Search functionality
- Filtering support
- Dockerized Redis
- Swagger JWT Authentication

---

## 🧱 Architecture

- API Layer → Controllers, Middleware, Swagger
- Application Layer → CQRS, DTOs, Validators, Interfaces
- Domain Layer → Entities, Enums
- Infrastructure Layer → EF Core, Identity, Redis, Services

---

## 🔐 Authentication

JWT Bearer Token is used.

### Steps:
1. Register/Login via `/api/auth/register` or `/api/auth/login`
2. Copy the JWT token from the response
3. Open Swagger UI
4. Click **Authorize 🔒**
5. Paste the token **without** the `Bearer` prefix:

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

> ⚠️ Swagger adds `Bearer` automatically — do **not** type it manually.

---

## 📌 API Features

### Projects
- Create Project
- Update Project
- Delete Project
- Get All Projects
- Pagination
- Search

### Tasks
- Create Task
- Update Task Status
- Delete Task
- Get Tasks By Project
- Pagination
- Search
- Filter by Status
- Filter by Priority

---

## 🧰 Tech Stack

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Redis (Docker)
- MediatR
- FluentValidation
- JWT
- Swagger / OpenAPI

---

## 🚀 How to Run

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Docker](https://www.docker.com/) (for Redis)

### Steps

**1. Clone the repository**
```bash
git clone https://github.com/Rabeamohamed/project-management-api.git
cd project-management-api
```

**2. Update connection string in `appsettings.json`**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ProjectManagementDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

**3. Apply database migrations**
```bash
dotnet ef database update --project src/ProjectManagement.Infrastructure --startup-project src/ProjectManagement.API
```

**4. Run the application**
```bash
dotnet run --project src/ProjectManagement.API
```

### Swagger
After running, open: `https://localhost:7053/swagger`

---

## 🐳 Run Redis (Docker)

**Start Redis container**
```bash
docker run --name projectmanagement-redis -d -p 6379:6379 redis
```

**Verify Redis is running**
```bash
docker exec -it projectmanagement-redis redis-cli ping
```
Expected output: `PONG`
