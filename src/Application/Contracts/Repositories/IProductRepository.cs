using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
}