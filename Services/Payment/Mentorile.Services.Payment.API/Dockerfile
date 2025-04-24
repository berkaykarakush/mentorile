FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY Services/Payment/Mentorile.Services.Payment.API/*.csproj Services/Payment/Mentorile.Services.Payment.API/
RUN dotnet restore Services/Payment/Mentorile.Services.Payment.API/*.csproj
COPY . .
RUN dotnet publish Services/Payment/Mentorile.Services.Payment.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
# VSDBG'yi yükle
RUN apt-get update 
RUN apt-get install -y unzip curl 
RUN mkdir -p /vsdbg 
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Services.Payment.API.dll" ]