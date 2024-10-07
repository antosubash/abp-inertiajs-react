FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

USER app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["InertiaDemo/InertiaDemo/InertiaDemo.csproj", "InertiaDemo/InertiaDemo/"]
RUN dotnet restore "InertiaDemo/InertiaDemo/InertiaDemo.csproj"
COPY . .
WORKDIR "/src/InertiaDemo/InertiaDemo"
# Update package list and install curl
RUN apt-get update && \
    apt-get install -y curl

# Install Node.js LTS version
RUN curl -fsSL https://deb.nodesource.com/setup_lts.x | bash - && \
    apt-get install -y nodejs
RUN dotnet build "InertiaDemo.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "InertiaDemo.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InertiaDemo.dll"]
