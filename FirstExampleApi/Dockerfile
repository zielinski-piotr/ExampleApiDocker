FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FirstExampleApi/FirstExampleApi.csproj", "FirstExampleApi/"]
RUN dotnet restore "FirstExampleApi/FirstExampleApi.csproj"
COPY . .
WORKDIR "/src/FirstExampleApi"
RUN dotnet build "FirstExampleApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FirstExampleApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FirstExampleApi.dll"]
