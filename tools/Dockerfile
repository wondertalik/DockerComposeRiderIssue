FROM mcr.microsoft.com/dotnet/sdk:6.0.413-jammy AS build
ENV PATH $PATH:/root/.dotnet/tools

RUN dotnet tool install --global dotnet-ef --version 7.0.10
RUN dotnet tool install --global Mapster.Tool --version 8.4.0-pre06

CMD "/bin/bash"