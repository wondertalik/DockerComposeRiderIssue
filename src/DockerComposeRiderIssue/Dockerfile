FROM mcr.microsoft.com/dotnet/aspnet:6.0.20-jammy AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0.412-jammy  AS build
WORKDIR /httpapi
ARG BUILD_CONFIGURATION

COPY ["src/DockerComposeRiderIssue/DockerComposeRiderIssue.csproj", "src/DockerComposeRiderIssue/"]

RUN \
    --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore "src/DockerComposeRiderIssue/DockerComposeRiderIssue.csproj"
    
COPY . .

WORKDIR "/httpapi/src"
RUN \
    --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet build "DockerComposeRiderIssue/DockerComposeRiderIssue.csproj" -c $BUILD_CONFIGURATION -o /app/build --no-restore --no-self-contained

FROM build AS publish
ARG BUILD_CONFIGURATION
RUN \
    --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "DockerComposeRiderIssue/DockerComposeRiderIssue.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore --no-self-contained

FROM base AS final
WORKDIR /app
RUN \
    --mount=type=cache,target=/var/cache/apt \
    apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerComposeRiderIssue.dll"]

