FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY Services/PhotoStock/Mentorile.Services.PhotoStock/*.csproj Services/PhotoStock/Mentorile.Services.PhotoStock/
RUN dotnet restore Services/PhotoStock/Mentorile.Services.PhotoStock/*.csproj
COPY . .
RUN dotnet publish Services/PhotoStock/Mentorile.Services.PhotoStock/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Services.PhotoStock.dll" ]