using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarritoCompras.Models
{
    public class CarritoDetalle

    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id {  get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [ForeignKey("Carrito")]
        public int IdCarrito {  get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [ForeignKey("Producto")]
        public int IdProducto {  get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(0, 1000)]
        public int Cantidad {  get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PrecioProducto {  get; set; }


        /// <summary>
        /// atributos denav
        /// </summary>
        public Carrito? Carritos { get; set; }

        public Producto? Producto { get; set; }    


    }
}
