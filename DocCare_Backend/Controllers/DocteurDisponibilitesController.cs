using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DocCare_Backend.Models;

namespace DocCare_Backend.Controllers
{
    [ApiController]
    [Route("api/Disponibilite")]
    public class DocteurDisponibilitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocteurDisponibilitesController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: DocteurDisponibilites/getAll
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> Index()
        {
            var disponibilites = await _context.DocteursDisponibilites
                .Select(d => new { d.Id, d.Time, d.Date })
                .ToListAsync();

            return Json(disponibilites);
        }

        // GET: DocteurDisponibilites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DocteursDisponibilites == null)
            {
                return NotFound();
            }

            var docteurDisponibilite = await _context.DocteursDisponibilites
                .Include(d => d.Docteur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (docteurDisponibilite == null)
            {
                return NotFound();
            }

            return View(docteurDisponibilite);
        }



        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] DocteurDisponibilite docteurDisponibilite)
        {
            if (ModelState.IsValid)
            {
                // Recherche du docteur correspondant à l'ID spécifié dans la DocteurDisponibilite
                var docteur = await _context.Docteurs.FindAsync(docteurDisponibilite.DocteurId);

                if (docteur != null)
                {
                    // Création de la disponibilité
                    var disponibilite = new DocteurDisponibilite
                    {
                        Docteur = docteur,
                        Date = docteurDisponibilite.Date,
                        Time = docteurDisponibilite.Time
                    };

                    // Ajout de la disponibilité à la table DisponibiliteDocteur
                    _context.DocteursDisponibilites.Add(disponibilite);
                    await _context.SaveChangesAsync();

                    return Ok(disponibilite); // Retourne l'objet disponibilité créé avec un code de statut 200 OK
                }
                else
                {
                    return NotFound("Docteur non trouvé"); // Retourne une réponse NotFound si le docteur n'est pas trouvé
                }
            }
            else
            {
                return BadRequest(ModelState); // Retourne les erreurs de validation avec un code de statut 400 Bad Request
            }

        }



        // GET: DocteurDisponibilites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DocteursDisponibilites == null)
            {
                return NotFound();
            }

            var docteurDisponibilite = await _context.DocteursDisponibilites.FindAsync(id);
            if (docteurDisponibilite == null)
            {
                return NotFound();
            }
            ViewData["DocteurId"] = new SelectList(_context.Docteurs, "Id", "Id", docteurDisponibilite.DocteurId);
            return View(docteurDisponibilite);
        }

        // POST: DocteurDisponibilites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocteurId,Id,Date,Time")] DocteurDisponibilite docteurDisponibilite)
        {
            if (id != docteurDisponibilite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(docteurDisponibilite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocteurDisponibiliteExists(docteurDisponibilite.Id))
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
            ViewData["DocteurId"] = new SelectList(_context.Docteurs, "Id", "Id", docteurDisponibilite.DocteurId);
            return View(docteurDisponibilite);
        }

        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Effectuez la suppression de la disponibilité avec l'ID spécifié
                var disponibilite = await _context.DocteursDisponibilites.FindAsync(id);
                if (disponibilite == null)
                {
                    return NotFound(); // Retourner 404 si la disponibilité n'est pas trouvée
                }

                _context.DocteursDisponibilites.Remove(disponibilite);
                await _context.SaveChangesAsync();

                return Ok("Disponibilité supprimée avec succès"); // Retourner un message OK si la suppression est réussie
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }


        // POST: DocteurDisponibilites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DocteursDisponibilites == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DocteursDisponibilites'  is null.");
            }
            var docteurDisponibilite = await _context.DocteursDisponibilites.FindAsync(id);
            if (docteurDisponibilite != null)
            {
                _context.DocteursDisponibilites.Remove(docteurDisponibilite);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocteurDisponibiliteExists(int id)
        {
          return (_context.DocteursDisponibilites?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
