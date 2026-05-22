using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TennisAcademyApp.Data
{
    public class TennisAcademyDbContextFactory : IDesignTimeDbContextFactory<TennisAcademyDbContext>
    {
        public TennisAcademyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TennisAcademyDbContext>();

            optionsBuilder.UseSqlServer("Server=DESKTOP-040SMQM;Database=TennisAcademyDb;Integrated Security=True; Encrypt=False");

            return new TennisAcademyDbContext(optionsBuilder.Options, null!);
        }
    }
}