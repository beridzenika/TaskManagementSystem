# Task Management System

A RESTful API built with ASP.NET Core for managing users, projects, tasks, and comments.

## Tech Stack

- **ASP.NET Core Web API** — framework
- **Entity Framework Core** — ORM for database access
- **MySQL** — database
- **FluentValidation** — input validation
- **xUnit** — unit testing framework
- **Moq** — mocking library for tests
- **Swagger** — API documentation and testing UI

## Project Structure

```
TaskManagementSystem/
├── Controllers/        # HTTP endpoints — thin, no business logic
├── Services/           # Business logic and database operations
├── Models/             # EF Core entity classes (database tables)
├── DTOs/               # Data Transfer Objects (request/response shapes)
├── Validators/         # FluentValidation rules
├── Data/               # AppDbContext
└── TaskManagement.Tests/
    └── Tests/          # xUnit unit tests
```

## Data Models

| Table | Fields |
|---|---|
| User | Id, FirstName, LastName, Email |
| Project | Id, Name, Description |
| TaskItem | Id, Title, Description, Status, ProjectId, AssignedUserId |
| Comment | Id, Content, TaskId|


## Getting Started

### Prerequisites

- .NET 10 SDK
- Sqlate Server

### Setup

1. Clone the repository and open the solution in Visual Studio.

2. Install dependencies via NuGet Package Manager Console:
```
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Sqlite
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package FluentValidation
Install-Package FluentValidation.AspNetCore
```

3. Run migrations to create the database tables:
```
Add-Migration InitialCreate
Update-Database
```

4. Run the project. Swagger UI will be available at:
```
https://localhost:{port}/swagger
```

## API Endpoints

### Users
| Method | Endpoint | Description |
|---|---|---|
| GET | /api/users | Get all users |
| GET | /api/users/{id} | Get user by id |
| POST | /api/users | Create a user |
| PUT | /api/users/{id} | Update a user |
| DELETE | /api/users/{id} | Delete a user |

### Projects
| Method | Endpoint | Description |
|---|---|---|
| GET | /api/programs | Get all projects |
| GET | /api/programs/{id} | Get project by id |
| POST | /api/programs | Create a project |
| PUT | /api/programs/{id} | Update a project |
| DELETE | /api/programs/{id} | Delete a project |

### Task Items
| Method | Endpoint | Description |
|---|---|---|
| GET | /api/taskitems | Get all tasks |
| GET | /api/taskitems/{id} | Get task by id |
| POST | /api/taskitems | Create a task |
| PUT | /api/taskitems/{id} | Update a task |
| DELETE | /api/taskitems/{id} | Delete a task |

### Comments
| Method | Endpoint | Description |
|---|---|---|
| GET | /api/comments | Get all comments |
| GET | /api/comments/{id} | Get comment by id |
| POST | /api/comments | Create a comment |
| PUT | /api/comments/{id} | Update a comment |
| DELETE | /api/comments/{id} | Delete a comment |

## Validation

Validation is handled by FluentValidation in the service layer.

| Resource | Rules |
|---|---|
| User | FirstName and LastName required. Email required and must be valid format. |
| Project | Name required, max 100 chars. |
| TaskItem | Title required. ProjectId and AssignedUserId must be positive and must reference existing records. |
| Comment | Content required. |


## Architecture

Business logic lives in the **service layer**, not controllers. Controllers only receive requests, call the service, and return the result.

```
Request → Controller → Service → Database
                    ↑
             Validator runs here
```

All services are registered with **scoped lifetime** via dependency injection in `Program.cs`, which means a new instance is created per HTTP request.

## Running Tests

Tests are in the `TaskManagement.Tests` project.
check: `https://github.com/beridzenika/TaskManagement.Tests`

Run them via:

- Visual Studio: **Test → Run All Tests**
- Or open the **Test Explorer** panel

Each service has unit tests covering happy paths, not found cases, and validation failures. Tests use:
- **In-memory EF Core database** — no real MySQL needed during tests
- **Moq** — to mock the validator so tests control validation outcomes independently
