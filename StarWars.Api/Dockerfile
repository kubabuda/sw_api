#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["StarWars.Api/StarWars.Api.csproj", "StarWars.Api/"]
RUN dotnet restore "StarWars.Api/StarWars.Api.csproj"
COPY . .
WORKDIR "/src/StarWars.Api"
RUN dotnet build "StarWars.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StarWars.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StarWars.Api.dll"]