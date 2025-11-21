# Task Manager API

The Task Manager API is built with ASP.NET Core and allows to create projects and their tasks.
Its functionality includes authentication, role-based authorization, architecture layers, and API documentation.

## Features

Authorized members with administrator privileges are able to create and delete projects.

Authorized members without privileges are allowed to add and remove tasks from any project.

Unauthorized users can retrieve a project by ID, list all projects, and filter them by words in their title.

## Architecture

The system architecture is organized into three layers:

- API: exposes the public interface of the system.
- Application: contains the core functionality of the application and interacts with the repository.
- Contract: defines the schemas involved in interacting with the API.

## Database

The database provider selected for the project is MySQL.
The repository implementation contains raw SQL commands to demonstrate manual query handling. Security is not considered. Changing provider requires a new implementation, as opposed to using an ORM like Entity Framework, where adapting the DbContext configuration would be sufficient.

## API Documentation

The API documentation is provided via Swagger UI, which is included in the ASP.NET Core configuration.
It can be accessed on `/swagger/index.html`

## Docker
A Dockerfile and a docker-compose.yaml file are provided to set up the application and database provider.
By default, ASP.NET will run on port `8080`, and the database server will run on port `3306`.

## Endpoints

### Projects
`POST /projects` -> Create a project.

`GET /projects` -> Retrieve all projects.

`GET /projects/{Id}` -> Retrieve a project by ID.

`DELETE /projects/{Id}` -> Delete a project by ID.

### Tasks

`POST /projects/{projectId}/tasks` -> Create a task for the given project ID.

`GET /projects/{projectId}/tasks/{taskId}` -> Retrieve a project's task by the given ID's.

`DELETE /projects/{projectId}/tasks/{taskId}` -> Delete a project's task by the given ID's.

### Members

`POST /member/register` -> Create a member.

`POST /member/login` -> Validate credentials in the JSON body and obtain JWT authorization.

`PUT /member/update` -> Update the member information provided in the JWT.

## First Steps
1. Register a member via `POST /member/register`. 
2. Log in to obtain a JWT via `POST /member/login`.
3. Promote the account to administrator via `PUT /member/update` (requires a JWT in the header).
4. With the **newly** provided JWT, any action requiring authorization can be performed.