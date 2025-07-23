using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Extensions;

internal static class CategoryExtension
{
    public static CategoryDTO ToDto(this Category category)
    {
        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
        };
    }

    public static Category ToEntity(this CategoryDTO categoryDTO)
    {
        return new Category(categoryDTO.Id, categoryDTO.Name);
    }
}
