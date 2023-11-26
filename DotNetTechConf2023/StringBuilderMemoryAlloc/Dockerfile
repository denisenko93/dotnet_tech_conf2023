FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["StringBuilderMemoryAlloc/StringBuilderMemoryAlloc.csproj", "StringBuilderMemoryAlloc/"]
RUN dotnet restore "StringBuilderMemoryAlloc/StringBuilderMemoryAlloc.csproj"
COPY . .
WORKDIR "/src/StringBuilderMemoryAlloc"
RUN dotnet build "StringBuilderMemoryAlloc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StringBuilderMemoryAlloc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StringBuilderMemoryAlloc.dll"]