FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/TTS_boilerplate.Web.Mvc/TTS_boilerplate.Web.Mvc.csproj", "src/TTS_boilerplate.Web.Mvc/"]
COPY ["src/TTS_boilerplate.Web.Core/TTS_boilerplate.Web.Core.csproj", "src/TTS_boilerplate.Web.Core/"]
COPY ["src/TTS_boilerplate.Application/TTS_boilerplate.Application.csproj", "src/TTS_boilerplate.Application/"]
COPY ["src/TTS_boilerplate.Core/TTS_boilerplate.Core.csproj", "src/TTS_boilerplate.Core/"]
COPY ["src/TTS_boilerplate.EntityFrameworkCore/TTS_boilerplate.EntityFrameworkCore.csproj", "src/TTS_boilerplate.EntityFrameworkCore/"]
WORKDIR "/src/src/TTS_boilerplate.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/TTS_boilerplate.Web.Mvc", "src/TTS_boilerplate.Web.Mvc"]
COPY ["src/TTS_boilerplate.Web.Core", "src/TTS_boilerplate.Web.Core"]
COPY ["src/TTS_boilerplate.Application", "src/TTS_boilerplate.Application"]
COPY ["src/TTS_boilerplate.Core", "src/TTS_boilerplate.Core"]
COPY ["src/TTS_boilerplate.EntityFrameworkCore", "src/TTS_boilerplate.EntityFrameworkCore"]
WORKDIR "/src/src/TTS_boilerplate.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "TTS_boilerplate.Web.Mvc.dll"]
