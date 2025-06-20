FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /console-publishing
COPY . .

RUN dotnet publish \
           -c Debug \
           -o publish \
           /console-publishing/Presentation.Console/Presentation.Console.csproj

FROM mcr.microsoft.com/dotnet/runtime:9.0-alpine AS runtime
WORKDIR /console-app
COPY --from=build /console-publishing/publish .

ENTRYPOINT [ "dotnet", "Presentation.Console.dll" ]