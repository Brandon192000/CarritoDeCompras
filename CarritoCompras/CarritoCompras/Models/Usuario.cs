using System.ComponentModel.DataAnnotations;

namespace CarritoCompras.Models
{
    public class Usuario
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id {  get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} tiene un maximo de 100 caracteres")]
        public string? Nombre { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} tiene un maximo de 100 caracteres")]
        public string? Correo { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(25, ErrorMessage = "El campo {0} tiene un maximo de 25 caracteres")]
        public string? Contrasenia { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaRegistro { get; set; }

        public bool Borrado { get; set; }

        /// <summary>
        /// atributos de nav
        /// </summary>
        public ICollection<Carrito>? Carrito { get; set; }
        public ICollection<ListaDeseo>? ListaDeseo { get; set; }
    }
}
