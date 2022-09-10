FROM mcr.microsoft.com/dotnet/aspnet:6.0.8-alpine3.16-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SecondExampleApi/SecondExampleApi.csproj", "SecondExampleApi/"]
RUN dotnet restore "SecondExampleApi/SecondExampleApi.csproj"
COPY . .
WORKDIR "/src/SecondExampleApi"
RUN dotnet build "SecondExampleApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecondExampleApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecondExampleApi.dll"]
