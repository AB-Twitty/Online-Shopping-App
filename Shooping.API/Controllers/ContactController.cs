using Shopping.BLL.Interfaces;
using Shopping.VM;
using Shopping.VM.ContactVM;
using Microsoft.AspNetCore.Mvc;
using Shopping.VM.AccountVM;
using Shopping.API.Authorization;
using Shopping.BLL;
using Microsoft.Extensions.Options;
using Shopping.API.Helpers;

namespace Shopping.API.Controllers
{
    public class ContactController : APIBaseController
    {
        [IsDeleted]
        public ContactController(IRepositoryWrapper repo, IJwtUtils utils, IOptions<AppSettings> app) : base(repo,utils,app) { }

        [HttpGet("ContactList")]
        [Authorize(Role.Admin, Role.Customer)]
        public async Task<ResponseModel<ContactInfoVM>> ContactList(int customerId)
        {
            return await _repo.Contact.ContactList(new RequestModel<int>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = customerId
            });
        }
    

        [HttpGet("ContactDetails")]
        [Authorize(Role.Admin)]
        public async Task<ResponseModel<ContactDetailsVM>> ContactDetails(int contactId)
        {
            return await _repo.Contact.ContactDetails(new RequestModel<int>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = contactId
            });
        }

        [HttpPost("AddContact")]
        [Authorize(Role.Customer)]
        public async Task<ResponseModel<bool>> AddContact([FromForm]ContactInfoVM contactVM)
        {
            return await _repo.Contact.AddContact(new RequestModel<ContactInfoVM>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = contactVM
            });
        }

        [HttpDelete("DeleteContact")]
        [Authorize(Role.Admin, Role.Customer)]
        public async Task<ResponseModel<bool>> DeleteContact(int contactId)
        {
            return await _repo.Contact.DeleteContact(new RequestModel<int>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = contactId
            });
        }

        [HttpPut("UpdateContact")]
        [Authorize(Role.Customer)]
        public async Task<ResponseModel<bool>> UpdateContact([FromForm]ContactInfoVM contactVM)
        {
            return await _repo.Contact.UpdateContact(new RequestModel<ContactInfoVM>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = contactVM
            });
        }
    }
}
