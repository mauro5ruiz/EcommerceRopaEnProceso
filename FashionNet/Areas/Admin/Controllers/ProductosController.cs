using FashionNet.Data.Repository.Interfaces;
using FashionNet.Modelos;
using FashionNet.Modelos.ViewModels;
using FashionNet.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FashionNet.Areas.Admin.Controllers
{
    [Authorize(Roles = CNT.Admin)]
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _enviroment;

        public ProductosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment enviroment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _enviroment = enviroment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var productos = _contenedorTrabajo.Producto.MostrarTodos(includeProperties: "Categoria,SubCategoria,Marca");
            return View(productos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var usuarioLogueado = _contenedorTrabajo.Producto.ObtenerUsuario(User.Identity.Name);
            if (usuarioLogueado == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var detallesTemproales = _contenedorTrabajo.Producto.ListaDetallesTemporales(usuarioLogueado);
            foreach (var item in detallesTemproales)
            {
                var valor = _contenedorTrabajo.DetalleProductoTmp.AplicarPropertys(item.DetalleProductoId);
            }
            var lista = new SelectList(_contenedorTrabajo.SubCategoria.GetSubCategoriasCascade(0), nameof(SubCategoria.SubCategoriaId), nameof(SubCategoria.Nombre));
            ProductoVM productoVM = new ProductoVM
            {
                Producto = new Modelos.Producto(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias(),
                ListaSubCategorias = lista,
                ListaMarcas = _contenedorTrabajo.Marca.GetListaMarcas(),
                Detalles = detallesTemproales
            };
            return View(productoVM);
        }

        [HttpPost]
        public IActionResult Create(ProductoVM productoVm)
        {
            var usuarioLogueado = _contenedorTrabajo.Producto.ObtenerUsuario(User.Identity.Name);
            var detallesTemproales = _contenedorTrabajo.Producto.ListaDetallesTemporales(usuarioLogueado);
            productoVm.Detalles = detallesTemproales;

            var lista = new SelectList(_contenedorTrabajo.SubCategoria.GetSubCategoriasCascade(productoVm.Producto.CategoriaId), nameof(SubCategoria.SubCategoriaId), nameof(SubCategoria.Nombre));
            if (!ModelState.IsValid)
            {
                productoVm = new ProductoVM
                {
                    Producto = new Modelos.Producto(),
                    ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias(),
                    ListaSubCategorias = lista,
                    ListaMarcas = _contenedorTrabajo.Marca.GetListaMarcas(),
                    Detalles = detallesTemproales
                };
                foreach (var item in detallesTemproales)
                {
                    var valor = _contenedorTrabajo.DetalleProductoTmp.AplicarPropertys(item.DetalleProductoId);
                }
                return View(productoVm);
            }

            if (detallesTemproales.Count > 0)
            {
                using (var tran = _contenedorTrabajo.Transaction())
                {
                    try
                    {
                        var producto = new Producto
                        {
                            CategoriaId = productoVm.Producto.CategoriaId,
                            SubCategoriaId = productoVm.Producto.SubCategoriaId,
                            MarcaId = productoVm.Producto.MarcaId,
                            Descripcion = productoVm.Producto.Descripcion,
                            Precio = productoVm.Producto.Precio,
                            Estado = true
                        };
                        _contenedorTrabajo.Producto.Agregar(producto);
                        _contenedorTrabajo.Save();

                        string rutaPrincipal = _enviroment.WebRootPath;
                        var archivos = HttpContext.Request.Form.Files;

                        foreach (var item in productoVm.Detalles)
                        {
                            var productoDetalle = new ColorTalle
                            {
                                ColorId = item.ColorId,
                                TalleId = item.TalleId,
                                ProductoId = producto.ProductoId,
                                Cantidad = item.Cantidad
                            };
                            _contenedorTrabajo.ColorTalle.Agregar(productoDetalle);
                            _contenedorTrabajo.DetalleProductoTmp.EliminarPorEntidad(item);
                        }

                        ////subida de las imágenes
                        ///
                        if (productoVm.mysFiles.Count > 0)
                        {
                            foreach (var item in productoVm.mysFiles)
                            {

                                //Guid es un tipo de dato que permite nombres extensos
                                string nombreArchivo = Guid.NewGuid().ToString();
                                //Las imágenes se guardarán en la carpeta articulos que a su evz está dentro de la carpeta imagenes
                                var subidas = Path.Combine(rutaPrincipal, @"imagenes\productos");
                                //Obtenemos la extensión del archivo
                                var extension = Path.GetExtension(archivos[0].FileName);

                                using (var filesStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                                {
                                    archivos[0].CopyTo(filesStreams);
                                }
                                Imagen imagen = new Imagen
                                {
                                    UrlImagen = @"\imagenes\productos\" + nombreArchivo + extension,
                                    ProductoId = producto.ProductoId
                                };
                                _contenedorTrabajo.Imagen.Agregar(imagen);
                                _contenedorTrabajo.Save();
                            }
                        }
                        _contenedorTrabajo.Save();
                        tran.Commit();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        return View(productoVm);
                    }
                }
                
            }
            productoVm = new ProductoVM
            {
                Producto = new Modelos.Producto(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias(),
                ListaSubCategorias =lista,
                ListaMarcas = _contenedorTrabajo.Marca.GetListaMarcas(),
                Detalles = detallesTemproales
            };
            //var lista = new SelectListItem();
            //productoVm.ListaCategorias = new SelectList(productoVm.ListaCategorias, nameof(Categoria.CategoriaId), nameof(Categoria.Nombre));
            //productoVm.ListaSubCategorias = new SelectList(lista, nameof(SubCategoria.SubCategoriaId), nameof(SubCategoria.Nombre));
            return View(productoVm);
        }

        [HttpGet]
        public IActionResult BorrarProducto(int id)
        {
            if (id==0)
            {
                return NotFound();
            }
            DetalleProductoTmp stock = _contenedorTrabajo.DetalleProductoTmp.ObtenerPorId(id);
            _contenedorTrabajo.DetalleProductoTmp.EliminarPorEntidad(stock);
            _contenedorTrabajo.Save();
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult AgregarStock()
        {
            var usuarioLogueado = _contenedorTrabajo.Producto.ObtenerUsuario(User.Identity.Name);
            if (usuarioLogueado == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var agregarStockVm = new AgregarStockVm
            {
                ListaCategoriaTalles = _contenedorTrabajo.CategoriaTalle.GetListaCategoriasPorTalle(),
                ListaTalles = _contenedorTrabajo.Talle.GetTallesCascade(0),
                ListaColores = _contenedorTrabajo.Color.GetListaColores()
            };

            return View(agregarStockVm);
        }


        [HttpPost]
        public ActionResult AgregarStock(AgregarStockVm agregarStockVm)
        {
            var usuarioLogueado = _contenedorTrabajo.Producto.ObtenerUsuario(User.Identity.Name);
            if (ModelState.IsValid)
            {
                var tmp = _contenedorTrabajo.Producto.DetalleActual(agregarStockVm.ColorId, agregarStockVm.TalleId);
                if (tmp == null)
                {
                    tmp = new DetalleProductoTmp
                    {
                        NombreUsuario = usuarioLogueado.UserName,
                        TalleId = agregarStockVm.TalleId,
                        ColorId = agregarStockVm.ColorId,
                        Cantidad = agregarStockVm.Cantidad
                    };
                    _contenedorTrabajo.DetalleProductoTmp.Agregar(tmp);
                }
                else
                {
                    tmp.Cantidad += agregarStockVm.Cantidad;
                }

                _contenedorTrabajo.Save();
                return RedirectToAction("Create");
            }
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public IActionResult Detalles(int id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var producto = _contenedorTrabajo.Producto.ObtenerPorId(id);
            producto = _contenedorTrabajo.Producto.AplicarPropertys(id);
            var stocks = _contenedorTrabajo.ColorTalle.ObtenerProductoRelacionado(id);
            foreach (var item in stocks)
            {
                var valor = _contenedorTrabajo.ColorTalle.AplicarPropertys(item.ColorTalleId);
            }
            ProductoVM productoVM = new ProductoVM
            {
                Producto = producto,
                Stocks = stocks
            };
            if (producto == null)
            {
                return NotFound();
            }
            return View(productoVM);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            ProductoVM producto = new ProductoVM
            {
                Producto = new Modelos.Producto(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias(),
                ListaSub = _contenedorTrabajo.SubCategoria.GetListaSubCategorias(),
                ListaMarcas = _contenedorTrabajo.Marca.GetListaMarcas()
            };
            producto.Producto = _contenedorTrabajo.Producto.ObtenerPorId(id);
            return View(producto);
        }

        public JsonResult GetSubCategorias(int categoriaId)
        {
            var subCategorias = _contenedorTrabajo.SubCategoria.GetSubCategoriasCascade(categoriaId);
            return Json(subCategorias);
        }

        public JsonResult GetTalles(int categoriaId)
        {
            var talles = _contenedorTrabajo.Talle.GetTallesCascade(categoriaId);
            return Json(talles);
        }

        public ActionResult Desactivar(int id)
        {
            if (id==null)
            {
                return NotFound();
            }
            _contenedorTrabajo.Producto.Desactivar(id);
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Activar(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _contenedorTrabajo.Producto.Activar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
