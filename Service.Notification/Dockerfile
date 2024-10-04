
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Service.Common/Service.Common.csproj Service.Common/
RUN dotnet restore Service.Common/Service.Common.csproj

COPY Service.Common/ Service.Common/

COPY Service.Notification/Service.Notification.csproj Service.Notification/
RUN dotnet restore Service.Notification/Service.Notification.csproj

COPY . .

RUN dotnet build Service.Notification/Service.Notification.csproj -c Release -o /app/build

FROM build AS publish

RUN dotnet publish Service.Notification/Service.Notification.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .
#Для HTTPS
# EXPOSE 5004 
EXPOSE 5005
