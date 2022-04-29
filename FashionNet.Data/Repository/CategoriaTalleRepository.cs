using FashionNet.Data.Repository.Interfaces;
using FashionNet.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository
{
    public class CategoriaTalleRepository : Repository<CategoriaTalle>, ICategoriaTalle
    {
        private readonly ApplicationDbContext dbSet;

        public CategoriaTalleRepository(ApplicationDbContext db) : base(db)
        {
            dbSet = db;
        }

        public IEnumerable<SelectListItem> GetListaCategoriasPorTalle()
        {
            return dbSet.CategoriaTalles.Select(c => new SelectListItem()
            {
                Text = c.Nombre,
                Value = c.CategoriaTalleId.ToString()
            });
        }
    }
}
