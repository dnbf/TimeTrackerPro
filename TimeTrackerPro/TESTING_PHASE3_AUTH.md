 

# 🧪 Authentication Flow Testing – Phase 3

## Prerequisites
- API running at `https://localhost:5001` (or configured port)
- Postman, Thunder Client, or similar

## Step 1: Register a new user

**Endpoint:** `POST /api/auth/register`

**Request:**
```json
{
  "name": "João Silva",
  "email": "joao@example.com",
  "password": "Password123"
}
```

**Expected response (201 Created):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "João Silva",
  "email": "joao@example.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Validations:**
- ✅ Saves the user to the database with a password hash
- ✅ Returns a valid JWT token
- ✅ If email already exists → 400 Bad Request

---

## Step 2: Log in with credentials

**Endpoint:** `POST /api/auth/login`

**Request:**
```json
{
  "email": "joao@example.com",
  "password": "Password123"
}
```

**Expected response (200 OK):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "João Silva",
  "email": "joao@example.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Validations:**
- ✅ Returns a valid JWT token
- ✅ If credentials are invalid → 401 Unauthorized
- ✅ If email does not exist → 401 Unauthorized

---

## Step 3: Get authenticated user data

**Endpoint:** `GET /api/auth/me`

**Required headers:**
Authorization: Bearer Content-Type: application/json

**Expected response (200 OK):**

json
 

{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "João Silva",
  "email": "joao@example.com",
  "token": ""
}




**Validations:**
- ✅ Returns data of the authenticated user
- ✅ Token is not returned in this endpoint (empty field)
- ✅ If token is invalid/expired → 401 Unauthorized
- ✅ If no token → 401 Unauthorized

---

## ⚠️ Error tests

### Test 1: Register with weak password
**Request:**

json
 

{
  "name": "Test",
  "email": "teste@example.com",
  "password": "weak"
}



**Expected:** 400 Bad Request – "Password must be at least 8 characters long"

### Test 2: Register without a number in the password
**Request:**

json
 

{
  "name": "Test",
  "email": "teste@example.com",
  "password": "WeakPassword"
}



**Expected:** 400 Bad Request – "Password must contain at least one number"

### Test 3: Register without an uppercase letter in the password
**Request:**

json
 

{
  "name": "Test",
  "email": "teste@example.com",
  "password": "weakpassword123"
}



**Expected:** 400 Bad Request – "Password must contain at least one uppercase letter"

### Test 4: Login with wrong password
**Request:**

json
 

{
  "email": "joao@example.com",
  "password": "WrongPassword123"
}



**Expected:** 401 Unauthorized – "Invalid credentials"

### Test 5: Access /me without token
**Expected:** 401 Unauthorized

---

## 📝 Important notes

1. **JWT token contains:**
   - `sub` (subject): UserId
   - `name`: User name
   - `email`: User email
   - `exp`: Expiration time (60 minutes by default)

2. **Password is hashed with:**
   - Algorithm: PBKDF2 (RFC2898) with SHA256
   - Iterations: 10,000
   - Salt: 16 random bytes

3. **Database:**
   - Local SQLite at `timetracker.db`
   - Re-run migrations if needed: `dotnet ef database update`

---

## ✅ Completion checklist

- [ ] POST /api/auth/register returns 201 with token
- [ ] POST /api/auth/login returns 200 with token
- [ ] GET /api/auth/me with token returns user data
- [ ] GET /api/auth/me without token returns 401
- [ ] Password validations work correctly
- [ ] Duplicate email returns 400
- [ ] Invalid credentials return 401
- [ ] JWT token is valid and decodable