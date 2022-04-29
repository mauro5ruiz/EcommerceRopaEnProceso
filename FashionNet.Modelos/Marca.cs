using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Modelos
{
    public class Marca
    {
        [Key]
        public int MarcaId { get; set; }

        [Required(ErrorMessage ="Debe ingresar el nombre de la marca")]
        public string Nombre { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
