using AutoMapper;
using Shopping.DAL;
using Shopping.VM.ImageVM;
using Shopping.VM.ProductVM;

namespace Shopping.Map
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductInfoVM>()
                .ForMember(dest => dest.id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.traderId, src => src.MapFrom(x => x.TraderId))
                .ForMember(dest => dest.quantity, src => src.MapFrom(x => x.Quantity))
                .ForMember(dest => dest.price, src => src.MapFrom(x => x.Price))
                .ForMember(dest => dest.name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.desc, src => src.MapFrom(x => x.Desc))
                .ForMember(dest => dest.categoryId, src => src.MapFrom(x => x.CategoryId))
                .ForMember(dest => dest.images, src => src.MapFrom(x => x.ProductImages));

            CreateMap<ProductInfoVM, Product>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.id))
                .ForMember(dest => dest.TraderId, src => src.MapFrom(x => x.traderId))
                .ForMember(dest => dest.Quantity, src => src.MapFrom(x => x.quantity))
                .ForMember(dest => dest.Price, src => src.MapFrom(x => x.price))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.name))
                .ForMember(dest => dest.Desc, src => src.MapFrom(x => x.desc))
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(x => x.categoryId));

            CreateMap<ProductAddVM, Product>()
                .IncludeBase<ProductInfoVM, Product>();
        }
    }
}
