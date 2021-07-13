using System;
using back_end.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace back_end
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;");
            }
        }*/
        
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Actor> Actors { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
