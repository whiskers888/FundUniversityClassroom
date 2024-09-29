
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Helper/Helper.csproj Helper/
RUN dotnet restore Helper/Helper.csproj

COPY Helper/ Helper/

COPY AccHousingService/AccHousingService.csproj AccHousingService/
RUN dotnet restore AccHousingService/AccHousingService.csproj

COPY . .

RUN dotnet build AccHousingService/AccHousingService.csproj -c Release -o /app/build

FROM build AS publish
# Авто миграция
# RUN dotnet tool install --global dotnet-ef
# ENV PATH="${PATH}:/root/.dotnet/tools"
# RUN dotnet ef database update --project AccHousingService/AccHousingService.csproj
RUN dotnet publish AccHousingService/AccHousingService.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 5000
EXPOSE 5001
# ENV ASPNETCORE_URLS=http://+:5001


# ENTRYPOINT ["dotnet", "AccHousingService.dll"]