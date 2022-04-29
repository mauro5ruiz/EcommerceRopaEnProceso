using FashionNet.Data.Repository.Interfaces;
using FashionNet.Modelos;
using FashionNet.Modelos.ViewModels;
using FashionNet.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionNet.Areas.Admin.Controllers
{

    [Authorize(Roles = CNT.Admin)]
    [Area("Admin")]
    public class SubCategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public SubCategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<SubCategoria>> Index()
        {
            var subCategorias = _contenedorTrabajo.SubCategoria.MostrarTodos(includeProperties: "Categoria");
            return View(subCategorias);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SubCategoriaVM subCategoriaVM = new SubCategoriaVM
            {
                SubCategoria = new Modelos.SubCategoria(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };
            return View(subCategoriaVM);
        }

        [HttpPost]
        public IActionResult Create(SubCategoriaVM subCategoriaVM)
        {
            if (ModelState.IsValid)
            {
                if (subCategoriaVM.SubCategoria.SubCategoriaId == 0)
                {
                    _contenedorTrabajo.SubCategoria.Agregar(subCategoriaVM.SubCategoria);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(subCategoriaVM);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            SubCategoriaVM subCategoriaVM = new SubCategoriaVM
            {
                SubCategoria = new Modelos.SubCategoria(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };
            subCategoriaVM.SubCategoria = _contenedorTrabajo.SubCategoria.ObtenerPorId(id);
            return View(subCategoriaVM);
        }

        [HttpPost]
        public IActionResult Edit(SubCategoriaVM subCategoriaVM)
        {
            if (ModelState.IsValid)
            {
                var subCategoriaDesdeBd = _contenedorTrabajo.SubCategoria.ObtenerPorId(subCategoriaVM.SubCategoria.SubCategoriaId);
                _contenedorTrabajo.SubCategoria.Update(subCategoriaVM.SubCategoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public ActionResult Borrar(int id)
        {
            if (id==0)
            {
                return View("NotFound");
            }
            var subCategoria = _contenedorTrabajo.SubCategoria.ObtenerPorId(id);
            subCategoria = _contenedorTrabajo.SubCategoria.AplicarPropertys(id);
            if (subCategoria == null)
            {
                return View("Sub-Categoría inexistente");
            }
            return View(subCategoria);
        }

        [HttpPost, ActionName("Borrar")]
        public ActionResult ConfimarBorrado(int id)
        {
            SubCategoria subCategoria = _contenedorTrabajo.SubCategoria.ObtenerPorId(id);
            _contenedorTrabajo.SubCategoria.EliminarPorEntidad(subCategoria);
            _contenedorTrabajo.Save();
            return RedirectToAction("Index");
        }
    }
}
