FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

RUN apt-get update && apt-get install -y iputils-ping

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "WebApplication-kubernetes.csproj"
RUN dotnet build "WebApplication-kubernetes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication-kubernetes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication-kubernetes.dll"]