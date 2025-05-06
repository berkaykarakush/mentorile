FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY Services/Discount/Mentorile.Services.Discount.API/*.csproj Services/Discount/Mentorile.Services.Discount.API/
COPY Services/Discount/Mentorile.Services.Discount.Application/*.csproj Services/Discount/Mentorile.Services.Discount.Application/
COPY Services/Discount/Mentorile.Services.Discount.Domain/*.csproj Services/Discount/Mentorile.Services.Discount.Domain/
COPY Services/Discount/Mentorile.Services.Discount.Infrastructure/*.csproj Services/Discount/Mentorile.Services.Discount.Infrastructure/
RUN dotnet restore Services/Discount/Mentorile.Services.Discount.API/*.csproj
COPY . .
RUN dotnet publish Services/Discount/Mentorile.Services.Discount.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Services.Discount.API.dll" ]