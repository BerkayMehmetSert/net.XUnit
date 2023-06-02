using Domain.Common;

namespace Core.Test.FakeData;

public abstract class BaseFakeData<TEntity> where TEntity : BaseEntity
{
    public List<TEntity> Data => CreateFakeData();
    public abstract List<TEntity> CreateFakeData();
}