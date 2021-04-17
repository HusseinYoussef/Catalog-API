FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app
EXPOSE 80

COPY . .
RUN dotnet restore "Catalog API.csproj"
RUN dotnet publish "Catalog API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS final
ENTRYPOINT ["dotnet", "publish/Catalog API.dll"]
