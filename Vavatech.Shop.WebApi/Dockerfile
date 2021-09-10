#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Vavatech.Shop.WebApi/Vavatech.Shop.WebApi.csproj", "Vavatech.Shop.WebApi/"]
COPY ["Vavatech.Shop.Fakers/Vavatech.Shop.Fakers.csproj", "Vavatech.Shop.Fakers/"]
COPY ["Vavatech.Shop.Models/Vavatech.Shop.Models.csproj", "Vavatech.Shop.Models/"]
COPY ["Vavatech.Shop.EFDbServices/Vavatech.Shop.EFDbServices.csproj", "Vavatech.Shop.EFDbServices/"]
COPY ["Vavatech.Shop.IServices/Vavatech.Shop.IServices.csproj", "Vavatech.Shop.IServices/"]
COPY ["Vavatech.Shop.Shared/Vavatech.Shop.Shared.csproj", "Vavatech.Shop.Shared/"]
COPY ["Vavatech.Shop.FakeServices/Vavatech.Shop.FakeServices.csproj", "Vavatech.Shop.FakeServices/"]
RUN dotnet restore "Vavatech.Shop.WebApi/Vavatech.Shop.WebApi.csproj"
COPY . .
WORKDIR "/src/Vavatech.Shop.WebApi"
RUN dotnet build "Vavatech.Shop.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vavatech.Shop.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vavatech.Shop.WebApi.dll"]