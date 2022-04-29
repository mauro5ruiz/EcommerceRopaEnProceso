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
    public class MarcaRepository : Repository<Marca>, IMarca
    {
        private readonly ApplicationDbContext dbContext;

        public MarcaRepository(ApplicationDbContext db) : base(db)
        {
            dbContext = db;
        }

        public void Update(Marca marca)
        {
            var marcaDesdeBd = dbContext.Marcas.FirstOrDefault(m => m.MarcaId == marca.MarcaId);
            marcaDesdeBd.Nombre = marca.Nombre;
            marcaDesdeBd.UrlImagen = marca.UrlImagen;

            dbContext.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetListaMarcas()
        {
            return dbContext.Marcas.Select(c => new SelectListItem()
            {
                Text = c.Nombre,
                Value = c.MarcaId.ToString()
            });
        }
    }
}
