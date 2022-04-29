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
    public class ColorRepository : Repository<Color>, IColores
    {
        private readonly ApplicationDbContext dbSet;

        public ColorRepository(ApplicationDbContext db) : base(db)
        {
            dbSet = db;
        }

        public IEnumerable<SelectListItem> GetListaColores()
        {
            return dbSet.Colores.Select(c => new SelectListItem()
            {
                Text = c.Nombre,
                Value = c.ColorId.ToString()
            });
        }
    }
}
