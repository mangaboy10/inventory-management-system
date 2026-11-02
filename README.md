# Inventory Management System

This is an inventory management application I built using C#, Blazor, and SQL Server. The purpose of this project was to create a system that allows users to manage inventory items, suppliers, staff, and transactions.

The application also keeps track of stock quantities whenever items are received or issued.

# Features

The application allows users to:

Add, edit, view, and delete inventory items.

Add, edit, view, and delete suppliers and staff members.

Receive items from suppliers and increase the available stock.

Issue items to staff members and decrease the available stock.

View transaction history to keep track of received and issued items.

See when an item's quantity is low based on its reorder level.

# Validation

I also added validation to prevent certain errors when managing inventory.

The quantity entered must be greater than zero.

Users cannot issue more items than what is currently available.

A supplier must be selected when receiving items.

A staff member must be selected when issuing items.

# Technologies Used

C#

ASP.NET Core Blazor

.NET 8

SQL Server

Entity Framework Core

Bootstrap

Visual Studio

# What I Learned

Working on this project helped me get more comfortable with C# and Blazor, especially when it comes to connecting an application to a database.

I also got more practice working with CRUD operations (Create, Read, Update, Delete), creating forms, adding validation, and using services to handle the application's logic.

One of the main things I focused on was making sure the inventory quantities updated correctly whenever items were received or issued.