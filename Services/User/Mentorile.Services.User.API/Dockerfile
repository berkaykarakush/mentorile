FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY Services/User/Mentorile.Services.User.API/*.csproj Services/User/Mentorile.Services.User.API/
COPY Services/User/Mentorile.Services.User.Application/*.csproj Services/User/Mentorile.Services.User.Application/
COPY Services/User/Mentorile.Services.User.Domain/*.csproj Services/User/Mentorile.Services.User.Domain/
COPY Services/User/Mentorile.Services.User.Infrastructure/*.csproj Services/User/Mentorile.Services.User.Infrastructure/
RUN dotnet restore Services/User/Mentorile.Services.User.API/*.csproj
COPY . .
RUN dotnet publish Services/User/Mentorile.Services.User.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Services.User.API.dll" ]