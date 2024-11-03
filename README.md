# Documentation for API and Website Frontend

## Overview
    This project consists of a backend API and a frontend web application. The backend is implemented using .NET 8 and Entity Framework Core, while the frontend is built with React and Material-UI. Both sections of the project are designed to work together, with well-defined API endpoints enabling seamless interaction between the two.

---

## Implementation Choices

### Frontend (React with Material-UI)
    - **Framework:** React is a popular JavaScript library for building dynamic user interfaces. Paired with Material-UI, it provides a rich set of pre-styled components that follow Material Design guidelines, simplifying the creation of a visually consistent and responsive UI.
    - **Form Handling & Validation:** Formik and Yup are utilized for managing forms and handling validation. Formik streamlines form state management, while Yup offers a schema-based approach to validation.

### Backend (.NET 8 with Entity Framework Core)
    - **Framework:** .NET 8, the latest version of the .NET framework, provides enhanced performance and new features, making it a solid choice for backend development. 
      - Entity Framework Core is a robust ORM (Object-Relational Mapper) that simplifies database operations and supports multiple database providers.
    - **API Endpoints:** The backend API offers CRUD endpoints for the People entity, allowing structured data management. This separation promotes modularity and maintainability, making it easier to scale and update.
    - **API Documentation:** Swagger (via Swashbuckle) is used to generate interactive API documentation, providing developers with a clear overview of available endpoints and their usage.

---

## Database Design (SQL Server)
    If using a SQL Server database, here’s an example SQL table design for the People entity:

```sql
CREATE TABLE People (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    DateCreated DATETIME DEFAULT GETDATE()
);
```

    I am using an in-memory database for this project, but you can easily switch to a SQL Server database by updating the connection string in the `appsettings.json` file.

---
## Project Documentation: API and Website Frontend

## Overview
    This project includes both a backend API and a frontend web application, designed to work together via well-defined API endpoints. 
    The backend, built with .NET 8 and Entity Framework Core, provides CRUD operations for managing data, while the frontend, built with React and Material-UI, 
    offers a dynamic and responsive user interface.

---

## Implementation Choices

### Frontend (React with Material-UI)
    - **Framework:** React is used for building dynamic UIs. Material-UI enhances the design, providing a library of pre-styled components following Material Design guidelines.
    - **Form Handling & Validation:** Formik and Yup streamline form state management and schema-based validation.

### Backend (.NET 8 with Entity Framework Core)
    - **Framework:** .NET 8 offers enhanced performance and features. Entity Framework Core simplifies database interactions and supports multiple providers.
    - **API Endpoints:** CRUD operations for the People entity enable a clear separation between frontend and backend.
    - **API Documentation:** Swagger (via Swashbuckle) generates interactive documentation for easy API access and understanding.

---

## Deployment Instructions

### 1. Backend Deployment
    1. Ensure you have the .NET 8 SDK installed.
    2. Build the project:
        - dotnet build
    3. Publish the project:
        - dotnet publish -c Release -o ./publish
    4. Deploy the published files to your hosting environment (e.g., Azure, AWS, or on-premises server).

### 2. Frontend Deployment
    1. Ensure Node.js and npm are installed.
    2. In the React project directory, install dependencies:
       - npm install
    3. Build the project:
       - npm run build
    4. Deploy the build output to your hosting environment.

---

## Testing Instructions
    1. Backend Testing
    	- Run the backend project
    2. Test API endpoints using tools like Postman or the provided .http file.
    3. Access the Swagger UI at http://localhost:7119/swagger for API documentation and endpoint testing.

### Frontend Testing
    1. Start the frontend application:
       - npm start
    2. Open a browser and navigate to http://localhost:3000 to interact with the application.
    3. Verify that the frontend communicates correctly with the backend API.

---

## Requirments
### Prerequisites / Libraries / Node Modules
#### Backend
    - .NET 8 SDK
    - Entity Framework Core
    - Swashbuckle (for Swagger API documentation)
#### Frontend
    - Node.js and npm
    - React
    - Material-UI
    - Formik
    - Yup
    - Axios

---

## Development Environment
    IDE Used: Visual Studio

    Reason: Visual Studio provides a robust environment for .NET development, with advanced debugging, an integrated terminal, and extensive support for extensions. 
        It is ideal for developing, testing, and deploying .NET applications.# Getting Started with Create React App
