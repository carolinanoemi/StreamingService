# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Kopiér HELE løsningen
COPY . .

# Gå ind i UserService-projektet (læg mærke til: src/UserService)
WORKDIR "/src/src/UserService"

# Restore + publish
RUN dotnet restore "UserService.csproj"
RUN dotnet publish "UserService.csproj" -c Release -o /app/publish

# Runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserService.dll"]
