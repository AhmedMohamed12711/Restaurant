# 🍽️ Restaurants API - Enterprise ASP.NET Core Clean Architecture Backend

[![.NET 8](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture%20%7C%20CQRS-brightgreen.svg)]()
[![Database](https://img.shields.io/badge/Database-MS%20SQL%20Server%20%7C%20EF%20Core-orange.svg)]()
[![Authentication](https://img.shields.io/badge/Auth-ASP.NET%20Core%20Identity%20%7C%20JWT-red.svg)]()
[![Testing](https://img.shields.io/badge/Testing-xUnit%20%7C%20Moq%20%7C%20FluentAssertions-purple.svg)]()
[![CI/CD](https://img.shields.io/badge/CI%2FCD-GitHub%20Actions%20%7C%20Azure-blue.svg)]()

A comprehensive, production-grade RESTful API built with **ASP.NET Core 8**, designed following the principles of **Clean Architecture** and **CQRS (Command Query Responsibility Segregation)** pattern with **MediatR**. 

This system provides a complete backend solution for managing restaurants, menus, dishes, user identity, roles, and resource-based authorization.

---

## 🏛️ Architecture Overview

The solution adheres strictly to **Clean Architecture** (Hexagonal/Onion Architecture), ensuring high maintainability, separation of concerns, and ease of automated testing:

```
RestaurantsAPI.sln
│
├── 📁 src/
│   ├── 🌐 RestaurantsAPI/               # Presentation Layer (Controllers, Middlewares, Program.cs)
│   ├── ⚙️ Restaurants.Application/      # Application Layer (CQRS Commands, Queries, DTOs, Validators)
│   ├── 🧱 Restaurants.Infrastructure/   # Infrastructure Layer (EF Core, Repositories, Migrations, Auth)
│   └── 💎 Rsestaurants.Domain/          # Domain Layer (Core Entities, Enums, Exceptions, Interfaces)
│
├── 📁 tests/
│   ├── 🧪 Restaurants.ApplicationTests/    # Unit Tests for Application Layer (Handlers, Validators, Mappings)
│   ├── 🧪 Restaurants.InfrastructureTests/ # Tests for Auth Requirements & Handlers
│   └── 🧪 RestaurantsAPI.Tests/            # Integration Tests (API Endpoints, Middleware with WebApplicationFactory)
│
└── 📁 .github/workflows/
    └── 🚀 ci.yml                         # Automated CI/CD Pipeline (Build, Test, Publish, Azure Deploy)
```

### 🧩 Architectural Highlights & Design Patterns
- **CQRS Pattern**: Decoupled read and write pipelines utilizing **MediatR**.
- **Repository Pattern**: Abstraction over data access via `IRestaurantsRepository` and `IDishesRepository`.
- **Domain-Driven Design (DDD) Elements**: Encapsulated domain logic, custom exceptions (`NotFoundException`, `ForbidException`), and owned value objects (`Address`).
- **Validation Pipeline**: FluentValidation integrated with automatic request validation before reaching handlers.
- **Object Mapping**: **AutoMapper** profiles for clean mapping between domain models and DTOs.

---

## ✨ Features & Capabilities

### 1. 🍽️ Restaurant & Dish Management
- **Full CRUD operations** for restaurants and their nested dishes.
- **Pagination, Sorting, & Filtering**: Efficient data retrieval using `PagedResult<T>` with customizable page sizes, search phrases, sort column, and sort direction.
- **Entity Relationships**: One-to-many relationship between restaurants and dishes, as well as users and owned restaurants.

### 2. 🔐 Authentication & Advanced Authorization
- **ASP.NET Core Identity API**: Native endpoints for user registration, login, and user management.
- **JWT & Bearer Authentication**: Secure token-based access with Swagger integration.
- **Role-Based Access Control (RBAC)**: Support for distinct user roles (`Admin`, `Owner`, `User`) with endpoints to assign and unassign roles.
- **Policy & Requirement-Based Authorization**:
  - `HasNationality`: Claim-based access control.
  - `AtLeast20`: Age verification via `MinimumAgeRequirement` & custom handler.
  - `CreatedAtleast2Restaurants`: Custom requirement verifying user ownership history.
- **Resource-Based Authorization**: Custom `RestaurantAuthorizationService` ensuring only owners or admins can modify or delete their respective restaurant resources.

### 3. 🛡️ Custom Middlewares
- **`ErrorHandlingMiddleware`**: Global exception interceptor providing standardized JSON error responses, handling `404 Not Found`, `403 Forbidden`, `400 Bad Request`, and logging unhandled `500 Server Errors`.
- **`RequestTimeLoggingMiddleware`**: Performance diagnostics monitoring HTTP execution times and warning when requests exceed thresholds (>4 seconds).

### 4. 📝 Structured Logging with Serilog
- Complete diagnostics with **Serilog**.
- Formatted console output for local development.
- Daily rolling file sink with compact JSON formatting stored under `Logs/Restaurant-Api-.log`.

### 5. 🧪 Automated Testing (34+ Tests)
- **Unit Tests (`Restaurants.ApplicationTests`)**:
  - Testing Command Handlers (`CreateRestaurant`, `UpdateRestaurant`).
  - FluentValidation rules verification (`CreateRestaurantCommandValidator`).
  - AutoMapper profiles (`RestaurantsProfileTests`).
  - Current user context resolution (`CurrentUserTests`, `UserContextTests`).
- **Infrastructure Tests (`Restaurants.InfrastructureTests`)**:
  - Testing authorization handlers (`CreatedMultipleRestaurantsRequirementHandlerTests`).
- **Integration Tests (`RestaurantsAPI.Tests`)**:
  - In-memory testing using `WebApplicationFactory<Program>`.
  - Mocked authorization using `FakePolicyEvaluator`.
  - Testing HTTP endpoints and `ErrorHandlingMiddleware`.

### 6. 🚀 CI/CD & Cloud Deployment
- **GitHub Actions (`.github/workflows/ci.yml`)**:
  - Automated triggers on Push & Pull Requests to `main`.
  - .NET 8 environment provisioning, dependency restoration, Release compilation, and test execution.
  - Code coverage extraction and build artifact publishing.
  - Ready for continuous deployment to **Microsoft Azure App Service / Cloud**.

---

## 🛠️ Technology Stack

| Layer / Concern | Technology |
|---|---|
| **Framework** | .NET 8.0 (C# 12) |
| **API Framework** | ASP.NET Core Web API |
| **ORM / Database** | Entity Framework Core 8, MS SQL Server |
| **Architecture Pattern** | Clean Architecture, CQRS |
| **Mediator** | MediatR |
| **Validation** | FluentValidation |
| **Mapping** | AutoMapper |
| **Security & Auth** | ASP.NET Core Identity, JWT Bearer |
| **Logging** | Serilog (Console & File Sinks) |
| **API Documentation** | Swagger / OpenAPI (Swashbuckle) |
| **Testing Tools** | xUnit, Moq, FluentAssertions, Microsoft.AspNetCore.Mvc.Testing |
| **CI/CD & Cloud** | GitHub Actions, Microsoft Azure |

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Microsoft SQL Server](https://www.microsoft.com/sql-server) or LocalDB
- [Git](https://git-scm.com/)

### Installation & Run

1. **Clone the repository**:
   ```bash
   git clone https://github.com/AhmedMohamed12711/Restaurant.git
   cd Restaurant
   ```

2. **Configure Connection String**:
   Open `src/RestaurantsAPI/appsettings.Development.json` and adjust your connection string if necessary:
   ```json
   {
     "ConnectionStrings": {
       "RestaurantsDb": "Server=(localdb)\\mssqllocaldb;Database=RestaurantsDb;Trusted_Connection=True;"
     }
   }
   ```

3. **Apply Database Migrations**:
   ```bash
   dotnet ef database update --project src/Restaurants.Infrastructure --startup-project src/RestaurantsAPI
   ```

4. **Run the API**:
   ```bash
   dotnet run --project src/RestaurantsAPI
   ```

5. **Explore Swagger UI**:
   Navigate to `https://localhost:7083/swagger` or `http://localhost:5242/swagger` in your browser.

---

## 🧪 Running Automated Tests

Run the full test suite across all layers:
```bash
dotnet test RestaurantsAPI.sln --verbosity normal
```

---

## 📬 API Endpoints Summary

### 🏢 Restaurants (`/api/restaurants`)
| Method | Endpoint | Description | Auth Required |
|---|---|---|---|
| `GET` | `/api/restaurants` | Get paginated, filtered, sorted restaurants | ❌ |
| `GET` | `/api/restaurants/{id}` | Get restaurant by ID with its dishes | ❌ |
| `POST` | `/api/restaurants` | Create a new restaurant | ✅ (Owner / Admin) |
| `PATCH` | `/api/restaurants/{id}` | Update restaurant details | ✅ (Owner / Admin) |
| `DELETE` | `/api/restaurants/{id}` | Delete a restaurant | ✅ (Owner / Admin) |

### 🍲 Dishes (`/api/restaurant/{restaurantId}/dishes`)
| Method | Endpoint | Description | Auth Required |
|---|---|---|---|
| `GET` | `/api/restaurant/{restaurantId}/dishes` | Get all dishes for a restaurant | ❌ |
| `GET` | `/api/restaurant/{restaurantId}/dishes/{dishId}` | Get dish details by ID | ❌ |
| `POST` | `/api/restaurant/{restaurantId}/dishes` | Add a new dish to a restaurant | ✅ |
| `DELETE` | `/api/restaurant/{restaurantId}/dishes` | Delete all dishes for a restaurant | ✅ |

### 👤 Identity & Users (`/api/identity`)
| Method | Endpoint | Description | Auth Required |
|---|---|---|---|
| `POST` | `/api/identity/register` | Register new user account | ❌ |
| `POST` | `/api/identity/login` | Login and obtain authentication token | ❌ |
| `PATCH` | `/api/identity/user` | Update user profile details (DOB, Nationality) | ✅ |
| `POST` | `/api/identity/userRole` | Assign role to user (`Owner`, `Admin`) | ✅ (Admin) |
| `DELETE` | `/api/identity/userRole` | Unassign role from user | ✅ (Admin) |

---

## 👨‍💻 Author

- **Ahmed Mohamed** - [GitHub Profile](https://github.com/AhmedMohamed12711)

---
*Built with passion and best enterprise software engineering practices.*
