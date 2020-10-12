#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["./NuGet.config", "/"]
COPY ["src/Hcs.MqWebProxy.Service/Hcs.MqWebProxy.Service.csproj", "src/Hcs.MqWebProxy.Service/"]
RUN dotnet restore "src/Hcs.MqWebProxy.Service/Hcs.MqWebProxy.Service.csproj"
COPY . .
WORKDIR "/src/src/Hcs.MqWebProxy.Service"
RUN dotnet build "Hcs.MqWebProxy.Service.csproj" --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hcs.MqWebProxy.Service.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hcs.MqWebProxy.Service.dll"]