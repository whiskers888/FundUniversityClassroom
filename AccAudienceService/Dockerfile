
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Helper/Helper.csproj Helper/
RUN dotnet restore Helper/Helper.csproj

COPY Helper/ Helper/

COPY AccAudienceService/AccAudienceService.csproj AccAudienceService/
RUN dotnet restore AccAudienceService/AccAudienceService.csproj

COPY . .

RUN dotnet build AccAudienceService/AccAudienceService.csproj -c Release -o /app/build

FROM build AS publish

# Авто миграция
# RUN dotnet tool install --global dotnet-ef
# ENV PATH="${PATH}:/root/.dotnet/tools"
# RUN dotnet ef database update --project AccAudienceService/AccAudienceService.csproj
RUN dotnet publish AccAudienceService/AccAudienceService.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .

# EXPOSE 5002
EXPOSE 5003

# ENV ASPNETCORE_URLS=http://+:5003

# ENTRYPOINT ["dotnet", "AccAudienceService.dll"]