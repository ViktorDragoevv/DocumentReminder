using Document.Data;
using Microsoft.EntityFrameworkCore;

namespace Document.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        protected Repository(ApplicationDbContext context)
        {
            Context = context;
            EntitySet = context.Set<TEntity>();
        }

        /// <summary>Gets application context.</summary>
        protected ApplicationDbContext Context { get; private set; }

        /// <summary>Gets the database set.</summary>
        protected DbSet<TEntity> EntitySet { get; private set; }

        /// <inheritdoc />
        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            var entities = await EntitySet.AsNoTracking().ToListAsync();
            return entities;
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            var entity = await EntitySet.FindAsync(id);
            return entity;
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await EntitySet.AddAsync(entity);
            return await SaveAsync(entry.Entity);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var entry = EntitySet.Update(entity);
            return await SaveAsync(entry.Entity);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> RemoveAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            return await RemoveAsync(entity);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> RemoveAsync(TEntity entity)
        {
            var entry = EntitySet.Remove(entity);
            return await SaveAsync(entry.Entity);
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        protected virtual async Task<TEntity> SaveAsync(TEntity entity)
        {
            var rows = await Context.SaveChangesAsync();
            return rows != 0 ? entity : null;
        }
    }
}
