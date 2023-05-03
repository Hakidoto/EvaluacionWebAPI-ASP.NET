using ClassLibrary1;
using Microsoft.EntityFrameworkCore;
namespace WebApiTest.Models
{
    public class CuestionarioContext : DbContext
    {
        public CuestionarioContext(DbContextOptions<CuestionarioContext> options) : base(options) { }
        public DbSet<Cuestionario> CuestionarioItems { get; set; } = null!;
    }

}
