using BackEnd.Contracts.PublicService;

namespace BackEnd.Contracts.PublicServiceCategory;

public record PublicServiceCategoryResponse
(
    int Id,
    string Name,
    string Description,
    List<PublicServiceResponse> Services
);
