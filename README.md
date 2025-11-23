# DotNetParis API

Ce projet est une application web API .NET 8.0 conçue pour démontrer des principes comme OCP (Open/Closed Principle) et LSP (Liskov Substitution Principle). Il inclut des endpoints pour gérer des produits et des prévisions météo.

> **Note** : Cette application est développée en parallèle de la lecture du livre "ASP.NET avec C# sous Visual Studio 2019" (Éditions ENI). La documentation et les commits sont en français, tandis que le code est en anglais.

## 🐳 Exécution avec Docker (Recommandé)

### Prérequis

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Démarrage rapide

Pour démarrer l'application avec Docker Compose :

```bash
docker compose up -d
```

Pour reconstruire l'image et démarrer :

```bash
docker compose up -d --build
```

Pour arrêter l'application :

```bash
docker compose down
```

### Accéder à l'API

Une fois l'application démarrée, vous pouvez accéder à :

- **Swagger UI** : [http://localhost:5153/swagger/index.html](http://localhost:5153/swagger/index.html)
- **API** : [http://localhost:5153](http://localhost:5153)

L'API est exposée sur le port `5151` dans le conteneur, qui est mappé au port `5153` sur la machine hôte.

## 💻 Exécution en local (Développement)

### Prérequis

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 20+](https://nodejs.org/) (pour l'application Angular)
- [Angular CLI](https://angular.io/cli) (installation globale)

```bash
npm install -g @angular/cli
```

### Commandes pour exécuter l'application

L'application doit écouter sur toutes les interfaces (`0.0.0.0`) pour fonctionner correctement dans Docker :

```bash
dotnet run --urls="http://0.0.0.0:5151"
dotnet watch --urls="http://0.0.0.0:5151"
```

En développement local (hors Docker), vous pouvez aussi utiliser :

```bash
dotnet run --urls="http://localhost:5151"
dotnet watch --urls="http://localhost:5151"
```

## ✨ Fonctionnalités

- **Gestion des produits** : Endpoints pour créer, récupérer, mettre à jour et supprimer des produits, incluant le filtrage par `PublicProduct` et `PrivateProduct`
- **Prévisions météo** : Un endpoint simple pour récupérer des prévisions météo avec des données aléatoires
- **Swagger/OpenAPI** : Interface Swagger UI intégrée pour l'exploration et les tests de l'API
- **Architecture multi-couches** : Séparation claire entre Controllers, Services, Repositories et Models
- **Conteneurisation** : Application entièrement dockerisée avec support Angular

## 🏗️ Structure du projet

```
dotnet-paris/
├── Controllers/           # Contrôleurs API (ProductController, WeatherController)
├── Models/               # Modèles de données (Product, PublicProduct, PrivateProduct, Weather)
├── Services/             # Couche de logique métier (ProductService)
├── Repositories/         # Couche d'accès aux données (ProductRepository)
├── Data/                 # Contexte de base de données en mémoire (ApplicationDbContext)
├── ClientApp/            # Application Angular (front-end)
├── docs/                 # Documentation (démos OCP/LSP)
├── Dockerfile            # Configuration Docker multi-stage
├── docker-compose.yml    # Orchestration des conteneurs
└── .dockerignore         # Fichiers exclus du build Docker
```

## 📚 Principes démontrés

### Open/Closed Principle (OCP)

L'application démontre l'OCP en permettant l'ajout de nouvelles fonctionnalités (par exemple, le filtrage des produits par type) sans modifier le code existant. Pour plus de détails, consultez [docs/01_ocp_lsp_demos.md](docs/01_ocp_lsp_demos.md).

### Liskov Substitution Principle (LSP)

L'application respecte le LSP en garantissant que les sous-classes (`PublicProduct` et `PrivateProduct`) peuvent remplacer leur classe de base (`Product`) sans affecter la correction du programme.

## 🔧 Configuration Docker

### Architecture du Dockerfile

Le `Dockerfile` utilise une approche multi-stage pour optimiser la taille de l'image finale :

1. **Stage 1 (angular-build)** : Construction de l'application Angular avec Node.js 20
2. **Stage 2 (build)** : Restauration des dépendances et compilation de l'application .NET
3. **Stage 3 (publish)** : Publication de l'application en mode Release
4. **Stage 4 (final)** : Image runtime légère avec ASP.NET Core 8.0

### Variables d'environnement

Les variables suivantes sont configurables dans le `docker-compose.yml` :

- `ASPNETCORE_ENVIRONMENT` : Environnement d'exécution (Development/Production)
- `ASPNETCORE_URLS` : URLs d'écoute de l'application

### Ports

- **Port du conteneur** : 5151
- **Port de l'hôte** : 5153

## 🛠️ Développement

### Mode développement avec Docker

### Mode développement avec Docker

Pour un développement avec rechargement automatique :

```bash
# Démarrer en mode développement
docker compose up

# Voir les logs en temps réel
docker compose logs -f dotnet-paris-api
```

### Mode développement local

Pour développer sans Docker :

```bash
# Restaurer les dépendances
dotnet restore

# Exécuter l'application en mode watch
dotnet watch run --urls="http://localhost:5151"
```

### Installation des dépendances Angular

```bash
cd ClientApp
npm install
npm start
```

### Configuration Swagger

Swagger est activé en environnement de développement. Pour explorer l'API, naviguez vers l'interface Swagger à l'URL mentionnée ci-dessus.

## 🐛 Débogage avec VS Code

### Prérequis pour le débogage

Assurez-vous d'avoir installé les extensions VS Code recommandées :

- **C# Dev Kit** (`ms-dotnettools.csdevkit`)
- **C#** (`ms-dotnettools.csharp`)
- **Docker** (`ms-azuretools.vscode-docker`)

### Options de débogage disponibles

Le projet offre trois configurations de débogage :

#### 1. `.NET Core Launch (web)` - Débogage local (Recommandé)

Lance l'application directement sans Docker. C'est la méthode la plus simple et rapide.

**Comment l'utiliser :**

1. Appuyez sur `F5` ou allez dans l'onglet "Run and Debug"
2. Sélectionnez "`.NET Core Launch (web)`"
3. L'application démarre et le navigateur s'ouvre automatiquement sur Swagger

**Avantages :**

- Démarrage rapide
- Rechargement automatique avec hot reload
- Points d'arrêt fonctionnels

#### 2. `Docker .NET Launch` - Débogage dans Docker

Lance l'application dans un conteneur Docker avec support de débogage.

**Comment l'utiliser :**

1. Sélectionnez "`Docker .NET Launch`" dans les configurations de débogage
2. Appuyez sur `F5`
3. VS Code construit l'image et attache le débogueur

**Avantages :**

- Environnement identique à la production
- Teste la conteneurisation

#### 3. `.NET Core Attach` - Attacher à un processus

Permet de s'attacher à un processus .NET en cours d'exécution.

**Comment l'utiliser :**

1. Démarrez l'application (avec `dotnet run` ou Docker)
2. Sélectionnez "`.NET Core Attach`"
3. Choisissez le processus `DotNetParis` dans la liste

### Points d'arrêt

Pour ajouter un point d'arrêt :

1. Cliquez dans la marge gauche d'une ligne de code (un point rouge apparaît)
2. Lancez le débogage avec `F5`
3. L'exécution s'arrêtera sur cette ligne

### Raccourcis clavier de débogage

- `F5` : Démarrer/Continuer le débogage
- `F9` : Ajouter/Retirer un point d'arrêt
- `F10` : Passer à l'instruction suivante (step over)
- `F11` : Entrer dans la fonction (step into)
- `Shift+F11` : Sortir de la fonction (step out)
- `Shift+F5` : Arrêter le débogage

## 🐞 Commandes utiles

### Docker

```bash
# Construire l'image sans cache
docker compose build --no-cache

# Voir les conteneurs en cours d'exécution
docker ps

# Accéder au shell du conteneur
docker exec -it dotnet-paris-api /bin/bash

# Nettoyer les conteneurs et volumes
docker compose down -v

# Voir l'utilisation des ressources
docker stats dotnet-paris-api
```

### .NET

```bash
# Restaurer les packages
dotnet restore

# Compiler le projet
dotnet build

# Exécuter les tests (si disponibles)
dotnet test

# Publier l'application
dotnet publish -c Release -o ./publish
```

## 📖 Référence du livre

Ce projet suit le livre **"ASP.NET avec C# sous Visual Studio 2019 - Conception et développement d'applications web"** par Brice-Arnaud GUÉRIN (Éditions ENI).

### Table des matières suivie

Chapitres couverts dans ce projet :

1. Nouveautés de Visual Studio 2019
2. La page de démarrage
3. Les différentes solutions de développement
   - [Et plus encore...]

## 📝 Licence

Ce projet est sous licence MIT. Voir le fichier `LICENSE` pour plus de détails.
