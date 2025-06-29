using BackEnd.Contracts.PublicServiceCategory;

namespace BackEnd.Services;

public interface IPublicServiceCategoryService
{
    Task<IEnumerable<PublicServiceCategoryResponse>> GetAllAsync();
    Task<PublicServiceCategoryResponse> CreateAsync(PublicServiceCategoryRequest request);
    Task<PublicServiceCategoryResponse?> UpdateAsync(int id, PublicServiceCategoryRequest request);
    Task<bool> DeleteAsync(int id);
}
