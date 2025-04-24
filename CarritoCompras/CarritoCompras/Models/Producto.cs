using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarritoCompras.Models
{
    public class Producto
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} tiene un maximo de 100 caracteres")]
        public string? Nombre { get; set; }


        [Range(0, 1000)]
        public int CantidadStock { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(300, ErrorMessage = "El campo {0} tiene un maximo de 300 caracteres")]
        public string? Descripcion { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Precio { get; set; }



        [MaxLength(500, ErrorMessage = "El campo {0} tiene un maximo de 50 caracteres")]
        public string Imagen {  get; set; }


        /// <summary>
        /// atributos denav
        /// </summary>
        public ICollection<ListaDetalle>? DetalleLista { get; set; }

        public ICollection<CarritoDetalle>? DetallesCarrito { get; set; }
    }
}
