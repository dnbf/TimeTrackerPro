# 🚀 GUIA COMPLETO DE TESTES - Fases 3, 4 e 5

## 📋 Índice
1. [Setup Inicial](#setup-inicial)
2. [Fase 3 - Autenticação](#fase-3--autenticação)
3. [Fase 4 - CRUD de Atividades](#fase-4--crud-de-atividades)
4. [Fase 5 - Relatórios](#fase-5--relatórios)
5. [Testes de Erro](#testes-de-erro)
6. [Checklist Final](#checklist-final)

---

## Setup Inicial

### Pré-requisitos
- Visual Studio 2026 aberto com o projeto TimeTrackerPro
- Postman, Thunder Client, ou Insomnia para testar endpoints
- API rodando em `https://localhost:5001` (ou porta configurada)

### Passos
1. Abra Visual Studio
2. Compile o projeto: `Ctrl + Shift + B` ou Build > Build Solution
3. Rode a aplicação: `F5` ou Debug > Start Debugging
4. Acesse Swagger em: `https://localhost:5001/swagger/index.html`
5. Você deve ver 3 controllers: **Auth**, **Activities**, **Reports**

✅ **Verificação:** Se o Swagger carregar com os 3 controllers, está tudo pronto!

---

# Fase 3 – Autenticação

## Teste 3.1: Registrar Usuário

**URL:** `POST https://localhost:5001/api/auth/register`

**Headers:**
```
Content-Type: application/json
```

**Body:**
```json
{
  "name": "João Silva",
  "email": "joao@example.com",
  "password": "Password123"
}
```

**Esperado (201 Created):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "João Silva",
  "email": "joao@example.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**✅ Validações:**
- [ ] Status 201 retornado
- [ ] ID é um GUID válido
- [ ] Token é uma string não vazia
- [ ] Usuário pode fazer login com essas credenciais

**💾 Salve o token** - você vai usar nos próximos testes!

---

## Teste 3.2: Tentar Registrar com Email Duplicado

**URL:** `POST https://localhost:5001/api/auth/register`

**Body:**
```json
{
  "name": "Outro Usuário",
  "email": "joao@example.com",
  "password": "Password456"
}
```

**Esperado (400 Bad Request):**
```json
{
  "message": "Email already registered"
}
```

**✅ Validações:**
- [ ] Status 400 retornado
- [ ] Mensagem de erro clara

---

## Teste 3.3: Tentar Registrar com Senha Fraca

**URL:** `POST https://localhost:5001/api/auth/register`

**Body (senha < 8 caracteres):**
```json
{
  "name": "Teste",
  "email": "teste@example.com",
  "password": "Pass12"
}
```

**Esperado (400 Bad Request):**
```json
{
  "message": "Password must be at least 8 characters long"
}
```

**Body (sem letra maiúscula):**
```json
{
  "name": "Teste",
  "email": "teste2@example.com",
  "password": "password123"
}
```

**Esperado (400 Bad Request):**
```json
{
  "message": "Password must contain at least one uppercase letter"
}
```

**Body (sem número):**
```json
{
  "name": "Teste",
  "email": "teste3@example.com",
  "password": "PasswordABC"
}
```

**Esperado (400 Bad Request):**
```json
{
  "message": "Password must contain at least one number"
}
```

**✅ Validações:**
- [ ] Rejeita senha < 8 caracteres
- [ ] Rejeita senha sem maiúscula
- [ ] Rejeita senha sem número

---

## Teste 3.4: Login com Credenciais Corretas

**URL:** `POST https://localhost:5001/api/auth/login`

**Headers:**
```
Content-Type: application/json
```

**Body:**
```json
{
  "email": "joao@example.com",
  "password": "Password123"
}
```

**Esperado (200 OK):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "João Silva",
  "email": "joao@example.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**✅ Validações:**
- [ ] Status 200 retornado
- [ ] Token retornado é válido
- [ ] Token é diferente de cada chamada (refresco)

**💾 Copie este token também - você vai usar!**

---

## Teste 3.5: Login com Credenciais Incorretas

**URL:** `POST https://localhost:5001/api/auth/login`

**Body (senha errada):**
```json
{
  "email": "joao@example.com",
  "password": "WrongPassword123"
}
```

**Esperado (401 Unauthorized):**
```json
{
  "message": "Invalid credentials"
}
```

**Body (email não existe):**
```json
{
  "email": "naoexiste@example.com",
  "password": "Password123"
}
```

**Esperado (401 Unauthorized):**
```json
{
  "message": "Invalid credentials"
}
```

**✅ Validações:**
- [ ] Rejeita senha errada com 401
- [ ] Rejeita email inexistente com 401
- [ ] Mensagem não revela qual campo está errado (segurança)

---

## Teste 3.6: Obter Dados do Usuário Autenticado

**URL:** `GET https://localhost:5001/api/auth/me`

**Headers:**
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json
```

**Esperado (200 OK):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "João Silva",
  "email": "joao@example.com",
  "token": ""
}
```

**✅ Validações:**
- [ ] Status 200 retornado
- [ ] Retorna dados do usuário correto
- [ ] Token é vazio nesta resposta (segurança)

---

## Teste 3.7: Acessar /me sem Token

**URL:** `GET https://localhost:5001/api/auth/me`

**Sem header Authorization**

**Esperado (401 Unauthorized)**

**✅ Validações:**
- [ ] Rejeita requisição sem token
- [ ] Não expõe informações sensíveis

---

# Fase 4 – CRUD de Atividades

## Teste 4.1: Criar Primeira Atividade

**URL:** `POST https://localhost:5001/api/activities`

**Headers:**
```
Authorization: Bearer <seu_token_do_login>
Content-Type: application/json
```

**Body:**
```json
{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "11:00:00",
  "category": 1,
  "description": "Implementação do CRUD de atividades"
}
```

**Esperado (201 Created):**
```json
{
  "id": "660e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "11:00:00",
  "durationMinutes": 120,
  "category": 1,
  "description": "Implementação do CRUD de atividades",
  "createdAt": "2024-01-15T09:00:00Z"
}
```

**✅ Validações:**
- [ ] Status 201 retornado
- [ ] DurationMinutes calculado corretamente (120 minutos = 2 horas)
- [ ] UserId preenchido automaticamente do token
- [ ] ID é um GUID válido

**💾 Salve o ID da atividade - você vai precisar!**

---

## Teste 4.2: Criar Segunda Atividade (mesmo dia)

**URL:** `POST https://localhost:5001/api/activities`

**Body:**
```json
{
  "date": "2024-01-15",
  "startTime": "14:00:00",
  "endTime": "15:30:00",
  "category": 2,
  "description": "Reunião com o time"
}
```

**Esperado (201 Created):**
- DurationMinutes = 90 minutos

**✅ Validações:**
- [ ] Segunda atividade criada sem problemas
- [ ] Pode ter múltiplas atividades no mesmo dia

**💾 Salve este ID também!**

---

## Teste 4.3: Criar Terceira Atividade (dia diferente)

**URL:** `POST https://localhost:5001/api/activities`

**Body:**
```json
{
  "date": "2024-01-16",
  "startTime": "09:00:00",
  "endTime": "12:30:00",
  "category": 1,
  "description": "Desenvolvimento - dia 16"
}
```

**Esperado (201 Created):**
- DurationMinutes = 210 minutos

**✅ Validações:**
- [ ] Terceira atividade em data diferente criada com sucesso

---

## Teste 4.4: Listar Todas as Atividades

**URL:** `GET https://localhost:5001/api/activities`

**Headers:**
```
Authorization: Bearer <seu_token>
Content-Type: application/json
```

**Esperado (200 OK):**
```json
[
  {
    "id": "660e8400-e29b-41d4-a716-446655440003",
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "date": "2024-01-16",
    "startTime": "09:00:00",
    "endTime": "12:30:00",
    "durationMinutes": 210,
    "category": 1,
    "description": "Desenvolvimento - dia 16",
    "createdAt": "2024-01-16T09:00:00Z"
  },
  {
    "id": "660e8400-e29b-41d4-a716-446655440002",
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "date": "2024-01-15",
    "startTime": "14:00:00",
    "endTime": "15:30:00",
    "durationMinutes": 90,
    "category": 2,
    "description": "Reunião com o time",
    "createdAt": "2024-01-15T14:00:00Z"
  },
  {
    "id": "660e8400-e29b-41d4-a716-446655440001",
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "date": "2024-01-15",
    "startTime": "09:00:00",
    "endTime": "11:00:00",
    "durationMinutes": 120,
    "category": 1,
    "description": "Implementação do CRUD de atividades",
    "createdAt": "2024-01-15T09:00:00Z"
  }
]
```

**✅ Validações:**
- [ ] Retorna todas as 3 atividades
- [ ] Apenas atividades do usuário autenticado aparecem
- [ ] Ordenadas por data descrescente, depois por hora descrescente
- [ ] Cada atividade tem todos os campos corretos

---

## Teste 4.5: Listar Atividades com Filtro de Data

**URL:** `GET https://localhost:5001/api/activities?startDate=2024-01-15&endDate=2024-01-15`

**Headers:**
```
Authorization: Bearer <seu_token>
Content-Type: application/json
```

**Esperado (200 OK):**
```json
[
  // Apenas as 2 atividades de 2024-01-15
]
```

**Teste com apenas startDate:**
```
GET https://localhost:5001/api/activities?startDate=2024-01-15
```

**Teste com apenas endDate:**
```
GET https://localhost:5001/api/activities?endDate=2024-01-16
```

**✅ Validações:**
- [ ] Filtro startDate funciona
- [ ] Filtro endDate funciona
- [ ] Ambos combinados funcionam
- [ ] Retorna apenas atividades no intervalo

---

## Teste 4.6: Listar Atividades com Filtro de Categoria

**URL:** `GET https://localhost:5001/api/activities?category=1`

**Headers:**
```
Authorization: Bearer <seu_token>
Content-Type: application/json
```

**Esperado (200 OK):**
```json
[
  // Apenas atividades com category=1 (Development)
  // Deve ter 2 atividades neste caso
]
```

**Teste com category=2:**
```
GET https://localhost:5001/api/activities?category=2
```

**Esperado:**
```json
[
  // Apenas 1 atividade com category=2 (Meeting)
]
```

**✅ Validações:**
- [ ] Filtro de categoria funciona
- [ ] Category=1 retorna 2 atividades
- [ ] Category=2 retorna 1 atividade

---

## Teste 4.7: Obter Atividade Específica

**URL:** `GET https://localhost:5001/api/activities/660e8400-e29b-41d4-a716-446655440001`

Substitua o ID por um ID válido retornado anteriormente

**Headers:**
```
Authorization: Bearer <seu_token>
Content-Type: application/json
```

**Esperado (200 OK):**
```json
{
  "id": "660e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "11:00:00",
  "durationMinutes": 120,
  "category": 1,
  "description": "Implementação do CRUD de atividades",
  "createdAt": "2024-01-15T09:00:00Z"
}
```

**✅ Validações:**
- [ ] Retorna atividade correta
- [ ] Todos os campos estão presentes

---

## Teste 4.8: Atualizar Atividade

**URL:** `PUT https://localhost:5001/api/activities/660e8400-e29b-41d4-a716-446655440001`

Substitua o ID por um ID válido

**Headers:**
```
Authorization: Bearer <seu_token>
Content-Type: application/json
```

**Body:**
```json
{
  "date": "2024-01-15",
  "startTime": "10:00:00",
  "endTime": "11:30:00",
  "category": 1,
  "description": "Implementação do CRUD - ATUALIZADO"
}
```

**Esperado (200 OK):**
```json
{
  "id": "660e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-01-15",
  "startTime": "10:00:00",
  "endTime": "11:30:00",
  "durationMinutes": 90,
  "category": 1,
  "description": "Implementação do CRUD - ATUALIZADO",
  "createdAt": "2024-01-15T09:00:00Z"
}
```

**✅ Validações:**
- [ ] Atividade atualizada com sucesso
- [ ] StartTime e EndTime atualizados
- [ ] DurationMinutes recalculado (agora 90 em vez de 120)
- [ ] Descrição atualizada
- [ ] CreatedAt não muda

---

## Teste 4.9: Deletar Atividade

**URL:** `DELETE https://localhost:5001/api/activities/660e8400-e29b-41d4-a716-446655440002`

Substitua o ID por um ID válido (preferencialmente o que você não vai precisar mais)

**Headers:**
```
Authorization: Bearer <seu_token>
Content-Type: application/json
```

**Esperado (204 No Content)**

**Validação:**
```
GET https://localhost:5001/api/activities/{id_deletado}
```

**Esperado (404 Not Found):**
```json
{
  "message": "Activity not found"
}
```

**✅ Validações:**
- [ ] Delete retorna 204
- [ ] Atividade não pode mais ser recuperada
- [ ] Lista de atividades não inclui a deletada

---

# Fase 5 – Relatórios

## Teste 5.1: Relatório de Tempo por Categoria (sem filtro)

**URL:** `GET https://localhost:5001/api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-16`

**Headers:**
```
Authorization: Bearer <seu_token>
Content-Type: application/json
```

**Esperado (200 OK):**
```json
[
  {
    "category": "Development",
    "totalMinutes": 330,
    "totalHours": 5.5,
    "activityCount": 2
  },
  {
    "category": "Meeting",
    "totalMinutes": 90,
    "totalHours": 1.5,
    "activityCount": 1
  }
]
```

**Cálculos:**
- Development: (90 + 210) = 300 minutos = 5.0 horas (2 atividades)
  - Nota: A primeira atividade foi atualizada de 120 para 90 minutos
- Meeting: 90 minutos = 1.5 horas (1 atividade)

**✅ Validações:**
- [ ] Relatório retornado com sucesso
- [ ] Minutos somados corretamente
- [ ] Horas convertidas corretamente (minutos / 60)
- [ ] Contagem de atividades correta
- [ ] Categorias ordenadas alfabeticamente

---

## Teste 5.2: Relatório de Tempo por Categoria Específica

**URL:** `GET https://localhost:5001/api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-16&category=1`

(Category 1 = Development)

**Headers:**
```
Authorization: Bearer <seu_token>
Content-Type: application/json
```

**Esperado (200 OK):**
```json
[
  {
    "category": "Development",
    "totalMinutes": 300,
    "totalHours": 5.0,
    "activityCount": 2
  }
]
```

**✅ Validações:**
- [ ] Filtra apenas a categoria solicitada
- [ ] Outras categorias não aparecem
- [ ] Cálculos estão corretos

---

## Teste 5.3: Relatório Diário

**URL:** `GET https://localhost:5001/api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-16`

**Headers:**
```
Authorization: Bearer <seu_token>
Content-Type: application/json
```

**Esperado (200 OK):**
```json
[
  {
    "date": "2024-01-15",
    "totalMinutes": 180,
    "totalHours": 3.0,
    "activityCount": 2
  },
  {
    "date": "2024-01-16",
    "totalMinutes": 210,
    "totalHours": 3.5,
    "activityCount": 1
  }
]
```

**Cálculos:**
- 2024-01-15: 90 + 90 = 180 minutos = 3.0 horas (2 atividades)
- 2024-01-16: 210 minutos = 3.5 horas (1 atividade)

**✅ Validações:**
- [ ] Relatório retornado com sucesso
- [ ] Minutos por dia somados corretamente
- [ ] Horas calculadas corretamente
- [ ] Atividades contadas corretamente
- [ ] Datas em ordem crescente

---

## Teste 5.4: Relatório de Um Dia Específico

**URL:** `GET https://localhost:5001/api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-15`

**Headers:**
```
Authorization: Bearer <seu_token>
Content-Type: application/json
```

**Esperado (200 OK):**
```json
[
  {
    "date": "2024-01-15",
    "totalMinutes": 180,
    "totalHours": 3.0,
    "activityCount": 2
  }
]
```

**✅ Validações:**
- [ ] Retorna apenas dados de um dia
- [ ] Cálculos corretos para esse dia

---

# Testes de Erro

## Erro 1: Criar Atividade sem Token

**URL:** `POST https://localhost:5001/api/activities`

**Sem header Authorization**

**Esperado (401 Unauthorized)**

---

## Erro 2: StartTime >= EndTime

**URL:** `POST https://localhost:5001/api/activities`

**Body:**
```json
{
  "date": "2024-01-15",
  "startTime": "11:00:00",
  "endTime": "10:00:00",
  "category": 1,
  "description": "Teste"
}
```

**Esperado (400 Bad Request):**
```json
{
  "message": "StartTime must be before EndTime"
}
```

---

## Erro 3: Categoria Inválida

**URL:** `POST https://localhost:5001/api/activities`

**Body:**
```json
{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:00:00",
  "category": 99,
  "description": "Teste"
}
```

**Esperado (400 Bad Request):**
```json
{
  "message": "Invalid category"
}
```

---

## Erro 4: Descrição muito longa

**URL:** `POST https://localhost:5001/api/activities`

**Body:**
```json
{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:00:00",
  "category": 1,
  "description": "Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua Ut enim ad minim veniam quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur Excepteur sint occaecat cupidatat non proident sunt in culpa qui officia deserunt mollit anim id est laborumLorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"
}
```

**Esperado (400 Bad Request):**
```json
{
  "message": "Description cannot exceed 500 characters"
}
```

---

## Erro 5: Relatório sem datas

**URL:** `GET https://localhost:5001/api/reports/time-by-category`

**Esperado (400 Bad Request):**
```json
{
  "message": "startDate and endDate are required"
}
```

---

## Erro 6: StartDate > EndDate em Relatório

**URL:** `GET https://localhost:5001/api/reports/daily-summary?startDate=2024-01-20&endDate=2024-01-15`

**Esperado (400 Bad Request):**
```json
{
  "message": "Start date must be before or equal to end date"
}
```

---

# Checklist Final

## ✅ Fase 3 - Autenticação
- [ ] POST /api/auth/register com dados válidos retorna 201
- [ ] POST /api/auth/register com email duplicado retorna 400
- [ ] POST /api/auth/register valida senha (8+ chars, 1 maiúscula, 1 número)
- [ ] POST /api/auth/login com credenciais corretas retorna 200
- [ ] POST /api/auth/login com credenciais erradas retorna 401
- [ ] GET /api/auth/me com token válido retorna 200
- [ ] GET /api/auth/me sem token retorna 401

## ✅ Fase 4 - Atividades
- [ ] POST /api/activities cria atividade com sucesso (201)
- [ ] POST /api/activities calcula DurationMinutes corretamente
- [ ] POST /api/activities preenche UserId do token automaticamente
- [ ] GET /api/activities retorna todas as atividades do usuário
- [ ] GET /api/activities filtra por startDate
- [ ] GET /api/activities filtra por endDate
- [ ] GET /api/activities filtra por categoria
- [ ] GET /api/activities/{id} retorna atividade específica
- [ ] PUT /api/activities/{id} atualiza atividade
- [ ] PUT /api/activities/{id} recalcula DurationMinutes
- [ ] DELETE /api/activities/{id} remove atividade (204)

## ✅ Fase 5 - Relatórios
- [ ] GET /api/reports/time-by-category retorna dados corretos
- [ ] GET /api/reports/time-by-category com filtro de categoria funciona
- [ ] Minutos são somados corretamente em time-by-category
- [ ] Horas são convertidas corretamente (minutos/60)
- [ ] Categorias em ordem alfabética
- [ ] GET /api/reports/daily-summary retorna dados corretos
- [ ] Datas em ordem crescente em daily-summary
- [ ] Minutos por dia somados corretamente

## ✅ Segurança e Isolamento
- [ ] Endpoints sem [Authorize] retornam 401
- [ ] Usuários só veem suas próprias atividades
- [ ] Usuários não podem atualizar/deletar atividades de outros
- [ ] Relatórios mostram apenas dados do usuário autenticado

## ✅ Validações
- [ ] StartTime < EndTime obrigatório
- [ ] Categoria válida obrigatória
- [ ] Descrição máx 500 caracteres
- [ ] Datas obrigatórias em relatórios
- [ ] StartDate <= EndDate obrigatório em relatórios

---

## 🎯 Resumo

Se todos os testes passarem:
- ✅ Autenticação JWT funcionando
- ✅ CRUD de atividades com filtros funcionando
- ✅ Relatórios gerando dados corretos
- ✅ Segurança implementada (tokens, isolamento de usuário)
- ✅ Validações de dados funcionando
- ✅ Tratamento de erros apropriado

Após completar todos os testes, você estará pronto para a **Fase 6 - Qualidade mínima (Testes Unitários)** 🚀
