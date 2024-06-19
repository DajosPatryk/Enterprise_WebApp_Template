ARG DOTNET_IMAGE=mcr.microsoft.com/dotnet/sdk:8.0

FROM $DOTNET_IMAGE AS build-env
WORKDIR /App

# Restore dependencies and compile app
COPY . .
RUN dotnet nuget locals all --clear
RUN dotnet restore
RUN dotnet build -c Debug -o out

# Build runtime
FROM $DOTNET_IMAGE
WORKDIR /App
COPY --from=build-env /App .

EXPOSE 8080
ENTRYPOINT ["dotnet", "watch", "run", "--project", "server.csproj"]
