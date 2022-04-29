using FashionNet.Modelos;
using FashionNet.Modelos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository.Interfaces
{
    public interface IProducto: IRepository<Producto>
    {
        void Update(Producto producto);
        List<DetalleProductoTmp> ListaDetallesTemporales(Usuario usuario);
        Usuario ObtenerUsuario(string usuario);
        DetalleProductoTmp ObtenerActual(Usuario usuario, AgregarStockVm stock);
        void Activar(int id);
        void Desactivar(int id);
        Producto AplicarPropertys(int id);
        List<Imagen> ObtenerImagenesDeCadaProducto(int id);
        List<ColorTalle> ObtenerInformacionDelProducto(int id);
        Imagen ObtenerPrimerImagen(int id);
        DetalleProductoTmp DetalleActual(int colorId, int talleId);
    }
}
