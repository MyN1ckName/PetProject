#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/PetProject.IdentityServer/src/PetProject.IdentityServer.Host/PetProject.IdentityServer.Host.csproj", "src/PetProject.IdentityServer/src/PetProject.IdentityServer.Host/"]
COPY ["src/PetProject.IdentityServer/src/PetProject.IdentityServer.Database/PetProject.IdentityServer.Database.csproj", "src/PetProject.IdentityServer/src/PetProject.IdentityServer.Database/"]
COPY ["src/PetProject.IdentityServer/src/PetProject.IdentityServer.Domain/PetProject.IdentityServer.Domain.csproj", "src/PetProject.IdentityServer/src/PetProject.IdentityServer.Domain/"]
RUN dotnet restore "src/PetProject.IdentityServer/src/PetProject.IdentityServer.Host/PetProject.IdentityServer.Host.csproj"
COPY . .
WORKDIR "/src/src/PetProject.IdentityServer/src/PetProject.IdentityServer.Host"
RUN dotnet build "PetProject.IdentityServer.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetProject.IdentityServer.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetProject.IdentityServer.Host.dll"]