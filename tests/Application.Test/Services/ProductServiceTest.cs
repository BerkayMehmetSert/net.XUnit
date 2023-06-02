using Application.Contracts.Messages;
using Application.Contracts.Requests;
using Application.Services;
using Application.Test.Mocks.FakeData;
using Application.Test.Mocks.Repositories;
using Core.Exceptions.Types;
using Xunit;

namespace Application.Test.Services;

public class ProductServiceTest : ProductMockRepository
{
    private readonly ProductService _productService;

    public ProductServiceTest(ProductFakeData fakeData) : base(fakeData)
    {
        _productService = new ProductService(MockRepository.Object, Mapper);
    }

    [Fact]
    public void CreateProduct_ShouldReturnSuccess()
    {
        var request = new CreateProductRequest
        {
            Name = "Product Test 1",
            Description = "Product Test",
            Price = 100
        };

        var result = _productService.CreateProduct(request);

        Assert.NotNull(result);
        Assert.True(result.IsCompletedSuccessfully);
    }

    [Fact]
    public void CreateProduct_WhenProductExistsByName_ShouldThrowBusinessException()
    {
        var request = new CreateProductRequest
        {
            Name = "Product 1",
            Description = "Product 1",
            Price = 100
        };

        var exception = Assert.ThrowsAsync<BusinessException>(() => _productService.CreateProduct(request));

        Assert.NotNull(exception);
        Assert.Equal(ProductBusinessMessages.ProductAlreadyExistsByName, exception.Result.Message);
    }

    [Fact]
    public void UpdateProduct_ShouldReturnSuccess()
    {
        var request = new UpdateProductRequest
        {
            Name = "Product Test 1",
            Description = "Product Test 1",
            Price = 100
        };

        var id = new Guid("11111111-1111-1111-1111-111111111111");

        var result = _productService.UpdateProduct(id, request);

        Assert.NotNull(result);
        Assert.True(result.IsCompletedSuccessfully);
    }

    [Fact]
    public void UpdateProduct_WhenProductNotExists_ShouldThrowNotFoundException()
    {
        var request = new UpdateProductRequest
        {
            Name = "Product Test 1",
            Description = "Product Test 1",
            Price = 100
        };

        var id = new Guid("33333333-3333-3333-3333-333333333333");

        var exception = Assert.ThrowsAsync<NotFoundException>(() => _productService.UpdateProduct(id, request));

        Assert.NotNull(exception);
        Assert.Equal(ProductBusinessMessages.ProductNotFoundById, exception.Result.Message);
    }

    [Fact]
    public void UpdateProduct_WhenProductExistsByName_ShouldThrowBusinessException()
    {
        var request = new UpdateProductRequest
        {
            Name = "Product 2",
            Description = "Product 2",
            Price = 100
        };

        var id = new Guid("11111111-1111-1111-1111-111111111111");

        var exception = Assert.ThrowsAsync<BusinessException>(() => _productService.UpdateProduct(id, request));

        Assert.NotNull(exception);
        Assert.Equal(ProductBusinessMessages.ProductAlreadyExistsByName, exception.Result.Message);
    }

    [Fact]
    public void DeleteProduct_ShouldReturnSuccess()
    {
        var id = new Guid("11111111-1111-1111-1111-111111111111");

        var result = _productService.DeleteProduct(id);

        Assert.NotNull(result);
        Assert.True(result.IsCompletedSuccessfully);
    }

    [Fact]
    public void DeleteProduct_WhenProductNotExists_ShouldThrowNotFoundException()
    {
        var id = new Guid("33333333-3333-3333-3333-333333333333");

        var exception = Assert.ThrowsAsync<NotFoundException>(() => _productService.DeleteProduct(id));

        Assert.NotNull(exception);
        Assert.Equal(ProductBusinessMessages.ProductNotFoundById, exception.Result.Message);
    }

    [Fact]
    public void GetProductById_ShouldReturnSuccess()
    {
        var id = new Guid("11111111-1111-1111-1111-111111111111");

        var result = _productService.GetProductById(id);

        Assert.NotNull(result.Result);
        Assert.Equal(id, result.Result.Id);
        Assert.True(result.IsCompletedSuccessfully);
    }

    [Fact]
    public void GetProductById_WhenProductNotExists_ShouldThrowNotFoundException()
    {
        var id = new Guid("33333333-3333-3333-3333-333333333333");

        var exception = Assert.ThrowsAsync<NotFoundException>(() => _productService.GetProductById(id));

        Assert.NotNull(exception);
        Assert.Equal(ProductBusinessMessages.ProductNotFoundById, exception.Result.Message);
    }

    [Fact]
    public void GetProductByName_ShouldReturnSuccess()
    {
        const string name = "Product 1";

        var result = _productService.GetProductByName(name);

        Assert.NotNull(result.Result);
        Assert.Equal(name, result.Result.Name);
        Assert.True(result.IsCompletedSuccessfully);
    }

    [Fact]
    public void GetProductByName_WhenProductNotExists_ShouldThrowNotFoundException()
    {
        const string name = "Product 3";

        var exception = Assert.ThrowsAsync<NotFoundException>(() => _productService.GetProductByName(name));

        Assert.NotNull(exception);
        Assert.Equal(ProductBusinessMessages.ProductNotFoundByName, exception.Result.Message);
    }

    [Fact]
    public void GetAllProducts_ShouldReturnSuccess()
    {
        var result = _productService.GetAllProducts();

        Assert.NotNull(result.Result);
        Assert.Equal(2, result.Result.Count);
        Assert.True(result.IsCompletedSuccessfully);
    }
}