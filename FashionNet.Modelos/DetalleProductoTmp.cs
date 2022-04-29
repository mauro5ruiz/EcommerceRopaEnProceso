using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Modelos
{
    public class DetalleProductoTmp
    {
        [Key]
        public int DetalleProductoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(256, ErrorMessage = "El campo {0} debe tener como maximo {1} caracteres")]
        public string NombreUsuario { get; set; }


        [Required(ErrorMessage = "Debe seleccionar el talle")]
        public int TalleId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el color")]
        public int ColorId { get; set; }

        [Required(ErrorMessage = "Debe ingresar el stock")]
        [Display(Name = "Stock")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar el valor de la {0} entre {1} y {2}")]
        public int Cantidad { get; set; }

        [ForeignKey("TalleId")]
        public Talle Talle { get; set; }

        [ForeignKey("ColorId")]
        public Color Color { get; set; }
    }
}
