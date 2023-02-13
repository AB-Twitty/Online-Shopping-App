using Shopping.BLL.Interfaces;
using Shopping.BLL;
using Shopping.API.Helpers;
using Microsoft.Extensions.Options;

namespace Shopping.API.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _app;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> app)
        {
            _next = next;
            _app = app.Value;
        }

        public async Task Invoke(HttpContext context, IRepositoryWrapper repo)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            IJwtUtils utils = new JwtUtils();
            utils.SetSecretKey(_app.Secret);
            var userId = utils.ValidateJwtToken(token);
            if (userId != null)
            {
                var response = await repo.Account.AccountInfo((int)userId);
                context.Items["User"] = response.Data;
            }
            await _next(context);
        }
    }
}
