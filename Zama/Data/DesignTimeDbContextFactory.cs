using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Zama.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ZamaDbContext>
    {
        public ZamaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ZamaDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ZamaRestaurant;Trusted_Connection=True;TrustServerCertificate=True");

            return new ZamaDbContext(optionsBuilder.Options);
        }
    }
}