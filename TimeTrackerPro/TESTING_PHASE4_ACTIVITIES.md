# 🧪 Teste do CRUD de ActivityEntry - Fase 4

## Pré-requisitos
- API rodando em `https://localhost:5001`
- Usuário registrado e token JWT obtido
- Postman, Thunder Client, ou similar

---

## Passo 1: Obter o token de autenticação

**Endpoint:** `POST /api/auth/login`

**Request:**
```json
{
  "email": "joao@example.com",
  "password": "Password123"
}
```

**Copie o token retornado para usar nos próximos passos**

---

## Passo 2: Criar uma atividade

**Endpoint:** `POST /api/activities`

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Request:**
```json
{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:30:00",
  "category": 1,
  "description": "Desenvolvimento de feature de autenticação"
}
```

**Categorias disponíveis:**
- `1` = Development
- `2` = Meeting
- `3` = Study
- `4` = Operations
- `5` = Break
- `6` = Other

**Response esperado (201 Created):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:30:00",
  "durationMinutes": 90,
  "category": 1,
  "description": "Desenvolvimento de feature de autenticação",
  "createdAt": "2024-01-15T10:30:00Z"
}
```

**Validações:**
- ✅ DurationMinutes é calculado automaticamente
- ✅ UserId é preenchido com base no token
- ✅ Se StartTime >= EndTime → 400 Bad Request
- ✅ Se categoria inválida → 400 Bad Request
- ✅ Se descrição > 500 caracteres → 400 Bad Request

---

## Passo 3: Listar atividades (sem filtros)

**Endpoint:** `GET /api/activities`

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Response esperado (200 OK):**
```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440001",
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "date": "2024-01-15",
    "startTime": "09:00:00",
    "endTime": "10:30:00",
    "durationMinutes": 90,
    "category": 1,
    "description": "Desenvolvimento de feature de autenticação",
    "createdAt": "2024-01-15T10:30:00Z"
  },
  {
    "id": "550e8400-e29b-41d4-a716-446655440002",
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "date": "2024-01-15",
    "startTime": "14:00:00",
    "endTime": "15:00:00",
    "durationMinutes": 60,
    "category": 2,
    "description": "Reunião com time",
    "createdAt": "2024-01-15T15:00:00Z"
  }
]
```

**Validações:**
- ✅ Retorna apenas atividades do usuário autenticado
- ✅ Ordenadas por data descrescente, depois por hora descrescente

---

## Passo 4: Listar atividades com filtro de data

**Endpoint:** `GET /api/activities?startDate=2024-01-15&endDate=2024-01-15`

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Response esperado (200 OK):**
```json
[
  // Apenas atividades no intervalo de 15/01/2024
]
```

**Variações de teste:**
```
# Apenas startDate
GET /api/activities?startDate=2024-01-15

# Apenas endDate
GET /api/activities?endDate=2024-01-15

# Ambas
GET /api/activities?startDate=2024-01-10&endDate=2024-01-20
```

---

## Passo 5: Listar atividades com filtro de categoria

**Endpoint:** `GET /api/activities?category=1`

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Response esperado (200 OK):**
```json
[
  // Apenas atividades com categoria Development (1)
]
```

---

## Passo 6: Combinar filtros

**Endpoint:** `GET /api/activities?startDate=2024-01-15&endDate=2024-01-20&category=2`

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Response esperado (200 OK):**
```json
[
  // Apenas atividades de Meetings (2) entre 15 e 20 de janeiro
]
```

---

## Passo 7: Obter uma atividade específica

**Endpoint:** `GET /api/activities/{id}`

Substitua `{id}` com um ID retornado nos passos anteriores

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Response esperado (200 OK):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:30:00",
  "durationMinutes": 90,
  "category": 1,
  "description": "Desenvolvimento de feature de autenticação",
  "createdAt": "2024-01-15T10:30:00Z"
}
```

**Validações:**
- ✅ Se ID não pertence ao usuário autenticado → 404 Not Found
- ✅ Se ID não existe → 404 Not Found

---

## Passo 8: Atualizar uma atividade

**Endpoint:** `PUT /api/activities/{id}`

Substitua `{id}` com um ID retornado anteriormente

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Request:**
```json
{
  "date": "2024-01-15",
  "startTime": "10:00:00",
  "endTime": "11:00:00",
  "category": 1,
  "description": "Desenvolvimento - atualizado"
}
```

**Response esperado (200 OK):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-01-15",
  "startTime": "10:00:00",
  "endTime": "11:00:00",
  "durationMinutes": 60,
  "category": 1,
  "description": "Desenvolvimento - atualizado",
  "createdAt": "2024-01-15T10:30:00Z"
}
```

**Validações:**
- ✅ DurationMinutes é recalculado
- ✅ Se ID não pertence ao usuário → 404 Not Found
- ✅ Se dados inválidos → 400 Bad Request

---

## Passo 9: Deletar uma atividade

**Endpoint:** `DELETE /api/activities/{id}`

Substitua `{id}` com um ID retornado anteriormente

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Response esperado (204 No Content)**

**Validações:**
- ✅ Atividade é removida do banco
- ✅ Se ID não pertence ao usuário → 404 Not Found
- ✅ Se tentar listar a atividade deletada → 404 Not Found

---

## ⚠️ Testes de erro

### Teste 1: Criar atividade sem token
**Endpoint:** `POST /api/activities`
**Sem header Authorization**
**Esperado:** 401 Unauthorized

### Teste 2: StartTime >= EndTime
**Request:**
```json
{
  "date": "2024-01-15",
  "startTime": "10:00:00",
  "endTime": "09:00:00",
  "category": 1,
  "description": "Teste"
}
```
**Esperado:** 400 Bad Request - "StartTime must be before EndTime"

### Teste 3: Categoria inválida
**Request:**
```json
{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:00:00",
  "category": 99,
  "description": "Teste"
}
```
**Esperado:** 400 Bad Request - "Invalid category"

### Teste 4: Descrição muito longa
**Request:**
```json
{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:00:00",
  "category": 1,
  "description": "Lorem ipsum dolor sit amet... (mais de 500 caracteres)"
}
```
**Esperado:** 400 Bad Request - "Description cannot exceed 500 characters"

### Teste 5: Atualizar atividade de outro usuário
1. Crie uma atividade com Usuário A
2. Faça login com Usuário B
3. Tente fazer PUT naquela atividade
**Esperado:** 404 Not Found

### Teste 6: Deletar atividade de outro usuário
1. Crie uma atividade com Usuário A
2. Faça login com Usuário B
3. Tente fazer DELETE naquela atividade
**Esperado:** 404 Not Found

---

## ✅ Checklist de conclusão

- [ ] POST /api/activities cria atividade com UserId correto
- [ ] POST /api/activities calcula DurationMinutes corretamente
- [ ] GET /api/activities retorna apenas atividades do usuário
- [ ] GET /api/activities filtra por startDate corretamente
- [ ] GET /api/activities filtra por endDate corretamente
- [ ] GET /api/activities filtra por category corretamente
- [ ] GET /api/activities/id retorna atividade específica
- [ ] GET /api/activities/id retorna 404 se ID não pertence ao usuário
- [ ] PUT /api/activities/id atualiza atividade
- [ ] PUT /api/activities/id recalcula DurationMinutes
- [ ] PUT /api/activities/id retorna 404 se ID não pertence ao usuário
- [ ] DELETE /api/activities/id remove atividade
- [ ] DELETE /api/activities/id retorna 404 se ID não pertence ao usuário
- [ ] Todos endpoints retornam 401 sem token válido
- [ ] Validações de hora funcionam (StartTime < EndTime)
- [ ] Validações de categoria funcionam
- [ ] Validações de descrição funcionam (max 500 chars)
