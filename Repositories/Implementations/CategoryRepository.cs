using API.Data;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations;

public class CategoryRepository(FinanceAppContext context) : Repository<Category>(context), ICategoryRepository
{
    private readonly FinanceAppContext _context = context;

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id) ?? 
               throw new NullReferenceException();
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        var existingCategory = await _context.Categories.FindAsync(category.Id);
        if (existingCategory == null)
            throw new KeyNotFoundException("Category not found");
        
        existingCategory.Name = category.Name;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        
        if (category == null)
            throw new KeyNotFoundException("Category not found");
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}