using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using GerenciarProcessos.Infrastructure.Data;

namespace GerenciarProcessos.Infrastructure.Factories
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer("Server=FILIPE\\SQLEXPRESS;Database=GerenciarProcessosDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
