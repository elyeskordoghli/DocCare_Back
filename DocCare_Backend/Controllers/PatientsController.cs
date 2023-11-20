using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DocCare_Backend.Models
{
    [ApiController]
    [Route("api/Patient")]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var patients = await _context.Patients.ToListAsync();

                if (patients != null)
                {
                    return Json(new { data = patients });
                }
                else
                {
                    return Problem("La liste des patients est vide.");
                }
            }
            catch (Exception ex)
            {
                return Problem($"Une erreur s'est produite : {ex.Message}");
            }
        }

        // GET: Patients/Details/5
        [HttpGet]
        [Route("getPatientById/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return Problem($"patient non trouvé ");
            }
            else
            {
                return Json(new { data = patient });

            }

        }


        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
       






















        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("EditPatient/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,DateN,Adresse,Num,DossierMedical")] Patient updatedPatient)
        {
            if (id != updatedPatient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var form = await Request.ReadFormAsync();

                    var nom = form["Nom"];
                    var prenom = form["Prenom"];
                    var dateN = form["DateN"];
                    var adresse = form["Adresse"];
                    var num = form["Num"];

                    var dossierMedicalFile = form.Files["DossierMedical"];
                    byte[]? dossierMedicalBytes = null;


                    var patient = await _context.Patients.FindAsync(id);

                    if (patient == null)
                    {
                        return NotFound();
                    }
                    // Mettre à jour les propriétés du patient avec les nouvelles données
                    patient.Nom = nom;
                    patient.Prenom = prenom;
                    patient.DateN = dateN;
                    patient.Adresse = adresse;
                    patient.Num = num;
                  //  patient.DossierMedical = dossierMedicalBytes;

                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Patient modifié avec succès." });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(updatedPatient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        



        // GET: Patients/Delete/5
        [HttpPost]
        [Route("DeletePatient/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Delete success" });
        }




        private bool PatientExists(int id)
        {
          return (_context.Patients?.Any(e => e.Id == id)).GetValueOrDefault();
        }






        // Endpoint pour la recherche
        [HttpGet]
        [Route("SearchPatient")]
        public async Task<IActionResult> Search(string q)
        {
            try
            {
                if (string.IsNullOrEmpty(q))
                {
                    return BadRequest("Veuillez fournir un paramètre de recherche.");
                }

                var patients = await _context.Patients
                    .Where(p =>
                        EF.Functions.Like(p.Nom, $"%{q}%") ||
                        EF.Functions.Like(p.Prenom, $"%{q}%") ||
                        EF.Functions.Like(p.DateN, $"%{q}%") ||
                        EF.Functions.Like(p.Adresse, $"%{q}%") ||
                        EF.Functions.Like(p.Num, $"%{q}%")
                    // Ajoutez d'autres colonnes de recherche ici
                    )
                    .ToListAsync();

                return Ok(new
                {
                    action = "getPatientSearch",
                    status = "success",
                    data = patients
                });
            }
            catch
            {
                return StatusCode(500, "Une erreur s'est produite lors de la recherche.");
            }
        }
    }
}
