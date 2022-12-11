namespace Document.Repositories
{
        public interface IRepository<TEntity>
        where TEntity : class, IEntity
        {
            /// <summary>
            /// Gets a list of all <typeparamref name="TEntity"/>.
            /// </summary>
            Task<IReadOnlyList<TEntity>> GetAllAsync();

            /// <summary>
            /// Gets a <typeparamref name="TEntity"/> by identifier.
            /// </summary>
            Task<TEntity> GetByIdAsync(object id);

            /// <summary>
            /// Add new <typeparamref name="TEntity"/> to the collection.
            /// </summary>
            Task<TEntity> AddAsync(TEntity entity);

            /// <summary>
            /// Update a <typeparamref name="TEntity"/> in the collection.
            /// </summary>
            Task<TEntity> UpdateAsync(TEntity entity);

            /// <summary>
            /// Remove a <typeparamref name="TEntity"/> from the collection.
            /// </summary>
            Task<TEntity> RemoveAsync(object id);

            /// <summary>
            /// Remove a <typeparamref name="TEntity"/> from the collection.
            /// </summary>
            Task<TEntity> RemoveAsync(TEntity entity);
        }
    }

