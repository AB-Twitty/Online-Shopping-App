using Shopping.VM;
using Shopping.VM.CountryVM;
using Shopping.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Authorization;
using Shopping.VM.AccountVM;
using Shopping.API.Helpers;
using Shopping.BLL;
using Microsoft.Extensions.Options;

namespace Shopping.API.Controllers
{
    public class CountryController : APIBaseController
    {
        [IsDeleted]
        public CountryController(IRepositoryWrapper repo, IJwtUtils utils, IOptions<AppSettings> app) : base(repo,utils,app) { }

        [HttpGet("Countries")]
        [Authorize(Role.Admin, Role.Trader, Role.Customer)]
        public async Task<ResponseModel<CountryVM>> CountriesList(bool isActive)
        {
            return await _repo.Country.CountriesList(new RequestModel<bool>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = isActive
            });
        }
    }
}
