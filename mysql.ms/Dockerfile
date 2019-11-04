FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["mysql.ms/mysql.ms.csproj", "mysql.ms/"]
RUN dotnet restore "mysql.ms/mysql.ms.csproj"
COPY . .
WORKDIR "/src/mysql.ms"
RUN dotnet build "mysql.ms.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "mysql.ms.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "mysql.ms.dll"]