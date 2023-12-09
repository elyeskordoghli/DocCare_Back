namespace DocCare_Backend.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required string Date { get; set; }
        public required string Time { get; set; }
        public string? Status { get; set; } = "hide"; // Déclaration de la propriété Status avec une valeur par défaut
        public byte[]? DossierMedical { get; set; }
        public int PatientId { get; set; } // ID du patient lié à cette consultation
        public virtual Patient? Patient { get; set; } // Propriété de navigation vers l'objet Patient

    }
}
