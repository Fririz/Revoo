FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем все .csproj для корректного восстановления зависимостей
COPY ./Services/UserService/UserService.API/UserService.API.csproj ./Services/UserService/UserService.API/
COPY ./Services/UserService/UserService.Application/UserService.Application.csproj ./Services/UserService/UserService.Application/
COPY ./Services/UserService/UserService.Domain/UserService.Domain.csproj ./Services/UserService/UserService.Domain/

RUN dotnet restore "./Services/UserService/UserService.API/UserService.API.csproj"

# Копируем весь исходный код
COPY . .

WORKDIR "/src/Services/UserService/UserService.API"
RUN dotnet build "UserService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.API.dll"]