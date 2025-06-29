using BackEnd.Contracts.PublicServiceCategory;
using BackEnd.Entities;
using BackEnd.Presistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services;

public class PublicServiceCategoryService(ApplicationDbContext context) : IPublicServiceCategoryService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<PublicServiceCategoryResponse>> GetAllAsync()
    {
        var categories = await _context.PublicServiceCategories.ToListAsync();
        return categories.Adapt<List<PublicServiceCategoryResponse>>();
    }

    public async Task<PublicServiceCategoryResponse> CreateAsync(PublicServiceCategoryRequest request)
    {
        var category = request.Adapt<PublicServiceCategory>();

        _context.PublicServiceCategories.Add(category);
        await _context.SaveChangesAsync();

        return category.Adapt<PublicServiceCategoryResponse>();
    }

    public async Task<PublicServiceCategoryResponse?> UpdateAsync(int id, PublicServiceCategoryRequest request)
    {
        var category = await _context.PublicServiceCategories.FindAsync(id);

        if (category is null)
            return null;

        category.Name = request.Name;
        category.Description = request.Description;
        category.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return category.Adapt<PublicServiceCategoryResponse>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.PublicServiceCategories.FindAsync(id);

        if (category is null)
            return false;

        _context.PublicServiceCategories.Remove(category);
        await _context.SaveChangesAsync();

        return true;
    }
}
