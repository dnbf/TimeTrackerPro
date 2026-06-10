# TimeTracker Pro
TimeTracker Pro is a personal productivity API designed to help developers track how they spend their time across different activities such as coding, meetings, studying, and operational tasks.

The goal of this project is to practice and showcase modern backend development skills using ASP.NET Core, Entity Framework Core, JWT authentication, and cloud-ready architecture.

Why I’m building this
I wanted to create a project that feels useful, realistic, and close to the kind of problems developers and tech consultants deal with every day.

## TimeTracker Pro is meant to be both:

a practical time tracking tool,
and a portfolio project that demonstrates how I design, build, and evolve backend systems.
Planned Features
Core Features
User registration and authentication with JWT
Activity entry management:
Create, update, delete, and list activity entries
Track date, time, category, and description
### Filtering by:
Date range
Category
Daily and weekly summaries
Time spent per category
Example Activity Categories
Development
Meeting
Study
Operations
Break
Other
Tech Stack
### Backend:
ASP.NET Core Web API
### Database:
EF Core with SQLite
### Authentication:
JWT
### Documentation:
Swagger / OpenAPI
### Architecture:
Layered approach
### Deployment:
Cloud-ready design
### Optional Future Frontend:
Angular

## Project Structure
The solution will be split into the following projects:

### TimeTrackerPro.Api
Exposes the HTTP endpoints, authentication, and API configuration.

### TimeTrackerPro.Application
Contains use cases, services, DTOs, and application rules.

### TimeTrackerPro.Domain
Contains the core entities and business rules.

### TimeTrackerPro.Infrastructure
Contains persistence, EF Core configuration, and database access.

## Roadmap
[ ] Create solution and project structure n/
[ ] Configure ASP.NET Core Web API
[ ] Model domain entities
[ ] Configure EF Core and SQLite
[ ] Implement user registration and login
[ ] Add JWT authentication
[ ] Build activity entry CRUD
[ ] Add filtering and reporting endpoints
[ ] Write tests for core use cases
[ ] Add Docker support
[ ] Deploy to cloud
## Goals of the Project
This project will help me strengthen and demonstrate skills in:

## API design
Clean and modular architecture
Authentication and authorization
Entity Framework Core
Reporting and aggregation logic
Cloud deployment
Building portfolio-ready backend solutions
## Status
This project is currently in the planning / early development phase.

As I build it, I’ll share updates, architecture decisions, and lessons learned.

## Contact
If you want to follow the progress of this project or discuss backend development, feel free to connect with me on LinkedIn.
