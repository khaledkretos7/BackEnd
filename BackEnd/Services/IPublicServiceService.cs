using BackEnd.Contracts.PublicService;
using BackEnd.Contracts.PublicServiceCategory;

namespace BackEnd.Services;

public interface IPublicServiceService
{
    Task<IEnumerable<PublicServiceCategoryResponse>> GetAllServicesByCategoryAsync();
    Task<PublicServiceResponse> CreateAsync(PublicServiceRequest request);
    Task<PublicServiceResponse?> UpdateAsync(int id, PublicServiceRequest request);
    Task<bool> DeleteAsync(int id);
}
