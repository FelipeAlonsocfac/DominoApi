using System.Linq.Expressions;

namespace Domino.Api.Infrastructure.Interfaces;

public interface IRepository<T>
{

    /// <summary>
    /// Inserts the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task InsertAsync(T entity);

    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <returns>An <see cref="IQueryable"/>&lt;<typeparamref name="T"/>&gt;.</returns>
    IQueryable<T> GetAll();

    /// <summary>
    /// Gets all asynchronous with expression.
    /// </summary>
    /// <param name="where">The where.</param>
    /// <returns>An <see cref="IQueryable"/>&lt;<typeparamref name="T"/>&gt;.</returns>
    IQueryable<T> GetAll(Expression<Func<T, bool>> where);

    /// <summary>
    /// Get first entity by predicate.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns>A <see cref="Task"/>&lt;<typeparamref name="T"/>?&gt; representing the asynchronous operation.</returns>
    Task<T?> GetFirst(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A <see cref="Task"/>&lt;<see cref="bool"/>&gt; representing the asynchronous operation.<br/>
    /// <c>true</c> if updated, <c>false</c> otherwise.</returns>
    Task<bool> UpdateAsync(T entity);

    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A <see cref="Task"/>&lt;<see cref="bool"/>&gt; representing the asynchronous operation.<br/>
    /// <c>true</c> if deleted, <c>false</c> otherwise.</returns>
    Task<bool> DeleteAsync(T entity);
}