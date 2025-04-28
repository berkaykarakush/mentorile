FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY Services/Study/Mentorile.Services.Study.API/*.csproj Services/Study/Mentorile.Services.Study.API/
COPY Services/Study/Mentorile.Services.Study.Application/*.csproj Services/Study/Mentorile.Services.Study.Application/
COPY Services/Study/Mentorile.Services.Study.Domain/*.csproj Services/Study/Mentorile.Services.Study.Domain/
COPY Services/Study/Mentorile.Services.Study.Infrastructure/*.csproj Services/Study/Mentorile.Services.Study.Infrastructure/
RUN dotnet restore Services/Study/Mentorile.Services.Study.API/*.csproj
COPY . .
RUN dotnet publish Services/Study/Mentorile.Services.Study.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Services.Study.API.dll" ]