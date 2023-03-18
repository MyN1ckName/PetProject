#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/PetProject.IdentityServer.DbSeeder/PetProject.IdentityServer.DbSeeder.csproj", "PetProject.IdentityServer.DbSeeder/"]
COPY ["src/PetProject.IdentityServer.Database/PetProject.IdentityServer.Database.csproj", "PetProject.IdentityServer.Database/"]
COPY ["src/PetProject.IdentityServer.Domain/PetProject.IdentityServer.Domain.csproj", "PetProject.IdentityServer.Domain/"]
RUN dotnet restore "PetProject.IdentityServer.DbSeeder/PetProject.IdentityServer.DbSeeder.csproj"
COPY ./src .
WORKDIR "/src/PetProject.IdentityServer.DbSeeder"
RUN dotnet build "PetProject.IdentityServer.DbSeeder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetProject.IdentityServer.DbSeeder.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetProject.IdentityServer.DbSeeder.dll"]
