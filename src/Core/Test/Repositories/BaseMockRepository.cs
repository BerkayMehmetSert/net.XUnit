using AutoMapper;
using Core.Persistence.Repositories;
using Core.Test.FakeData;
using Core.Test.Helpers;
using Domain.Common;
using Moq;

namespace Core.Test.Repositories;

public class BaseMockRepository<TRepository, TEntity, TMapper, TFakeData>
    where TRepository : class, IBaseRepository<TEntity>
    where TEntity : BaseEntity, new()
    where TMapper : Profile, new()
    where TFakeData : BaseFakeData<TEntity>
{
    public IMapper Mapper;
    public Mock<TRepository> MockRepository;
    
    public BaseMockRepository(TFakeData fakeData)
    {
        MapperConfiguration mapperConfig =
            new(c =>
            {
                c.AddProfile<TMapper>();
            });
        Mapper = mapperConfig.CreateMapper();

        MockRepository = MockRepositoryHelper.GetRepository<TRepository, TEntity>(fakeData.Data);
    }
}