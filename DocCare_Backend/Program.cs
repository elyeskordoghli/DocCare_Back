using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using DocCare_Backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajoutez un service de base de donn�es en utilisant Entity Framework Core
try
{
    // Ajoutez un service de base de donn�es en utilisant Entity Framework Core
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        // Utilisez la cha�ne de connexion depuis appsettings.json
        options.UseMySql(
            builder.Configuration.GetConnectionString("MySqlDbConnection"),
            new MySqlServerVersion(new Version(10, 4, 28))); // Sp�cifiez la version de MariaDB ici
    });

    var dbName = builder.Configuration.GetConnectionString("MySqlDbConnection");
    System.Diagnostics.Debug.WriteLine("Nom de la base de donn�es : " + dbName);
    Console.WriteLine("Connexion � la base de donn�es '" + dbName + "' r�ussie !");

}
catch (Exception ex)
{
    Console.WriteLine("�chec de la connexion � la base de donn�es : " + ex.Message);
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
    pattern: "api/Doctor/NewDoctorSignUp", // Chemin personnalis� pour l'inscription d'un nouveau docteur
    defaults: new { controller = "Doctor", action = "SignUpDoctor" }
);





app.Run();
