
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Service.Common/Service.Common.csproj Service.Common/
RUN dotnet restore Service.Common/Service.Common.csproj

COPY Service.Common/ Service.Common/

COPY Service.Audience/Service.Audience.csproj Service.Audience/
RUN dotnet restore Service.Audience/Service.Audience.csproj

COPY . .

RUN dotnet build Service.Audience/Service.Audience.csproj -c Release -o /app/build

FROM build AS publish

RUN dotnet publish Service.Audience/Service.Audience.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .
#Для HTTPS
# EXPOSE 5002 
EXPOSE 5003
