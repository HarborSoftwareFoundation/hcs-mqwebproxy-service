#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["./NuGet.config", "/"]
COPY ["src/Hcs.MqWebProxy.Api/Hcs.MqWebProxy.Api.csproj", "src/Hcs.MqWebProxy.Api/"]
RUN dotnet restore "src/Hcs.MqWebProxy.Api/Hcs.MqWebProxy.Api.csproj"
COPY . .
WORKDIR "/src/src/Hcs.MqWebProxy.Api"
RUN dotnet build "Hcs.MqWebProxy.Api.csproj" --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hcs.MqWebProxy.Api.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Harbor.Hcs.MqWebProxy.Api.dll"]