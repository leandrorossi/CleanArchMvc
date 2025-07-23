using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Extensions;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<CategoryDTO> GetByIdAsync(int id)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(id);
        return categoryEntity.ToDto();
    }

    public async Task<ICollection<CategoryDTO>> GetCategoriesAsync()
    {
        var categoriesEntity = await _categoryRepository.GetCategoriesAsync();
        return categoriesEntity.Select(c => c.ToDto()).ToList();
    }

    public async Task AddAsync(CategoryDTO categoryDTO)
    {
        var categoryEntity = categoryDTO.ToEntity();
        await _categoryRepository.CreateAsync(categoryEntity);
    }

    public async Task UpdateAsync(CategoryDTO categoryDTO)
    {
        var categoryEntity = categoryDTO.ToEntity();
        await _categoryRepository.UpdateAsync(categoryEntity);
    }

    public async Task DeleteAsync(int id)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(id);
        await _categoryRepository.DeleteAsync(categoryEntity);
    }
}
