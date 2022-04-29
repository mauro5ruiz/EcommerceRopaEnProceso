using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository.Interfaces
{
    public interface IContenedorTrabajo : IDisposable
    {
        ICategoria Categoria { get; }
        ISubCategoria SubCategoria { get; }
        IMarca Marca { get; }
        IUsuario Usuario { get; }
        IProducto Producto { get; }
        ICategoriaTalle CategoriaTalle { get; }
        ITalle Talle { get; }
        IColores Color { get; }
        IDetalleProductoTmp DetalleProductoTmp { get; }
        IColorTalle ColorTalle { get; }
        IImagenes Imagen { get; }
        void Save();
        IDbContextTransaction Transaction();
    }
}
