FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /Rollcall

COPY *.csproj ./
RUN dotnet restore

COPY ./ ./
RUN dotnet build -c Release -o ReleaseBuild

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /Rollcall
COPY --from=build-env /Rollcall/ReleaseBuild .
ENTRYPOINT ["dotnet","rollcall.dll"]
