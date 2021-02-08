# EducationProject
Backend platform (Domain driven design) and REST API - For storing project and task data with code

This repository contains source code for:

- REST API
- Domain Layer
- Application Layer
- Implementation layer
- DataAccess layer (Azure storage)

## Technologies

- .NET Core
- Azure Table Storage, Azure Blob Storage

## Instalation


## Databases
Applicaiton uses Azure Storage SDK to communicate with the Azure Storage API to easily work with unstructured data

## API Documentation and JSON schemas - Swagger

API documentation is auto generated and can be found here: http://localhost:5000/swagger

## Postman support
## Dependencies
- Fluent Validation (https://fluentvalidation.net/) - used for middleware entity validation
- AutoMapper (https://automapper.org/) - Mapping Data Transfer objects to Data objects and reverse
- AuzreStorage (https://docs.microsoft.com/en-us/dotnet/api/overview/azure/storage) - Interacting with azure storage
- Swashbuckle.AspNetCore (https://swagger.io/) - Document and test
