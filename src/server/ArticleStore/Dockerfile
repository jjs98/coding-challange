FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ArticleStore/ArticleStore.csproj", "ArticleStore/"]
RUN dotnet restore "ArticleStore/ArticleStore.csproj"
COPY . .
WORKDIR "/src/ArticleStore"
RUN dotnet build "ArticleStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArticleStore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArticleStore.dll"]