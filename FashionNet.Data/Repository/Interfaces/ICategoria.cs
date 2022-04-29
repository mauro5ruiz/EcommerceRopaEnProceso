using FashionNet.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository.Interfaces
{
    public interface ICategoria : IRepository<Categoria>
    {
        IEnumerable<SelectListItem> GetListaCategorias();

        void Editar(Categoria categoria);
    }
}
