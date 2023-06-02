using Application.Contracts.Requests;
using Application.Contracts.Responses;

namespace Application.Contracts.Services;

public interface IProductService
{
    Task CreateProduct(CreateProductRequest request);
    Task UpdateProduct(Guid id, UpdateProductRequest request);
    Task DeleteProduct(Guid id);
    Task<ProductResponse> GetProductById(Guid id);
    Task<ProductResponse> GetProductByName(string name);
    Task<List<ProductResponse>> GetAllProducts();
}