using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = "", int? take = null, int? skip = null);
        Task<T> Get(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        Task<T> GetById(int Id);
        Task Insert(T entity);
        void Update(T entity);
        Task Delete(int Id);
        Task<bool> Exists(int Id);
    }
}
