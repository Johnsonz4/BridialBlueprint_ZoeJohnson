# BridalBlueprint
AdvancedWebProject

Bridal Blueprint

Bridal Blueprint is a wedding planning web application designed to help couples and planners coordinate essential aspects of a wedding, including tasks, guests, and budgets. Built with ASP.NET Core MVC and Entity Framework Core, the app is fully responsive, accessible, and secured with user authentication.

How to Run the Application

Clone the repository from GitHub.

Open the solution in Visual Studio.

Update the database using: Update-Database from the Package Manager Console.

Run the project. 

Register a new user to access the dashboard and CRUD features.

Note: For full JSON responses from the REST API, you must be logged in.




 Features

 Data Model 

Three interconnected entities: Wedding, WeddingTask, Guest, BudgetItem

One many-to-many: WeddingUser

Data annotations for validation 

Server-side validation in controllers

 Application Functionalities 

Full CRRUD (Create, Read-All, Read, Update, Delete) for:

Wedding

Guest

BudgetItem

WeddingTask

Complex feature: WeddingTask linked to Wedding with dashboard and AJAX checkbox

Authentication and role-based authorization (Planner/Couple)

Dashboard with total guests, tasks, budget allocation

 RESTful API 

API for WeddingTasks:

GET all tasks

GET task by ID

POST (create)

PUT (update)

DELETE

Uses correct HTTP methods with JSON serialization

 JavaScript and AJAX 

AJAX checkbox toggling task completion

AJAX RSVP dropdown updates guest status in real time

 User Interface and UX 

Built with Bootstrap

Custom color theme

Clean navigation and layout

Accessible and responsive

