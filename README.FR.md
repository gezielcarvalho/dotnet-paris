# API DotNetParis

Ce projet est une application web API .NET 8.0 conçue pour démontrer des principes comme le OCP (Principe Ouvert/Fermé) et le LSP (Principe de Substitution de Liskov). Elle inclut des points de terminaison pour la gestion des produits et des prévisions météorologiques.

## Exécution de l'Application

L'application s'exécute dans un conteneur Docker, et les URLs doivent écouter sur toutes les interfaces. Cela est réalisé en configurant le paramètre `urls` dans la commande `dotnet run`. L'application utilise `0.0.0.0` au lieu de `localhost` pour se lier à toutes les interfaces.

### Commandes pour Exécuter l'Application

Utilisez les commandes suivantes pour démarrer l'application :

```bash
dotnet run --urls="http://0.0.0.0:5151"
dotnet watch --urls="http://0.0.0.0:5151"
```

### Accès à l'API

L'API est exposée sur le port `5151` à l'intérieur du conteneur, qui est mappé au port `5153` sur la machine hôte. Vous pouvez accéder à la documentation Swagger de l'API à l'adresse suivante :

[http://localhost:5153/swagger/index.html](http://localhost:5153/swagger/index.html)

## Fonctionnalités

- **Gestion des Produits** : Points de terminaison pour créer, récupérer, mettre à jour et supprimer des produits, y compris le filtrage par `PublicProduct` et `PrivateProduct`.
- **Prévisions Météorologiques** : Un point de terminaison simple pour récupérer des prévisions météorologiques avec des données aléatoires.
- **Swagger/OpenAPI** : Interface Swagger intégrée pour explorer et tester l'API.

## Environnement de Développement

### Prérequis

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/)
- [Visual Studio Code](https://code.visualstudio.com/) (optionnel, pour le développement)

### Exécution en Mode Développement

L'application est configurée pour utiliser l'environnement `Development` par défaut. Vous pouvez modifier les paramètres de l'environnement dans le fichier `Properties/launchSettings.json`.

### Configuration de Swagger

Swagger est activé dans l'environnement de développement. Pour explorer l'API, accédez à l'interface Swagger à l'URL mentionnée ci-dessus.

## Structure du Projet

- **Controllers** : Contient les contrôleurs API comme `ProductController` et `WeatherController`.
- **Models** : Définit les modèles de données tels que `Product`, `PublicProduct`, `PrivateProduct` et `Weather`.
- **Services** : Couche de logique métier, incluant `ProductService`.
- **Repositories** : Couche d'accès aux données, incluant `ProductRepository`.
- **Data** : Contient le `ApplicationDbContext` en mémoire.

## Principes Clés Démontrés

### Principe Ouvert/Fermé (OCP)

L'application démontre le OCP en permettant d'ajouter de nouvelles fonctionnalités (par exemple, le filtrage des produits par type) sans modifier le code existant. Pour plus de détails, consultez [docs/01_ocp_lsp_demos.md](docs/01_ocp_lsp_demos.md).

### Principe de Substitution de Liskov (LSP)

L'application respecte le LSP en garantissant que les sous-classes (`PublicProduct` et `PrivateProduct`) peuvent remplacer leur classe de base (`Product`) sans affecter la correction du programme.

## Licence

Ce projet est sous licence MIT. Consultez le fichier `LICENSE` pour plus de détails.