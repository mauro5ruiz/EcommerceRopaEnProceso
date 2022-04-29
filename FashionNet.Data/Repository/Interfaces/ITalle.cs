using FashionNet.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository.Interfaces
{
    public interface ITalle : IRepository<Talle>
    {
        IEnumerable<SelectListItem> GetListaTalles();
        List<Talle> GetTallesCascade(int id);
    }
}
