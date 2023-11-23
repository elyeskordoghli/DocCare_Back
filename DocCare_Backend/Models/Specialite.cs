namespace DocCare_Backend.Models
{
    public class Specialite
    {
        public int SpecialiteId { get; set; }
        public string? Nom { get; set; }
        public string? ImagePath { get; set; } // Chemin de l'image

        // Propri�t� de navigation inverse
        public ICollection<Docteur>? Docteurs { get; set; }


    }
}
