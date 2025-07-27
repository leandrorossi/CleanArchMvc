using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> GetByIdAsync(int? id)
    {
        return await _context.Product.FindAsync(id);
    }

    public async Task<Product> GetProductCategoryAsync(int id)
    {
        return await _context.Product.Include(p => p.Category).AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ICollection<Product>> GetProductsAsync()
    {
        return await _context.Product.AsNoTracking().ToListAsync();
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task<Product> DeleteAsync(Product product)
    {
        _context.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }
}
