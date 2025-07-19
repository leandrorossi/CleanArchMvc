using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces;

public interface IProductRepository
{
    Task<ICollection<Product>> GetProductsAsync();

    Task<Product> GetByIdAsync(int id);

    Task<Product> CreateAsync(Product product);

    Task<Product> UpdateAsync(Product product);

    Task<Product> DeleteAsync(Product product);
}
