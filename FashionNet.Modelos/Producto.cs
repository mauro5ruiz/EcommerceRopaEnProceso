using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Modelos
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar la categoriía")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar la sub-categoriía")]
        [Display(Name = "SubCategoría")]
        public int SubCategoriaId { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar la marca")]
        [Display(Name = "Marca")]
        public int MarcaId { get; set; }


        [Required(ErrorMessage = "Debe ingresar la descripción")]
        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        [Required(ErrorMessage = "Debe ingresar el precio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(1, double.MaxValue, ErrorMessage = "Debe ingresar el valor del {0} entre {1} y {2}")]
        public decimal Precio { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual SubCategoria SubCategoria { get; set; }
        public virtual Marca Marca { get; set; }

        public virtual ICollection<Imagen> Imagenes { get; set; }
        public virtual ICollection<ColorTalle> ColoresTalles { get; set; }

        public string Activo => "Activo";
        public string Inactivo => "Inactivo";
    }
}
