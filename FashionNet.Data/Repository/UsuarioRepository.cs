using FashionNet.Data.Repository.Interfaces;
using FashionNet.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuario
    {
        private readonly ApplicationDbContext _db;
        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Bloquear(string idUsuario)
        {
            var usuarioDesdeBd = _db.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            usuarioDesdeBd.LockoutEnd = DateTime.Now.AddYears(120);
            _db.SaveChanges();
        }

        public void Desbloquear(string idUsuario)
        {
            var usuarioDesdeBd = _db.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            usuarioDesdeBd.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
