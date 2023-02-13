using Shopping.DAL;
using Shopping.VM.CountryVM;
using Shopping.VM;

namespace Shopping.BLL.Interfaces
{
    public interface ICountryRepository : IRepositoryBase<Country>
    {
        public Task<ResponseModel<CountryVM>> CountriesList(RequestModel<bool> request);
    }
}
