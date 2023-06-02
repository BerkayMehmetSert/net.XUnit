using Core.Test.FakeData;
using Domain.Entities;

namespace Application.Test.Mocks.FakeData;

public class ProductFakeData : BaseFakeData<Product>
{
    public override List<Product> CreateFakeData()
    {
        return new List<Product>()
        {
            new()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "Product 1",
                Description = "Product 1 Description",
                Price = 100
            },

            new()
            {
                Id = new Guid("22222222-2222-2222-2222-222222222222"),
                Name = "Product 2",
                Description = "Product 2 Description",
                Price = 200
            },
        };
    }
}