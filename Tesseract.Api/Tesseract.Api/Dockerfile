#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt update && apt install tesseract-ocr libtesseract-dev -y

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Tesseract.Api/Tesseract.Api.csproj", "Tesseract.Api/"]
RUN dotnet restore "Tesseract.Api/Tesseract.Api.csproj"
COPY . .
WORKDIR "/src/Tesseract.Api"
RUN dotnet build "Tesseract.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tesseract.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tesseract.Api.dll"]