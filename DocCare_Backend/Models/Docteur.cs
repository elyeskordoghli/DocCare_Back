using System.ComponentModel.DataAnnotations.Schema;

namespace DocCare_Backend.Models
{
    public class Docteur
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required string Adresse { get; set; }
        public required string NumeroTelephone { get; set; }
        public required string Photo { get; set; }
        public required string Genre { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? R_Token { get; set; }
        // Clé étrangère
        public int SpecialiteId { get; set; } // Changer int en int?
        public Specialite? Specialite { get; set; }

        public ICollection<DocteurDisponibilite>? DocteursDisponibilites { get; set; }

    }




}
