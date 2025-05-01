using Microsoft.EntityFrameworkCore;
using CarritoCompras.Models;

namespace CarritoCompras.Data
{
    public class DBAppContext : DbContext
    {
        public DBAppContext(DbContextOptions<DBAppContext> options)
           : base(options)
        {
        }

        // Definición de las tablas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoDetalle> CarritoDetalles { get; set; }
        public DbSet<ListaDeseo> ListaDeseos { get; set; }
        public DbSet<ListaDetalle> ListaDetalles { get; set; }

        // Configuración de relaciones (si es necesario, se puede hacer mediante Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relaciones entre las tablas (ya definidas con Data Annotations)
            base.OnModelCreating(modelBuilder);

            // Relación entre Usuario y Carrito (uno a muchos)
            modelBuilder.Entity<Carrito>()
                .HasOne(c => c.Usuario) // Relación con Usuario
                .WithMany(u => u.Carrito) // Un usuario puede tener muchos carritos
                .HasForeignKey(c => c.IdUsuario) // Clave foránea en Carrito
                .OnDelete(DeleteBehavior.Cascade); // Eliminar carritos si el usuario es eliminado

            // Relación entre Carrito y CarritoDetalle (uno a muchos)
            modelBuilder.Entity<CarritoDetalle>()
                .HasOne(cd => cd.Carrito) // Relación con Carrito
                .WithMany(c => c.DetallesCarrito) // Un carrito puede tener muchos detalles
                .HasForeignKey(cd => cd.IdCarrito) // Clave foránea en CarritoDetalle
                .OnDelete(DeleteBehavior.Cascade); // Eliminar detalles si el carrito es eliminado

            // Relación entre Usuario y ListaDeseo (uno a muchos)
            modelBuilder.Entity<ListaDeseo>()
                .HasOne(ld => ld.Usuario) // Relación con Usuario
                .WithMany(u => u.ListaDeseo) // Un usuario puede tener muchas listas de deseos
                .HasForeignKey(ld => ld.IdUsuario) // Clave foránea en ListaDeseo
                .OnDelete(DeleteBehavior.Cascade); // Eliminar listas de deseos si el usuario es eliminado

            // Relación entre ListaDeseo y ListaDetalle (uno a muchos)
            modelBuilder.Entity<ListaDetalle>()
                .HasOne(ld => ld.ListaDeseo) // Relación con ListaDeseo
                .WithMany(ld => ld.DetalleLista) // Una lista de deseos puede tener muchos detalles
                .HasForeignKey(ld => ld.IdOrden); // Clave foránea en ListaDetalle

        }
    }
}