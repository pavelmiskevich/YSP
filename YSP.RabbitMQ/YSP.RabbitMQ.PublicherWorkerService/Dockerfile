#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["YSP.RabbitMQ/YSP.RabbitMQ.PublicherWorkerService/YSP.RabbitMQ.PublicherWorkerService.csproj", "YSP.RabbitMQ/YSP.RabbitMQ.PublicherWorkerService/"]
RUN dotnet restore "YSP.RabbitMQ/YSP.RabbitMQ.PublicherWorkerService/YSP.RabbitMQ.PublicherWorkerService.csproj"
COPY . .
WORKDIR "/src/YSP.RabbitMQ/YSP.RabbitMQ.PublicherWorkerService"
RUN dotnet build "YSP.RabbitMQ.PublicherWorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YSP.RabbitMQ.PublicherWorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YSP.RabbitMQ.PublicherWorkerService.dll"]