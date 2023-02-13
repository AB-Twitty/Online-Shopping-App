using Shopping.DAL;
using Shopping.VM;
using Shopping.VM.ContactVM;
using Shopping.BLL.Interfaces;
using AutoMapper;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Shopping.VM.AccountVM;

namespace Shopping.BLL.Repository_Classes
{
    internal class ContactRepository : RepositoryBase<CustomerContact>, IContactRepository
    {
        public ContactRepository(ShoppingDBContext context, IRepositoryWrapper repo, IMapper mapper) : base(context,repo,mapper) { }

        public async Task<ResponseModel<ContactInfoVM>> ContactList(RequestModel<int> request)
        {
            try
            {
                if (request.User.accountType==Role.Admin || (request.User.accountType==Role.Customer && request.User.id==request.Data))
                {
                    return new ResponseModel<ContactInfoVM>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        DataList = _mapper.Map<List<ContactInfoVM>>(await FindByCondition(x => x.CustomerId == request.Data).Include(x => x.ContactType).ToListAsync()),
                        Message = "The Request has seccedded",
                    };
                }
                return new ResponseModel<ContactInfoVM>
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Token = request.Token,
                    Message = "Bad Request",
                };
            }
            catch
            {
                return new ResponseModel<ContactInfoVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Message = "Internal Server Error",
                };
            }
        }

        public async Task<ResponseModel<ContactDetailsVM>> ContactDetails(RequestModel<int> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.Data).AnyAsync())
                {
                    return new ResponseModel<ContactDetailsVM>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = _mapper.Map<ContactDetailsVM>(await FindByCondition(x => x.Id == request.Data).Include(x => x.ContactType).FirstAsync()),
                        Message = "Contact Details"
                    };
                }
                return new ResponseModel<ContactDetailsVM>
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Token = request.Token,
                    Message = "Not Found"
                };
            }
            catch
            {
                return new ResponseModel<ContactDetailsVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Message = "Internal Server Error",
                };
            }
        }

        public async Task<ResponseModel<bool>> AddContact(RequestModel<ContactInfoVM> request)
        {
            try
            {
                CustomerContact contact = new CustomerContact();
                contact = _mapper.Map<CustomerContact>(request.Data);
                contact.CustomerId = request.User.id;
                contact.CreationDate = contact.LastModifiedDate = DateTime.Now;
                await Create(contact);
                await _repo.Save();
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.OK,
                    Token = request.Token,
                    Data = true,
                    Message = "Contact Added Successfully"
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

        public async Task<ResponseModel<bool>> DeleteContact(RequestModel<int> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.Data && x.CustomerId == request.User.id).AnyAsync() || request.User.accountType==Role.Admin)
                {
                    Delete(await FindByCondition(x => x.Id == request.Data).FirstAsync());
                    await _repo.Save();
                    return new ResponseModel<bool> 
                    { 
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = true,
                        Message = "Contact Deleted Successfully"
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

        public async Task<ResponseModel<bool>> UpdateContact(RequestModel<ContactInfoVM> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id==request.Data!.id && x.ContactTypeId==request.Data!.typeId && x.CustomerId==request.User.id).AnyAsync())
                {
                    CustomerContact contact = await FindByCondition(x => x.Id == request.Data!.id).FirstAsync();
                    contact = _mapper.Map<CustomerContact>(request.Data);
                    contact.LastModifiedDate = DateTime.Now;
                    Update(contact);
                    await _repo.Save();
                    return new ResponseModel<bool>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = true,
                        Message = "Contact Updated Successfully"
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
