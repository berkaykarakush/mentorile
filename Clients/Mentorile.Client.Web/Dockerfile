FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Clients/Mentorile.Client.Web/*.csproj Clients/Mentorile.Client.Web/
RUN dotnet restore Clients/Mentorile.Client.Web/*.csproj
COPY . .
RUN dotnet publish Clients/Mentorile.Client.Web/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Client.Web.dll" ]