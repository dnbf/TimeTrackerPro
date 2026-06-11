# 🧪 Teste de Relatórios - Fase 5

## Pré-requisitos
- API rodando em `https://localhost:5001`
- Usuário registrado com token JWT
- Pelo menos 3-4 atividades criadas em datas diferentes
- Postman, Thunder Client, ou similar

---

## Setup: Criar dados de teste

Antes de testar os relatórios, crie várias atividades. Exemplo:

### Atividade 1 - 15 de janeiro
**POST /api/activities**
```json
{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "11:00:00",
  "category": 1,
  "description": "Desenvolvimento - Manha"
}
```
**Esperado:** DurationMinutes = 120 minutos

### Atividade 2 - 15 de janeiro
**POST /api/activities**
```json
{
  "date": "2024-01-15",
  "startTime": "14:00:00",
  "endTime": "15:30:00",
  "category": 1,
  "description": "Desenvolvimento - Tarde"
}
```
**Esperado:** DurationMinutes = 90 minutos

### Atividade 3 - 15 de janeiro
**POST /api/activities**
```json
{
  "date": "2024-01-15",
  "startTime": "16:00:00",
  "endTime": "17:00:00",
  "category": 2,
  "description": "Reuniao"
}
```
**Esperado:** DurationMinutes = 60 minutos

### Atividade 4 - 16 de janeiro
**POST /api/activities**
```json
{
  "date": "2024-01-16",
  "startTime": "09:00:00",
  "endTime": "12:30:00",
  "category": 1,
  "description": "Desenvolvimento"
}
```
**Esperado:** DurationMinutes = 210 minutos

### Atividade 5 - 16 de janeiro
**POST /api/activities**
```json
{
  "date": "2024-01-16",
  "startTime": "13:00:00",
  "endTime": "14:00:00",
  "category": 3,
  "description": "Estudo"
}
```
**Esperado:** DurationMinutes = 60 minutos

---

## Passo 1: Relatório de Tempo por Categoria (sem filtro)

**Endpoint:** `GET /api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-16`

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Response esperado (200 OK):**
```json
[
  {
    "category": "Development",
    "totalMinutes": 420,
    "totalHours": 7.0,
    "activityCount": 3
  },
  {
    "category": "Meeting",
    "totalMinutes": 60,
    "totalHours": 1.0,
    "activityCount": 1
  },
  {
    "category": "Study",
    "totalMinutes": 60,
    "totalHours": 1.0,
    "activityCount": 1
  }
]
```

**Cálculos:**
- Development: 120 + 90 + 210 = 420 minutos = 7.0 horas (3 atividades)
- Meeting: 60 minutos = 1.0 hora (1 atividade)
- Study: 60 minutos = 1.0 hora (1 atividade)

**Validações:**
- ✅ Soma correta de minutos por categoria
- ✅ Conversão correta para horas (minutos / 60)
- ✅ Contagem correta de atividades
- ✅ Retorna apenas categorias com atividades
- ✅ Ordenação alfabética por categoria

---

## Passo 2: Relatório de Tempo por Categoria (com filtro de categoria)

**Endpoint:** `GET /api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-16&category=1`

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Response esperado (200 OK):**
```json
[
  {
    "category": "Development",
    "totalMinutes": 420,
    "totalHours": 7.0,
    "activityCount": 3
  }
]
```

**Validações:**
- ✅ Filtra corretamente apenas Development (categoria 1)
- ✅ Somatório está correto
- ✅ Outras categorias não aparecem

---

## Passo 3: Relatório Diário (Daily Summary)

**Endpoint:** `GET /api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-16`

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Response esperado (200 OK):**
```json
[
  {
    "date": "2024-01-15",
    "totalMinutes": 270,
    "totalHours": 4.5,
    "activityCount": 3
  },
  {
    "date": "2024-01-16",
    "totalMinutes": 270,
    "totalHours": 4.5,
    "activityCount": 2
  }
]
```

**Cálculos:**
- 15 de janeiro: 120 + 90 + 60 = 270 minutos = 4.5 horas (3 atividades)
- 16 de janeiro: 210 + 60 = 270 minutos = 4.5 horas (2 atividades)

**Validações:**
- ✅ Soma correta de minutos por dia
- ✅ Conversão correta para horas
- ✅ Contagem correta de atividades
- ✅ Ordenação crescente por data

---

## Passo 4: Relatório com intervalo menor

**Endpoint:** `GET /api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-15`

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Response esperado (200 OK):**
```json
[
  {
    "date": "2024-01-15",
    "totalMinutes": 270,
    "totalHours": 4.5,
    "activityCount": 3
  }
]
```

**Validações:**
- ✅ Retorna apenas atividades de 15 de janeiro

---

## Passo 5: Relatório com data sem atividades

**Endpoint:** `GET /api/reports/daily-summary?startDate=2024-01-20&endDate=2024-01-25`

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Response esperado (200 OK):**
```json
[]
```

**Validações:**
- ✅ Retorna lista vazia se nenhuma atividade no intervalo

---

## Passo 6: Relatório de categoria específica sem dados

**Endpoint:** `GET /api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-16&category=5`

(Categoria 5 = Break, que não foi criada nos dados de teste)

**Headers:**
```
Authorization: Bearer <seu_token_aqui>
Content-Type: application/json
```

**Response esperado (200 OK):**
```json
[]
```

**Validações:**
- ✅ Retorna lista vazia se categoria não tem atividades

---

## ⚠️ Testes de erro

### Teste 1: Sem token
**Endpoint:** `GET /api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-16`
**Sem header Authorization**
**Esperado:** 401 Unauthorized

### Teste 2: StartDate faltando
**Endpoint:** `GET /api/reports/time-by-category?endDate=2024-01-16`
**Esperado:** 400 Bad Request - "startDate and endDate are required"

### Teste 3: EndDate faltando
**Endpoint:** `GET /api/reports/time-by-category?startDate=2024-01-15`
**Esperado:** 400 Bad Request - "startDate and endDate are required"

### Teste 4: StartDate > EndDate
**Endpoint:** `GET /api/reports/daily-summary?startDate=2024-01-20&endDate=2024-01-15`
**Esperado:** 400 Bad Request - "Start date must be before or equal to end date"

### Teste 5: Formato de data inválido
**Endpoint:** `GET /api/reports/time-by-category?startDate=2024-13-01&endDate=2024-01-16`
**Esperado:** 400 Bad Request (validação de formato)

---

## 📋 Testes com múltiplos usuários

1. **Crie Usuário A com atividades de 15-16 jan**
2. **Crie Usuário B com atividades de 20-21 jan**
3. **Faça login com Usuário A**
4. **GET /api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-16**

**Esperado:**
- Retorna apenas atividades do Usuário A
- Não inclui atividades do Usuário B
- Relatório contém dados corretos do Usuário A

---

## ✅ Checklist de conclusão

- [ ] GET /api/reports/time-by-category retorna dados corretos
- [ ] TotalMinutes está correto
- [ ] TotalHours está correto (minutos/60)
- [ ] ActivityCount está correto
- [ ] Categorias estão em ordem alfabética
- [ ] Filtro de categoria funciona
- [ ] GET /api/reports/daily-summary retorna dados corretos
- [ ] Datas estão em ordem crescente
- [ ] Apenas atividades do usuário autenticado aparecem
- [ ] Retorna lista vazia se sem dados no intervalo
- [ ] StartDate > EndDate retorna erro
- [ ] Datas faltando retornam erro
- [ ] Sem token retorna 401
- [ ] Conversão de horas é precisa (2 casas decimais)

---

## 📊 Exemplo de payload JSON decodificado para diferentes cenários

### Cenário 1: Uma semana com muitas atividades
```json
GET /api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-21

Response:
[
  {
    "category": "Development",
    "totalMinutes": 1980,
    "totalHours": 33.0,
    "activityCount": 12
  },
  {
    "category": "Meeting",
    "totalMinutes": 240,
    "totalHours": 4.0,
    "activityCount": 3
  },
  {
    "category": "Study",
    "totalMinutes": 180,
    "totalHours": 3.0,
    "activityCount": 2
  },
  {
    "category": "Break",
    "totalMinutes": 150,
    "totalHours": 2.5,
    "activityCount": 3
  }
]
```

### Cenário 2: Daily summary da mesma semana
```json
GET /api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-21

Response:
[
  {
    "date": "2024-01-15",
    "totalMinutes": 450,
    "totalHours": 7.5,
    "activityCount": 5
  },
  {
    "date": "2024-01-16",
    "totalMinutes": 420,
    "totalHours": 7.0,
    "activityCount": 4
  },
  {
    "date": "2024-01-17",
    "totalMinutes": 480,
    "totalHours": 8.0,
    "activityCount": 5
  },
  ...
]
```
