# 🧪 Reports Testing – Phase 5

## Prerequisites
- API running at `https://localhost:5001`
- Registered user with JWT token
- At least 3–4 activities created on different dates
- Postman, Thunder Client, or similar

---

## Setup: Create test data

Before testing the reports, create several activities. Example:

### Activity 1 – January 15
**POST /api/activities**
```json
{
  "date": "2024-01-15",
  "startTime": "09:00:00",
  "endTime": "11:00:00",
  "category": 1,
  "description": "Development - Morning"
}
```
**Expected:** DurationMinutes = 120 minutes

### Activity 2 – January 15
**POST /api/activities**
```json
{
  "date": "2024-01-15",
  "startTime": "14:00:00",
  "endTime": "15:30:00",
  "category": 1,
  "description": "Development - Afternoon"
}
```
**Expected:** DurationMinutes = 90 minutes

### Activity 3 – January 15
**POST /api/activities**
```json
{
  "date": "2024-01-15",
  "startTime": "16:00:00",
  "endTime": "17:00:00",
  "category": 2,
  "description": "Meeting"
}
```
**Expected:** DurationMinutes = 60 minutes

### Activity 4 – January 16
**POST /api/activities**
```json
{
  "date": "2024-01-16",
  "startTime": "09:00:00",
  "endTime": "12:30:00",
  "category": 1,
  "description": "Development"
}
```
**Expected:** DurationMinutes = 210 minutes

### Activity 5 – January 16
**POST /api/activities**
```json
{
  "date": "2024-01-16",
  "startTime": "13:00:00",
  "endTime": "14:00:00",
  "category": 3,
  "description": "Study"
}
```
**Expected:** DurationMinutes = 60 minutes

---

## Step 1: Time by Category report (no category filter)

**Endpoint:** `GET /api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-16`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected response (200 OK):**

json
 

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




**Calculations:**
- Development: 120 + 90 + 210 = 420 minutes = 7.0 hours (3 activities)
- Meeting: 60 minutes = 1.0 hour (1 activity)
- Study: 60 minutes = 1.0 hour (1 activity)

**Validations:**
- ✅ Correct sum of minutes per category
- ✅ Correct conversion to hours (minutes / 60)
- ✅ Correct activity counts
- ✅ Only categories with activities are returned
- ✅ Categories sorted alphabetically

---

## Step 2: Time by Category report (with category filter)

**Endpoint:** `GET /api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-16&category=1`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected response (200 OK):**

json
 

[
  {
    "category": "Development",
    "totalMinutes": 420,
    "totalHours": 7.0,
    "activityCount": 3
  }
]




**Validations:**
- ✅ Correctly filters only Development (category 1)
- ✅ Totals are correct
- ✅ Other categories do not appear

---

## Step 3: Daily Summary report

**Endpoint:** `GET /api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-16`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected response (200 OK):**

json
 

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




**Calculations:**
- January 15: 120 + 90 + 60 = 270 minutes = 4.5 hours (3 activities)
- January 16: 210 + 60 = 270 minutes = 4.5 hours (2 activities)

**Validations:**
- ✅ Correct sum of minutes per day
- ✅ Correct conversion to hours
- ✅ Correct activity counts
- ✅ Dates in ascending order

---

## Step 4: Report with smaller range

**Endpoint:** `GET /api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-15`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected response (200 OK):**

json
 

[
  {
    "date": "2024-01-15",
    "totalMinutes": 270,
    "totalHours": 4.5,
    "activityCount": 3
  }
]




**Validations:**
- ✅ Returns only activities from January 15

---

## Step 5: Report with dates with no activities

**Endpoint:** `GET /api/reports/daily-summary?startDate=2024-01-20&endDate=2024-01-25`

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected response (200 OK):**

json
 

[]




**Validations:**
- ✅ Returns empty list if there are no activities in the range

---

## Step 6: Specific category report with no data

**Endpoint:** `GET /api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-16&category=5`

(Category 5 = Break, which we didn’t create in test data)

**Headers:**
Authorization: Bearer Content-Type: application/json

**Expected response (200 OK):**

json
 

[]




**Validations:**
- ✅ Returns empty list if the category has no activities

---

## ⚠️ Error tests

### Test 1: No token
**Endpoint:** `GET /api/reports/time-by-category?startDate=2024-01-15&endDate=2024-01-16`  
**No Authorization header**  
**Expected:** 401 Unauthorized

### Test 2: Missing startDate
**Endpoint:** `GET /api/reports/time-by-category?endDate=2024-01-16`  
**Expected:** 400 Bad Request – "startDate and endDate are required"

### Test 3: Missing endDate
**Endpoint:** `GET /api/reports/time-by-category?startDate=2024-01-15`  
**Expected:** 400 Bad Request – "startDate and endDate are required"

### Test 4: StartDate > EndDate
**Endpoint:** `GET /api/reports/daily-summary?startDate=2024-01-20&endDate=2024-01-15`  
**Expected:** 400 Bad Request – "Start date must be before or equal to end date"

### Test 5: Invalid date format
**Endpoint:** `GET /api/reports/time-by-category?startDate=2024-13-01&endDate=2024-01-16`  
**Expected:** 400 Bad Request (format validation)

---

## 📋 Tests with multiple users

1. **Create User A with activities on Jan 15–16**  
2. **Create User B with activities on Jan 20–21**  
3. **Log in as User A**  
4. **GET /api/reports/daily-summary?startDate=2024-01-15&endDate=2024-01-16**

**Expected:**
- Only activities from User A appear
- No activities from User B
- Report data is correct for User A

---

## ✅ Completion checklist

- [ ] GET /api/reports/time-by-category returns correct data
- [ ] TotalMinutes is correct
- [ ] TotalHours is correct (minutes/60)
- [ ] ActivityCount is correct
- [ ] Categories in alphabetical order
- [ ] Category filter works
- [ ] GET /api/reports/daily-summary returns correct data
- [ ] Dates in ascending order
- [ ] Only authenticated user’s activities appear
- [ ] Empty list when no data in range
- [ ] StartDate > EndDate returns error
- [ ] Missing dates return error
- [ ] No token returns 401
- [ ] Hour conversion is precise (2 decimal places)

---

## 📊 Example decoded JSON payloads for different scenarios

### Scenario 1: One busy week

json
 

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




### Scenario 2: Daily summary for the same week

json
 

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

json
 

[]
