# 🧪 Teste do Fluxo de Autenticação - Fase 3

## Pré-requisitos
- API rodando em `https://localhost:5001` (ou porta configurada)
- Postman, Thunder Client, ou similar

## Passo 1: Registrar um novo usuário

**Endpoint:** `POST /api/auth/register`

**Request:**
```json
{
  "name": "João Silva",
  "email": "joao@example.com",
  "password": "Password123"
}
```

**Response esperado (201 Created):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "João Silva",
  "email": "joao@example.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Validações:**
- ✅ Salva o usuário no banco com hash de senha
- ✅ Retorna token JWT válido
- ✅ Se email já existe → 400 Bad Request

---

## Passo 2: Fazer login com as credenciais

**Endpoint:** `POST /api/auth/login`

**Request:**
```json
{
  "email": "joao@example.com",
  "password": "Password123"
}
```

**Response esperado (200 OK):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "João Silva",
  "email": "joao@example.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Validações:**
- ✅ Retorna token JWT válido
- ✅ Se credenciais inválidas → 401 Unauthorized
- ✅ Se email não existe → 401 Unauthorized

---

## Passo 3: Obter dados do usuário autenticado

**Endpoint:** `GET /api/auth/me`

**Headers obrigatórios:**
```
Authorization: Bearer <token_do_passo_2>
Content-Type: application/json
```

**Response esperado (200 OK):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "João Silva",
  "email": "joao@example.com",
  "token": ""
}
```

**Validações:**
- ✅ Retorna dados do usuário autenticado
- ✅ Token não é retornado neste endpoint (campo vazio)
- ✅ Se token inválido/expirado → 401 Unauthorized
- ✅ Se sem token → 401 Unauthorized

---

## ⚠️ Testes de erro

### Teste 1: Registrar com senha fraca
**Request:**
```json
{
  "name": "Teste",
  "email": "teste@example.com",
  "password": "weak"
}
```
**Esperado:** 400 Bad Request - "Password must be at least 8 characters long"

### Teste 2: Registrar sem número na senha
**Request:**
```json
{
  "name": "Teste",
  "email": "teste@example.com",
  "password": "WeakPassword"
}
```
**Esperado:** 400 Bad Request - "Password must contain at least one number"

### Teste 3: Registrar sem letra maiúscula na senha
**Request:**
```json
{
  "name": "Teste",
  "email": "teste@example.com",
  "password": "weakpassword123"
}
```
**Esperado:** 400 Bad Request - "Password must contain at least one uppercase letter"

### Teste 4: Login com senha errada
**Request:**
```json
{
  "email": "joao@example.com",
  "password": "WrongPassword123"
}
```
**Esperado:** 401 Unauthorized - "Invalid credentials"

### Teste 5: Acessar /me sem token
**Esperado:** 401 Unauthorized

---

## 📝 Notas importantes

1. **Token JWT contém:**
   - `sub` (subject): UserId
   - `name`: Nome do usuário
   - `email`: Email do usuário
   - `exp`: Tempo de expiração (60 minutos por padrão)

2. **Senha é hash com:**
   - Algoritmo: PBKDF2 (RFC2898) com SHA256
   - Iterações: 10.000
   - Salt: 16 bytes aleatório

3. **Banco de dados:**
   - SQLite local em `timetracker.db`
   - Reexecute migrations se necessário: `dotnet ef database update`

---

## ✅ Checklist de conclusão

- [ ] POST /api/auth/register retorna 201 com token
- [ ] POST /api/auth/login retorna 200 com token
- [ ] GET /api/auth/me com token retorna dados do usuário
- [ ] GET /api/auth/me sem token retorna 401
- [ ] Validações de senha funcionam corretamente
- [ ] Email duplicado retorna 400
- [ ] Credenciais inválidas retornam 401
- [ ] Token JWT é válido e decodificável
