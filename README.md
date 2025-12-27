# Cagnotte Solidaire

Projet de développement web **.NET 3-tiers** réalisé dans le cadre du cours  
**[IR 3A] Applications n-tiers**.

---

## 🎯 Objectif du projet

**Cagnotte Solidaire** est une application web permettant à des associations
de créer des cagnottes en ligne afin de financer leurs projets, et à des
participants de contribuer sous forme d’intentions de don.

Le projet met en œuvre les concepts vus en cours et en TPs :
- Architecture **3-tiers**
- **DDD** (Domain-Driven Design)
- **CQRS** avec MediatR
- **API REST .NET**
- **Blazor**
- **Entity Framework Core**
- Tests de la couche métier

---

## 👥 Types d’utilisateurs

- **Gestionnaire (Association)**
  - Inscription via l’API publique du Journal Officiel des Associations
  - Création et gestion de cagnottes
  - Consultation de la progression des cagnottes
  - Clôture ou annulation d’une cagnotte (avec notification des participants)

- **Participant**
  - Authentification
  - Participation à une cagnotte via son identifiant
  - Intention de don (sans paiement réel)

---

## 🧱 Architecture

Le projet respecte une **architecture 3-tiers découplée** :

CagnotteSolidaire
├── CagnotteSolidaire.Domain // Domaine (DDD)
├── CagnotteSolidaire.Application // Logique métier (CQRS, MediatR)
├── CagnotteSolidaire.Infrastructure // Accès aux données (EF Core, Repositories)
├── CagnotteSolidaire.API // API REST
├── CagnotteSolidaire.Blazor // Interface utilisateur
├── CagnotteSolidaire.Tests // Tests de la couche métier


### Détails des couches

- **Domain**
  - Entités, Value Objects, exceptions métier
  - Indépendant de toute technologie

- **Application**
  - Commands & Queries (CQRS)
  - Handlers MediatR
  - Interfaces des repositories
  - Logique métier et règles fonctionnelles

- **Infrastructure**
  - Implémentations EF Core
  - DbContext
  - Accès à l’API du Journal Officiel des Associations

- **API**
  - Exposition REST de la couche métier
  - Authentification JWT
  - Sécurisation des endpoints par rôles

- **Blazor**
  - Interface web
  - Consommation de l’API REST
  - Interfaces distinctes selon le rôle (Participant / Gestionnaire)

- **Tests**
  - Tests d’acceptation de la couche métier
  - Ciblent les handlers MediatR
  - Repositories mockés

---

## 🛠️ Stack technique

- **.NET 8**
- **ASP.NET Core Web API**
- **Blazor**
- **Entity Framework Core (Code First)**
- **SQL Server** (recommandé)
- **MediatR**
- **JWT** pour l’authentification
- **xUnit / NUnit / Moq** (tests)

---

## ▶️ Lancer le projet (état actuel)

> ⚠️ Le projet est en cours de développement.

### Prérequis
- .NET SDK **8.x**
- SQL Server (local ou distant)

### Démarrage
1. Cloner le dépôt
2. Ouvrir `CagnotteSolidaire.sln` dans Visual Studio
3. Lancer :
   - `CagnotteSolidaire.API`
   - `CagnotteSolidaire.Blazor`

---

## 🧪 Tests

Les tests portent sur la **couche Application** :
- Commands et Queries MediatR
- Repositories mockés
- Validation des règles métier

---

## 📌 État du projet

- Structure globale mise en place
- Architecture validée
- Passage complet en **.NET 8**
- Nettoyage du dépôt Git (`bin/` et `obj/` exclus)
- Développement fonctionnel en cours

---

## 👩‍💻 Travail en binôme

Le développement est réalisé en binôme avec une organisation par
fonctionnalités et par branches Git, à partir d’un `main` stable.

---

## 📄 Licence

Projet académique – usage pédagogique.
