using Microsoft.EntityFrameworkCore;
using CarritoCompras.Models;

namespace CarritoCompras.Data


{
    public class DBAppContext : DbContext
    {

        public DBAppContext(DbContextOptions<DBAppContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Carrito> Carrito { get; set; }

        public DbSet<CarritoDetalle> CarritoDetalles { get; set; }

        public DbSet<ListaDeseo> ListaDeseos { get; set; }

        public DbSet<ListaDetalle> ListaDetalle { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
