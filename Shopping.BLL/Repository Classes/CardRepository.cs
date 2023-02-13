using Shopping.DAL;
using Shopping.VM;
using Shopping.VM.CardVM;
using Shopping.BLL.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Shopping.VM.AccountVM;

namespace Shopping.BLL.Repository_Classes
{
    internal class CardRepository : RepositoryBase<CreditCard>, ICardRepository
    {
        public CardRepository(ShoppingDBContext context, IRepositoryWrapper repo, IMapper mapper) : base(context,repo,mapper) { }

        public async Task<ResponseModel<CardVM>> CardList(RequestModel<int> request)
        {
            try
            {
                if (request.User.accountType == Role.Admin || (request.User.accountType == Role.Customer && request.User.id == request.Data))
                {
                    return new ResponseModel<CardVM>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        DataList = _mapper.Map<IList<CardVM>>(await FindByCondition(x => x.CustomerId == request.Data).ToListAsync()),
                        Message = "Credit Cards List"
                    };
                }
                return new ResponseModel<CardVM>
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Token = request.Token,
                    Message = "Bad Request",
                };
            }
            catch
            {
                return new ResponseModel<CardVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Message = "Internal Server Error",
                };
            }
        }

        public async Task<ResponseModel<bool>> AddCard(RequestModel<CardVM> request)
        {
            try
            {
                CreditCard card = _mapper.Map<CreditCard>(request.Data);
                card.CustomerId = request.User.id;
                await Create(card);
                await _repo.Save();
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.OK,
                    Token = request.Token,
                    Data = true,
                    Message = "Credit Card Added Successfully"
                };
            }
            catch
            {
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Data = false,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<bool>> DeleteCard(RequestModel<int> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.Data && x.CustomerId == request.User.id).AnyAsync() || request.User.accountType == Role.Admin)
                {
                    Delete(await FindByCondition(x => x.Id == request.Data).FirstAsync());
                    await _repo.Save();
                    return new ResponseModel<bool>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = true,
                        Message = "Credit Card Deleted Successfully"
                    };
                }
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Token = request.Token,
                    Data = false,
                    Message = "Bad Request"
                };
            }
            catch
            {
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Data = false,
                    Message = "Internal Server Error"
                };
            }
        }
    }
}
