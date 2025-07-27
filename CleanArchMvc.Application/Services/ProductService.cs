using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Extensions;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ProductDTO> GetByIdAsync(int? id)
    {
        var productEntity = await _productRepository.GetByIdAsync(id);
        return productEntity.ToDTO();
    }

    public async Task<ProductDTO> GetProductCategoryAsync(int id)
    {
        var productEntity = await _productRepository.GetProductCategoryAsync(id);
        return productEntity.ToDTO();
    }

    public async Task<ICollection<ProductDTO>> GetProductsAsync()
    {
        var productsEntity = await _productRepository.GetProductsAsync();
        return productsEntity.Select(p => p.ToDTO()).ToList();
    }
    public async Task AddAsync(ProductDTO productDTO)
    {
        var productEntity = productDTO.ToEntity();
        await _productRepository.CreateAsync(productEntity);
    }

    public async Task UpdateAsync(ProductDTO productDTO)
    {
        var productEntity = productDTO.ToEntity();
        await _productRepository.UpdateAsync(productEntity);
    }

    public async Task DeleteAsync(int id)
    {
        var productEntity = await _productRepository.GetByIdAsync(id);
        await _productRepository.DeleteAsync(productEntity);
    }
}
