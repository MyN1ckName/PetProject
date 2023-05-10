#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/PetProject.ProductAPI/src/PetProject.ProductAPI.Host/PetProject.ProductAPI.Host.csproj", "src/PetProject.ProductAPI/src/PetProject.ProductAPI.Host/"]
COPY ["src/PetProject.ProductAPI/src/PetProject.ProductAPI.Application/PetProject.ProductAPI.Application.csproj", "src/PetProject.ProductAPI/src/PetProject.ProductAPI.Application/"]
COPY ["src/PetProject.ProductAPI/src/PetProject.ProductAPI.Application.Contracts/PetProject.ProductAPI.Application.Contracts.csproj", "src/PetProject.ProductAPI/src/PetProject.ProductAPI.Application.Contracts/"]
COPY ["src/PetProject.ProductAPI/src/PetProject.ProductAPI.Domain/PetProject.ProductAPI.Domain.csproj", "src/PetProject.ProductAPI/src/PetProject.ProductAPI.Domain/"]
COPY ["src/PetProject.ProductAPI/src/PetProject.ProductAPI.MongoDb/PetProject.ProductAPI.MongoDb.csproj", "src/PetProject.ProductAPI/src/PetProject.ProductAPI.MongoDb/"]
RUN dotnet restore "src/PetProject.ProductAPI/src/PetProject.ProductAPI.Host/PetProject.ProductAPI.Host.csproj"
COPY . .
WORKDIR "/src/src/PetProject.ProductAPI/src/PetProject.ProductAPI.Host"
RUN dotnet build "PetProject.ProductAPI.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetProject.ProductAPI.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetProject.ProductAPI.Host.dll"]