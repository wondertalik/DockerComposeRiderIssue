# Introduction

It is an example that shows an issue explained [here](https://youtrack.jetbrains.com/issue/IDEA-320867/Stop-services-doest-work-correcty-because-of-.env-file)

# Development Environment

## Install need tools and add .env.Development file

- install [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- clone repository from gitHub
- in root directory create .env.dev file with
```.env.dev
DATABASE_DB=dbbase
DATABASE_USERNAME=dbuser
DATABASE_PASSWORD=dbpass
DATABASE_HOST_PORT=5433

DOTNET_ENVIRONMENT=Development
BUILD_CONFIGURATION=Debug
VOLUMES_PATH=~/Works/var/bcconfig

APPLICATION_NAME=DockerComposeRiderIssue
APP_URL=localhost
```

### Run during development

From a root directory of project run command. Will execute db migrations and seeds
```bash
docker compose -f docker-compose.yaml -f docker-compose.dev.yaml --env-file .env.dev -p docker-compose-rider-issue up --build --remove-orphans
```

### Stop and remove containers
```bash
docker compose -f docker-compose.yaml -f docker-compose.dev.yaml --env-file .env.dev -p docker-compose-rider-issue down
```


# Migrations

#### Connect to container

```bash
docker compose -f docker-compose.yaml -f docker-compose.dev.yaml --env-file .env.dev -p docker-compose-rider-issue exec dbmanage-migrations /bin/bash
```

#### Listing migrations

```bash
dotnet ef migrations list --startup-project ManageDatabaseApp --project DockerComposeRiderIssue.Infrastructure.Migrations
```

#### Add new migration

```bash
dotnet ef migrations add [migrationName] --startup-project ManageDatabaseApp --project DockerComposeRiderIssue.Infrastructure.Migrations
```

where [migrationName] is a name of migration

#### Update database

```bash
dotnet ef database update --startup-project ManageDatabaseApp --project DockerComposeRiderIssue.Infrastructure.Migrations
```
