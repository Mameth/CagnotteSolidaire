# Cagnotte Solidaire

Projet réalisé dans le cadre du cours **Applications n-tiers**.

La solution met en œuvre une application web en **architecture 3-tiers** avec :
- une couche métier basée sur le **DDD** et **CQRS (MediatR)**,
- une couche données avec **Entity Framework Core**,
- une **API REST sécurisée par JWT**,
- une interface **Blazor**.

---

## État du projet

- ✅ **API REST fonctionnelle**  
  L’ensemble des fonctionnalités (authentification, gestion des utilisateurs, cagnottes, participations) a été testé avec succès via **Swagger**.

- ⚠️ **Interface Blazor partiellement fonctionnelle**  
  Les écrans principaux et la logique applicative sont implémentés.  
  Toutefois, des problèmes de configuration liés à l’authentification côté Blazor Server n’ont pas pu être entièrement stabilisés dans le temps imparti.

Le cœur du projet (métier + API) est pleinement opérationnel et conforme aux objectifs pédagogiques du cours.

---

## Lancer le projet

### Prérequis
- .NET 8
- SQL Server (LocalDB recommandé)

### 1. Base de données
La base de données est créée automatiquement au démarrage de l’API via Entity Framework Core  
(connection string définie dans `appsettings.json`).

### 2. Lancer l’API
```bash
cd CagnotteSolidaire.API
dotnet run
```

API : http://localhost:5009

Swagger : http://localhost:5009/swagger

### 2. Lancer l’API
 ``` bash
cd CagnotteSolidaire.Blazor
dotnet run
```