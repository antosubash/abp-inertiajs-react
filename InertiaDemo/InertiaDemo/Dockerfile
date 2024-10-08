# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG RUNTIME_IDENTIFIER=linux-x64
WORKDIR /src
COPY ["InertiaDemo/NuGet.Config", "InertiaDemo/"]
COPY ["InertiaDemo/InertiaDemo/InertiaDemo.csproj", "InertiaDemo/InertiaDemo/"]
RUN dotnet restore "./InertiaDemo/InertiaDemo/InertiaDemo.csproj"
COPY . .
WORKDIR "/src/InertiaDemo/InertiaDemo"
# Update package list and install curl
RUN apt-get update && \
    apt-get install -y curl

# Install Node.js LTS version
RUN curl -fsSL https://deb.nodesource.com/setup_lts.x | bash - && \
    apt-get install -y nodejs

# Install Sqllite
RUN apt-get install -y sqlite3 libsqlite3-dev
RUN dotnet tool install -g Volo.Abp.Cli
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN abp install-libs
RUN dotnet build "./InertiaDemo.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InertiaDemo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InertiaDemo.dll"]