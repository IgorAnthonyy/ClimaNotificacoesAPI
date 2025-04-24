using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ClimaNotificacoesAPI.Infrastructure.Data
{
    public class ClimaNotificacoesDBContextFactory : IDesignTimeDbContextFactory<ClimaNotificacoesDBContext>
    {
        public ClimaNotificacoesDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "ClimaNotificacoesAPI.API"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ClimaNotificacoesDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new ClimaNotificacoesDBContext(optionsBuilder.Options);
        }
    }
}
