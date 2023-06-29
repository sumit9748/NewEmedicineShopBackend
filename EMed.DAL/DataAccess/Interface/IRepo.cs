using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Emedicine.DAL.DataAccess.Interface
{
    public interface IRepo<T> where T : class
    {
        public void AddAsync(T entity);
        public void Remove(T entity);
        public void RemoveRange(IEnumerable<T> entities);
        void UpdateExisting(T entity);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAllListAsync(Expression<Func<T, bool>> filter);

    }
}
