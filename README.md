Employment System
Overview

The Employment System is a web application designed to facilitate job management between employers and applicants. It provides a platform for employers to create vacancies, manage applications, and archive expired vacancies. Applicants can search for vacancies, apply for positions, and manage their applications.
Features
For Employers:

    Vacancy Management:
        Create new vacancies with detailed descriptions.
        Edit existing vacancies.
        Delete vacancies when no longer needed.
        View a list of applicants for each vacancy.
        Archive expired vacancies automatically.

For Applicants:

    Job Search and Application:
        Search for vacancies based on title.
        Apply for vacancies with restrictions on daily applications.

Technologies Used

    Backend:
        ASP.NET Core 8
        C# Programming Language
        Entity Framework Core
        LINQ
        SQL Server

Prerequisites

Before you start, make sure you have the following installed and set up:

    .NET SDK 8.0.7 or later
    Visual Studio (or any IDE of your choice)
    SQL Server (Express or another edition)

Setup Instructions
1. Clone the Repository

bash

git clone https://github.com/amrayman1/EmploymentSystem.git
cd employment-system

2. Database Setup

    update the connection string in appsettings.json.

json

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=EmploymentSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}

    Apply Entity Framework Core Migrations:

bash

dotnet ef database update

3. Configure Application Settings

Update appsettings.json with your specific settings:

json

{
  "Jwt": {
    "Key": "your_secret_key",
    "Issuer": "your_issuer",
    "Audience": "your_audience"
  }
}

4. Run the Application

bash

dotnet run

The application will be hosted at http://localhost:5000 (or another specified port).
API Endpoints
Employer APIs

    Create Vacancy: POST /api/employers/CreateVacancy
    Edit Vacancy: PUT /api/employers/EditVacancy/{id}
    Delete Vacancy: DELETE /api/employers/DeleteVacancy/{id}
    Get All Applicants: GET /api/employers/GetAllApplicants/{id}
    Get All Vacancies: GET /api/employers/GetAllVacancies
    Get Active Vacancies: GET /api/employers/GetActiveVacancies

Applicant APIs

    Apply for Vacancy: POST /api/applicants/ApplyForVacancy
    Search Vacancies: GET /api/applicants/SearchVacancies?title={title}

Additional Notes

    Ensure to secure your application endpoints and handle authentication and authorization properly.
    Customize the README further based on additional features, deployment instructions, or specific configurations relevant to your project.
