FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/EngQuest.Web/EngQuest.Web.csproj", "src/EngQuest.Web/"]
COPY ["src/EngQuest.Application/EngQuest.Application.csproj", "src/EngQuest.Application/"]
COPY ["src/EngQuest.Domain/EngQuest.Domain.csproj", "src/EngQuest.Domain/"]
COPY ["src/EngQuest.Infrastructure/EngQuest.Infrastructure.csproj", "src/EngQuest.Infrastructure/"]
RUN dotnet restore "src/EngQuest.Web/EngQuest.Web.csproj"
COPY . .
WORKDIR "/src/src/EngQuest.Web"
RUN dotnet build "EngQuest.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EngQuest.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY /src/engquest.client/dist /app/wwwroot

ENTRYPOINT ["dotnet", "EngQuest.Web.dll"]