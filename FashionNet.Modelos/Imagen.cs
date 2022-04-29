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
    public class Imagen
    {
        [Key]
        public int ImagenId { get; set; }

        [Display(Name = "Imágen")]
        public string UrlImagen { get; set; }

        public int ProductoId { get; set; }


        public virtual Producto Producto { get; set; }
    }
}
