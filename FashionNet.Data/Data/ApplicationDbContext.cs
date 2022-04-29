using FashionNet.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FashionNet.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<SubCategoria> SubCategorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Talle> Talles { get; set; }
        public DbSet<ColorTalle> ColoresTalles { get; set; }
        public DbSet<CategoriaTalle> CategoriaTalles { get; set; }
        public DbSet<DetalleProductoTmp> DetalleProductoTmps { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
    }
}
