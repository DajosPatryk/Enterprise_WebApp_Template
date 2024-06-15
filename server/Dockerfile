ARG DOTNET_IMAGE=mcr.microsoft.com/dotnet/sdk:8.0
FROM $DOTNET_IMAGE AS base
WORKDIR /app

FROM base AS setup
COPY *.csproj ./
RUN dotnet nuget locals all --clear
RUN dotnet restore

FROM base AS development
COPY --from=setup /app/*.csproj ./
COPY . .
RUN dotnet build -c Debug -o out
EXPOSE 8080

ENTRYPOINT ["dotnet", "watch", "run", "--project", "server.csproj"]
