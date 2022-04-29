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
    public class DetalleProductoTmpRepository : Repository<DetalleProductoTmp>, IDetalleProductoTmp
    {
        private readonly ApplicationDbContext dbSet;

        public DetalleProductoTmpRepository(ApplicationDbContext db) : base(db)
        {
            dbSet = db;
        }

        public DetalleProductoTmp AplicarPropertys(int id)
        {
            return dbContext.DetalleProductoTmps.Include(dpt => dpt.Color).Include(dpt => dpt.Talle).FirstOrDefault(s => s.DetalleProductoId == id);
        }

    }
}
