using Shopping.DAL;
using Shopping.VM;
using Shopping.VM.ContactVM;

namespace Shopping.BLL.Interfaces
{
    public interface IContactRepository : IRepositoryBase<CustomerContact>
    {
        public Task<ResponseModel<ContactInfoVM>> ContactList(RequestModel<int> request);
        public Task<ResponseModel<ContactDetailsVM>> ContactDetails(RequestModel<int> request);
        public Task<ResponseModel<bool>> AddContact(RequestModel<ContactInfoVM> request);
        public Task<ResponseModel<bool>> DeleteContact(RequestModel<int> request);
        public Task<ResponseModel<bool>> UpdateContact(RequestModel<ContactInfoVM> request);
    }
}
