using AutoMapper;
using Shopping.DAL;
using Shopping.VM.CountryVM;

namespace Shopping.Map
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryVM, Country>()
                .ReverseMap();
        }
    }
}
