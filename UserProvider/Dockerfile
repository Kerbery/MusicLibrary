#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UserProvider/UserProvider.csproj", "UserProvider/"]
RUN dotnet restore "UserProvider/UserProvider.csproj"
COPY . .
WORKDIR "/src/UserProvider"
RUN dotnet build "UserProvider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserProvider.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserProvider.dll"]