FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS base
WORKDIR /app

# Maintaner
LABEL Maintaner="yigit.at@trendyol.com" 

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=base /app/out .

# Check App status 
HEALTHCHECK --interval=5s --timeout=3s --retries=3 CMD curl --fail http://localhost:5070/api/healthcheck || exit 1

# Up & Run app
ENTRYPOINT ["dotnet", "TY.Services.Bank.dll"]