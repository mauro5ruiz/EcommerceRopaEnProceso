using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Modelos
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del color")]
        [Display(Name = "Color")]
        public string Nombre { get; set; }

        public string CodigoRgba { get; set; }

        public virtual ICollection<ColorTalle> ColoresTalles { get; set; }
        public virtual ICollection<DetalleProductoTmp> DetallesProductoTmps { get; set; }
    }
}
