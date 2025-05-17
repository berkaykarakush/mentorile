FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY Services/PhotoStock/Mentorile.Services.PhotoStock.API/*.csproj Services/PhotoStock/Mentorile.Services.PhotoStock.API/
COPY Services/PhotoStock/Mentorile.Services.PhotoStock.Application/*.csproj Services/PhotoStock/Mentorile.Services.PhotoStock.Application/
COPY Services/PhotoStock/Mentorile.Services.PhotoStock.Domain/*.csproj Services/PhotoStock/Mentorile.Services.PhotoStock.Domain/
COPY Services/PhotoStock/Mentorile.Services.PhotoStock.Infrastructure/*.csproj Services/PhotoStock/Mentorile.Services.PhotoStock.Infrastructure/
COPY Services/PhotoStock/Mentorile.Services.PhotoStock.Abstractions/*.csproj Services/PhotoStock/Mentorile.Services.PhotoStock.Abstractions/
RUN dotnet restore Services/PhotoStock/Mentorile.Services.PhotoStock.API/*.csproj
COPY . .
RUN dotnet publish Services/PhotoStock/Mentorile.Services.PhotoStock.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Services.PhotoStock.API.dll" ]