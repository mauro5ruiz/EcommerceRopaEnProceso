using FashionNet.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;

        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Categoria = new CategoriaRepository(_db);
            SubCategoria = new SubCategoriaRepository(_db);
            Marca = new MarcaRepository(_db);
            Usuario = new UsuarioRepository(_db);
            Producto = new ProductoRepository(_db);
            CategoriaTalle = new CategoriaTalleRepository(_db);
            Talle = new TalleRepository(_db);
            Color = new ColorRepository(_db);
            DetalleProductoTmp = new DetalleProductoTmpRepository(_db);
            ColorTalle = new ColorTalleReository(_db);
            Imagen = new ImagenesRepository(_db);
        }


        public ICategoria Categoria { get; private set; }
        public ISubCategoria SubCategoria { get; private set; }
        public IMarca Marca { get; private set; }
        public IUsuario Usuario { get; private set; }
        public IProducto Producto { get; private set; }
        public ICategoriaTalle CategoriaTalle { get; private set; }
        public ITalle Talle { get; private set; }
        public IColores Color { get; private set; }
        public IDetalleProductoTmp DetalleProductoTmp { get; private set; }

        public IColorTalle ColorTalle { get; private set; }
        public IImagenes Imagen { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IDbContextTransaction Transaction()
        {
            return _db.Database.BeginTransaction();
        }
    }
}
