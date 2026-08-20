# Stage 1: Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Stage 2: SDK for building
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Directory.Packages.props", "."]
# Copy csproj files and restore dependencies
# This layer is cached unless your dependencies change
COPY ["Portfolio.Webapp/Portfolio.Webapp.csproj", "Portfolio.Webapp/"]
COPY ["Portfolio.Core/Portfolio.Core.csproj", "Portfolio.Core/"]
RUN dotnet restore "Portfolio.Webapp/Portfolio.Webapp.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/Portfolio.Webapp"
RUN dotnet build "Portfolio.Webapp.csproj" -c Release -o /app/build

# Stage 3: Publish the app
FROM build AS publish
RUN dotnet publish "Portfolio.Webapp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.Webapp.dll"]