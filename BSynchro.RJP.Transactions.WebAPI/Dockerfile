# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BSynchro.RJP.Transactions.WebAPI/BSynchro.RJP.Transactions.WebAPI.csproj", "BSynchro.RJP.Transactions.WebAPI/"]
COPY ["BSynchro.RJP.Transactions.Application/BSynchro.RJP.Transactions.Application.csproj", "BSynchro.RJP.Transactions.Application/"]
COPY ["BSynchro.RJP.Transactions.Domain/BSynchro.RJP.Transactions.Domain.csproj", "BSynchro.RJP.Transactions.Domain/"]
COPY ["Common.MessageQueueSender/Common.MessageQueueSender.csproj", "Common.MessageQueueSender/"]
COPY ["BSynchro.RJP.Transactions.Infrastructure/BSynchro.RJP.Transactions.Infrastructure.csproj", "BSynchro.RJP.Transactions.Infrastructure/"]
COPY ["Common.MongoDb/Common.MongoDb.csproj", "Common.MongoDb/"]
COPY ["Common.Utitlities/Common.Utitlities.csproj", "Common.Utitlities/"]
RUN dotnet restore "./BSynchro.RJP.Transactions.WebAPI/BSynchro.RJP.Transactions.WebAPI.csproj"
COPY . .
WORKDIR "/src/BSynchro.RJP.Transactions.WebAPI"
RUN dotnet build "./BSynchro.RJP.Transactions.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BSynchro.RJP.Transactions.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BSynchro.RJP.Transactions.WebAPI.dll"]