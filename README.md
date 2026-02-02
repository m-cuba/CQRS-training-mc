# Microservice Template
## Architecture
The architecture of this solution template is a Microservice following the Clean Architecture approach which is based on the [DDD Architecture](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice) and SOLID principles.
Clean Architecture provides a way to organize dependencies in the appplication by defining four different layers:
* Core (Domain Entities)
* Application (Use Cases)
* API 
* Infrastructure

![Clean Architecture Schema](https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg)

Source code dependencies can only point inwards

### Core (Domain Entities)
It holds the business model, which includes entities, repositories, and interfaces (contracts). These interfaces include abstractions for operations that will be performed using Infrastructure, such as data access, file system access, network calls, etc.
It also include [ValueObjects](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects) folder to for this kind of EF objects.

### Application (Use Cases)
These are the use cases of the application. They contain the application's business logic and interact with the Entities.
It's divided in the next folders:
* Behavior (collect all fluent validators for FluentValidation)
* Exceptions
* Extensions (Service registration)
* Mappers
	- Mappings are carried out by Mapster
* DTOs
	- All Dtos must to implement the BaseDTO class to be discovered by the assembling scanning. (Addition info on [link](https://sd.blackball.lv/en/articles/read/18850))
* Validators ([Fluent Validation](https://docs.fluentvalidation.net/en/latest/) implementation)

### Infrastructure
This layer mantains all the database migrations and database context Objects and have the repositories of all the domain model objects.
It's divided in the next folders:
* Data (DbContext CRUD operations)
* Extensions
* Migrations (If EF)
* Repositories

### API
Startup project with API Presentation layer with controllers.
Swagger and http file ([new feature on VS 2022](https://learn.microsoft.com/es-es/aspnet/core/test/http-files?view=aspnetcore-8.0)) ready for testing endpoints.

### Tests
On this folder have to be placed the unit test projects based on NUnit testing framework.
Follow Arrange/Act/Assert ([AAA](https://www.c-sharpcorner.com/UploadFile/dacca2/fundamental-of-unit-testing-understand-aaa-in-unit-testing/)) pattern for arranging and formatting code in Unit Test methods.
