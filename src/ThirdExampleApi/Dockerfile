﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0.8-alpine3.16-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ThirdExampleApi/ThirdExampleApi.csproj", "ThirdExampleApi/"]
RUN dotnet restore "ThirdExampleApi/ThirdExampleApi.csproj"
COPY . .
WORKDIR "/src/ThirdExampleApi"
RUN dotnet build "ThirdExampleApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThirdExampleApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThirdExampleApi.dll"]
