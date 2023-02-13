using Shopping.DAL;
using Shopping.VM;
using Shopping.VM.CountryVM;
using Shopping.BLL.Interfaces;
using AutoMapper;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Shopping.BLL.Repository_Classes
{
    internal class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(ShoppingDBContext context, IRepositoryWrapper repo, IMapper mapper) : base(context,repo,mapper) { }

        public async Task<ResponseModel<CountryVM>> CountriesList(RequestModel<bool> request)
        {
            try
            {
                return new ResponseModel<CountryVM>
                {
                    Status = (int)HttpStatusCode.OK,
                    Token = request.Token,
                    DataList = _mapper.Map<IList<CountryVM>>(await FindByCondition(x => x.IsActive == request.Data).ToListAsync()),
                    Message = "Active countries list"
                };
            }
            catch
            {
                return new ResponseModel<CountryVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Message = "Internal Server Error"
                };
            }
        }
    }
}
