using Shopping.DAL;
using Shopping.VM.ContactVM;
using AutoMapper;

namespace Shopping.Map
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactInfoVM, CustomerContact>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.id))
                .ForMember(dest => dest.Contact, src => src.MapFrom(x => x.contact))
                .ForMember(dest => dest.ContactTypeId, src => src.MapFrom(x => x.typeId))
                .ForMember(dest => dest.CustomerId, src => src.MapFrom(x => x.customerId));

            CreateMap<CustomerContact, ContactInfoVM>()
                .ForMember(dest => dest.id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.contact, src => src.MapFrom(x => x.Contact))
                .ForMember(dest => dest.typeId, src => src.MapFrom(x => x.ContactTypeId))
                .ForMember(dest => dest.customerId, src => src.MapFrom(x => x.CustomerId))
                .ForMember(dest => dest.type, src => src.MapFrom(x => x.ContactType.Name));

            CreateMap<CustomerContact, ContactDetailsVM>()
                .IncludeBase<CustomerContact, ContactInfoVM>()
                .ForMember(dest => dest.CreationDate, src => src.MapFrom(x => x.CreationDate))
                .ForMember(dest => dest.LastModifiedDate, src => src.MapFrom(x => x.LastModifiedDate));

        }
    }
}
