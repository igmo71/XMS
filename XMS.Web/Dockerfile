# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["XMS.Web/XMS.Web.csproj", "XMS.Web/"]
COPY ["XMS.Application/XMS.Application.csproj", "XMS.Application/"]
COPY ["XMS.Domain/XMS.Domain.csproj", "XMS.Domain/"]
COPY ["XMS.Infrastructure/XMS.Infrastructure.csproj", "XMS.Infrastructure/"]
RUN dotnet restore "./XMS.Web/XMS.Web.csproj"
COPY . .
WORKDIR "/src/XMS.Web"
RUN dotnet build "./XMS.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./XMS.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XMS.Web.dll"]