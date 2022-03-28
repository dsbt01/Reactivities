using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.iRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, string? includeProperties = null);

        IEnumerable<T> GetAll(string? includeProperties = null);

        void Add(T entity);

        T GetFirstOrDefault(Expression<Func<T,bool>> filter, string? includeProperties = null);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
