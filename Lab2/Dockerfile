﻿FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Lab2/Lab2.csproj", "Lab2/"]
RUN dotnet restore "Lab2/Lab2.csproj"
COPY . .
WORKDIR "/src/Lab2"
RUN dotnet build "Lab2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab2.dll"]
