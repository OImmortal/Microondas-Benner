using MicroondasMVC_Benner.Models.API;
using Microsoft.EntityFrameworkCore;

namespace MicroondasMVC_Benner
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<UserAuthModel> UserAuth { get; set; }
    }
}
