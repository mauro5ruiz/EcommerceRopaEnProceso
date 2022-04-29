using FashionNet.Data.Repository.Interfaces;
using FashionNet.Modelos;
using FashionNet.Modelos.ViewModels;
using FashionNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FashionNet.Controllers
{
    [Area("Clientes")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo,ILogger<HomeController> logger )
        {
            _contenedorTrabajo = contenedorTrabajo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListaProductos()
        {

            var listaProductos = new List<ProductoVM>();
            var productos = _contenedorTrabajo.Producto.MostrarTodos(includeProperties: "Categoria,SubCategoria,Marca").ToList();
            foreach (var item in productos)
            {
                var imagenes = _contenedorTrabajo.Producto.ObtenerImagenesDeCadaProducto(item.ProductoId);
                var imagen = _contenedorTrabajo.Producto.ObtenerPrimerImagen(item.ProductoId);
                var stock = _contenedorTrabajo.Producto.ObtenerInformacionDelProducto(item.ProductoId);
                List<Talle> talles = new List<Talle>();
                List<int> idTalles = new List<int>();
                List<Color> colores = new List<Color>();
                List<int> idTColores = new List<int>();
                foreach (var colorTalle in stock)
                {
                    if (!idTalles.Contains(colorTalle.TalleId))
                    {
                        idTalles.Add(colorTalle.TalleId);
                        talles.Add(colorTalle.Talle);
                    }
                    if (!idTColores.Contains(colorTalle.ColorId))
                    {
                        idTColores.Add(colorTalle.ColorId);
                        colores.Add(colorTalle.Color);
                    }
                    _contenedorTrabajo.ColorTalle.AplicarPropertys(colorTalle.ColorTalleId);
                }
                ProductoVM productoVM = new ProductoVM
                {
                    Producto = item,
                    Imagenes = imagenes,
                    Imagen = imagen,
                    Stocks = stock,
                    Talles = talles,
                    Colores = colores
                };
                _contenedorTrabajo.Producto.AplicarPropertys(item.ProductoId);
                listaProductos.Add(productoVM);
            }
            ViewData["ColorId"] = new List<SelectListItem>();
            return View(listaProductos);
        }

        public IActionResult Carrito()
        {
            return View();
        }

        public JsonResult GetTalles(int categoriaId)
        {
            var talles = _contenedorTrabajo.Talle.GetTallesCascade(categoriaId);
            return Json(talles);
        }

        public JsonResult GetColores(int id)
        {
            var colores = _contenedorTrabajo.ColorTalle.GetColoresTalles(id);
            return Json(colores);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
