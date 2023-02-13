using Shopping.VM.CardVM;
using Shopping.DAL;
using AutoMapper;

namespace Shopping.Map
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<CardVM, CreditCard>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.id))
                .ForMember(dest => dest.CustomerId, src => src.MapFrom(x => x.customerId))
                .ForMember(dest => dest.CardNumber, src => src.MapFrom(x => x.cardNumber))
                .ForMember(dest => dest.Provider, src => src.MapFrom(x => x.provider))
                .ReverseMap();
        }
    }
}
