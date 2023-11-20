using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocCare_Backend.Models
{
    public class DossierMedical
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        // Clé étrangère vers Patient
        public int PatientId { get; set; }

        // Propriété de navigation vers le patient
        public  Patient Patient { get; set; }

        public static implicit operator string(DossierMedical v)
        {
            throw new NotImplementedException();
        }
    }
}
