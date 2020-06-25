using GoogleAuth.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace GoogleAuth
{
	public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = BuildWebHost(args).Build();
            await Migrate(host);
                host.Run();
        }
        private async static Task Migrate(IHost host)
		{
            using var serviceScope = host.Services.CreateScope() ;
            var services = serviceScope.ServiceProvider;
            var db = services.GetRequiredService<ApplicationDbContext>();

            await db.Database.MigrateAsync();

        }
        public static IHostBuilder BuildWebHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
