using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DocCare_Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocCare_Backend.Controllers
{
    [ApiController]
    [Route("api/Consultation")]
    public class ConsultationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consultations
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> Index()
        {
              return _context.Consultations != null ? 
                          View(await _context.Consultations.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Consultations'  is null.");
        }

        // GET: Consultations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consultations == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }









        // POST: Consultations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("Nom,Prenom,Date,Time,Status,DossierMedical")] Consultation consultation)
        {
            try
            {
                var form = await Request.ReadFormAsync();

                // Vérifier si le patient existe déjà dans la base de données
                var existingPatient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.Nom == consultation.Nom && p.Prenom == consultation.Prenom);

                if (existingPatient == null)
                {
                    // Le patient n'existe pas, créer un nouveau patient
                    var newPatient = new Patient
                    {
                        Nom = consultation.Nom,
                        Prenom = consultation.Prenom,
                        DateN = form["DateN"], // Assigner la valeur du champ DateN depuis le formulaire
                        Adresse = form["Adresse"], // Assigner la valeur du champ Adresse depuis le formulaire
                        Num = form["Num"], // Assigner la valeur du champ Num depuis le formulaire
                                           // Autres champs du patient à assigner ici
                    };

                    // Vérifier si le dossier médical est présent dans la consultation
                    if (consultation.DossierMedical != null && consultation.DossierMedical.Length > 0)
                    {
                        // Patient nouveau, dossier médical doit être null
                       // newPatient.DossierMedical = null;
                    }

                    // Associer les informations du patient à la consultation
                    consultation.Nom = newPatient.Nom;
                    consultation.Prenom = newPatient.Prenom;
                    consultation.Date = form["Date"]; // Assigner la valeur du champ Date depuis le formulaire
                    consultation.Time = form["Time"]; // Assigner la valeur du champ Time depuis le formulaire
                    consultation.Status = form["Status"]; // Assigner la valeur du champ Status depuis le formulaire
                   // consultation.DossierMedical = newPatient.DossierMedical;

                    // Ajouter la consultation à la base de données
                    _context.Add(consultation);
                }
                else
                {
                    // Le patient existe déjà, assigner les informations du patient à la consultation
                    consultation.Nom = existingPatient.Nom;
                    consultation.Prenom = existingPatient.Prenom;
                    consultation.Date = form["Date"]; // Assigner la valeur du champ Date depuis le formulaire
                    consultation.Time = form["Time"]; // Assigner la valeur du champ Time depuis le formulaire
                    consultation.Status = form["Status"]; // Assigner la valeur du champ Status depuis le formulaire

                    // Vérifier si la consultation contient un nouveau dossier médical
                    if (consultation.DossierMedical != null && consultation.DossierMedical.Length > 0)
                    {
                        // Combiner les fichiers en ajoutant les nouveaux au fichier existant
                       // var existingMedicalFile = existingPatient.DossierMedical;
                       // var updatedMedicalFile = CombineFiles(existingMedicalFile, consultation.DossierMedical);

                        // Mettre à jour le dossier médical du patient existant
                       // existingPatient.DossierMedical = updatedMedicalFile;
                    }

                    // Ajouter la consultation à la base de données
                    _context.Add(consultation);
                }

                await _context.SaveChangesAsync();

                // Consultation enregistrée avec succès, retourner un message JSON
                return Ok(new { message = "Consultation enregistrée avec succès." });
            }
            catch (Exception ex)
            {
                // En cas d'erreur, retourner un message d'erreur
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }





        // Fonction pour combiner les fichiers en ajoutant les nouveaux au fichier existant
        private byte[] CombineFiles(byte[] existingFile, byte[] newFile)
        {
            // Implémentez la logique pour combiner les fichiers
            // Par exemple, concaténer les données ou créer un fichier ZIP avec les deux
            // Assurez-vous de gérer les cas particuliers, tels que si le fichier existant est null

            // Exemple de pseudo-code pour concaténer les données
            if (existingFile == null)
            {
                return newFile; // Aucun fichier existant, retourner simplement le nouveau fichier
            }
            else
            {
                // Concaténer les données des deux fichiers (vous devez implémenter la logique appropriée)
                // Par exemple :
                var combinedFile = existingFile.Concat(newFile).ToArray();
                return combinedFile;
            }
        }















        // GET: Consultations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consultations == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }
            return View(consultation);
        }

        // POST: Consultations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Date,Time,Status,DossierMedical")] Consultation consultation)
        {
            if (id != consultation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultationExists(consultation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(consultation);
        }

        // GET: Consultations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consultations == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // POST: Consultations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consultations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Consultations'  is null.");
            }
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation != null)
            {
                _context.Consultations.Remove(consultation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultationExists(int id)
        {
          return (_context.Consultations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
