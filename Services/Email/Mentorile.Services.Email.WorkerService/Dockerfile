FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY Services/Email/Mentorile.Services.Email.Application/*.csproj Services/Email/Mentorile.Services.Email.Application/
COPY Services/Email/Mentorile.Services.Email.Domain/*.csproj Services/Email/Mentorile.Services.Email.Domain/
COPY Services/Email/Mentorile.Services.Email.Infrastructure/*.csproj Services/Email/Mentorile.Services.Email.Infrastructure/
COPY Services/Email/Mentorile.Services.Email.Templates/*.csproj Services/Email/Mentorile.Services.Email.Templates/
COPY Services/Email/Mentorile.Services.Email.WorkerService/*.csproj Services/Email/Mentorile.Services.Email.WorkerService/
RUN dotnet restore Services/Email/Mentorile.Services.Email.WorkerService/*.csproj
COPY . .
RUN dotnet publish Services/Email/Mentorile.Services.Email.WorkerService/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Services.Email.WorkerService.dll" ]