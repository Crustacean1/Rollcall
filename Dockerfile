FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dev
WORKDIR /Rollcall
RUN dotnet tool install -g dotnet-ef
RUN apt-get update --yes && apt-get install zsh --yes
ENV PATH $PATH:/root/.dotnet/tools
ENTRYPOINT ["zsh"]

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS prod-build
WORKDIR /Rollcall
COPY *.csproj ./
RUN dotnet restore
COPY ./ ./
RUN dotnet build -c Release -o ReleaseBuild

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /Rollcall
COPY --from=prod-build /Rollcall/ReleaseBuild .
ENTRYPOINT ["dotnet","rollcall.dll"]
