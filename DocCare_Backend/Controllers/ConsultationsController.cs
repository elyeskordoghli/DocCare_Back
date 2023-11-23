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
        public async Task<IActionResult> GetAllConsultations()
        {
            try
            {
                var consultations = await _context.Consultations.ToListAsync();

                if (consultations != null)
                {
                    return Json(new { data = consultations });
                }
                else
                {
                    return Problem("La liste des consultations est vide.");
                }
            }
            catch (Exception ex)
            {
                return Problem($"Une erreur s'est produite : {ex.Message}");
            }
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
        public async Task<IActionResult> Create([FromForm] Consultation consultation)
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
                        Nom = form["Nom"],
                        Prenom = form["prenom"],
                        DateN = form["DateN"], // Assigner la valeur du champ DateN depuis le formulaire
                        Adresse = form["Adresse"], // Assigner la valeur du champ Adresse depuis le formulaire
                        Num = form["Num"],
                                           // Autres champs du patient à assigner ici
                    };

                    // Vérifier si le dossier médical est présent dans la consultation
                    if (form.Files["DossierMedical"] != null)
                    {
                        var dossierMedicalFile = form.Files["DossierMedical"];
                        byte[]? dossierMedicalBytes = null;

                        using (var memoryStream = new MemoryStream())
                        {
                            // Copier le fichier médical dans un tableau de bytes
                            await dossierMedicalFile.CopyToAsync(memoryStream);
                            dossierMedicalBytes = memoryStream.ToArray();
                        }
                        newPatient.DossierMedical = dossierMedicalBytes;
                    }
                    _context.Add(newPatient);
                    await _context.SaveChangesAsync();

                    // Associer les informations du patient à la consultation
                    consultation.Nom = form["Nom"];
                    consultation.Prenom = form["prenom"];
                    consultation.Date = form["Date"]; // Assigner la valeur du champ Date depuis le formulaire
                    consultation.Time = form["Time"]; // Assigner la valeur du champ Time depuis le formulaire
                    consultation.Status = form["Status"]; // Assigner la valeur du champ Status depuis le formulaire
                    consultation.Patient = newPatient;
                    consultation.PatientId = newPatient.Id;
                    consultation.DossierMedical = newPatient.DossierMedical;

                    // Ajouter la consultation à la base de données
                    _context.Add(consultation);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    // Le patient existe déjà, assigner les informations du patient à la consultation
                    Console.WriteLine($"ID du patient existant : {existingPatient.Id}");

                    consultation.Patient = existingPatient;
                    consultation.Nom = form["Nom"];
                    consultation.Prenom = form["Prenom"];
                    consultation.Date = form["Date"]; // Assigner la valeur du champ Date depuis le formulaire
                    consultation.Time = form["Time"]; // Assigner la valeur du champ Time depuis le formulaire
                    consultation.Status = form["Status"]; // Assigner la valeur du champ Status depuis le formulaire
                    consultation.PatientId = existingPatient.Id;
                    consultation.DossierMedical = existingPatient.DossierMedical;
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
        [Route("EditConsultation/{id}")]
        public async Task<IActionResult> EditConsultationStatus(int id, [FromBody] string Status)
        {
            try
            {
                // Récupérer la consultation à partir de l'ID
                var consultation = await _context.Consultations.FindAsync(id);

                if (consultation == null)
                {
                    return NotFound("Consultation non trouvée");
                }

                // Mettre à jour le statut de la consultation avec le nouveau statut
                consultation.Status = Status;

                // Mettre à jour la consultation dans la base de données
                _context.Consultations.Update(consultation);
                await _context.SaveChangesAsync();

                return Ok("Statut de la consultation mis à jour avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
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
