#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#https://stackoverflow.com/questions/58116807/net-core-3-0-docker-container-wont-connect-to-sql-server
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
#RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
#RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /usr/lib/ssl/openssl.cnf
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["YSP.MVC/YSP.MVC.csproj", "YSP.MVC/"]
RUN dotnet restore "YSP.MVC/YSP.MVC.csproj"
COPY . .
WORKDIR "/src/YSP.MVC"
RUN dotnet build "YSP.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YSP.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YSP.MVC.dll"]