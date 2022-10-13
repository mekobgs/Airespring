# Airespring

This is a brand new project created with ASP.NET Core 6 + SQL Server Database. Dapper is used to avoid LINQ or EF and use Queries

To run this project please:

- Run this script on your local database: 
 ``` 
 Create Database Airespring;
 
 Use Airespring;
 
 Create table Employee(
 Id int IDENTITY(1,1) PRIMARY KEY,
 LastName varchar(100),
 FirstName varchar(100),
 Zip varchar(10),
 Phone varchar(15),
 HiredDate Date
 ) 
 ```
  
 - Update appsettings.json with your credentials:
    ``` 
    "SqlConnection": "server=localhost; database=AirespringDb; User Id= user; Password=password; TrustServerCertificate=True"
    ``` 
    
 - Press F5 and run this project.
 
 
 # Features
 - List of Employees
 - View details of selected employee
 - Create a new Employee
 - Search by LastName and PhoneNumber and keep current sort order
 - Sort by hiredDate and keep your current search
 
 
