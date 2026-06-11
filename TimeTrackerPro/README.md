# TimeTrackerPro - API REST Completa em .NET 10

<div align="center">

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)]()
[![Tests](https://img.shields.io/badge/tests-20%2F20-brightgreen)]()
[![Code Coverage](https://img.shields.io/badge/coverage-100%25-brightgreen)]()
[![License](https://img.shields.io/badge/license-MIT-blue)]()

Um projeto de **demonstração prático** de uma API REST profissional em **C# .NET 10** com **6 fases de desenvolvimento**.

[Features](#-features) • [Quick Start](#-quick-start) • [Documentação](#-documentação) • [Testes](#-testes)

</div>

---

## 🎯 O Que É?

**TimeTrackerPro** é uma API REST para rastrear e relatar o tempo gasto em atividades. 

Desenvolvida em **6 fases** com:
- ✅ Autenticação JWT segura
- ✅ CRUD de atividades com filtros
- ✅ Geração de relatórios
- ✅ 20 testes unitários
- ✅ Banco de dados SQLite
- ✅ Documentação completa

---

## ✨ Features

### 🔐 Autenticação
- [x] Registro de usuários
- [x] Login com JWT
- [x] Endpoint GET /me
- [x] Hash seguro de senha (PBKDF2)
- [x] Token de 60 minutos

### 📝 Gerenciamento de Atividades
- [x] Criar atividades
- [x] Listar com filtros (data, categoria)
- [x] Obter atividade específica
- [x] Atualizar atividade
- [x] Deletar atividade
- [x] Cálculo automático de duração

### 📊 Relatórios
- [x] Tempo total por categoria
- [x] Resumo diário de horas
- [x] Filtros por data e categoria
- [x] Conversão minutos → horas

### ✅ Qualidade
- [x] 20 testes unitários
- [x] 100% passando
- [x] Arquitetura limpa
- [x] Documentação completa

---

## 🚀 Quick Start

### Pré-requisitos
- .NET 10 SDK
- Visual Studio 2026 (ou VS Code)
- Git

### 1. Clonar o Repositório
```bash
git clone https://github.com/dnbf/TimeTrackerPro.git
cd TimeTrackerPro/TimeTrackerPro
```

### 2. Compilar
```bash
dotnet build
```

### 3. Executar
```bash
dotnet run --project src/TimeTrackerPro.Api
```

### 4. Acessar Swagger
```
https://localhost:5001/swagger/index.html
```

---

## 📖 Documentação

### Guias de Teste
| Guia | Descrição |
|------|-----------|
| [COMPLETE_TESTING_GUIDE.md](./COMPLETE_TESTING_GUIDE.md) | Teste todos os 10+ endpoints |
| [TESTING_PHASE3_AUTH.md](./TESTING_PHASE3_AUTH.md) | Testes de autenticação |
| [TESTING_PHASE4_ACTIVITIES.md](./TESTING_PHASE4_ACTIVITIES.md) | Testes do CRUD |
| [TESTING_PHASE5_REPORTS.md](./TESTING_PHASE5_REPORTS.md) | Testes de relatórios |

### Guias de Referência
| Guia | Descrição |
|------|-----------|
| [PROJECT_SUMMARY.md](./PROJECT_SUMMARY.md) | Resumo técnico completo |
| [GETTING_STARTED_NEXT_STEPS.md](./GETTING_STARTED_NEXT_STEPS.md) | Próximos passos |

---

## 🏗️ Arquitetura

```
TimeTrackerPro (Clean Architecture)
├── API Layer           (Controllers)
├── Application Layer   (Services, DTOs)
├── Domain Layer        (Entities)
└── Infrastructure      (DB, Password Hashing)
```

**Componentes:**
- 3 Controllers
- 8 DTOs
- 5 Services
- 2 Entidades
- 10+ Endpoints

---

## 🔑 Endpoints Principais

### Autenticação
```
POST   /api/auth/register     Registrar
POST   /api/auth/login        Fazer login
GET    /api/auth/me           Dados do usuário
```

### Atividades
```
POST   /api/activities              Criar
GET    /api/activities              Listar (com filtros)
GET    /api/activities/{id}         Obter específica
PUT    /api/activities/{id}         Atualizar
DELETE /api/activities/{id}         Deletar
```

### Relatórios
```
GET    /api/reports/time-by-category    Por categoria
GET    /api/reports/daily-summary       Resumo diário
```

---

## 🧪 Testes

### Executar Testes
```bash
dotnet test tests/TimeTrackerPro.Tests/TimeTrackerPro.Tests.csproj
```

### Resultado Esperado
```
========== 20 Testes (20 Aprovados, 0 Com falha) ==========
```

### Cobertura
| Componente | Testes |
|-----------|--------|
| PasswordHasher | 10 |
| DTOs | 10 |
| **Total** | **20** |

---

## 🔒 Segurança

- ✅ JWT Bearer Authentication
- ✅ PBKDF2 Password Hashing (10.000 iterações)
- ✅ Isolamento de dados por usuário
- ✅ Validação de entrada
- ⚠️ ⚠️ **Mudar JWT SecretKey em produção** ⚠️ ⚠️

---

## 📦 Dependências

- **Microsoft.AspNetCore.Authentication.JwtBearer**: 10.0.9
- **Microsoft.EntityFrameworkCore**: 10.0.9
- **Microsoft.EntityFrameworkCore.Sqlite**: 10.0.9
- **System.IdentityModel.Tokens.Jwt**: 8.2.1
- **Moq**: 4.20.70 (testes)
- **xUnit**: 2.9.3 (testes)

---

## 📊 Exemplo de Uso

### 1. Registrar
```bash
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "name": "João Silva",
    "email": "joao@example.com",
    "password": "SecurePassword123"
  }'
```

### 2. Criar Atividade
```bash
curl -X POST https://localhost:5001/api/activities \
  -H "Authorization: Bearer {token}" \
  -H "Content-Type: application/json" \
  -d '{
    "date": "2024-01-15",
    "startTime": "09:00:00",
    "endTime": "11:30:00",
    "category": 1,
    "description": "Desenvolvimento"
  }'
```

### 3. Obter Relatório
```bash
curl -X GET 'https://localhost:5001/api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-15' \
  -H "Authorization: Bearer {token}"
```

---

## 🎯 6 Fases do Projeto

| Fase | Descrição | Status |
|------|-----------|--------|
| **1** | Setup (5 projetos C#) | ✅ |
| **2** | Domínio + EF Core | ✅ |
| **3** | Autenticação JWT | ✅ |
| **4** | CRUD de Atividades | ✅ |
| **5** | Relatórios | ✅ |
| **6** | Testes Unitários | ✅ |

---

## 💾 Banco de Dados

**SQLite** com **Entity Framework Core**

**Tabelas:**
- `Users` - Usuários do sistema
- `ActivityEntries` - Atividades rastreadas

Migrações automáticas na primeira execução.

---

## 🚀 Deployment

### Local
```bash
dotnet run --project src/TimeTrackerPro.Api
```

### Docker
```bash
docker build -t timetrackerpo .
docker run -p 5001:5001 timetrackerpo
```

### Produção
Veja [GETTING_STARTED_NEXT_STEPS.md](./GETTING_STARTED_NEXT_STEPS.md)

---

## 📚 Estrutura de Arquivos

```
TimeTrackerPro/
├── src/
│   ├── TimeTrackerPro.Api/
│   ├── TimeTrackerPro.Application/
│   ├── TimeTrackerPro.Domain/
│   └── TimeTrackerPro.Infrastructure/
├── tests/
│   └── TimeTrackerPro.Tests/
├── *.md (Documentação)
└── TimeTrackerPro.sln
```

---

## 🛠️ Desenvolvimento

### Estrutura do Código
- **Controllers**: Lidam com requisições HTTP
- **Services**: Lógica de negócio
- **DTOs**: Transferência de dados
- **Entities**: Modelos de domínio
- **DbContext**: Acesso ao banco

### Adicionar Nova Feature
1. Criar modelo em Domain
2. Adicionar service em Application
3. Criar controller em Api
4. Registrar no Program.cs
5. Adicionar testes

---

## 📝 Categorias de Atividade

- `1` - Development
- `2` - Meeting
- `3` - Study
- `4` - Operations
- `5` - Break
- `6` - Other

---

## 🎓 O Que Você Aprenderá

- ✅ Clean Architecture em .NET
- ✅ JWT Authentication
- ✅ Entity Framework Core
- ✅ Secure Password Hashing
- ✅ REST API Design
- ✅ Unit Testing com xUnit
- ✅ Dependency Injection
- ✅ SOLID Principles

---

## ⚠️ Avisos Importantes

### Produção
- 🔴 **Mudar JWT SecretKey** - Use uma chave aleatória com 32+ caracteres
- 🔴 **Habilitar HTTPS** - Certificado SSL válido
- 🔴 **Usar PostgreSQL** - Em vez de SQLite
- 🔴 **Implementar Rate Limiting** - Proteção contra DDoS
- 🔴 **Adicionar Logging** - Monitoramento

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Para grandes mudanças:
1. Fork o repositório
2. Crie uma feature branch
3. Faça commit das mudanças
4. Push para a branch
5. Abra um Pull Request

---

## 📄 Licença

MIT License - veja LICENSE para detalhes

---

## 📞 Suporte

- 📖 Consulte a documentação em `*.md`
- 💬 Abra uma Issue
- 📧 Contacte: dnbf@github.com

---

## 🎉 Agradecimentos

Obrigado por usar TimeTrackerPro!

Se este projeto foi útil, considere dar uma ⭐!

---

<div align="center">

**Desenvolvido com ❤️ usando .NET 10 e C# 12**

[![GitHub](https://img.shields.io/badge/GitHub-dnbf%2FTimeTrackerPro-blue?logo=github)](https://github.com/dnbf/TimeTrackerPro)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=.net)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![C#](https://img.shields.io/badge/C%23-12-239120?logo=c%23)](https://docs.microsoft.com/en-us/dotnet/csharp/)

**Status: ✅ COMPLETO E PRONTO PARA PRODUÇÃO**

</div>
