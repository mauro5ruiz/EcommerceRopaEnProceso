using FashionNet.Data.Repository.Interfaces;
using FashionNet.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository
{
    public class ColorTalleReository : Repository<ColorTalle>, IColorTalle
    {
        private readonly ApplicationDbContext dbSet;

        public ColorTalleReository(ApplicationDbContext db) : base(db)
        {
            dbSet = db;
        }

        public List<ColorTalle> ObtenerProductoRelacionado(int id)
        {
            return dbSet.ColoresTalles.Where(ct => ct.ProductoId == id).ToList();
        }

        public ColorTalle AplicarPropertys(int id)
        {
            return dbSet.ColoresTalles.Include(dpt => dpt.Color).Include(dpt => dpt.Talle).FirstOrDefault(s => s.ColorTalleId == id);
        }

        public List<ColorTalle> GetColoresTalles(int id)
        {
            List<int> colores = new List<int>();
            List<ColorTalle> listaColoresTalles = new List<ColorTalle>();
            var ct= dbContext.ColoresTalles.FirstOrDefault(ct=>ct.ColorTalleId == id);
            int productoId = ct.ProductoId;
            int talleId = ct.TalleId;
            var coloresTalles = dbContext.ColoresTalles.Include(ct=>ct.Color).Where(ct => ct.ProductoId == productoId && ct.TalleId == talleId).ToList();
            return coloresTalles;
        }

    }
}
