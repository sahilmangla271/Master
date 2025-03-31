Set up instructions

**Scripts for Database Migrations in Package Manage Console**

**Add-Migration InitialCreate**

It generates a new migration file under the Migrations folder. 
This file contains:
Table creation commands based on your DbContext and entity classes.
Constraints such as primary keys, foreign keys, and indexes.
Initial database schema changes.

**Update-Database**
Checks for pending migrations in the Migrations folder.
Applies migrations to the database (e.g., creates or modifies tables, adds constraints, updates indexes).

**Remove-Migration** if you want to remove the last migration.
The Remove-Migration command is used to undo the last migration before it has been applied to the database. It deletes the latest migration file from the Migrations folder.

**CSV Upload:** Supports bulk data upload from members.csv and inventory.csv via API or web form.

**Inventory Booking:** Users can book available items from the inventory, with a limit of 2 active bookings per user.

**Booking Management:**
/book: Books an item for a user.
/cancel: Cancels an existing booking.
Data Persistence: Stores all transactions in a SQL database.

**Unit Testing:** XUnit-based tests for API endpoints.


**Tech Stack**

Backend: .NET 8, ASP.NET Core Web API

Design Pattern: Repository Pattern

Database: SQLite

XUnit (Unit testing)

Swagger API documentation
