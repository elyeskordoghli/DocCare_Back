using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using DocCare_Backend;
using DocCare_Backend.Models;
public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // Ajoutez un service de base de données en utilisant Entity Framework Core
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                // Vous pouvez utiliser le contexte ici, par exemple, appliquer des migrations
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de la configuration de la base de données : " + ex.Message);
            }
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
