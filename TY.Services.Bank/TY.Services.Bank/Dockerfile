FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TY.Kong/TY.Services.Bank/TY.Services.Bank.csproj", "TY.Services.Bank/"]
RUN dotnet restore "TY.Kong/TY.Services.Bank/TY.Services.Bank.csproj"
COPY . .
WORKDIR "/src/TY.Kong/TY.Services.Bank"
RUN dotnet build "TY.Services.Bank.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TY.Services.Bank.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TY.Services.Bank.dll"]