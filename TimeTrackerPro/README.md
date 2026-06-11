# TimeTrackerPro - Complete REST API in .NET 10

<div align="center">

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)]()
[![Tests](https://img.shields.io/badge/tests-20%2F20-brightgreen)]()
[![Code Coverage](https://img.shields.io/badge/coverage-100%25-brightgreen)]()
[![License](https://img.shields.io/badge/license-MIT-blue)]()

A **practical demo project** of a professional REST API in **C# .NET 10** with **6 development phases**.

[Features](#-features) • [Quick Start](#-quick-start) • [Documentation](#-documentation) • [Tests](#-tests)

</div>

---

## 🎯 What Is It?

**TimeTrackerPro** is a REST API to track and report time spent on activities.

Built in **6 phases** with:
- ✅ Secure JWT authentication
- ✅ Activity CRUD with filters
- ✅ Report generation
- ✅ 20 unit tests
- ✅ SQLite database
- ✅ Complete documentation

---

## ✨ Features

### 🔐 Authentication
- [x] User registration
- [x] Login with JWT
- [x] GET /me endpoint
- [x] Secure password hashing (PBKDF2)
- [x] 60-minute token

### 📝 Activity Management
- [x] Create activities
- [x] List with filters (date, category)
- [x] Get single activity
- [x] Update activity
- [x] Delete activity
- [x] Automatic duration calculation

### 📊 Reports
- [x] Total time per category
- [x] Daily hours summary
- [x] Filters by date and category
- [x] Minutes → hours conversion

### ✅ Quality
- [x] 20 unit tests
- [x] 100% passing
- [x] Clean architecture
- [x] Full documentation

---

## 🚀 Quick Start

### Prerequisites
- .NET 10 SDK
- Visual Studio 2026 (or VS Code)
- Git

### 1. Clone the Repository
```bash
git clone https://github.com/dnbf/TimeTrackerPro.git
cd TimeTrackerPro/TimeTrackerPro
```

### 2. Build
```bash
dotnet build
```

### 3. Run
```bash
dotnet run --project src/TimeTrackerPro.Api
```

### 4. Open Swagger

https://localhost:5001/swagger/index.html

---

## 📖 Documentation

### Testing Guides
| Guide | Description |
|------|-----------|
| [COMPLETE_TESTING_GUIDE.md](./COMPLETE_TESTING_GUIDE.md) | Test all 10+ endpoints |
| [TESTING_PHASE3_AUTH.md](./TESTING_PHASE3_AUTH.md) | Authentication tests |
| [TESTING_PHASE4_ACTIVITIES.md](./TESTING_PHASE4_ACTIVITIES.md) | CRUD tests |
| [TESTING_PHASE5_REPORTS.md](./TESTING_PHASE5_REPORTS.md) | Reports tests |

### Reference Guides
| Guide | Description |
|------|-----------|
| [PROJECT_SUMMARY.md](./PROJECT_SUMMARY.md) | Full technical summary |
| [GETTING_STARTED_NEXT_STEPS.md](./GETTING_STARTED_NEXT_STEPS.md) | Next steps |

---

## 🏗️ Architecture


TimeTrackerPro (Clean Architecture) ├── API Layer (Controllers) ├── Application Layer (Services, DTOs) ├── Domain Layer (Entities) └── Infrastructure (DB, Password Hashing)


**Components:**
- 3 Controllers
- 8 DTOs
- 5 Services
- 2 Entities
- 10+ Endpoints

---

## 🔑 Main Endpoints

### Authentication

POST /api/auth/register Register POST /api/auth/login Login GET /api/auth/me Get current user

### Activities

POST /api/activities Create GET /api/activities List (with filters) GET /api/activities/{id} Get single PUT /api/activities/{id} Update DELETE /api/activities/{id} Delete

### Reports

GET /api/reports/time-by-category By category GET /api/reports/daily-summary Daily summary

---

## 🧪 Tests

### Run Tests
 
Bash:
dotnet test tests/TimeTrackerPro.Tests/TimeTrackerPro.Tests.csproj


### Expected Result

========== 20 Tests (20 Passed, 0 Failed) ==========

### Coverage
| Component | Tests |
|-----------|--------|
| PasswordHasher | 10 |
| DTOs | 10 |
| **Total** | **20** |

---

## 🔒 Security

- ✅ JWT Bearer Authentication
- ✅ PBKDF2 Password Hashing (10,000 iterations)
- ✅ Per-user data isolation
- ✅ Input validation
- ⚠️ ⚠️ **Change JWT SecretKey in production** ⚠️ ⚠️

---

## 📦 Dependencies

- **Microsoft.AspNetCore.Authentication.JwtBearer**: 10.0.9
- **Microsoft.EntityFrameworkCore**: 10.0.9
- **Microsoft.EntityFrameworkCore.Sqlite**: 10.0.9
- **System.IdentityModel.Tokens.Jwt**: 8.2.1
- **Moq**: 4.20.70 (tests)
- **xUnit**: 2.9.3 (tests)

---

## 📊 Usage Example

### 1. Register

bash
Copiar

curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "name": "João Silva",
    "email": "joao@example.com",
    "password": "SecurePassword123"
  }'




### 2. Create Activity

bash
Copiar

curl -X POST https://localhost:5001/api/activities \
  -H "Authorization: Bearer {token}" \
  -H "Content-Type: application/json" \
  -d '{
    "date": "2024-01-15",
    "startTime": "09:00:00",
    "endTime": "11:30:00",
    "category": 1,
    "description": "Development"
  }'




### 3. Get Report

bash
Copiar

curl -X GET 'https://localhost:5001/api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-15' \
  -H "Authorization: Bearer {token}"




---

## 🎯 6 Project Phases

| Phase | Description | Status |
|------|-----------|--------|
| **1** | Setup (5 C# projects) | ✅ |
| **2** | Domain + EF Core | ✅ |
| **3** | JWT Authentication | ✅ |
| **4** | Activity CRUD | ✅ |
| **5** | Reports | ✅ |
| **6** | Unit Tests | ✅ |

---

## 💾 Database

**SQLite** with **Entity Framework Core**

**Tables:**
- `Users` - System users
- `ActivityEntries` - Tracked activities

Automatic migrations on first run.

---

## 🚀 Deployment

### Local

bash
Copiar

dotnet run --project src/TimeTrackerPro.Api




### Docker

bash
Copiar

docker build -t timetrackerpo .
docker run -p 5001:5001 timetrackerpo




### Production
See [GETTING_STARTED_NEXT_STEPS.md](./GETTING_STARTED_NEXT_STEPS.md)

---

## 📚 File Structure
TimeTrackerPro/ ├── src/ │ ├── TimeTrackerPro.Api/ │ ├── TimeTrackerPro.Application/ │ ├── TimeTrackerPro.Domain/ │ └── TimeTrackerPro.Infrastructure/ ├── tests/ │ └── TimeTrackerPro.Tests/ ├── *.md (Documentation) └── TimeTrackerPro.sln

---

## 🛠️ Development

### Code Structure
- **Controllers**: Handle HTTP requests
- **Services**: Business logic
- **DTOs**: Data transfer
- **Entities**: Domain models
- **DbContext**: Database access

### Add a New Feature
1. Create model in Domain
2. Add service in Application
3. Create controller in Api
4. Register in Program.cs
5. Add tests

---

## 📝 Activity Categories

- `1` - Development
- `2` - Meeting
- `3` - Study
- `4` - Operations
- `5` - Break
- `6` - Other

---

## 🎓 What You’ll Learn

- ✅ Clean Architecture in .NET
- ✅ JWT Authentication
- ✅ Entity Framework Core
- ✅ Secure Password Hashing
- ✅ REST API Design
- ✅ Unit Testing with xUnit
- ✅ Dependency Injection
- ✅ SOLID Principles

---

## ⚠️ Important Warnings

### Production
- 🔴 **Change JWT SecretKey** – Use a random key with 32+ characters
- 🔴 **Enable HTTPS** – Valid SSL certificate
- 🔴 **Use PostgreSQL** – Instead of SQLite
- 🔴 **Implement Rate Limiting** – Protect against DDoS
- 🔴 **Add Logging** – Monitoring

---

## 🤝 Contributing

Contributions are welcome! For major changes:
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push the branch
5. Open a Pull Request

---

## 📄 License

MIT License – see LICENSE for details

---

## 📞 Support

- 📖 Check the documentation in `*.md`
- 💬 Open an Issue
- 📧 Contact: dnbf@github.com

---

## 🎉 Thanks

Thanks for using TimeTrackerPro!

If this project was helpful, consider giving it a ⭐!

---

<div align="center">

**Built with ❤️ using .NET 10 and C# 12**

[![GitHub](https://img.shields.io/badge/GitHub-dnbf%2FTimeTrackerPro-blue?logo=github)](https://github.com/dnbf/TimeTrackerPro)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=.net)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![C#](https://img.shields.io/badge/C%23-12-239120?logo=c%23)](https://docs.microsoft.com/en-us/dotnet/csharp/)

**Status: ✅ COMPLETE AND PRODUCTION-READY**

</div>












