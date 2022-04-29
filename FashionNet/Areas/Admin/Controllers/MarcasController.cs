using FashionNet.Data.Repository.Interfaces;
using FashionNet.Modelos;
using FashionNet.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FashionNet.Areas.Admin.Controllers
{
    [Authorize(Roles = CNT.Admin)]
    [Area("Admin")]
    public class MarcasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        //sirve para trabajar con la subida de archivos
        private readonly IWebHostEnvironment _enviroment;
        public MarcasController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment enviroment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _enviroment = enviroment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_contenedorTrabajo.Marca.MostrarTodos().ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Marca marca)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _enviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (marca.MarcaId == 0)
                {
                    //Guid es un tipo de dato que permite nombres extensos
                    string nombreArchivo = Guid.NewGuid().ToString();
                    //Las imágenes se guardarán en la carpeta articulos que a su evz está dentro de la carpeta imagenes
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\marcas");
                    //Obtenemos la extensión del archivo
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var filesStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(filesStreams);
                    }

                    marca.UrlImagen = @"\imagenes\marcas\" + nombreArchivo + extension;
                    _contenedorTrabajo.Marca.Agregar(marca);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(marca);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            Marca marca = new Marca();
            if (id != 0)
            {
                marca = _contenedorTrabajo.Marca.ObtenerPorId(id);
                if (marca == null)
                {
                    return NotFound();
                }
            }
            
            return View(marca);
        }

        [HttpPost]
        public IActionResult Editar(Marca marca)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _enviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                var marcaDesdeBd = _contenedorTrabajo.Marca.ObtenerPorId(marca.MarcaId);
                if (archivos.Count > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\marcas");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);
                    var rutaImagen = Path.Combine(rutaPrincipal, marcaDesdeBd.UrlImagen.TrimStart('\\'));
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }
                    //Subimos nuevamente el archivo
                    using (var filesStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                    {
                        archivos[0].CopyTo(filesStreams);
                    }
                    marca.UrlImagen = @"\imagenes\marcas\" + nombreArchivo + nuevaExtension;
                    _contenedorTrabajo.Marca.Update(marca);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //cuando la imagen ya existe y no se reemplaza debe conservar la foto que ya está en la base de datos
                    marca.UrlImagen = marcaDesdeBd.UrlImagen;
                }
                _contenedorTrabajo.Marca.Update(marca);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        [HttpGet]
        public ActionResult Borrar(int id)
        {
            if (id == 0)
            {
                return View("NotFound");
            }
            Marca marca = _contenedorTrabajo.Marca.ObtenerPorId(id);
            if (marca == null)
            {
                return View("Marca inexistente");
            }
            return View(marca);
        }

        [HttpPost, ActionName("Borrar")]
        public ActionResult ConfirmarBorrado(int id)
        {
            Marca marca = _contenedorTrabajo.Marca.ObtenerPorId(id);
            _contenedorTrabajo.Marca.EliminarPorEntidad(marca);
            _contenedorTrabajo.Save();
            return RedirectToAction("Index");
        }
    }
}
