using Microsoft.EntityFrameworkCore;
using MvcPaginacionExamenMauricio.Models;

namespace MvcPaginacionExamenMauricio.Data
{
    public class ZapatillasContext : DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options) : base(options)
        {
        }
        public DbSet<Zapatilla> Zapatillas { get; set; }
        public DbSet<ImagenZapatilla> ImagenesZapatillas { get; set; }
    }
}
