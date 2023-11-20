using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocCare_Backend.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required String Nom { get; set; }
        public required String Prenom { get; set; }
        public required String DateN { get; set; }
        public required String Adresse { get; set; }
        public required String Num { get; set; }
        public virtual ICollection<DossierMedical>? DossiersMedicaux { get; set; }


    }
}
