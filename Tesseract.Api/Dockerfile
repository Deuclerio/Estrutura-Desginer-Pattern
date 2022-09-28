FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

RUN apt update && apt install tesseract-ocr libtesseract-dev -y

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
#COPY ["OCR_API/OCR_API.WebApi/NuGet.Config", "OCR_API/OCR_API/OCR_API.WebApi/NuGet.Config"]
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
#COPY ./CounterPage/build ./wwwroot

RUN useradd -m myappuser
USER myappuser

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Tesseract.Api.dll