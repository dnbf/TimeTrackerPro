# 🚀 COMPLETE TESTING GUIDE - Phases 3, 4, and 5

## 📋 Table of Contents
1. [Initial Setup](#initial-setup)
2. [Phase 3 - Authentication](#phase-3--authentication)
3. [Phase 4 - Activity CRUD](#phase-4--activity-crud)
4. [Phase 5 - Reports](#phase-5--reports)
5. [Error Tests](#error-tests)
6. [Final Checklist](#final-checklist)

---

## Initial Setup

### Prerequisites
- Visual Studio 2026 open with the TimeTrackerPro project
- Postman, Thunder Client, or Insomnia to test endpoints
- API running at `https://localhost:5001` (or configured port)

### Steps
1. Open Visual Studio
2. Build the project: `Ctrl + Shift + B` or Build > Build Solution
3. Run the application: `F5` or Debug > Start Debugging
4. Open Swagger at: `https://localhost:5001/swagger/index.html`
5. You should see 3 controllers: **Auth**, **Activities**, **Reports**

✅ **Verification:** If Swagger loads with the 3 controllers, everything is ready!

---

# Phase 3 – Authentication

## Test 3.1: Register User

**URL:** `POST https://localhost:5001/api/auth/register`

**Headers:**
Content-Type: application/json

**Body:**

json
 

{
  "name": "João Silva",
  "email": "joao@example.com",
  "password": "Password123"
}




**Expected (201 Created):**

json
 

{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "João Silva",
  "email": "joao@example.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}




**✅ Validations:**
- [ ] Status 201 returned
- [ ] ID is a valid GUID
- [ ] Token is a non-empty string
- [ ] User can log in with these credentials

**💾 Save the token** — you will use it in the next tests!

---

## Test 3.2: Try Registering with a Duplicate Email

**URL:** `POST https://localhost:5001/api/auth/register`

**Body:**

json
 

{
  "name": "Other User",
  "email": "joao@example.com",
  "password": "Password456"
}




**Expected (400 Bad Request):**

json
 

{
  "message": "Email already registered"
}




**✅ Validations:**
- [ ] Status 400 returned
- [ ] Clear error message

---

## Test 3.3: Try Registering with a Weak Password

**URL:** `POST https://localhost:5001/api/auth/register`

**Body (password < 8 characters):**

json
 

{
  "name": "Test",
  "email": "teste@example.com",
  "password": "Pass12"
}




**Expected (400 Bad Request):**

json
 

{
  "message": "Password must be at least 8 characters long"
}




**Body (without uppercase letter):**

json
 

{
  "name": "Test",
  "email": "teste2@example.com",
  "password": "password123"
}




**Expected (400 Bad Request):**

json
 

{
  "message": "Password must contain at least one uppercase letter"
}




**Body (without a number):**

json
 

{
  "name": "Test",
  "email": "teste3@example.com",
  "password": "PasswordABC"
}




**Expected (400 Bad Request):**

json
 

{
  "message": "Password must contain at least one number"
}




**✅ Validations:**
- [ ] Rejects passwords shorter than 8 characters
- [ ] Rejects passwords without an uppercase letter
- [ ] Rejects passwords without a number

---

## Test 3.4: Log In with Correct Credentials

**URL:** `POST https://localhost:5001/api/auth/login`

**Headers:**
Content-Type: application/json

**Body:**

json
 

{
  "email": "joao@example.com",
  "password": "Password123"
}




**Expected (200 OK):**

json
 

{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "João Silva",
  "email": "joao@example.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}




**✅ Validations:**
- [ ] Status 200 returned
- [ ] Returned token is valid
- [ ] Token is different on each call (refresh)

**💾 Copy this token too — you will use it!**

---

## Test 3.5: Log In with Incorrect Credentials

**URL:** `POST https://localhost:5001/api/auth/login`

**Body (wrong password):**

json
 

{
  "email": "joao@example.com",
  "password": "WrongPassword123"
}




**Expected (401 Unauthorized):**

json
 

{
  "message": "Invalid credentials"
}




**Body (email does not exist):**

json
 

{
  "email": "doesnotexist@example.com",
  "password": "Password123"
}




**Expected (401 Unauthorized):**

json
 

{
  "message": "Invalid credentials"
}




**✅ Validations:**
- [ ] Rejects wrong password with 401
- [ ] Rejects non-existing email with 401
- [ ] Message does not reveal which field is wrong (security)

---

## Test 3.6: Get Authenticated User Data

**URL:** `GET https://localhost:5001/api/auth/me`

**Required Headers:**
Authorization: Bearer Content-Type: application/json

**Expected (200 OK):**

json
 

{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "João Silva",
  "email": "joao@example.com",
  "token": ""
}




**✅ Validations:**
- [ ] Status 200 returned
- [ ] Returns the correct user data
- [ ] Token is empty in this response (security)

---

## Test 3.7: Access /me Without Token

**URL:** `GET https://localhost:5001/api/auth/me`

**Without Authorization header**

**Expected (401 Unauthorized)**

**✅ Validations:**
- [ ] Rejects request without token
- [ ] Does not expose sensitive information

---

# Phase 4 – Activity CRUD

## Test 4.1: Create First Activity

**URL:** `POST https://localhost:5001/api/activities`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Body:**

json
 

{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "11:00:00",
  "category": 1,
  "description": "Implementation of the activity CRUD"
}




**Expected (201 Created):**

json
 

{
  "id": "660e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "11:00:00",
  "durationMinutes": 120,
  "category": 1,
  "description": "Implementation of the activity CRUD",
  "createdAt": "2024-01-15T09:00:00Z"
}




**✅ Validations:**
- [ ] Status 201 returned
- [ ] DurationMinutes calculated correctly (120 minutes = 2 hours)
- [ ] UserId filled automatically from the token
- [ ] ID is a valid GUID

**💾 Save the activity ID — you will need it!**

---

## Test 4.2: Create Second Activity (same day)

**URL:** `POST https://localhost:5001/api/activities`

**Body:**

json
 

{
  "date": "2024-01-15",
  "startTime": "14:00:00",
  "endTime": "15:30:00",
  "category": 2,
  "description": "Team meeting"
}




**Expected (201 Created):**
- DurationMinutes = 90 minutes

**✅ Validations:**
- [ ] Second activity created successfully
- [ ] Multiple activities can exist on the same day

**💾 Save this ID too!**

---

## Test 4.3: Create Third Activity (different day)

**URL:** `POST https://localhost:5001/api/activities`

**Body:**

json
 

{
  "date": "2024-01-16",
  "startTime": "09:00:00",
  "endTime": "12:30:00",
  "category": 1,
  "description": "Development - day 16"
}




**Expected (201 Created):**
- DurationMinutes = 210 minutes

**✅ Validations:**
- [ ] Third activity created successfully on a different date

---

## Test 4.4: List All Activities

**URL:** `GET https://localhost:5001/api/activities`

**Headers:**
json
 

{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "11:00:00",
  "category": 1,
  "description": "Implementation of the activity CRUD"
}
**Expected (200 OK):**

json
 

[
  {
    "id": "660e8400-e29b-41d4-a716-446655440003",
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "date": "2024-01-16",
    "startTime": "09:00:00",
    "endTime": "12:30:00",
    "durationMinutes": 210,
    "category": 1,
    "description": "Development - day 16",
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
    "description": "Team meeting",
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
    "description": "Implementation of the activity CRUD",
    "createdAt": "2024-01-15T09:00:00Z"
  }
]




**✅ Validations:**
- [ ] Returns all 3 activities
- [ ] Only activities from the authenticated user appear
- [ ] Ordered by date descending, then by time descending
- [ ] Each activity has all correct fields

---

## Test 4.5: List Activities with Date Filter

**URL:** `GET https://localhost:5001/api/activities?startDate=2024-01-15&endDate=2024-01-15`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected (200 OK):**

json
 

[
  // Only activities from 2024-01-15
]




**Test with only startDate:**
GET https://localhost:5001/api/activities?startDate=2024-01-15

**Test with only endDate:**
GET https://localhost:5001/api/activities?endDate=2024-01-16

**✅ Validations:**
- [ ] startDate filter works
- [ ] endDate filter works
- [ ] Both combined work
- [ ] Returns only activities in the interval

---

## Test 4.6: List Activities with Category Filter

**URL:** `GET https://localhost:5001/api/activities?category=1`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected (200 OK):**

json
 

[
  // Only activities with category=1 (Development)
  // Should have 2 activities in this case
]




**Test with category=2:**
GET https://localhost:5001/api/activities?category=2

**Expected:**

json
 

[
  // Only 1 activity with category=2 (Meeting)
]




**✅ Validations:**
- [ ] Category filter works
- [ ] Category=1 returns 2 activities
- [ ] Category=2 returns 1 activity

---

## Test 4.7: Get a Specific Activity

**URL:** `GET https://localhost:5001/api/activities/660e8400-e29b-41d4-a716-446655440001`

Replace the ID with a valid ID returned earlier

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected (200 OK):**

json
 

{
  "id": "660e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "11:00:00",
  "durationMinutes": 120,
  "category": 1,
  "description": "Implementation of the activity CRUD",
  "createdAt": "2024-01-15T09:00:00Z"
}




**✅ Validations:**
- [ ] Returns the correct activity
- [ ] All fields are present

---

## Test 4.8: Update Activity

**URL:** `PUT https://localhost:5001/api/activities/660e8400-e29b-41d4-a716-446655440001`

Replace the ID with a valid ID

**Headers:**
Authorization: Bearer Content-Type: application/json

**Body:**

json
 

{
  "date": "2024-01-15",
  "startTime": "10:00:00",
  "endTime": "11:30:00",
  "category": 1,
  "description": "Implementation of CRUD - UPDATED"
}




**Expected (200 OK):**

json
 

{
  "id": "660e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-01-15",
  "startTime": "10:00:00",
  "endTime": "11:30:00",
  "durationMinutes": 90,
  "category": 1,
  "description": "Implementation of CRUD - UPDATED",
  "createdAt": "2024-01-15T09:00:00Z"
}




**✅ Validations:**
- [ ] Activity updated successfully
- [ ] StartTime and EndTime updated
- [ ] DurationMinutes recalculated (now 90 instead of 120)
- [ ] Description updated
- [ ] CreatedAt does not change

---

## Test 4.9: Delete Activity

**URL:** `DELETE https://localhost:5001/api/activities/660e8400-e29b-41d4-a716-446655440002`

Replace the ID with a valid ID (preferably one you no longer need)

**Headers:**
json
 

{
  "date": "2024-01-15",
  "startTime": "10:00:00",
  "endTime": "11:30:00",
  "category": 1,
  "description": "Implementation of CRUD - UPDATED"
}
**Expected (204 No Content)**

**Validation:**
GET https://localhost:5001/api/activities/{deleted_id}

**Expected (404 Not Found):**

json
 

{
  "message": "Activity not found"
}




**✅ Validations:**
- [ ] Delete returns 204
- [ ] Activity can no longer be retrieved
- [ ] Activity list does not include the deleted one

---

# Phase 5 – Reports

## Test 5.1: Time by Category Report (no filter)

**URL:** `GET https://localhost:5001/api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-16`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected (200 OK):**

json
 

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




**Calculations:**
- Development: (90 + 210) = 300 minutes = 5.0 hours (2 activities)
  - Note: The first activity was updated from 120 to 90 minutes
- Meeting: 90 minutes = 1.5 hours (1 activity)

**✅ Validations:**
- [ ] Report returned successfully
- [ ] Minutes summed correctly
- [ ] Hours converted correctly (minutes / 60)
- [ ] Activity count is correct
- [ ] Categories sorted alphabetically

---

## Test 5.2: Time by Specific Category Report

**URL:** `GET https://localhost:5001/api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-16&category=1`

(Category 1 = Development)

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected (200 OK):**

json
 

[
  {
    "category": "Development",
    "totalMinutes": 300,
    "totalHours": 5.0,
    "activityCount": 2
  }
]




**✅ Validations:**
- [ ] Filters only the requested category
- [ ] Other categories do not appear
- [ ] Calculations are correct

---

## Test 5.3: Daily Report

**URL:** `GET https://localhost:5001/api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-16`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected (200 OK):**

json
 

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




**Calculations:**
- 2024-01-15: 90 + 90 = 180 minutes = 3.0 hours (2 activities)
- 2024-01-16: 210 minutes = 3.5 hours (1 activity)

**✅ Validations:**
- [ ] Report returned successfully
- [ ] Minutes per day summed correctly
- [ ] Hours calculated correctly
- [ ] Activity count is correct
- [ ] Dates are in ascending order

---

## Test 5.4: Report for a Single Day

**URL:** `GET https://localhost:5001/api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-15`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected (200 OK):**

json
 

[
  {
    "date": "2024-01-15",
    "totalMinutes": 180,
    "totalHours": 3.0,
    "activityCount": 2
  }
]




**✅ Validations:**
- [ ] Returns only one day of data
- [ ] Calculations are correct for that day

---

# Error Tests

## Error 1: Create Activity Without Token

**URL:** `POST https://localhost:5001/api/activities`

**Without Authorization header**

**Expected (401 Unauthorized)**

---

## Error 2: StartTime >= EndTime

**URL:** `POST https://localhost:5001/api/activities`

**Body:**

json
 

{
  "date": "2024-01-15",
  "startTime": "11:00:00",
  "endTime": "10:00:00",
  "category": 1,
  "description": "Test"
}




**Expected (400 Bad Request):**

json
 

{
  "message": "StartTime must be before EndTime"
}




---

## Error 3: Invalid Category

**URL:** `POST https://localhost:5001/api/activities`

**Body:**

json
 

{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:00:00",
  "category": 99,
  "description": "Test"
}




**Expected (400 Bad Request):**

json
 

{
  "message": "Invalid category"
}




---

## Error 4: Description Too Long

**URL:** `POST https://localhost:5001/api/activities`

**Body:**

json
 

{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "10:00:00",
  "category": 1,
  "description": "Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua Ut enim ad minim veniam quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur Excepteur sint occaecat cupidatat non proident sunt in culpa qui officia deserunt mollit anim id est laborumLorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"
}




**Expected (400 Bad Request):**

json
 

{
  "message": "Description cannot exceed 500 characters"
}




---

## Error 5: Report Without Dates

**URL:** `GET https://localhost:5001/api/reports/time-by-category`

**Expected (400 Bad Request):**

json
 

{
  "message": "startDate and endDate are required"
}




---

## Error 6: StartDate > EndDate in Report

**URL:** `GET https://localhost:5001/api/reports/daily-summary?startDate=2024-01-20&endDate=2024-01-15`

**Expected (400 Bad Request):**

json
 

{
  "message": "Start date must be before or equal to end date"
}




---

# Final Checklist

## ✅ Phase 3 - Authentication
- [ ] POST /api/auth/register with valid data returns 201
- [ ] POST /api/auth/register with duplicate email returns 400
- [ ] POST /api/auth/register validates password (8+ chars, 1 uppercase, 1 number)
- [ ] POST /api/auth/login with correct credentials returns 200
- [ ] POST /api/auth/login with wrong credentials returns 401
- [ ] GET /api/auth/me with valid token returns 200
- [ ] GET /api/auth/me without token returns 401

## ✅ Phase 4 - Activities
- [ ] POST /api/activities creates activity successfully (201)
- [ ] POST /api/activities calculates DurationMinutes correctly
- [ ] POST /api/activities fills UserId from the token automatically
- [ ] GET /api/activities returns all user activities
- [ ] GET /api/activities filters by startDate
- [ ] GET /api/activities filters by endDate
- [ ] GET /api/activities filters by category
- [ ] GET /api/activities/{id} returns a specific activity
- [ ] PUT /api/activities/{id} updates activity
- [ ] PUT /api/activities/{id} recalculates DurationMinutes
- [ ] DELETE /api/activities/{id} removes activity (204)

## ✅ Phase 5 - Reports
- [ ] GET /api/reports/time-by-category returns correct data
- [ ] GET /api/reports/time-by-category with category filter works
- [ ] Minutes are summed correctly in time-by-category
- [ ] Hours are converted correctly (minutes/60)
- [ ] Categories are in alphabetical order
- [ ] GET /api/reports/daily-summary returns correct data
- [ ] Dates are in ascending order in daily-summary
- [ ] Minutes per day are summed correctly

## ✅ Security and Isolation
- [ ] Endpoints without [Authorize] return 401
- [ ] Users only see their own activities
- [ ] Users cannot update/delete other users' activities
- [ ] Reports show only data for the authenticated user

## ✅ Validations
- [ ] StartTime < EndTime is required
- [ ] Valid category is required
- [ ] Description max 500 characters
- [ ] Dates are required in reports
- [ ] StartDate <= EndDate is required in reports

---

## 🎯 Summary

If all tests pass:
- ✅ JWT authentication is working
- ✅ Activity CRUD with filters is working
- ✅ Reports are generating correct data
- ✅ Security is implemented (tokens, user isolation)
- ✅ Data validations are working
- ✅ Error handling is appropriate

After completing all tests, you’ll be ready for **Phase 6 - Minimum Quality (Unit Tests)**