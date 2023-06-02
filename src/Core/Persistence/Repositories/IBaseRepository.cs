using System.Linq.Expressions;
using Domain.Common;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistence.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T?> GetAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool enableTracking = true
    );

    Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool enableTracking = true
    );
    
    Task<T> AddAsync(T entity);

    Task<ICollection<T>> AddRangeAsync(ICollection<T> entities);

    Task<T> UpdateAsync(T entity);

    Task<ICollection<T>> UpdateRangeAsync(ICollection<T> entities);

    Task<T> DeleteAsync(T entity);

    Task<ICollection<T>> DeleteRangeAsync(ICollection<T> entities);
}