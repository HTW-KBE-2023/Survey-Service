#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Survey-API/Survey-API.csproj", "Survey-API/"]
RUN dotnet restore "Survey-API/Survey-API.csproj"
COPY . .
WORKDIR "/src/Survey-API"
RUN dotnet build "Survey-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Survey-API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN apt-get update 
RUN apt-get --yes install curl
HEALTHCHECK --interval=5s --timeout=10s --retries=3 CMD curl --silent --fail http://localhost/api/health || exit 1
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Survey-API.dll"]