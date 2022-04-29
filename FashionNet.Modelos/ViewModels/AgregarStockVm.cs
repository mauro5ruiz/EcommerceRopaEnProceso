using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Modelos.ViewModels
{
    public class AgregarStockVm
    {
        
        public IEnumerable<SelectListItem> ListaCategoriaTalles { get; set; }
        public IEnumerable<SelectListItem> ListaColores { get; set; }
        public List<Talle> ListaTalles { get; set; }

        public string Talle { get; set; }
        public string Categoria { get; set; }
        public string Color { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public int TalleId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public int ColorId { get; set; }
        public int CategoriaTalleId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar el valor de {0} entre {1} y {2}")]
        public int Cantidad { get; set; }
    }
}
