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
1. Register/Login user
2. Copy JWT token
3. Open Swagger UI
4. Click **Authorize 🔒**
5. Paste token like this:


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

## 🐳 Run Redis (Docker)

```bash
docker run --name redis -d -p 6379:6379 redis


dotnet restore
dotnet build
dotnet run --project ProjectManagement.API