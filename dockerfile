# ベースイメージを指定
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# SDKイメージを使用してアプリをビルド
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["keiba_Timer.csproj", "./"]
RUN dotnet restore "keiba_Timer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "keiba_Timer.csproj" -c Release -o /app/build

# リリース用にアプリを公開
FROM build AS publish
RUN dotnet publish "keiba_Timer.csproj" -c Release -o /app/publish

# ランタイムイメージを使ってアプリを実行
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "keiba_Timer.dll"]
