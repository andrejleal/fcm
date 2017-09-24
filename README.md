# Summary
The FCM repository provides an application for component configuration management. It has been develop for a company that has many applications using the same toggle component with toggle properties stored in a properties file. This service will allow the management of those configurations without restarting the dependent services.

Check some docs in folder [**/docs**](/docs).

## Requirements
* .NET Core 1.1 SDK from https://go.microsoft.com/fwlink/?LinkID=835014)
* Sql Server (optional)

## Domain Entity Model
* External System - Represents any entity that as rights to consume FCM application.
* Component - Represents application components like button, toggles, etc.
* ComponentProperty - Represents properties of components like value, width, size, etc.

## Configuration to use a InMemory database

The FCM Application is configured to use a InMemory database has a default database provider.

## Configuration to use a Sql Server database

* open src\FCM.API.REST\appsetting.json and update **dbProvider** and **connection string entries**
* open src\FCM.Repositories.EF.SQLServer\appsetting.json and update **dbProvider** and **connection string entries**
* open command line in folder src\FCM.Repositories.EF.SQLServer and execute **dotnet ef database update -c FCMContext**

## Start application
* execute **dotnet run** in folder src\FCM.API.REST
* execute **dotnet run** in folder src\FCM.Client.App


## FCM.API.REST
A REST Web API that provides endpoint to manage components and external systems.

Check API documentation at: **http://localhost:5001/swagger/ui**

### Authentication
Token based authentication that must be provided in http header **Authorization**

Each External System has two token associated that can be used to access APIs. The following that show the default configuration for external system tokens.

| ExternalSystem Name | Token         | AlternateToken |
| -------------       |:-------------:| --------------:|
| sysadmin            | sysadmin      | sa             |
| clientApp1          | clientAppPw1  | clientAppPw1   |
| clientApp2          | clientAppPw2  | clientAppPw2   |
| clientApp3          | clientAppPw3  | clientAppPw3   |
| clientApp4          | clientAppPw4  | clientAppPw4   |

##Authorization
The FCM.API.REST has two roles (sysadmin and normal).
All routes except **GET /api/ExternalSystems/components** requires sysadmin role.


# FCM.Client.APP
An application to simulate interactions between FCM.API.REST and its consumers.

Check API documentation at: * **http://localhost:5002/swagger/ui**

### Authentication
Token based authentication that must be provided in http header **Authorization**

| ExternalSystem Name | NotificationToken         | 
| -------------       |:-------------:| 
| clientApp1          | clientAppNotificationToken1  | 
| clientApp2          | clientAppNotificationToken2  | 
| clientApp3          | clientAppNotificationToken3  | 
| clientApp4          | clientAppNotificationToken4  | 


The NotificationToken needs to be used to force the component configuration refresh (
**POST /api/ExternalSystems/{name}/components/refresh** )

##Authorization
The FCM.API.REST has two roles (sysadmin and normal).
All routes except **GET /api/ExternalSystems/components** requires sysadmin role.
