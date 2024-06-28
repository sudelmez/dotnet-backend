using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductEntity, ProductDto>();
        CreateMap<AddProductDto, AddProductEntity>();
    }
}