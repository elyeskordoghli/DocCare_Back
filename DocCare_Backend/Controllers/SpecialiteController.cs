using DocCare_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace DocCare_Backend.Controllers
{
    [ApiController]
    [Route("api/Specialite")] // Route de base pour les API des Specialites
    public class SpecialiteController : Controller
    {
        // GET: SpecialiteController
       
        private readonly ApplicationDbContext _context;


        public SpecialiteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Specialites/Details/5
        [HttpGet]
        [Route("getSpecialiteById/{SpecialiteId }")]
        public async Task<IActionResult> Details(int? SpecialiteId)
        {
            if (SpecialiteId == null || _context.Specialites == null)
            {
                return NotFound();
            }

            var specialite = await _context.Specialites
                .FirstOrDefaultAsync(m => m.SpecialiteId == SpecialiteId );
            if (specialite == null)
            {
                return Problem($"specialite non trouvé ");
            }
            else
            {
                return Json(new { data = specialite });

            }

        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var specialites = await _context.Specialites.ToListAsync();

                if (specialites != null)
                {
                    return Json(new { data = specialites });
                }
                else
                {
                    return Problem("La liste des Specialite est vide.");
                }
            }
            catch (Exception ex)
            {
                return Problem($"Une erreur s'est produite : {ex.Message}");
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("CreateSpecialite")] // Nouvelle route pour l'ajout d'un nouveau Specialite
        public IActionResult CreateSpecialite(Specialite nouveauSpecialite)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Créer un nouvel objet Specialite avec les détails fournis
                    var newSpecialite = new Specialite
                    {
                        Nom = nouveauSpecialite.Nom,
                        ImagePath = nouveauSpecialite.ImagePath,
                    };

                    // Ajouter le nouveau Specialite à la table Specialites
                    _context.Specialites.Add(newSpecialite);
                    _context.SaveChanges();

                    return Ok(new
                    {
                        message = "Ajout de l specialité  réussie",
                        Specialite = newSpecialite,
                        status = 200
                    });
                }
                catch (Exception ex)
                {
                    // Gérer les exceptions liées à l'enregistrement dans la base de données
                    return StatusCode(500, $"Une erreur s'est produite lors de l'ajout du Specialite : {ex.Message}");
                }
            }

            // Retourner les erreurs de validation si le modèle n'est pas valide
            return BadRequest(ModelState);
        }
        // GET: SpecialiteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpecialiteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        } 
    }
}
