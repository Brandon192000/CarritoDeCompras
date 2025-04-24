using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarritoCompras.Models
{
    public class ListaDeseo
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [ForeignKey("Usuario")]
        public int IdUsuario {  get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaOrden {  get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Total {  get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} tiene un maximo de 50 caracteres")]
        public string? Estado { get; set; }


        /// <summary>
        /// atributos denav
        /// </summary>
        public Usuario? Usuario { get; set; }

        public ICollection<ListaDetalle>? DetalleLista { get; set; }


    }
}
