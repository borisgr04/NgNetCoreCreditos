using NgNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace NgNetCore.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Credito> Creditos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        
      
    }
}
