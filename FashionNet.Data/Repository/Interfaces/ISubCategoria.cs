using FashionNet.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository.Interfaces
{
    public interface ISubCategoria: IRepository<SubCategoria>
    {
        void Update(SubCategoria subCategoria);
        SubCategoria AplicarPropertys(int id);
        IEnumerable<SelectListItem> GetListaSubCategorias();
        List<SubCategoria> GetSubCategoriasCascade(int id);
    }
}
