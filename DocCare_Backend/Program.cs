using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using DocCare_Backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajoutez un service de base de données en utilisant Entity Framework Core
try
{
    // Ajoutez un service de base de données en utilisant Entity Framework Core
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        // Utilisez la chaîne de connexion depuis appsettings.json
        options.UseMySql(
            builder.Configuration.GetConnectionString("MySqlDbConnection"),
            new MySqlServerVersion(new Version(10, 4, 28))); // Spécifiez la version de MariaDB ici
    });

    var dbName = builder.Configuration.GetConnectionString("MySqlDbConnection");
    System.Diagnostics.Debug.WriteLine("Nom de la base de données : " + dbName);
    Console.WriteLine("Connexion à la base de données '" + dbName + "' réussie !");

}
catch (Exception ex)
{
    Console.WriteLine("Échec de la connexion à la base de données : " + ex.Message);
}


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "DoctorSignUp",
    pattern: "api/Doctor/NewDoctorSignUp", // Chemin personnalisé pour l'inscription d'un nouveau docteur
    defaults: new { controller = "Doctor", action = "SignUpDoctor" }
);





app.Run();
