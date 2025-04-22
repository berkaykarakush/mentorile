FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY IdentityServer/Mentorile.IdentityServer/*.csproj IdentityServer/Mentorile.IdentityServer/
RUN dotnet restore IdentityServer/Mentorile.IdentityServer/*.csproj
COPY . .
RUN dotnet publish IdentityServer/Mentorile.IdentityServer/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.IdentityServer.dll" ]