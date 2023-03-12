# coding-challange

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
- [Angular Material](angular-material) for control library and themeing

### Backend
- [.Net 6](dotnet)
- [EntityFramework](nuget-ef) for SQL Server
- [docker SqlServer][docker-sqlserver]

[github-nswag]: https://github.com/RicoSuter/NSwag
[pnpm]: https://pnpm.io/
[angular-material]: https://material.angular.io/
[dotnet]: https://learn.microsoft.com/de-de/dotnet/core/whats-new/dotnet-6
[nuget-ef]: https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/
[docker-sqlserver]: https://hub.docker.com/_/microsoft-mssql-server