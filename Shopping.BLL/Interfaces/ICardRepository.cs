using Shopping.DAL;
using Shopping.VM;
using Shopping.VM.CardVM;

namespace Shopping.BLL.Interfaces
{
    public interface ICardRepository : IRepositoryBase<CreditCard>
    {
        public Task<ResponseModel<CardVM>> CardList(RequestModel<int> request);
        public Task<ResponseModel<bool>> AddCard(RequestModel<CardVM> request);
        public Task<ResponseModel<bool>> DeleteCard(RequestModel<int> request);
    }
}
