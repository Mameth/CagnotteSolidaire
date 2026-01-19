namespace CagnotteSolidaire.Blazor.Models;

public class InscriptionGestionnaireModel
{
    public string Nom { get; set; } = "";
    public string Prenom { get; set; } = "";
    public string Email { get; set; } = "";

    public Guid AssociationId { get; set; }
    public string AssociationNom { get; set; } = "";
    public string AssociationRna { get; set; } = "";
}
