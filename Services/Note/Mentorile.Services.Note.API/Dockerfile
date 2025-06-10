FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY Services/Note/Mentorile.Services.Note.API/*.csproj Services/Note/Mentorile.Services.Note.API/
COPY Services/Note/Mentorile.Services.Note.Application/*.csproj Services/Note/Mentorile.Services.Note.Application/
COPY Services/Note/Mentorile.Services.Note.Domain/*.csproj Services/Note/Mentorile.Services.Note.Domain/
COPY Services/Note/Mentorile.Services.Note.Infrastructure/*.csproj Services/Note/Mentorile.Services.Note.Infrastructure/
RUN dotnet restore Services/Note/Mentorile.Services.Note.API/*.csproj
COPY . .
RUN dotnet publish Services/Note/Mentorile.Services.Note.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Services.Note.API.dll" ]