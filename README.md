# User Management System
This is an implementation of a fully functional user management system that allows us to manage (add, update, delete) users and store the changes in a database. 
Besides basic CRUD operations, other features such as: filtering by any field, ordering and permission management, are also available.

## Tech stack
- Backend: .NET 5 with Entity Framework Core 5 (code first approach)
- Database: MSSql (Microsoft SQL Server)
- Frontend: This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 14.0.0.

## How to run the application
- Check the connection string stored in `appSettings.json` file. It should look like this: `"Server(LocalDb)\\MSSQLLocalDB;Database=UserManagement;Trusted_Connection=True;"`
Make sure to update the server name if your local server name differs.
- In order to create the database, open the Package Manager Console (with default project set to `Data`) and run `Update-Database` command.
- On frontend project, run `npm i` to install required packages. Run `npm start` to start a dev server.
- Navigate to `http://localhost:4200/` and the app UI should display.
