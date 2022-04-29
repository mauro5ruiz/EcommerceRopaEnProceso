using FashionNet.Data.Repository.Interfaces;
using FashionNet.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FashionNet.Areas.Admin.Controllers
{

    [Authorize(Roles = CNT.Admin)]
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult ListadoUsuarios()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_contenedorTrabajo.Usuario.MostrarTodos(u => u.Id != usuarioActual.Value));
        }

        public IActionResult Bloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _contenedorTrabajo.Usuario.Bloquear(id);
            return RedirectToAction(nameof(ListadoUsuarios));
        }

        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _contenedorTrabajo.Usuario.Desbloquear(id);
            return RedirectToAction(nameof(ListadoUsuarios));
        }
    }
}
