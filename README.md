# Coding-challange articlestore

## Build and run Docker

Execute `docker-compose build` and `docker-compose up` to run the container locally.

- The client can be accessed via http://localhost:5000
- The web-app is hosted on port 8081
  - The swagger-api can be accessed via http://localhost:8081/swagger/index.html
- The PostgreSQL is hosted on port 5432
- The Adminer is hosted on port http://localhost:8080

Build for linux Amd64

## Used technologies

### Client
- [pnpm](https://pnpm.io/) as a package manager
- [NSwag](https://github.com/RicoSuter/NSwag) to generate client apis
- [Angular Material](https://material.angular.io/) for control library and themeing

### Backend
- [.Net 6](https://learn.microsoft.com/de-de/dotnet/core/whats-new/dotnet-6)
- [EntityFramework for PostgreSQL](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL/)
- [Docker PostgreSQL](https://hub.docker.com/_/postgres)
- [Docker Adminer](https://hub.docker.com/_/adminer)

## Tests

### Client
Todo

### Backend
Unittests are located in the ArticleStore.UnitTests.csproj