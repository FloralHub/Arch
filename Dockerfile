FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY Directory.Build.props ./
COPY ["src/AlchemyLub.Arch.Api/AlchemyLub.Arch.Api.csproj", "AlchemyLub.Arch.Api/"]
COPY ["src/AlchemyLub.Arch.Core/AlchemyLub.Arch.Core.csproj", "AlchemyLub.Arch.Core/"]
RUN dotnet restore "./AlchemyLub.Arch.Api/AlchemyLub.Arch.Api.csproj"

COPY . .
WORKDIR "/src/src/AlchemyLub.Arch.Api"
RUN dotnet build "./AlchemyLub.Arch.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./AlchemyLub.Arch.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlchemyLub.Arch.Api.dll"]
