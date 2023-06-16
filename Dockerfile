FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY ./ ./


RUN dotnet restore "AwesomeURLShortener.API/AweSomeURLShortener.API.csproj"
COPY . .
RUN dotnet build "AwesomeURLShortener.API/AweSomeURLShortener.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwesomeURLShortener.API/AweSomeURLShortener.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


EXPOSE 5009


ENTRYPOINT ["dotnet", "AweSomeURLShortener.API.dll"]

