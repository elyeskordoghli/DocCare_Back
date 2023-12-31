﻿using DocCare_Backend.Models;
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
    [Route("api/User")] // Route de base pour les API des Users
    public class UserController : Controller
    {
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }
        private readonly ApplicationDbContext _context;


        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("NewUserSignUp")] // Nouvelle route pour l'inscription d'un nouveau utilisateur
        public IActionResult SignUpUser(User nouveauUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Créer un nouvel objet User avec les détails fournis
                    var newUser = new User
                    {
                        
                        Email = nouveauUser.Email,
                        Password = nouveauUser.Password,
                        R_Token = null // R_Token à null lors du sign-up

                    };

                    // Ajouter le nouveau User à la table Users
                    _context.Users.Add(newUser);
                    _context.SaveChanges();

                    return Ok(new
                    {
                        message = "Inscription réussie",
                        User = newUser,
                        status = 200
                    });
                }
                catch (Exception ex)
                {
                    // Gérer les exceptions liées à l'enregistrement dans la base de données
                    return StatusCode(500, $"Une erreur s'est produite lors de l'inscription du User : {ex.Message}");
                }
            }

            // Retourner les erreurs de validation si le modèle n'est pas valide
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var email = userLogin.Email;
                var password = userLogin.Password;

                // Vérification des informations d'identification dans la base de données
                var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

                if (user == null)
                {
                    return NotFound(new { error = "Utilisateur non trouvé ou mot de passe incorrect.", status = 400 });
                }
                // Génération du token JWT
                var key = GenerateSecurityKey(); // Appel à une méthode pour générer la clé
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, email),
                        // Ajoutez d'autres revendications (claims) si nécessaire
                    }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var tokenStringWithCustomFormat = tokenString[0]+ "|" + tokenString;

                user.R_Token = tokenStringWithCustomFormat;


                // Enregistrez les modifications dans la base de données
                _context.SaveChanges();

                // Vous pouvez renvoyer le tokenString dans la réponse ou le stocker côté client pour les futures requêtes
                return Ok(new
                {
                    message = "Authentification réussie",
                    User = user,
                    token = tokenStringWithCustomFormat,
                    status = 200
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Une erreur s'est produite lors de la connexion.", exception = ex.Message });
            }
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


















        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
