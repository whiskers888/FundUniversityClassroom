
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Service.Data/Service.Data.csproj Service.Data/
RUN dotnet restore Service.Data/Service.Data.csproj

COPY Service.Data/ Service.Data/

COPY Helper/Helper.csproj Helper/
RUN dotnet restore Helper/Helper.csproj

COPY Helper/ Helper/

COPY Service.Housing/Service.Housing.csproj Service.Housing/
RUN dotnet restore Service.Housing/Service.Housing.csproj

COPY . .

RUN dotnet build Service.Housing/Service.Housing.csproj -c Release -o /app/build

FROM build AS publish
# Авто миграция
# RUN dotnet tool install --global dotnet-ef
# ENV PATH="${PATH}:/root/.dotnet/tools"
# RUN dotnet ef database update --project AccHousingService/AccHousingService.csproj
RUN dotnet publish Service.Housing/Service.Housing.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .
#Для HTTPS
# EXPOSE 5000 
EXPOSE 5001