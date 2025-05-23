FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY Services/Basket/Mentorile.Services.Basket.API/*.csproj Services/Basket/Mentorile.Services.Basket.API/
COPY Services/Basket/Mentorile.Services.Basket.Application/*.csproj Services/Basket/Mentorile.Services.Basket.Application/
COPY Services/Basket/Mentorile.Services.Basket.Domain/*.csproj Services/Basket/Mentorile.Services.Basket.Domain/
COPY Services/Basket/Mentorile.Services.Basket.Infrastructure/*.csproj Services/Basket/Mentorile.Services.Basket.Infrastructure/
RUN dotnet restore Services/Basket/Mentorile.Services.Basket.API/*.csproj
COPY . .
RUN dotnet publish Services/Basket/Mentorile.Services.Basket.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Services.Basket.API.dll" ]