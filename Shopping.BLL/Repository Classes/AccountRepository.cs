using System.Net;
using AutoMapper;
using Shopping.BLL.Interfaces;
using Shopping.DAL;
using Shopping.VM;
using Shopping.VM.AccountVM;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Shopping.BLL.Repository_Classes
{
    internal class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(ShoppingDBContext context, IRepositoryWrapper repo, IMapper mapper) : base(context, repo, mapper) { }

        private string UploadedFile(IFormFile? ImageFile)
        {
            string uniqueFileName;
            if (ImageFile != null)
            {
                var currentExtension = Path.GetExtension(ImageFile.FileName).ToLower().Trim();
                var extensions = new List<string>() { ".jpeg", ".png", ".jpg" };
                if (!extensions.Contains(currentExtension)) //handle this return 
                    return "Invalid File";
                string uploadFolder = "C:\\Users\\bobof\\Desktop\\ShoppingApp\\Attachments\\Profile Images";
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);
                uniqueFileName = "CustomerImages" + "_" + DateTime.Now.ToString("yyyy_MM_dd_hhmmss") + "_" + ImageFile.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }
            }
            else
            {
                uniqueFileName = "default.png";
            }
            return uniqueFileName;
        }

        public async Task<ResponseModel<AccountInfoVM>> Register(RegisterVM registerVM)
        {
            ResponseModel<AccountInfoVM> response;
            try
            {
                Account account = new Account();
                account = _mapper.Map<Account>(registerVM);
                account.CreationDate = account.LastModifiedDate = DateTime.Now;
                account.ImageUrl = UploadedFile(registerVM.imageFile);
                await Create(account);
                await _repo.Save();
                AccountInfoVM accountVM = new AccountInfoVM();
                response = new ResponseModel<AccountInfoVM>
                {
                    Status = (int)HttpStatusCode.OK,
                    Data = _mapper.Map<AccountInfoVM>(account),
                    Message = "Rigestered Successfully",
                };
                return response;
            }
            catch
            {
                response = new ResponseModel<AccountInfoVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "Internal Server Error"
                };
                return response;
            }
        }

        public async Task<ResponseModel<AccountInfoVM>> Login(LoginVM loginVM, IJwtUtils utils)
        {
            ResponseModel<AccountInfoVM> response;
            try
            {
                if (await FindByCondition(x => (x.Username == loginVM.usernameOrEmail || x.Email == loginVM.usernameOrEmail) && x.Password == loginVM.password).AnyAsync())
                {
                    Account account = await FindByCondition(x => x.Username == loginVM.usernameOrEmail || x.Email == loginVM.usernameOrEmail).FirstAsync();
                    response = new ResponseModel<AccountInfoVM>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Data = _mapper.Map<AccountInfoVM>(account),
                        Token = utils.GenerateJwtToken(_mapper.Map<AccountInfoVM>(account)),
                        Message = "Logged in Successfully"
                    };
                    return response;
                }
                else
                {
                    response = new ResponseModel<AccountInfoVM>
                    {
                        Status = (int)HttpStatusCode.NotFound,
                        Data = null,
                        Message = "Invalid Username or Password"
                    };
                    return response;
                }
            }
            catch
            {
                response = new ResponseModel<AccountInfoVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "Internal Server Error"
                };
                return response;
            }
        }

        public async Task<ResponseModel<AccountInfoVM>> UpdateAccount(RequestModel<AccountInfoVM> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.User.id).AnyAsync())
                {
                    Account account = await FindByCondition(x => x.Id == request.User.id).FirstAsync();
                    account = _mapper.Map<Account>(request.Data);
                    account.LastModifiedDate = DateTime.Now;
                    Update(account);
                    await _repo.Save();
                    return new ResponseModel<AccountInfoVM>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = _mapper.Map<AccountInfoVM>(account),
                        Message = "Internal Server Error",
                    };
                }
                return new ResponseModel<AccountInfoVM>
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Token = request.Token,
                    Data = null,
                    Message = "Not Found"
                };
            }
            catch
            {
                return new ResponseModel<AccountInfoVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Data = null,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<AccountInfoVM>> AddProfileImage(RequestModel<IFormFile> request)
        {
            try
            {
                Account account = await FindByCondition(x => x.Id == request.User.id).FirstAsync();
                account.LastModifiedDate = DateTime.Now;
                account.ImageUrl = UploadedFile(request.Data);
                Update(account);
                await _repo.Save();
                return new ResponseModel<AccountInfoVM>
                {
                    Status = (int)HttpStatusCode.OK,
                    Token = request.Token,
                    Data = _mapper.Map<AccountInfoVM>(account),
                    Message = "Profile Image Added Successfully"
                };
            }
            catch
            {
                return new ResponseModel<AccountInfoVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Data = null,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<AccountDetailsVM>> AccountDetails(RequestModel<int> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.Data).AnyAsync())
                {
                    return new ResponseModel<AccountDetailsVM>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = _mapper.Map<AccountDetailsVM>(await FindByCondition(x => x.Id == request.Data).FirstAsync()),
                        Message = "The Request has succeeded"
                    };
                }
                return new ResponseModel<AccountDetailsVM>
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Token = request.Token,
                    Data = null,
                    Message = "Not Found"
                };
            }
            catch
            {
                return new ResponseModel<AccountDetailsVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Data = null,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<AccountInfoVM>> AccountInfo(RequestModel<int> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.User.id).AnyAsync())
                {
                    return new ResponseModel<AccountInfoVM>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = _mapper.Map<AccountInfoVM>(await FindByCondition(x => x.Id == request.User.id).FirstAsync()),
                        Message = "The Request has succeeded"
                    };
                }
                return new ResponseModel<AccountInfoVM>
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Token = request.Token,
                    Data = null,
                    Message = "Not Found"
                };
            }
            catch
            {
                return new ResponseModel<AccountInfoVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Data = null,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<bool>> DeleteAccount(RequestModel<int> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.Data).AnyAsync())
                {
                    Account account = await FindByCondition(x => x.Id == request.Data).FirstAsync();
                    account.IsDeleted = true;
                    account.LastModifiedDate = DateTime.Now;
                    Update(account);
                    await _repo.Save();
                    return new ResponseModel<bool>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = true,
                        Message = "Deleted Successfully"
                    };
                }
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Token = request.Token,
                    Data = false,
                    Message = "Not Found"
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

        public async Task<ResponseModel<bool>> RestoreAccount(RequestModel<int> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.Data).AnyAsync())
                {
                    Account account = await FindByCondition(x => x.Id == request.Data).FirstAsync();
                    account.IsDeleted = false;
                    account.LastModifiedDate = DateTime.Now;
                    Update(account);
                    await _repo.Save();
                    return new ResponseModel<bool>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = true,
                        Message = "Restored Successfully"
                    };
                }
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Token = request.Token,
                    Data = false,
                    Message = "Not Found"
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

        public async Task<ResponseModel<AccountInfoVM>> AccountInfo(int id)
        {
            try
            {
                if (await FindByCondition(x => x.Id == id).AnyAsync())
                {
                    return new ResponseModel<AccountInfoVM>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Data = _mapper.Map<AccountInfoVM>(await FindByCondition(x => x.Id == id).FirstAsync()),
                        Message = "The Request has succeeded"
                    };
                }
                return new ResponseModel<AccountInfoVM>
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Data = null,
                    Message = "Not Found"
                };
            }
            catch
            {
                return new ResponseModel<AccountInfoVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "Internal Server Error"
                };
            }

        }
    }
}
