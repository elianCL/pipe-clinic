using Microsoft.EntityFrameworkCore;
using PipeClinic.Models;

namespace PipeClinic.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=pipeclinic.db"); // Substitua pelo seu caminho de banco de dados SQLite
            }
        }
    }
}
