# Architecture

```text
Client Angular (squelette)
          |
          | intégration non démontrée
          v
API ASP.NET Core 8
  contrôleurs -> services -> repositories -> EF Core InMemory
          |
          +--> Swagger et conteneur Docker local
```

L'API contient un domaine produit limité, un repository et un service. Le client Angular est présent comme squelette, sans preuve d'un parcours e-commerce intégré. Docker Compose fournit un environnement local de démonstration.

Les descriptions du README principal concernant un magasin virtuel complet sont une intention pédagogique/feuille de route et non une représentation exhaustive du code actuel.
