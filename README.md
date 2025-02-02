Description
The Current Account API is a backend service built using C# with .NET Core to facilitate the creation of new current accounts for existing customers. 
The API ensures proper handling of account creation, transactions, and user data retrieval. 
The architecture is structured for enterprise-level applications, focusing on testability, maintainability, and scalability.

Features:

Create New Account:
Accepts customerID and initialCredit as input.

Creates a new current account linked to the given customer.
If initialCredit > 0, a transaction is recorded for the new account.

Retrieve User Information:
Returns the customer's name, surname, balance, and transactions.

Microservices Architecture:
Accounts Service and Transactions Service are implemented separately.

Scalable Design:
Utilizes RabbitMQ for messaging.
Supports MongoDB for NoSQL storage.
Implements Entity Framework Core for relational data handling.

Technologies Used:
.NET Core (Backend)
Angular 19(Frontend)
Entity Framework Core (Database Interaction)
Unit of Work & Repository Pattern (Data Access Layer)
Dependency Injection (Decoupled Services)
Factory Pattern & HTTP Helper (Efficient API Communication)
Fluent Validation (Request Validation)
AutoMapper (Object Mapping)
RabbitMQ (Message Queue for Asynchronous/Synchronous Processing)
MongoDB (NoSQL Storage)
CI/CD

To run the backend project:
  1)Clone the repository.
  2)You will need a local or hosted SQL Server Database named BSynchro.
  3)Navigate to appsettings.json under BSynchro.RJP.Accounts.WebAPI to fix it.
  4)Configure the RabbitMQ configuration as well.
  5)Navigate to appsettings.json under BSynchro.RJP.Transactions.API.
  6)Replace MongoDB configuration with your configuration.
  7)Also update the RabbitMQ.
  8)Rebuild the project.
  9)Set BSynchro.RJP.Accounts.WebAPI as a startup project.
  10)Open Package Manager Console.
  11)Run this command: Add-Migration InitialDbMigration -c CoreDbContext.
  12)This will create an initial database migration.
  13)Run this command: Update-Database -Context CoreDbContext.
  14)This will apply changes to the database.
  15)Now set the BSynchro.RJP.Accounts.WebAPI and BSynchro.RJP.Transactions.API to run as startup projects.
  16)Projects will run smoothly.
