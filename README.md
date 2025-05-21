# ExpenseApplication

## About the Project  
This is a web app for tracking your money. You can add your income and expenses. It helps you see where your money goes. The app uses React.js for the front, C# for the backend, and MSSQL for the database.

## Features  
- Register and login  
- Add, update, delete income and expenses  
- See reports by month and year  
- Manage your money easily

![image](https://github.com/user-attachments/assets/ab1d828f-b019-430b-b2b6-eb3149e41f1b)

## Technologies  
- React.js (Frontend)  
- C# (Backend, n-tier architecture)  
- MSSQL (Database)  
- JWT (User login security)

## Backend Details  
The backend is made with C# and uses n-tier architecture.

It has five layers:  
1. **Data Access Layer** (talks with the database)  
2. **Business Logic Layer** (all the rules and logic)  
3. **Presentation Layer** (the API, talks with the frontend)  
4. **Core Layer** (contains shared utilities and interfaces used by other layers)  
5. **Entity Layer** (defines the data models and entities used across the app)

It uses MSSQL for storing data like users, incomes, and expenses.  
JWT is used for safe user login and to keep sessions secure.

## How to Run Backend

1. **Requirements:**  
   - You must have .NET SDK installed (for example, .NET 6 or higher)  
   - MSSQL Server must be running  

2. **Setup Database:**  
   - Database will be created automatically by Entity Framework Code First when you run migrations  
   - Run migrations to create database and tables  

3. **Change Connection String:**  
   - Open the `DbContext` class in the `DataAccess` project  
   - Update the connection string directly inside the DbContext or constructor, for example:  
     ```
     Server=YOUR_SERVER;Database=ExpenseDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;
     ```

4. **Run Migrations:**  
   - Open terminal in backend project folder  
   - Run these commands to create and apply migrations:  
     ```bash
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

5. **Run Backend:**  
   - Run the backend API with:  
     ```bash
     dotnet run
     ```  
   - Backend will start

6. **Test API:**  
   - Use Postman or other tools to test API  
   - Or connect frontend to backend to check

---

## 🚀 Frontend

You can find the frontend code here:  
[🔗 ExpenseApplication Frontend Repository](https://github.com/KeremHavlc/ExpenseAppFront)
