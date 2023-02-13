using Microsoft.AspNetCore.Http;
using Shopping.DAL;
using Shopping.VM;
using Shopping.VM.AccountVM;

namespace Shopping.BLL.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        public Task<ResponseModel<AccountInfoVM>> Register(RegisterVM registerVM);
        public Task<ResponseModel<AccountInfoVM>> Login(LoginVM loginVM, IJwtUtils utils);
        public Task<ResponseModel<AccountInfoVM>> UpdateAccount(RequestModel<AccountInfoVM> request);
        public Task<ResponseModel<AccountInfoVM>> AddProfileImage(RequestModel<IFormFile> request);
        public Task<ResponseModel<AccountDetailsVM>> AccountDetails(RequestModel<int> request);
        public Task<ResponseModel<AccountInfoVM>> AccountInfo(RequestModel<int> request);
        public Task<ResponseModel<bool>> DeleteAccount(RequestModel<int> request);
        public Task<ResponseModel<bool>> RestoreAccount(RequestModel<int> request);
        public Task<ResponseModel<AccountInfoVM>> AccountInfo(int id);
    }
}
