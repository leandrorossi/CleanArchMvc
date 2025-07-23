using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces;

public interface IProductService
{
    Task<ICollection<ProductDTO>> GetProductsAsync();
    Task<ProductDTO> GetByIdAsync(int id);
    Task<ProductDTO> GetProductCategoryAsync(int id);
    Task AddAsync(ProductDTO productDTO);
    Task UpdateAsync(ProductDTO productDTO);
    Task DeleteAsync(int id);
}
