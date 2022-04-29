using FashionNet.Data.Repository.Interfaces;
using FashionNet.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository
{
    public class ImagenesRepository : Repository<Imagen>, IImagenes
    {
        private readonly ApplicationDbContext dbSet;

        public ImagenesRepository(ApplicationDbContext db) : base(db)
        {
            dbSet = db;
        }
    }
}
