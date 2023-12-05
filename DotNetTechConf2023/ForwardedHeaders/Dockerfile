FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ForwardedHeaders/ForwardedHeaders.csproj", "ForwardedHeaders/"]
RUN dotnet restore "ForwardedHeaders/ForwardedHeaders.csproj"
COPY . .
WORKDIR "/src/ForwardedHeaders"
RUN dotnet build "ForwardedHeaders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ForwardedHeaders.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ForwardedHeaders.dll"]
