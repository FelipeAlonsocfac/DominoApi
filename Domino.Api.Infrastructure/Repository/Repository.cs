using Domino.Api.Infrastructure.DataAccess;
using Domino.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domino.Api.Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected DominoDBContext _context;

    public Repository(DominoDBContext context)
    {
        _context = context;
        _context.ChangeTracker.LazyLoadingEnabled = true;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task InsertAsync(T entity)
    {
        entity.GetType().GetProperty("CreatedAt")!.SetValue(entity, DateTime.UtcNow);

        await _context.AddAsync(entity);
        _context.Entry(entity).State = EntityState.Added;

        await _context.SaveChangesAsync();
    }

    public IQueryable<T> GetAll()
    {
        var entitySet = _context.Set<T>();

        return entitySet.AsQueryable();
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> where)
    {
        return GetAll().Where(where);
    }

    public Task<T?> GetFirst(Expression<Func<T, bool>> predicate)
    {
        var entitySet = _context.Set<T>();

        return entitySet.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        entity.GetType().GetProperty("UpdatedAt")!.SetValue(entity, DateTime.UtcNow);
        _context.Update(entity);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _context.Remove(entity);

        return await _context.SaveChangesAsync() > 0;
    }
}