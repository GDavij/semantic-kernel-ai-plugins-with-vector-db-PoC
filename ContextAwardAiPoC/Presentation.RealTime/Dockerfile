FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /build
COPY . .
RUN dotnet restore "Presentation.RealTime/Presentation.RealTime.csproj"
RUN dotnet build "Presentation.RealTime/Presentation.RealTime.csproj" -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
COPY --from=build /app/publish .
WORKDIR /app

EXPOSE 443;
ENTRYPOINT ["dotnet", "Presentation.RealTime.dll"]
