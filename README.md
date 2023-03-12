# Coding-challange articlestore

## Build and run Docker

Execute `docker-compose build` and `docker-compose up` to run the container locally.

- The client can be accessed via http://localhost:5000
- The web-app is hosted on port 8080
  - The swagger-api can be accessed via http://localhost:8080/swagger/index.html
- The SQLServer is hosted on port 1433

Build for linux Amd64

## Used technologies

### Client
- [pnpm](https://pnpm.io/) as a package manager
- [NSwag](https://github.com/RicoSuter/NSwag) to generate client apis
- [Angular Material](https://material.angular.io/) for control library and themeing

### Backend
- [.Net 6](https://learn.microsoft.com/de-de/dotnet/core/whats-new/dotnet-6)
- [EntityFramework](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/) for SQL Server
- [docker SqlServer](https://hub.docker.com/_/microsoft-mssql-server)
