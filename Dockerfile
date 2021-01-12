#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001
EXPOSE 5555

#COPY ["YSP.Api/testcert.crt", "/usr/share/ca-certificates"]
COPY ["YSP.Api/testcert.crt", "/usr/local/share/ca-certificates"]
#COPY testcert.crt /usr/share/ca-certificates
#cp testcert.crt /usr/local/share/ca-certificates/testcert.crt
RUN update-ca-certificates
#RUN pip install --cert /etc/ssl/certs/testcert.pem

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["YSP.Api/YSP.Api.csproj", "YSP.Api/"]
RUN dotnet restore "YSP.Api/YSP.Api.csproj"
COPY . .
WORKDIR "/src/YSP.Api"
RUN dotnet build "YSP.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YSP.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YSP.Api.dll"]