using Application.Contracts.Mapper;
using Application.Contracts.Repositories;
using Application.Test.Mocks.FakeData;
using Core.Test.Repositories;
using Domain.Entities;

namespace Application.Test.Mocks.Repositories;

public class ProductMockRepository : BaseMockRepository<IProductRepository, Product, ProductMapper, ProductFakeData>
{
    public ProductMockRepository(ProductFakeData fakeData) : base(fakeData)
    {
    }
}