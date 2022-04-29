using FashionNet.Data.Repository.Interfaces;
using FashionNet.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository
{
    public class SubCategoriaRepository : Repository<SubCategoria>, ISubCategoria
    {
        private readonly ApplicationDbContext dbSet;
        internal DbSet<SubCategoria> db;

        public SubCategoriaRepository(ApplicationDbContext db) : base(db)
        {
            dbSet = db;
            this.db = dbSet.Set<SubCategoria>();
        }

        public void Update(SubCategoria subCategoria)
        {
            var objDesdeDb = dbSet.SubCategorias.FirstOrDefault(s => s.SubCategoriaId == subCategoria.SubCategoriaId);
            objDesdeDb.Nombre = subCategoria.Nombre;
            objDesdeDb.CategoriaId = subCategoria.CategoriaId;

            dbSet.SaveChanges();
        }

        public SubCategoria AplicarPropertys(int id)
        {
            return dbSet.SubCategorias.Include(c => c.Categoria).FirstOrDefault(s => s.SubCategoriaId == id);
        }

        public IEnumerable<SelectListItem> GetListaSubCategorias()
        {
            return dbSet.SubCategorias.Select(c => new SelectListItem()
            {
                Text = c.Nombre,
                Value = c.SubCategoriaId.ToString()
            });
        }

        public List<SubCategoria> GetSubCategoriasCascade(int id)
        {
            return dbSet.SubCategorias.Where(s => s.CategoriaId == id).ToList();
        }
    }
}
