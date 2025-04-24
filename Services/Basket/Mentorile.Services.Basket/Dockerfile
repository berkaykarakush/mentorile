FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Mentorile.Shared/*.csproj Shared/Mentorile.Shared/
COPY Services/Basket/Mentorile.Services.Basket/*.csproj Services/Basket/Mentorile.Services.Basket/
RUN dotnet restore Services/Basket/Mentorile.Services.Basket/*.csproj
COPY . .
RUN dotnet publish Services/Basket/Mentorile.Services.Basket/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
# VSDBG'yi yükle
RUN apt-get update 
RUN apt-get install -y unzip curl 
RUN mkdir -p /vsdbg 
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Mentorile.Services.Basket.dll" ]