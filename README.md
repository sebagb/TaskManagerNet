# Task Manager API

The Task Manager API is built with ASP.NET Core and allows to create projects and their tasks.
It functionality shows authentication, role-based authorization, architecture layers and API documentation.

## Features

Authorized members with an administrator privilege are able to create and delete projects.

Authorized members with no privilege are allowed to add and remove tasks to any project.

Unauthorized users can get a project by ID, list all projects and filter them by words in their title.

## Architecture

The system architecture is organized three layers:

- API: exposes the public interface of the system.
- Application: contains the core functionality of the application and interacts with the repository.
- Contract: schemas involved in interacting with the API.

## Database

The database provider selected for the project is MySQL.
The repository implementation contains raw SQL commands to demonstrate manual query handling. Security is not considered. Changing provider requires a new implementation as opposed of using an ORM like Entity Framework where adapting the DbContext configuration would be enough.

## API Documentation

The API documentation is provided via Swagger UI included with ASP.NET Core configuration.
It can be accessed on `/swagger/index.html`

## Docker
A dockerfile and docker-compose.yaml are provided to setup the application and database provider.
By default ASP.NET will run on port `8080` and the database server on port `3306`

## First Steps
1. Register a member on `POST /member/register`. 
2. Login to obtain a JWT on `POST /member/login`.
3. Promote the account to administrator on `PUT /member/update` (requires JWT in the header)
4. With the **new** provided JWT any action requiring authorization can be accomplished.