using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Data
{
    public class ClaimContext:DbContext
    {
        public ClaimContext(DbContextOptions<ClaimContext>options):
            base(options)
        { 
        }
        public DbSet<Claim> Claims {  get; set; }

    }
}
