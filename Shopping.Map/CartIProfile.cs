using Shopping.DAL;
using Shopping.VM.CartItemVM;
using AutoMapper;
using Shopping.VM.CartVM;

namespace Shopping.Map
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItem, CartItemInfoVM>()
                .ForMember(dest => dest.id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.cartId, src => src.MapFrom(x => x.CartId))
                .ForMember(dest => dest.productId, src => src.MapFrom(x => x.ProductId))
                .ForMember(dest => dest.quantity, src => src.MapFrom(x => x.Quantity))
                .ReverseMap();

            CreateMap<ShoppingCart, CartVM>()
                .ForMember(dest => dest.Items, src => src.MapFrom(x => x.CartItems));
        }
    }
}
