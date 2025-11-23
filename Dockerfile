# Étape 1: Build de l'application .NET avec Angular
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Installer Node.js dans l'image .NET SDK
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_22.x | bash - && \
    apt-get install -y nodejs && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

# Copier et construire l'application Angular
WORKDIR /app/ClientApp
COPY ClientApp/package*.json ./
RUN npm ci
COPY ClientApp/ ./
RUN npm run build

# Copier le fichier csproj et restaurer les dépendances
WORKDIR /src
COPY ["DotNetParis.csproj", "./"]
RUN dotnet restore "DotNetParis.csproj"

# Copier le reste des fichiers et construire l'application
COPY . .
RUN dotnet build "DotNetParis.csproj" -c Release -o /app/build

# Publier l'application
FROM build AS publish
RUN dotnet publish "DotNetParis.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Étape 2: Image finale pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

# Copier les fichiers publiés depuis l'étape de publication
COPY --from=publish /app/publish .

# Copier les fichiers Angular buildés depuis l'étape de build
COPY --from=build /app/ClientApp/dist ./ClientApp/dist

# Exposer le port de l'application
EXPOSE 5151

# Définir les variables d'environnement
ENV ASPNETCORE_URLS=http://+:5151
ENV ASPNETCORE_ENVIRONMENT=Production

# Démarrer l'application
ENTRYPOINT ["dotnet", "DotNetParis.dll"]
