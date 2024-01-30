FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["APIChatPlus.csproj", "."]
RUN dotnet restore "./APIChatPlus.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "APIChatPlus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIChatPlus.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Establecer la variable de entorno ASPNETCORE_URLS
ENV ASPNETCORE_URLS=http://+:5202

EXPOSE 5202
ENTRYPOINT ["dotnet", "APIChatPlus.dll"]
