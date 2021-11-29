#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["envisio-backendv2.csproj", "."]
RUN dotnet restore "envisio-backendv2.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "envisio-backendv2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "envisio-backendv2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "envisio-backendv2.dll"]
