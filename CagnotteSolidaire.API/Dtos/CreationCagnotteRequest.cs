namespace CagnotteSolidaire.API.Dtos;
public record CreationCagnotteRequest(
    string Nom,
    string Description,
    decimal Objectif
);