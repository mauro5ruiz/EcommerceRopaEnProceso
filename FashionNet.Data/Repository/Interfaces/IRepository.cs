using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T ObtenerPorId(int id);

        IEnumerable<T> MostrarTodos(Expression<Func<T, bool>> filter = null, string includeProperties = null);

        void Agregar(T entity);

        void EliminarPorEntidad(T entity);
    }
}
