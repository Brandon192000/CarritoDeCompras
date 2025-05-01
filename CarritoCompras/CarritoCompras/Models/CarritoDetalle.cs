using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarritoCompras.Models
{
    public class CarritoDetalle

    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdCarrito { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(0, 1000)]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PrecioProducto { get; set; }

        // Relaciones explícitas con ForeignKey correctamente definidos
        [ForeignKey(nameof(IdCarrito))]
        public Carrito? Carrito { get; set; }

        [ForeignKey(nameof(IdProducto))]
        public Producto? Producto { get; set; }

    }
}
