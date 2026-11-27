# DoofPlus Platform

![.NET](https://img.shields.io/badge/.NET-10-512bd4?style=for-the-badge&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-10-512bd4?style=for-the-badge)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-10-512bd4?style=for-the-badge)
![MySQL](https://img.shields.io/badge/MySQL-8-4479a1?style=for-the-badge&logo=mysql&logoColor=white)

RESTful API of **DoofPlus**, the platform of IngesCompanyCC for pharmaceutical laboratories: quality documents, deviations and CAPA, batch release with electronic signature, manufacturing against approved master formulas, IoT monitoring of critical equipment and subscriptions paid with Niubiz.

It is the backend of the [DoofPlus Web Application](https://github.com/IngesCompanyCC/doofplus-web-application). It follows the course base project (`learning-center-platform`): ASP.NET Core, Entity Framework Core with MySQL, Domain-Driven Design with one folder per bounded context, CQRS with command and query services, and OpenAPI documentation.

## 📖 Documentation

| What | URL |
|------|-----|
| Interactive reference (Scalar) | https://doofplus-platform-aw.onrender.com/scalar |
| OpenAPI document | https://doofplus-platform-aw.onrender.com/openapi/v1.json |
| Base URL of the API | `https://doofplus-platform-aw.onrender.com/api/v1` |

> The API runs on the free plan of Render, which stops the service after 15 minutes without requests. The first request after that takes about a minute, so open the Scalar page once before a demo.

Locally, the same pages are at `http://localhost:5228/scalar` and `http://localhost:5228/openapi/v1.json`.

## 🚀 Run locally

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) 8 or later, running on `localhost:3306`.

### Setup

1. Set the connection string of your local MySQL. `appsettings.Development.json` uses `user=root;password=root;database=doofplus`. If your credentials are different, override them with an environment variable instead of editing the file:

   ```powershell
   $env:ConnectionStrings__DefaultConnection = "server=localhost;user=<user>;password=<password>;database=doofplus;"
   ```

2. Run the API from the repository root:

   ```bash
   dotnet run --project IngesCompanyCCPlatform.API --launch-profile http
   ```

3. Open `http://localhost:5228/scalar`.

On startup the API applies the pending Entity Framework Core migrations (`IngesCompanyCCPlatform.API/Migrations`), so the `doofplus` database and its tables are created automatically.

### Migrations

After changing an entity or its configuration, add a migration from the repository root:

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add <MigrationName> --project IngesCompanyCCPlatform.API
```

## ⚙️ Configuration

Every setting can be given in `appsettings*.json` or as an environment variable, replacing `:` with `__` (for example `TokenSettings__Secret`).

| Setting | Purpose |
|---------|---------|
| `ConnectionStrings:DefaultConnection` | MySQL connection string. In Production it is a template whose `%VARIABLE%` parts are read from the environment (see the Deployment section). |
| `TokenSettings:Secret` | Key that signs the JSON Web Tokens (at least 32 characters). Only `appsettings.Development.json` has a value; **any other environment does not start without `TokenSettings__Secret`**. |
| `Cors:AllowedOrigins` | Origins allowed to call the API: the Web Application on Firebase, the Website on GitHub Pages and `http://localhost:5173`. To change them, set `Cors__AllowedOrigins__0`, `Cors__AllowedOrigins__1`… |
| `TokenSettings:ExpirationDays` | Days a token stays valid (7 by default). |
| `ThingsBoard:ApiKey` | Key that ThingsBoard must send in the `X-Api-Key` header of `POST /api/v1/telemetry`. Without a key every reading is rejected. |
| `Niubiz:BaseUrl`, `Niubiz:MerchantId`, `Niubiz:Username`, `Niubiz:Password` | Niubiz payment gateway (sandbox by default), used by `POST /api/v1/subscriptions/{subscriptionId}/payments`. |

Never commit real secrets: set them as environment variables of the hosting provider.

## 🧩 Bounded contexts and resources

| Bounded context | Folder | Resources (`/api/v1/...`) |
|-----------------|--------|---------------------------|
| Identity & Access Management | `Iam` | `authentication/sign-in`, `authentication/sign-up`, `tokens`, `users`, `roles`, `invitations` |
| Organization Management | `Organizations` | `organizations`, `facilities`, `user-profiles`, `administrative-activities`, `contact-inquiries` |
| Subscriptions & Payments | `Subscriptions` | `plans`, `subscriptions` (with their payments and plan changes) |
| Manufacturing & Batch Management | `Manufacturing` | `products`, `master-formulas`, `production-orders`, `production-batches`, `material-lots` |
| IoT Monitoring | `Monitoring` | `equipment`, `sensors`, `readings`, `alerts`, `telemetry` (ThingsBoard) |
| Quality & Compliance | `Quality` | `quality-documents`, `deviations`, `evidence`, `capa-plans`, `capa-actions`, `analytical-results`, `batch-releases`, `audits`, `audit-findings`, `audit-events`, `regulatory-reports`, `collaboration-tasks`, `task-comments`, `traceability-gaps`, `deviation-trends` |

`Shared` has the base classes of the domain and the repositories, the unit of work, the domain events (dispatched with Cortex.Mediator), the Problem Details answers, the kebab-case route convention, the localized messages and the OpenAPI configuration.

The Scalar reference lists every operation with its parameters, its request body and its responses.

## 🏗️ Architecture (DDD)

```text
IngesCompanyCCPlatform.API/<BoundedContext>/
├── Domain/
│   ├── Model/            # Aggregates, entities, value objects, commands, queries and errors
│   └── Repositories/     # Repository interfaces
├── Application/
│   ├── CommandServices/  # Command service interfaces
│   ├── QueryServices/    # Query service interfaces
│   └── Internal/         # Their implementations (use cases) and outbound services
├── Infrastructure/       # Entity Framework Core configuration and repositories, external gateways
└── Interfaces/
    └── Rest/             # Controllers, resources and assemblers
```

Controllers only translate resources into commands or queries, and results into HTTP answers. The business rules live in the aggregates and the command services. IAM also exposes a facade (`Iam/Interfaces/Acl`) for the other bounded contexts.

## 🔐 Authentication

1. **Register an organization** with `POST /api/v1/organizations`, and its first user with `POST /api/v1/authentication/sign-up`. The first user of an organization is always its **administrator**; the role sent by the client is ignored.
2. The administrator invites the rest of the team with `POST /api/v1/invitations`. An invited person signs up with the same email and gets the **role, organization and facility of the invitation**. Without an invitation, signing up to an organization that already has users answers `403 Forbidden`.
3. `POST /api/v1/authentication/sign-in` with the email, the password and, if the user enrolled it, the six-digit two-factor code answers the user and a `token`.
4. Send the token in every other request:

   ```http
   Authorization: Bearer <token>
   ```

Every endpoint needs the token except the public ones: the sign-in and the sign-up, `POST /tokens`, `POST /users`, `POST /contact-inquiries` (Contact us form), `POST /telemetry` (protected by the ThingsBoard API key), `GET /plans` and the calls of the registration of a new organization (`POST /organizations`, `GET /organizations?ruc=`, the first `POST /facilities` and `POST /subscriptions`). A test fails if an endpoint is added without `[Authorize]` or `[AllowAnonymous]`.

Passwords and two-factor codes are stored hashed with BCrypt.

## 🌐 Internationalization

The answers follow the `Accept-Language` header of the request: English (`en`, default) and Spanish (`es`). The messages are in the `.resx` files of `Shared/Resources` and of the Manufacturing, Monitoring, Quality and Subscriptions bounded contexts.

## 🧪 Tests

```bash
dotnet test
```

`IngesCompanyCCPlatform.API.Tests` uses xUnit with in-memory test doubles. It covers the sign-in with two-factor authentication and the sign-up rules, the invitations, the subscriptions, their payments and the Niubiz gateway, the quality aggregates and the authorization of every endpoint.

## ☁️ Deployment

The API is published as a **Docker** container on **Render**, and the database is a **MySQL** service on **Railway**.

1. In [Railway](https://railway.com/), create a MySQL service and copy its host, port, database, user and password.
2. In [Render](https://render.com/), create a **Web Service** from this repository, branch `main`, runtime **Docker** (it uses the `Dockerfile` of the root).
3. Add these environment variables to the service:

   | Variable | Value |
   |----------|-------|
   | `DATABASE_URL` | Host of the Railway database |
   | `DATABASE_PORT` | Port of the Railway database |
   | `DATABASE_NAME` | Name of the database |
   | `DATABASE_USER` | User of the database |
   | `DATABASE_PASSWORD` | Password of the database |
   | `TokenSettings__Secret` | A long random value (at least 32 characters; **required**: the API does not start without it) |
   | `ThingsBoard__ApiKey` | The key configured in the ThingsBoard rule chain |
   | `Niubiz__MerchantId`, `Niubiz__Username`, `Niubiz__Password` | Credentials of Niubiz |

4. Every push to `main` deploys a new version. The container listens on the `PORT` that Render gives it and applies the migrations on startup.

The Web Application reads the URL of the API from `VITE_PLATFORM_API_URL` in its `.env.production`.

## 🔀 Version control

- **GitFlow:** `feature/<name>` branches from `develop`; `release/v<version>` from `develop` to `main` with a tag; `hotfix/v<version>` from `main` for errors in production. Every branch is merged with `--no-ff` and then deleted.
- **Conventional Commits** for the messages (`feat`, `fix`, `docs`, `refactor`, `test`, `chore`).
- **Semantic Versioning** for the releases. The changes of each version are in the [CHANGELOG](CHANGELOG.md).

## 🛠️ Tech stack

- **Framework:** ASP.NET Core 10 (C#) with controllers.
- **Persistence:** Entity Framework Core 10 with the MySQL provider (`MySql.EntityFrameworkCore`), snake_case names and pluralized table names (Humanizer).
- **Mediator:** Cortex.Mediator for the domain events.
- **Security:** JSON Web Tokens and BCrypt.
- **Documentation:** OpenAPI (`Microsoft.AspNetCore.OpenApi`) with the Scalar reference.
- **External services:** Niubiz (payments) and ThingsBoard (IoT telemetry).
- **Tests:** xUnit.

## 👥 Team IngesCompanyCC

| Member | GitHub |
|--------|--------|
| Angulo Ramírez, Marcelo Martín | [Zock2005](https://github.com/Zock2005) |
| Cobades Zamora, Yhoshua Hebert | [YhoshuaCZ](https://github.com/YhoshuaCZ) |
| Rojas Ambicho, Nestor Daniel | [danRO-20](https://github.com/danRO-20) |
| Yarleque Ruiz, Cristina Marcela | [Cris06luna](https://github.com/Cris06luna) |
| Zavaleta Gutierrez, Rodolfo Martin | [gutierrezrodolfo360-bit](https://github.com/gutierrezrodolfo360-bit) |

UPC · Ingeniería de Software · 1ASI0730 Aplicaciones Web · 2026-20
