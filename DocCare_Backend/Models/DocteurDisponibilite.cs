namespace DocCare_Backend.Models
{
    public class DocteurDisponibilite
    {
        public int DocteurId { get; set; }
        public Docteur? Docteur { get; set; }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
    }
}
