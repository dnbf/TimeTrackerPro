# 🧪 ActivityEntry CRUD Testing – Phase 4

## Prerequisites
- API running at `https://localhost:5001`
- Registered user and JWT token obtained
- Postman, Thunder Client, or similar

---

## Step 1: Get authentication token

**Endpoint:** `POST /api/auth/login`

**Request:**
```json
{
  "email": "joao@example.com",
  "password": "Password123"
}
```

**Copy the returned token to use in the next steps.**

---

## Step 2: Create an activity

**Endpoint:** `POST /api/activities`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Request:**

json


{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:30:00",
  "category": 1,
  "description": "Authentication feature development"
}




**Available categories:**
- `1` = Development
- `2` = Meeting
- `3` = Study
- `4` = Operations
- `5` = Break
- `6` = Other

**Expected response (201 Created):**

json


{
  "id": "550e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:30:00",
  "durationMinutes": 90,
  "category": 1,
  "description": "Authentication feature development",
  "createdAt": "2024-01-15T10:30:00Z"
}




**Validations:**
- ✅ DurationMinutes is calculated automatically
- ✅ UserId is filled based on the token
- ✅ If StartTime >= EndTime → 400 Bad Request
- ✅ If category is invalid → 400 Bad Request
- ✅ If description > 500 characters → 400 Bad Request

---

## Step 3: List activities (no filters)

**Endpoint:** `GET /api/activities`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected response (200 OK):**

json


[
  {
    "id": "550e8400-e29b-41d4-a716-446655440001",
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "date": "2024-01-15",
    "startTime": "09:00:00",
    "endTime": "10:30:00",
    "durationMinutes": 90,
    "category": 1,
    "description": "Authentication feature development",
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
    "description": "Team meeting",
    "createdAt": "2024-01-15T15:00:00Z"
  }
]




**Validations:**
- ✅ Returns only activities of the authenticated user
- ✅ Sorted by date descending, then time descending

---

## Step 4: List activities with date filter

**Endpoint:**  
`GET /api/activities?startDate=2024-01-15&endDate=2024-01-15`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected response (200 OK):**

json


[
  // Only activities in the 2024-01-15 range
]




**Test variations:**

http
 

# Only startDate
GET /api/activities?startDate=2024-01-15

# Only endDate
GET /api/activities?endDate=2024-01-15

# Both
GET /api/activities?startDate=2024-01-10&endDate=2024-01-20




---

## Step 5: List activities with category filter

**Endpoint:** `GET /api/activities?category=1`

**Headers:**
json
 

[
  // Only activities in the 2024-01-15 range
]
**Expected response (200 OK):**

json
 

[
  // Only activities with category Development (1)
]




---

## Step 6: Combine filters

**Endpoint:**  
`GET /api/activities?startDate=2024-01-15&endDate=2024-01-20&category=2`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected response (200 OK):**

json
 

[
  // Only Meeting (2) activities between January 15 and 20
]




---

## Step 7: Get a specific activity

**Endpoint:** `GET /api/activities/{id}`

Replace `{id}` with an ID returned in previous steps.

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected response (200 OK):**

json
 

{
  "id": "550e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:30:00",
  "durationMinutes": 90,
  "category": 1,
  "description": "Authentication feature development",
  "createdAt": "2024-01-15T10:30:00Z"
}




**Validations:**
- ✅ If ID does not belong to authenticated user → 404 Not Found
- ✅ If ID does not exist → 404 Not Found

---

## Step 8: Update an activity

**Endpoint:** `PUT /api/activities/{id}`

Replace `{id}` with a previously returned ID.

**Headers:**
Authorization: Bearer Content-Type: application/json

**Request:**

json
 

{
  "date": "2024-01-15",
  "startTime": "10:00:00",
  "endTime": "11:00:00",
  "category": 1,
  "description": "Development - updated"
}




**Expected response (200 OK):**

json
 

{
  "id": "550e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-01-15",
  "startTime": "10:00:00",
  "endTime": "11:00:00",
  "durationMinutes": 60,
  "category": 1,
  "description": "Development - updated",
  "createdAt": "2024-01-15T10:30:00Z"
}




**Validations:**
- ✅ DurationMinutes is recalculated
- ✅ If ID does not belong to user → 404 Not Found
- ✅ If data is invalid → 400 Bad Request

---

## Step 9: Delete an activity

**Endpoint:** `DELETE /api/activities/{id}`

Replace `{id}` with a previously returned ID.

**Headers:**
json
 

{
  "date": "2024-01-15",
  "startTime": "10:00:00",
  "endTime": "11:00:00",
  "category": 1,
  "description": "Development - updated"
}
**Expected response (204 No Content)**

**Validations:**
- ✅ Activity is removed from the database
- ✅ If ID does not belong to user → 404 Not Found
- ✅ If you try to fetch the deleted activity → 404 Not Found

---

## ⚠️ Error tests

### Test 1: Create activity without token
**Endpoint:** `POST /api/activities`  
**No Authorization header**  
**Expected:** 401 Unauthorized

### Test 2: StartTime >= EndTime
**Request:**

json
 

{
  "date": "2024-01-15",
  "startTime": "10:00:00",
  "endTime": "09:00:00",
  "category": 1,
  "description": "Test"
}



**Expected:** 400 Bad Request – "StartTime must be before EndTime"

### Test 3: Invalid category
**Request:**

json
 

{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:00:00",
  "category": 99,
  "description": "Test"
}



**Expected:** 400 Bad Request – "Invalid category"

### Test 4: Description too long
**Request:**

json
 

{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:00:00",
  "category": 1,
  "description": "Lorem ipsum dolor sit amet... (more than 500 characters)"
}



**Expected:** 400 Bad Request – "Description cannot exceed 500 characters"

### Test 5: Update activity from another user
1. Create an activity with User A  
2. Log in as User B  
3. Try to PUT that activity  
**Expected:** 404 Not Found

### Test 6: Delete activity from another user
1. Create an activity with User A  
2. Log in as User B  
3. Try to DELETE that activity  
**Expected:** 404 Not Found

---

## ✅ Completion checklist

- [ ] POST /api/activities creates activity with correct UserId
- [ ] POST /api/activities calculates DurationMinutes correctly
- [ ] GET /api/activities returns only user activities
- [ ] GET /api/activities filters correctly by startDate
- [ ] GET /api/activities filters correctly by endDate
- [ ] GET /api/activities filters correctly by category
- [ ] GET /api/activities/{id} returns specific activity
- [ ] GET /api/activities/{id} returns 404 if ID does not belong to user
- [ ] PUT /api/activities/{id} updates activity
- [ ] PUT /api/activities/{id} recalculates DurationMinutes
- [ ] PUT /api/activities/{id} returns 404 if ID does not belong to user
- [ ] DELETE /api/activities/{id} removes activity
- [ ] DELETE /api/activities/{id} returns 404 if ID does not belong to user
- [ ] All endpoints return 401 without a valid token
- [ ] Time validations work (StartTime < EndTime)
- [ ] Category validations work
- [ ] Description validations work (max 500 chars)