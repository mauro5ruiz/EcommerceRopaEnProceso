using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Modelos
{
    public class Talle
    {
        [Key]
        public int TalleId { get; set; }

        [Display(Name = "Talle")]
        [Required(ErrorMessage = "Debe ingresar el talle")]
        public string NombreTalle  { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la categoría")]
        public int CategoriaTalleId { get; set; }

        [ForeignKey("CategoriaTalleId")]
        public CategoriaTalle Categoria { get; set; }

        public virtual ICollection<ColorTalle> ColoresTalles { get; set; }
        public virtual ICollection<DetalleProductoTmp> DetallesProductoTmps { get; set; }
    }
}
