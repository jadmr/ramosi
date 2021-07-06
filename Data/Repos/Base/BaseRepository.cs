using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos.Base
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly RamosiContext context;
        private DbSet<T> entities;

        protected BaseRepository(RamosiContext context)
        {
            this.context = context;
            entities = this.context.Set<T>();
        }

        public T Create(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"Entity of type {nameof(T)} cannot be null");

            entities.Add(entity);
            return entity;
        }

        public void Delete(Guid guid)
        {
            var entity = Get(guid);

            if (entity == null)
                throw new ArgumentException($"No entity of type {nameof(T)} with guid {guid} was found");

            entities.Remove(entity);
        }

        public T Edit(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Cannot edit null entity");

            var original = Get(entity.Guid);

            if (original == null)
                throw new ArgumentException($"No entity of type {nameof(T)} with guid {entity.Guid} was found");

            UpdateOriginalEntityFields(original, entity);
            entities.Update(original);

            return original;
        }

        public T Get(Guid guid)
        {
            return entities.FirstOrDefault(x => x.Guid == guid);
        }

        public IEnumerable<T> Get()
        {
            return entities.AsEnumerable();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        protected abstract void UpdateOriginalEntityFields(T original, T edited);
    }
}