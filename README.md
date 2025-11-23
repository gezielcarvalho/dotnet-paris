# DotNetParis API - Magasin Virtuel

Ce projet est une application web API .NET 8.0 conçue pour démontrer **tous les principes SOLID** à travers la création d'un **magasin virtuel**. L'application implémente un système complet de e-commerce avec gestion de produits, panier d'achat, commandes, et paiements.

> **Note** : Cette application est développée en parallèle de la lecture du livre "ASP.NET avec C# sous Visual Studio 2019" (Éditions ENI). La documentation et les commits sont en français, tandis que le code est en anglais.

## 🎯 Objectif pédagogique

Ce projet illustre concrètement les **cinq principes SOLID** dans le contexte d'un magasin virtuel :

- **S** - Single Responsibility Principle (SRP)
- **O** - Open/Closed Principle (OCP)
- **L** - Liskov Substitution Principle (LSP)
- **I** - Interface Segregation Principle (ISP)
- **D** - Dependency Inversion Principle (DIP)

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

## ✨ Fonctionnalités du magasin virtuel

### Gestion des produits

- Catalogue de produits avec catégories
- Recherche et filtrage de produits
- Gestion du stock et des prix
- Produits publics et privés (membres premium)

### Panier d'achat

- Ajout/suppression d'articles
- Calcul automatique des totaux
- Application de remises et promotions
- Persistance du panier

### Système de commandes

- Création et suivi de commandes
- Historique des achats
- Statuts de commande (en attente, traitée, expédiée, livrée)
- Notifications par email

### Paiements

- Support de multiples méthodes de paiement
- Validation de paiement
- Gestion des remboursements
- Intégration avec passerelles de paiement

### Fonctionnalités techniques

- **API RESTful** avec documentation Swagger/OpenAPI
- **Architecture multi-couches** : Controllers, Services, Repositories, Models
- **Conteneurisation** complète avec Docker
- **Base de données** en mémoire (Entity Framework Core)
- **Client Angular** pour l'interface utilisateur

## 🏗️ Structure du projet

```
dotnet-paris/
├── Controllers/           # Contrôleurs API (Products, Orders, Cart, Payments)
├── Models/               # Modèles de domaine
│   ├── Products/         # Product, PublicProduct, PrivateProduct
│   ├── Orders/           # Order, OrderItem, OrderStatus
│   ├── Cart/             # ShoppingCart, CartItem
│   └── Payments/         # Payment, PaymentMethod, Transaction
├── Services/             # Couche de logique métier
│   ├── ProductService
│   ├── OrderService
│   ├── CartService
│   ├── PaymentService
│   └── Interfaces/       # Contrats de services (ISP, DIP)
├── Repositories/         # Couche d'accès aux données
│   ├── ProductRepository
│   ├── OrderRepository
│   └── Interfaces/       # Contrats de repositories (DIP)
├── Data/                 # Contexte de base de données
├── Validators/           # Validation métier (SRP)
├── Notifications/        # Service de notifications (SRP)
├── Pricing/              # Stratégies de pricing (OCP)
├── ClientApp/            # Application Angular (front-end)
├── docs/                 # Documentation des principes SOLID
│   ├── 01_SRP_examples.md
│   ├── 02_OCP_examples.md
│   ├── 03_LSP_examples.md
│   ├── 04_ISP_examples.md
│   └── 05_DIP_examples.md
├── Dockerfile            # Configuration Docker multi-stage
├── docker-compose.yml    # Orchestration des conteneurs
└── .dockerignore         # Fichiers exclus du build Docker
```

## 📚 Principes SOLID démontrés

### 🔹 S - Single Responsibility Principle (SRP)

**Principe** : Une classe ne devrait avoir qu'une seule raison de changer.

**Exemples dans le projet :**

- `OrderValidator` : responsable uniquement de la validation des commandes
- `EmailNotificationService` : responsable uniquement de l'envoi d'emails
- `PriceCalculator` : responsable uniquement du calcul des prix
- `StockManager` : responsable uniquement de la gestion du stock

**Avantages** : Code plus maintenable, testable et réutilisable.

### 🔹 O - Open/Closed Principle (OCP)

**Principe** : Les entités logicielles doivent être ouvertes à l'extension mais fermées à la modification.

**Exemples dans le projet :**

- **Stratégies de pricing** : `IPricingStrategy`, `RegularPricing`, `PromotionalPricing`, `SeasonalPricing`
- **Méthodes de paiement** : `IPaymentMethod`, `CreditCardPayment`, `PayPalPayment`, `CryptoPayment`
- **Calculateurs de remise** : `IDiscountCalculator`, `PercentageDiscount`, `FixedAmountDiscount`, `BuyOneGetOneDiscount`
- **Filtres de produits** : Extension du système de filtrage sans modifier le code existant

**Avantages** : Ajout de nouvelles fonctionnalités sans risquer de casser l'existant.

### 🔹 L - Liskov Substitution Principle (LSP)

**Principe** : Les objets d'une classe dérivée doivent pouvoir remplacer les objets de la classe de base sans altérer le comportement du programme.

**Exemples dans le projet :**

- `Product` (classe de base) → `PublicProduct`, `PrivateProduct`, `DigitalProduct`
- `PaymentMethod` (classe de base) → `CreditCard`, `DebitCard`, `PrepaidCard`
- `ShippingMethod` → `StandardShipping`, `ExpressShipping`, `InternationalShipping`

**Garanties** :

- Toutes les sous-classes respectent le contrat de la classe de base
- Les méthodes substituées ne lancent pas d'exceptions inattendues
- Les pré-conditions ne sont pas renforcées
- Les post-conditions ne sont pas affaiblies

**Avantages** : Polymorphisme fiable et prévisible.

### 🔹 I - Interface Segregation Principle (ISP)

**Principe** : Les clients ne devraient pas être forcés de dépendre d'interfaces qu'ils n'utilisent pas.

**Exemples dans le projet :**

Au lieu d'une seule interface monolithique `IProductRepository` avec toutes les méthodes :

```csharp
// ❌ Mauvais : Interface trop large
public interface IProductRepository
{
    Task<Product> GetById(int id);
    Task<List<Product>> GetAll();
    Task Create(Product product);
    Task Update(Product product);
    Task Delete(int id);
    Task<List<Product>> Search(string query);
    Task<List<Product>> GetByCategory(int categoryId);
    Task UpdateStock(int productId, int quantity);
    Task<decimal> GetAveragePrice();
}
```

On utilise des interfaces ségrégées :

```csharp
// ✅ Bon : Interfaces séparées selon les besoins
public interface IProductReader
{
    Task<Product> GetById(int id);
    Task<List<Product>> GetAll();
}

public interface IProductWriter
{
    Task Create(Product product);
    Task Update(Product product);
    Task Delete(int id);
}

public interface IProductSearchable
{
    Task<List<Product>> Search(string query);
    Task<List<Product>> GetByCategory(int categoryId);
}

public interface IStockManager
{
    Task UpdateStock(int productId, int quantity);
}
```

**Autres exemples** :

- `IOrderReader` vs `IOrderWriter` vs `IOrderProcessor`
- `ICartReader` vs `ICartModifier`
- `IPaymentProcessor` vs `IPaymentValidator` vs `IRefundHandler`

**Avantages** : Classes clientes plus légères, dépendances minimales, meilleure testabilité.

### 🔹 D - Dependency Inversion Principle (DIP)

**Principe** : Les modules de haut niveau ne doivent pas dépendre des modules de bas niveau. Les deux doivent dépendre d'abstractions.

**Exemples dans le projet :**

```csharp
// Les services dépendent d'abstractions, pas d'implémentations concrètes
public class OrderService
{
    private readonly IOrderRepository _orderRepository;      // Abstraction
    private readonly IPaymentProcessor _paymentProcessor;    // Abstraction
    private readonly INotificationService _notificationService; // Abstraction

    public OrderService(
        IOrderRepository orderRepository,
        IPaymentProcessor paymentProcessor,
        INotificationService notificationService)
    {
        _orderRepository = orderRepository;
        _paymentProcessor = paymentProcessor;
        _notificationService = notificationService;
    }
}
```

**Configuration dans `Program.cs`** :

```csharp
// Injection de dépendances - les implémentations concrètes sont injectées
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentProcessor, StripePaymentProcessor>();
builder.Services.AddScoped<INotificationService, EmailNotificationService>();
builder.Services.AddScoped<OrderService>();
```

**Avantages** :

- Facilite les tests unitaires (mock des dépendances)
- Permet de changer d'implémentation sans modifier le code
- Réduit le couplage entre les composants
- Améliore la flexibilité et la maintenabilité

### 📖 Documentation détaillée

Pour des exemples de code complets et des explications approfondies, consultez la documentation dans le dossier `docs/` :

- [01_SRP_examples.md](docs/01_SRP_examples.md) - Single Responsibility Principle
- [02_OCP_examples.md](docs/02_OCP_examples.md) - Open/Closed Principle
- [03_LSP_examples.md](docs/03_LSP_examples.md) - Liskov Substitution Principle
- [04_ISP_examples.md](docs/04_ISP_examples.md) - Interface Segregation Principle
- [05_DIP_examples.md](docs/05_DIP_examples.md) - Dependency Inversion Principle

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

### Objectifs d'apprentissage

À travers le développement de ce **magasin virtuel**, vous apprendrez :

1. **Les principes SOLID** appliqués à un projet réel
2. **Architecture en couches** (Presentation, Business, Data)
3. **Patterns de conception** (Repository, Strategy, Factory, etc.)
4. **Injection de dépendances** et inversion de contrôle
5. **Tests unitaires** avec mocking des dépendances
6. **API RESTful** avec ASP.NET Core
7. **Entity Framework Core** pour l'accès aux données
8. **Docker** et conteneurisation
9. **Angular** pour le front-end
10. **CI/CD** et bonnes pratiques DevOps

### Progression du projet

Le projet est développé de manière incrémentale, en ajoutant progressivement des fonctionnalités tout en respectant les principes SOLID :

**Phase 1 : Fondations** (Chapitres 1-3)

- Configuration du projet ASP.NET Core
- Structure de base et architecture
- Premiers contrôleurs et modèles

**Phase 2 : Gestion des produits** (Chapitres 4-6)

- Implémentation du catalogue produits
- Démonstration de SRP et OCP
- Filtres et recherche extensibles

**Phase 3 : Panier et commandes** (Chapitres 7-9)

- Système de panier d'achat
- Gestion des commandes
- Démonstration de LSP et ISP

**Phase 4 : Paiements et notifications** (Chapitres 10-12)

- Intégration de paiements
- Service de notifications
- Démonstration de DIP

**Phase 5 : Tests et déploiement** (Chapitres 13-15)

- Tests unitaires et d'intégration
- Conteneurisation Docker
- Déploiement et monitoring

### Ressources supplémentaires

- **Documentation officielle** : [ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- **Principes SOLID** : [SOLID Principles Explained](https://stackify.com/solid-design-principles/)
- **Clean Architecture** : [Clean Architecture by Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

## 📝 Licence

Ce projet est sous licence MIT. Voir le fichier `LICENSE` pour plus de détails.
