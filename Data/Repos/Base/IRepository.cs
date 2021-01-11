using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Entities;

namespace Data.Repos.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(Guid guid);
        IEnumerable<T> Get();
        T Create(T entity);
        T Edit(T entity);
        void Delete(Guid guid);
        Task SaveChanges();
    }
}
