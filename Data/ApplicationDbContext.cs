using Microsoft.EntityFrameworkCore;
using PruebaTecnicaBookRadar.Models;

namespace PruebaTecnicaBookRadar.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<HistorialBusqueda> HistorialBusquedas { get; set; }
    }
}
