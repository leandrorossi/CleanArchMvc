using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Extensions;

internal static class ProductExtension
{
    public static ProductDTO ToDTO(this Product product)
    {
        return new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            Image = product.Image,
            CategoryId = product.CategoryId,
            Category = product.Category,
        };
    }

    public static Product ToEntity(this ProductDTO productDTO)
    {
        return new(productDTO.Id, productDTO.Name, productDTO.Description, productDTO.Price, productDTO.Stock, productDTO.Image)
        {
            Category = productDTO.Category,
            CategoryId = productDTO.CategoryId
        };
    }
}
