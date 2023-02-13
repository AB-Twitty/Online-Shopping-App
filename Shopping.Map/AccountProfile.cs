using AutoMapper;
using Shopping.DAL;
using Shopping.VM.AccountVM;

namespace Shopping.Map
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        { 
            CreateMap<AccountInfoVM, Account>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.id))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.name))
                .ForMember(dest => dest.Username, src => src.MapFrom(x => x.username))
                .ForMember(dest => dest.Email, src => src.MapFrom(x => x.email))
                .ForMember(dest => dest.BirthDate, src => src.MapFrom(x => x.birthDate))
                .ForMember(dest => dest.CountryId, src => src.MapFrom(x => x.countryId))
                .ForMember(dest => dest.NationalId, src => src.MapFrom(x => x.nationalId))
                .ForMember(dest => dest.ImageUrl, src => src.MapFrom(x => x.imageURL))
                .ReverseMap();

            CreateMap<RegisterVM, Account>()
                .IncludeBase<AccountInfoVM, Account>()
                .ForMember(dest => dest.Password, src => src.MapFrom(x => x.password))
                .ReverseMap();

            CreateMap<AccountDetailsVM, Account>()
                .IncludeBase<AccountInfoVM, Account>()
                .ForMember(dest => dest.CreationDate, src => src.MapFrom(x => x.creationDate))
                .ForMember(dest => dest.LastModifiedDate, src => src.MapFrom(x => x.lastModifiedDate))
                .ReverseMap();
        }
    }
}
