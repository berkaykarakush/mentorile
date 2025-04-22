FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Gateways/Mentorile.Gateway.API/*.csproj Gateways/Mentorile.Gateway.API/
RUN dotnet restore Gateways/Mentorile.Gateway.API/*.csproj
COPY . .
RUN dotnet publish Gateways/Mentorile.Gateway.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Gateway.API.dll" ]