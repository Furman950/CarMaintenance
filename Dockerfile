FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 5555

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY CarMaintenance/*.csproj ./CarMaintenance/

COPY . .
WORKDIR /src/CarMaintenance
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base as final
ENV ASPNETCORE_URLS=http://+:5555
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT [ "dotnet", "CarMaintenance.dll" ]