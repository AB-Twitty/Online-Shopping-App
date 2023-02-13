using Shopping.BLL.Interfaces;
using Shopping.VM;
using Shopping.VM.CardVM;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Authorization;
using Shopping.VM.AccountVM;
using Shopping.API.Helpers;
using Shopping.BLL;
using Microsoft.Extensions.Options;

namespace Shopping.API.Controllers
{
    [IsDeleted]
    public class CardController : APIBaseController
    {
        public CardController(IRepositoryWrapper repo, IJwtUtils utils, IOptions<AppSettings> app) : base(repo,utils,app) { }

        [HttpGet("CardList")]
        [Authorize(Role.Admin, Role.Customer)]
        public async Task<ResponseModel<CardVM>> CardList(int customerId)
        {
            return await _repo.Card.CardList(new RequestModel<int>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = customerId
            });
        }

        [HttpPost("AddCard")]
        [Authorize(Role.Customer)]
        public async Task<ResponseModel<bool>> AddCard([FromForm]CardVM cardVM)
        {
            return await _repo.Card.AddCard(new RequestModel<CardVM>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = cardVM
            });
        }

        [HttpDelete("DeleteCard")]
        [Authorize(Role.Admin, Role.Customer)]
        public async Task<ResponseModel<bool>> DeleteCard(int cardId)
        {
            return await _repo.Card.DeleteCard(new RequestModel<int>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = cardId
            });
        }
    }
}
