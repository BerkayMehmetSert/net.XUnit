using System.Linq.Expressions;
using Core.Persistence.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace Core.Test.Helpers;

public class MockRepositoryHelper
{
    public static Mock<TRepository> GetRepository<TRepository, TEntity>(List<TEntity> list)
        where TEntity : BaseEntity
        where TRepository : class, IBaseRepository<TEntity>
    {
        var mockRepo = new Mock<TRepository>();

        Build(mockRepo, list);
        return mockRepo;
    }

    private static void Build<TRepository, TEntity>(Mock<TRepository> mockRepo, List<TEntity> entityList)
        where TEntity : BaseEntity
        where TRepository : class, IBaseRepository<TEntity>
    {
        SetupGetAllAsync(mockRepo, entityList);
        SetupGetAsync(mockRepo, entityList);
        SetupAddAsync(mockRepo, entityList);
        SetupUpdateAsync(mockRepo, entityList);
        SetupDeleteAsync(mockRepo, entityList);
    }

    private static void SetupGetAllAsync<TRepository, TEntity>(Mock<TRepository> mockRepo, List<TEntity> entityList)
        where TEntity : BaseEntity
        where TRepository : class, IBaseRepository<TEntity>
    {
        mockRepo
            .Setup(
                s =>
                    s.GetAllAsync(
                        It.IsAny<Expression<Func<TEntity, bool>>>(),
                        It.IsAny<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(),
                        It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>(),
                        It.IsAny<bool>()
                    )
            )
            .ReturnsAsync(
                (
                    Expression<Func<TEntity, bool>> expression,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                    bool enableTracking
                ) =>
                {
                    var list = entityList;

                    if (expression is not null)
                    {
                        list = entityList.Where(predicate: expression.Compile()).ToList();
                    }

                    return orderBy is not null
                        ? orderBy(list.AsQueryable()).ToList()
                        : list;
                }
            );
    }

    private static void SetupGetAsync<TRepository, TEntity>(Mock<TRepository> mockRepo, List<TEntity> entityList)
        where TEntity : BaseEntity
        where TRepository : class, IBaseRepository<TEntity>
    {
        mockRepo
            .Setup(
                s =>
                    s.GetAsync(
                        It.IsAny<Expression<Func<TEntity, bool>>>(),
                        It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>(),
                        It.IsAny<bool>()
                    )
            )
            .ReturnsAsync(
                (
                    Expression<Func<TEntity, bool>> expression,
                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                    bool enableTracking
                ) => entityList.FirstOrDefault(predicate: expression.Compile()));
    }

    private static void SetupAddAsync<TRepository, TEntity>(Mock<TRepository> mockRepo,
        ICollection<TEntity> entityList)
        where TEntity : BaseEntity
        where TRepository : class, IBaseRepository<TEntity>
    {
        mockRepo
            .Setup(r => r.AddAsync(It.IsAny<TEntity>()))
            .ReturnsAsync(
                (TEntity entity) =>
                {
                    entityList.Add(entity);
                    return entity;
                }
            );
    }

    private static void SetupUpdateAsync<TRepository, TEntity>(Mock<TRepository> mockRepo,
        List<TEntity> entityList)
        where TEntity : BaseEntity
        where TRepository : class, IBaseRepository<TEntity>
    {
        mockRepo
            .Setup(r => r.UpdateAsync(It.IsAny<TEntity>()))!
            .ReturnsAsync(
                (TEntity entity) =>
                {
                    var result = entityList.FirstOrDefault(x => x.Id!.Equals(entity.Id));

                    return result ?? entity;
                }
            );
    }

    private static void SetupDeleteAsync<TRepository, TEntity>(Mock<TRepository> mockRepo,
        ICollection<TEntity> entityList)
        where TEntity : BaseEntity
        where TRepository : class, IBaseRepository<TEntity>
    {
        mockRepo
            .Setup(r => r.DeleteAsync(It.IsAny<TEntity>()))
            .ReturnsAsync(
                (TEntity entity) =>
                {
                    entityList.Remove(entity);
                    return entity;
                }
            );
    }
}