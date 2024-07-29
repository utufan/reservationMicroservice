FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5265
ENV ASPNETCORE_URLS=http://+:5265

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["dfdsMicroserviceProject.csproj", "./"]
RUN dotnet restore "dfdsMicroserviceProject.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "dfdsMicroserviceProject.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "dfdsMicroserviceProject.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY wait-for-it.sh /usr/local/bin/
RUN chmod +x /usr/local/bin/wait-for-it.sh

ENTRYPOINT ["wait-for-it.sh", "db:5432", "--", "dotnet", "dfdsMicroserviceProject.dll"]
