using FashionNet.Data.Repository.Interfaces;
using FashionNet.Modelos;
using FashionNet.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FashionNet.Areas.Admin.Controllers
{

    [Authorize(Roles =CNT.Admin)]
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<Categoria>> Index()
        {
            return View(_contenedorTrabajo.Categoria.MostrarTodos().ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Agregar(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            Categoria categoria;
            categoria = _contenedorTrabajo.Categoria.ObtenerPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Editar(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return View("NotFound");
            }
            Categoria categoria = _contenedorTrabajo.Categoria.ObtenerPorId(id);
            if (categoria == null)
            {
                return View("Categoria inexistente");
            }
            return View(categoria);
        }

        // POST: Interpretes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = _contenedorTrabajo.Categoria.ObtenerPorId(id);
            _contenedorTrabajo.Categoria.EliminarPorEntidad(categoria);
            _contenedorTrabajo.Save();
            return RedirectToAction("Index");
        }
    }
}
