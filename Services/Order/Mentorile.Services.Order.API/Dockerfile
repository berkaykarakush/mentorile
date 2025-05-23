FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY Services/Order/Mentorile.Services.Order.API/*.csproj Services/Order/Mentorile.Services.Order.API/
COPY Services/Order/Mentorile.Services.Order.Application/*.csproj Services/Order/Mentorile.Services.Order.Application/
COPY Services/Order/Mentorile.Services.Order.Domain/*.csproj Services/Order/Mentorile.Services.Order.Domain/
COPY Services/Order/Mentorile.Services.Order.Domain.Core/*.csproj Services/Order/Mentorile.Services.Order.Domain.Core/
COPY Services/Order/Mentorile.Services.Order.Infrastructure/*.csproj Services/Order/Mentorile.Services.Order.Infrastructure/
RUN dotnet restore Services/Order/Mentorile.Services.Order.API/*.csproj
COPY . .
RUN dotnet publish Services/Order/Mentorile.Services.Order.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Services.Order.API.dll" ]