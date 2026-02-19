# Udemy First Project – Getting Started with .NET Clean Architecture (Single Entity Focus)

🎓 **Udemy Course (source):** https://www.udemy.com/course/getting-started-with-net-core-clean-architecture/   
📜 **Certificate:** https://drive.google.com/file/d/1s9o987DcKpfzg9jvmMEConmgTctSvR_K/view?usp=drive_link  
🎥 **Testing / Walkthrough Video:** https://drive.google.com/file/d/1CH5f1HBFkDtt5-0H_jpRDOf6E4xp1_QL/view?usp=drive_link  

---

## Overview

This project is an implementation of **Clean Architecture in .NET**, based on the Udemy course  
**“Getting Started with .NET Core Clean Architecture”**.

The main goal is to deeply understand:
- **Layered separation**
- **MediatR**
- **Command / Handler flow**

To keep the learning focused and clear, the system is implemented around **a single entity**.

---

## Architecture (8 Layers)

The solution follows Clean Architecture principles to ensure **loose coupling**, **high cohesion**, and **testability**.

### 1) Domain Layer
- Contains core **domain entities (models)**.
- Defines **repository interfaces** only (no implementations).
- Includes **Commands** and **Command Handlers definitions** related to the domain.
- Completely independent of infrastructure and frameworks.

### 2) Domain.Core Layer
- Provides the **Mediator handler abstraction** (communication contract).
- Defines **Domain Events**.
- Includes a shared abstract **Command base class** used across the system.

### 3) Application Layer
- Contains **application logic only** (no infrastructure concerns).
- Includes:
  - Services (interfaces & implementations)
  - DTOs / ViewModels
  - Mapping logic between ViewModels, Commands, and domain models
- Responsible for orchestrating **use cases** and **business workflows**.

### 4) Infrastructure.Data Layer
- Contains EF Core **DbContext** and **Migrations**.
- Implements repository interfaces defined in the Domain layer.
- Handles all database operations via **Entity Framework Core**.

### 5) Infrastructure.Bus Layer
- Implements the Mediator handler.
- Sends commands to **MediatR** and dispatches them to the correct handlers.
- Acts as the messaging bridge between Application and Domain logic.

### 6) Infrastructure.IoC Layer
- Centralized **Dependency Injection** container.
- Registers:
  - Repositories
  - Services
  - Commands & Command Handlers
- **Note:** MediatR and AutoMapper registrations are configured in `Program.cs` (to keep mediator setup explicit and easy to follow).

### 7) API Layer
- Contains **thin controllers** (no business logic).
- Exposes REST endpoints and delegates work to the Application layer.
- `Program.cs` configures:
  - EF Core connection
  - MediatR
  - Swagger
  - Dependency Injection

### 8) MVC (UI) Layer
- Client application used to **consume and test the API**.
- Contains MVC Controllers + Razor Views focused only on UI rendering.
- ASP.NET Identity is scaffolded here, so Identity and its DbContext live in the MVC project.
- This was done intentionally to keep the Clean Architecture flow focused on **Mediator + Commands**.

---

## Request Flow (Create Entity Example)

1. User sends a request from the MVC UI  
2. MVC Controller calls the API endpoint  
3. API Controller calls the Application Service  
4. Service maps the ViewModel (DTO) → **CreateEntityCommand** (constructor enforces required data)  
5. Command is sent through the Bus layer → forwarded to **MediatR**  
6. MediatR resolves the correct `IRequestHandler<CreateEntityCommand>`  
7. Handler:
   - Maps Command → Domain Entity (manual mapping)
   - Uses repository to persist data
   - Saves changes to the database  
8. Result returns back through layers to the UI

---

## Notes
- **Mediator pattern** is the main focus of this project.
- Identity is scaffolded only for authentication support and **not part of the Clean Architecture flow**.
- A testing video is included to demonstrate the end-to-end request lifecycle.
