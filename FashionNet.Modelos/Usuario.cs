using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Modelos
{
    public class Usuario : IdentityUser
    {
        [Required(ErrorMessage = "Debe ingresar al menos un nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar el apellido")]
        public string Apellido { get; set; }
    }
}
