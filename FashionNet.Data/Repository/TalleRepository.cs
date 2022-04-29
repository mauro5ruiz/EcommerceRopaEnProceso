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
    public class TalleRepository : Repository<Talle>, ITalle
    {
        private readonly ApplicationDbContext dbSet;

        public TalleRepository(ApplicationDbContext db) : base(db)
        {
            dbSet = db;
        }

        public IEnumerable<SelectListItem> GetListaTalles()
        {
            return dbSet.Talles.Select(c => new SelectListItem()
            {
                Text = c.NombreTalle,
                Value = c.TalleId.ToString()
            });
        }

        public List<Talle> GetTallesCascade(int id)
        {
            return dbSet.Talles.Where(s => s.CategoriaTalleId == id).ToList();
        }
    }
}
