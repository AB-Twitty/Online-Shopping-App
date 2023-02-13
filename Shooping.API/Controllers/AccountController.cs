using Microsoft.AspNetCore.Mvc;
using Shopping.BLL.Interfaces;
using Shopping.VM;
using Shopping.VM.AccountVM;
using Shopping.API.Authorization;
using Shopping.API.Helpers;
using Shopping.BLL;
using Microsoft.Extensions.Options;

namespace Shopping.API.Controllers
{
    public class AccountController : APIBaseController
    {
        public AccountController(IRepositoryWrapper repo,IJwtUtils utils, IOptions<AppSettings> app) : base(repo,utils,app) { }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ResponseModel<AccountInfoVM>> Register([FromForm] RegisterVM registerVM)
        {
            return await _repo.Account.Register(registerVM);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ResponseModel<AccountInfoVM>> Login([FromForm] LoginVM loginVM)
        {
            return await _repo.Account.Login(loginVM, _utils);
        }

        [HttpPut("UpdateAccount")]
        [Authorize(Role.Admin, Role.Trader, Role.Customer)]
        [IsDeleted]
        public async Task<ResponseModel<AccountInfoVM>> UpdateAccount([FromForm]AccountInfoVM accountVM)
        {
            return await _repo.Account.UpdateAccount(new RequestModel<AccountInfoVM>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = accountVM
            });
        }

        [HttpPost("ProfileImage/{accountId}")]
        [Authorize(Role.Admin, Role.Trader, Role.Customer)]
        [IsDeleted]
        public async Task<ResponseModel<AccountInfoVM>> AddProfileImage(IFormFile imageFile)
        {
            return await _repo.Account.AddProfileImage(new RequestModel<IFormFile>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = imageFile
            });
        }

        [HttpGet("AccountDetails")]
        [Authorize(Role.Admin)]
        [IsDeleted]
        public async Task<ResponseModel<AccountDetailsVM>> AccountDetails(int accountId)
        {
            return await _repo.Account.AccountDetails(new RequestModel<int>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = accountId
            });
        }

        [HttpGet("AccountInfo")]
        [Authorize(Role.Admin, Role.Trader, Role.Customer)]
        [IsDeleted]
        public async Task<ResponseModel<AccountInfoVM>> AccountInfo()
        {
            return await _repo.Account.AccountInfo(new RequestModel<int>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
            });
        }

        [HttpGet("DeleteAccount")]
        [Authorize(Role.Admin)]
        [IsDeleted]
        public async Task<ResponseModel<bool>> DeleteAccount(int accountId)
        {
            return await _repo.Account.DeleteAccount(new RequestModel<int>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = accountId
            });
        }

        [HttpGet("RestoreAccount")]
        [Authorize(Role.Admin)]
        [IsDeleted]
        public async Task<ResponseModel<bool>> RestoreAccount(int accountId)
        {
            return await _repo.Account.RestoreAccount(new RequestModel<int>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = accountId
            });
        }
    }
}
