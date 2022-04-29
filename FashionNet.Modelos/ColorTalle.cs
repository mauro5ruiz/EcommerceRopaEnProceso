using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Modelos
{
    public class ColorTalle
    {
        [Key]
        public int ColorTalleId { get; set; }

        public int ColorId { get; set; }
        public int TalleId { get; set; }
        public int ProductoId { get; set; }

        [Required(ErrorMessage ="Debe ingresar la cantidad")]
        public int Cantidad { get; set; }

        public virtual Color Color { get; set; }
        public virtual Talle Talle { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
