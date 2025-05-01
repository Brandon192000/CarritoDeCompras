using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarritoCompras.Models
{
    public class Carrito
    {


        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaCreacionCarrito { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} tiene un máximo de 50 caracteres")]
        public string? Estado { get; set; }

        /// <summary>
        /// Atributos de navegación
        /// </summary>
        [ForeignKey(nameof(IdUsuario))]  // Aquí está la clave: se refiere a la propiedad real
        public Usuario? Usuario { get; set; }

        public ICollection<CarritoDetalle>? DetallesCarrito { get; set; }


    }
}
