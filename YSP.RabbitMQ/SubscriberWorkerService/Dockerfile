#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["YSP.RabbitMQ/SubscriberWorkerService/SubscriberWorkerService.csproj", "YSP.RabbitMQ/SubscriberWorkerService/"]
RUN dotnet restore "YSP.RabbitMQ/SubscriberWorkerService/SubscriberWorkerService.csproj"
COPY . .
WORKDIR "/src/YSP.RabbitMQ/SubscriberWorkerService"
RUN dotnet build "SubscriberWorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SubscriberWorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SubscriberWorkerService.dll"]