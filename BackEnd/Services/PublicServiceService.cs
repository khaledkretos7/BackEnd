using BackEnd.Contracts.PublicService;
using BackEnd.Contracts.PublicServiceCategory;
using BackEnd.Entities;
using BackEnd.Presistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services;

public class PublicServiceService(ApplicationDbContext context) : IPublicServiceService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<PublicServiceCategoryResponse>> GetAllServicesByCategoryAsync()
    {
        var categories = await _context.PublicServiceCategories
            .Include(c => c.Services)
            .ToListAsync();

        var result = categories.Select(category => new PublicServiceCategoryResponse(
            category.Id,
            category.Name,
            category.Description,
            category.Services.Select(service => new PublicServiceResponse(
                service.Id,
                service.Name,
                service.PhoneNumber,
                service.Status,
                service.CategoryId
            )).ToList()
        )).ToList();

        return result;
    }


    public async Task<PublicServiceResponse> CreateAsync(PublicServiceRequest request)
    {
        var category = await _context.PublicServiceCategories.FindAsync(request.CategoryId);
        if (category is null)
            throw new Exception("Category not found.");

        var service = request.Adapt<PublicService>();

        _context.PublicServices.Add(service);
        await _context.SaveChangesAsync();

        return service.Adapt<PublicServiceResponse>();
    }

    public async Task<PublicServiceResponse?> UpdateAsync(int id, PublicServiceRequest request)
    {
        var service = await _context.PublicServices.FindAsync(id);
        if (service is null)
            return null;

        var category = await _context.PublicServiceCategories.FindAsync(request.CategoryId);
        if (category is null)
            throw new Exception("Category not found.");

        service.Name = request.Name;
        service.PhoneNumber = request.PhoneNumber;
        service.Status = request.Status;
        service.CategoryId = request.CategoryId;
        service.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return service.Adapt<PublicServiceResponse>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var service = await _context.PublicServices.FindAsync(id);

        if (service is null)
            return false;

        _context.PublicServices.Remove(service);
        await _context.SaveChangesAsync();

        return true;
    }
}
