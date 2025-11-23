# Étape 1: Build de l'application Angular
FROM node:20-alpine AS angular-build

WORKDIR /app/ClientApp

# Copier les fichiers package.json et installer les dépendances
COPY ClientApp/package*.json ./
RUN npm ci

# Copier le reste des fichiers Angular et construire
COPY ClientApp/ ./
RUN npm run build

# Étape 2: Build de l'application .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copier le fichier csproj et restaurer les dépendances
COPY ["DotNetParis.csproj", "./"]
RUN dotnet restore "DotNetParis.csproj"

# Copier le reste des fichiers et construire l'application
COPY . .
RUN dotnet build "DotNetParis.csproj" -c Release -o /app/build

# Publier l'application
FROM build AS publish
RUN dotnet publish "DotNetParis.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Étape 3: Image finale pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

# Copier les fichiers publiés depuis l'étape de publication
COPY --from=publish /app/publish .

# Copier les fichiers Angular buildés (si nécessaire pour le front-end)
COPY --from=angular-build /app/ClientApp/dist ./ClientApp/dist

# Exposer le port de l'application
EXPOSE 5151

# Définir les variables d'environnement
ENV ASPNETCORE_URLS=http://+:5151
ENV ASPNETCORE_ENVIRONMENT=Production

# Démarrer l'application
ENTRYPOINT ["dotnet", "DotNetParis.dll"]
