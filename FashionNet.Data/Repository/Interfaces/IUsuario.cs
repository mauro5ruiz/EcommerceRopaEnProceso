using FashionNet.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository.Interfaces
{
    public interface IUsuario : IRepository<Usuario>
    {
        public void Bloquear(string idUsuario);
        public void Desbloquear(string idUsuario);
    }
}
