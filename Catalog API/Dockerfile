FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Catalog API.csproj", "./"]
RUN dotnet restore "Catalog API.csproj"
COPY . .
# Publish includes building the project
RUN dotnet publish "Catalog API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Catalog API.dll"]
