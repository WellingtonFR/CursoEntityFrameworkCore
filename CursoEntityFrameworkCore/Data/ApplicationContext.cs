using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Data
{
    public class ApplicationContext : DbContext
    {

    public DbSet<Pedido> Pedidos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=efcurso;User Id=sa;Password=#Sudo#4321*;");
        }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}