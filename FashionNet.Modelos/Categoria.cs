using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Modelos
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Categoría")]
        public string Nombre { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
