using FashionNet.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository.Interfaces
{
    public interface IDetalleProductoTmp : IRepository<DetalleProductoTmp>
    {
        DetalleProductoTmp AplicarPropertys(int id);
    }
}
