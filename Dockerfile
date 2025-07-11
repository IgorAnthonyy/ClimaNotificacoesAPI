FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . . 

RUN dotnet restore ClimaNotificacoesAPI.sln

WORKDIR /app/ClimaNotificacoesAPI.API
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
EXPOSE 80
ENTRYPOINT ["dotnet", "ClimaNotificacoesAPI.API.dll"]