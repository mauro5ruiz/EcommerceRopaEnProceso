using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Modelos.ViewModels
{
    public class SubCategoriaVM
    {
        public SubCategoria SubCategoria { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
