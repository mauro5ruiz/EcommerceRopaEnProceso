using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Modelos.ViewModels
{
    public class ProductoVM
    {
        public Producto Producto { get; set; }
        public Imagen Imagen { get; set; }
        public Talle Talle { get; set; }
        public Color Color { get; set; }
        public ColorTalle ColorTalle { get; set; }
        public List<IFormFile> mysFiles { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
        public SelectList ListaSubCategorias { get; set; }
        public IEnumerable<SelectListItem> ListaSub { get; set; }
        public IEnumerable<SelectListItem> ListaMarcas { get; set; }
        public List<DetalleProductoTmp> Detalles { get; set; }
        public List<ColorTalle> Stocks { get; set; }
        public List<Imagen> Imagenes { get; set; }
        public List<Talle> Talles { get; set; }
        public List<Color> Colores { get; set; }
    }
}
