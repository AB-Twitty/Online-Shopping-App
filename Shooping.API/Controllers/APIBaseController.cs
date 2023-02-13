using Microsoft.AspNetCore.Mvc;
using Shopping.BLL.Interfaces;
using Shopping.BLL;
using Shopping.API.Helpers;
using Microsoft.Extensions.Options;
using Shopping.VM;

namespace Shopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {
        protected readonly IRepositoryWrapper _repo;
        protected readonly IJwtUtils _utils;
        private readonly AppSettings _app;

        public APIBaseController(IRepositoryWrapper repo,IJwtUtils utils, IOptions<AppSettings> app)
        {
            _repo = repo;
            _utils = utils;
            _app = app.Value;
            _utils.SetSecretKey(_app.Secret);
        }
    }
}
