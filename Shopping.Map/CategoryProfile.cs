using Shopping.VM.CategoryVM;
using Shopping.DAL;
using AutoMapper;

namespace Shopping.Map
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryVM>().ReverseMap();
        }
    }
}
