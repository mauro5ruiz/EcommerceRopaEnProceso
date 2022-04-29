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
    public class CategoriaRepository : Repository<Categoria>, ICategoria
    {
        private readonly ApplicationDbContext dbSet;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            dbSet = db;
        }

        public void Editar(Categoria categoria)
        {
            var objDesdeDb = dbSet.Categorias.FirstOrDefault(s => s.CategoriaId == categoria.CategoriaId);
            objDesdeDb.Nombre = categoria.Nombre;

            dbSet.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return dbSet.Categorias.Select(c => new SelectListItem()
            {
                Text = c.Nombre,
                Value = c.CategoriaId.ToString()
            });
        }
    }
}
