FROM mcr.microsoft.com/dotnet/runtime:6.0.21-jammy AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0.413-jammy AS build
WORKDIR /app
ARG BUILD_CONFIGURATION

COPY ["src/DockerComposeRiderIssue.Infrastructure.Entities/DockerComposeRiderIssue.Infrastructure.Entities.csproj", "src/DockerComposeRiderIssue.Infrastructure.Entities/"]
COPY ["src/DockerComposeRiderIssue.Infrastructure.Data/DockerComposeRiderIssue.Infrastructure.Data.csproj", "src/DockerComposeRiderIssue.Infrastructure.Data/"]
COPY ["src/DockerComposeRiderIssue.Infrastructure.Migrations/DockerComposeRiderIssue.Infrastructure.Migrations.csproj", "src/DockerComposeRiderIssue.Infrastructure.Migrations/"]
COPY ["src/ManageDatabaseApp/ManageDatabaseApp.csproj", "src/ManageDatabaseApp/"]

RUN \
    --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore "src/ManageDatabaseApp/ManageDatabaseApp.csproj"
COPY . .

WORKDIR "/app/src"

RUN \
    --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet build "ManageDatabaseApp/ManageDatabaseApp.csproj" -c $BUILD_CONFIGURATION -o /app/build --no-restore --no-self-contained


FROM build AS publish
ARG BUILD_CONFIGURATION
RUN \
    --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "ManageDatabaseApp/ManageDatabaseApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore --no-self-contained
COPY ["src/ManageDatabaseApp/efbundle-arm64", "/app/publish/efbundle-arm64"]
COPY ["src/ManageDatabaseApp/efbundle-amd64", "/app/publish/efbundle-amd64"]
COPY ["src/ManageDatabaseApp/migrate.sh", "/app/publish/migrate.sh"]
RUN chmod +x /app/publish/migrate.sh

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD "/bin/bash"
