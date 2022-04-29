using FashionNet.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository.Interfaces
{
    public interface IColorTalle : IRepository<ColorTalle>
    {
        List<ColorTalle> ObtenerProductoRelacionado(int id);
        ColorTalle AplicarPropertys(int id);
        List<ColorTalle> GetColoresTalles(int id);
    }
}
