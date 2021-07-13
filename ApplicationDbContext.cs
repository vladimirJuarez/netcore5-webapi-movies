using System.Reflection;
using back_end.Entidades;
using Microsoft.EntityFrameworkCore;

namespace back_end
{
    /*public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        
        public DbSet<Genre> Genres { get; set; }
    }*/
}