using BackEnd.Contracts.PublicService;
using BackEnd.Contracts.PublicServiceCategory;
using BackEnd.Entities;
using Mapster;

namespace BackEnd.Mapping;

public class MappingConfigration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PublicServiceCategory, PublicServiceCategoryResponse>();

        config.NewConfig<PublicService, PublicServiceResponse>();

        config.NewConfig<PublicServiceRequest, PublicService>()
            .Map(dest => dest.CategoryId, src => src.CategoryId);

        config.NewConfig<PublicServiceCategoryRequest, PublicServiceCategory>();

    }
}
