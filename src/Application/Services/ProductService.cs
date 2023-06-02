using Application.Contracts.Repositories;
using Application.Contracts.Requests;
using Application.Contracts.Responses;
using Application.Contracts.Services;
using AutoMapper;
using Core.Exceptions.Types;
using Domain.Entities;
using static Application.Contracts.Messages.ProductBusinessMessages;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task CreateProduct(CreateProductRequest request)
    {
        await CheckIfProductExistsByName(request.Name);

        var product = _mapper.Map<Product>(request);

        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProduct(Guid id, UpdateProductRequest request)
    {
        var product = await GetProductEntity(id);

        if (!string.Equals(product.Name, request.Name, StringComparison.OrdinalIgnoreCase))
        {
            await CheckIfProductExistsByName(request.Name);
        }

        var updatedProduct = _mapper.Map(request, product);

        await _productRepository.UpdateAsync(updatedProduct);
    }

    public async Task DeleteProduct(Guid id)
    {
        var product = await GetProductEntity(id);

        await _productRepository.DeleteAsync(product);
    }

    public async Task<ProductResponse> GetProductById(Guid id)
    {
        var product = await GetProductEntity(id);

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> GetProductByName(string name)
    {
        var product = await _productRepository.GetAsync(x => x.Name.Equals(name));

        return product is not null 
            ? _mapper.Map<ProductResponse>(product)
            : throw new NotFoundException(ProductNotFoundByName);
    }

    public async Task<List<ProductResponse>> GetAllProducts()
    {
        var products = await _productRepository.GetAllAsync();
        
        return _mapper.Map<List<ProductResponse>>(products);
    }

    private async Task CheckIfProductExistsByName(string name)
    {
        var product = await _productRepository.GetAsync(x => x.Name.Equals(name));

        if (product is not null)
        {
            throw new BusinessException(ProductAlreadyExistsByName);
        }
    }

    private async Task<Product> GetProductEntity(Guid id)
    {
        var product = await _productRepository.GetAsync(x => x.Id.Equals(id));

        return product ?? throw new NotFoundException(ProductNotFoundById);
    }
}