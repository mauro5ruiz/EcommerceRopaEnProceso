using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Modelos
{
    public class SubCategoria
    {
        [Key]
        public int SubCategoriaId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la categoría")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage ="Debe ingresar el nombre de la subCategoría")]
        public string Nombre { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
