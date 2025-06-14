FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine3.21 AS publish
WORKDIR /presentation-console
COPY . .

RUN dotnet restore /presentation-console/Presentation.Console/Presentation.Console.csproj
RUN dotnet publish -c Debug -o publish --no-restore /presentation-console/Presentation.Console/Presentation.Console.csproj


FROM mcr.microsoft.com/dotnet/runtime:9.0-alpine3.21 AS runtime
WORKDIR /app
COPY --from=publish /presentation-console/publish .
ENTRYPOINT ["dotnet", "ContextAwardAiPoC.Presentation.Console.dll"]