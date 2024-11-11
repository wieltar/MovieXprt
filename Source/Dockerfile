# Build stage
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /artifacts
EXPOSE 80
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o ../out
# publish out folder
FROM base AS final
WORKDIR /out
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Api.dll"]

# TODO split indexer as a sidecar container or as a background process