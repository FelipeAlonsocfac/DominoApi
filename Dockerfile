#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT="dev"

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Domino.Api/Domino.Api.csproj", "Domino.Api/"]
RUN dotnet restore "Domino.Api/Domino.Api.csproj"
COPY . .
WORKDIR "/src/Domino.Api."
RUN dotnet build "Domino.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Domino.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Domino.Api.dll"]