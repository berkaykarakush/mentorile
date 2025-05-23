FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY Services/Course/Mentorile.Services.Course.API/*.csproj Services/Course/Mentorile.Services.Course.API/
COPY Services/Course/Mentorile.Services.Course.Application/*.csproj Services/Course/Mentorile.Services.Course.Application/
COPY Services/Course/Mentorile.Services.Course.Domain/*.csproj Services/Course/Mentorile.Services.Course.Domain/
COPY Services/Course/Mentorile.Services.Course.Infrastructure/*.csproj Services/Course/Mentorile.Services.Course.Infrastructure/
RUN dotnet restore Services/Course/Mentorile.Services.Course.API/*.csproj
COPY . .
RUN dotnet publish Services/Course/Mentorile.Services.Course.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Services.Course.API.dll" ]