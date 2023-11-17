using DocCare_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace DocCare_Backend.Controllers
{
    [ApiController]
    [Route("api/Doctor")] // Route de base pour les API des docteurs
    public class DoctorController : Controller
    {
        // GET: DoctorController
        public ActionResult Index()
        {
            return View();
        }
        private readonly ApplicationDbContext _context;


        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("NewDoctorSignUp")] // Nouvelle route pour l'inscription d'un nouveau docteur
        public IActionResult SignUpDoctor(Docteur nouveauDocteur)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Créer un nouvel objet Docteur avec les détails fournis
                    var newDoctor = new Docteur
                    {
                        Nom = nouveauDocteur.Nom,
                        Prenom = nouveauDocteur.Prenom,
                        Adresse = nouveauDocteur.Adresse,
                        NumeroTelephone = nouveauDocteur.NumeroTelephone,
                        Photo = nouveauDocteur.Photo,
                        Genre = nouveauDocteur.Genre,
                        Email = nouveauDocteur.Email,
                        Password = nouveauDocteur.Password,
                        Specialite = nouveauDocteur.Specialite
                    };

                    // Ajouter le nouveau docteur à la table Docteurs
                    _context.Docteurs.Add(newDoctor);
                    _context.SaveChanges();

                    return Ok("Inscription du docteur réussie !");
                }
                catch (Exception ex)
                {
                    // Gérer les exceptions liées à l'enregistrement dans la base de données
                    return StatusCode(500, $"Une erreur s'est produite lors de l'inscription du docteur : {ex.Message}");
                }
            }

            // Retourner les erreurs de validation si le modèle n'est pas valide
            return BadRequest(ModelState);
        }










        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            string email = userLogin.Email;
            string password = userLogin.Password;

            // Vérification des informations d'identification dans la base de données
            var user = _context.Docteurs.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Génération d'une clé secrète pour signer le token
                var key = GenerateSecurityKey(); // Appel à une méthode pour générer la clé

                // Génération du token JWT
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, email),
                        // Ajoutez d'autres revendications (claims) si nécessaire
                    }),
                    Expires = DateTime.UtcNow.AddHours(24), // Durée de validité du token (1 heure dans cet exemple)
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Vous pouvez renvoyer le tokenString dans la réponse ou le stocker côté client pour les futures requêtes
                return Ok(new { Token = tokenString });
            }

            return NotFound("Utilisateur non trouvé ou mot de passe incorrect.");
        }

        // Méthode pour générer une clé aléatoire sécurisée
        private byte[] GenerateSecurityKey()
        {
            byte[] key = new byte[256]; // 256 bits pour HmacSha256
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }


















        // GET: DoctorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DoctorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorController/Create
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

        // GET: DoctorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: DoctorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
