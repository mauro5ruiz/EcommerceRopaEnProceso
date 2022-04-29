using FashionNet.Data.Repository.Interfaces;
using FashionNet.Modelos;
using FashionNet.Modelos.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository
{
    public class ProductoRepository : Repository<Producto>,IProducto
    {
        private readonly ApplicationDbContext dbContext;

        public ProductoRepository(ApplicationDbContext db) : base(db)
        {
            dbContext = db;
        }
        
        public void Update(Producto producto)
        {
            var objDesdeDb = dbContext.Productos.FirstOrDefault(s => s.ProductoId == producto.ProductoId);
            objDesdeDb.Descripcion = producto.Descripcion;
            objDesdeDb.Estado = producto.Estado;
            objDesdeDb.CategoriaId = producto.CategoriaId;
            objDesdeDb.CategoriaId = producto.SubCategoriaId;
            objDesdeDb.CategoriaId = producto.MarcaId;

            dbContext.SaveChanges();
        }

        public List<DetalleProductoTmp> ListaDetallesTemporales(Usuario usuario)
        {
            return dbContext.DetalleProductoTmps.Where(s => s.NombreUsuario == usuario.Email).ToList();
        }

        public Usuario ObtenerUsuario(string usuario)
        {
            return dbContext.Usuarios.FirstOrDefault(u => u.Email == usuario);
        }

        public DetalleProductoTmp ObtenerActual(Usuario usuario, AgregarStockVm stock)
        {
            return dbContext.DetalleProductoTmps.FirstOrDefault(dpt => dpt.NombreUsuario == usuario.UserName);
        }

        public void Activar(int id)
        {
            var productoDesdeBd = dbContext.Productos.FirstOrDefault(u => u.ProductoId == id);
            productoDesdeBd.Estado = true;
            dbContext.SaveChanges();
        }

        public void Desactivar(int id)
        {
            var productoDesdeBd = dbContext.Productos.FirstOrDefault(u => u.ProductoId == id);
            productoDesdeBd.Estado = false;
            dbContext.SaveChanges();
        }

        public Producto AplicarPropertys(int id)
        {
            return dbContext.Productos.Include(p => p.SubCategoria).Include(p => p.Categoria).Include(p => p.Marca).FirstOrDefault(s => s.ProductoId == id);
        }

        public List<Imagen> ObtenerImagenesDeCadaProducto(int id)
        {
            return dbContext.Imagen.Where(i => i.ProductoId == id).ToList();
        }

        public List<ColorTalle> ObtenerInformacionDelProducto(int id)
        {
            List<ColorTalle> listaColoresTalles = new List<ColorTalle>();
            var coloresTalles = dbContext.ColoresTalles.Include(ct=>ct.Talle).Include(ct => ct.Color).Include(ct => ct.Producto).Where(ct => ct.ProductoId == id).ToList();
            return coloresTalles;
        }

        public Imagen ObtenerPrimerImagen(int id)
        {
            return dbContext.Imagen.FirstOrDefault(i => i.ProductoId == id);
        }

        public DetalleProductoTmp DetalleActual(int colorId, int talleId)
        {
            return dbContext.DetalleProductoTmps.FirstOrDefault(dpt => dpt.ColorId == colorId && dpt.TalleId == talleId);
        }
    }
}
