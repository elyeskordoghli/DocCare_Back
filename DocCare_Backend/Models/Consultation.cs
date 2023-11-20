﻿namespace DocCare_Backend.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required string Date { get; set; }
        public required string Time { get; set; }
        public required string Status { get; set; }
        public byte[]? DossierMedical { get; set; }
        public virtual required Patient Patient { get; set; } // Propriété de navigation vers l'objet Patient

    }
}