FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV SYNCFUSION_LICENSE_KEY=""
ENV SPELLCHECK_DICTIONARY_PATH=""
ENV SPELLCHECK_JSON_FILENAME=""
ENV SPELLCHECK_CACHE_COUNT=""
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source
COPY ["src/ej2-documenteditor-server/ej2-documenteditor-server.csproj", "./ej2-documenteditor-server/ej2-documenteditor-server.csproj"]
COPY ["src/ej2-documenteditor-server/NuGet.Config", "./ej2-documenteditor-server/"]
RUN dotnet restore "./ej2-documenteditor-server/ej2-documenteditor-server.csproj"
COPY . .
WORKDIR "/source/src"
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
RUN apt-get update && apt-get install -y libfontconfig
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ej2-documenteditor-server.dll"]