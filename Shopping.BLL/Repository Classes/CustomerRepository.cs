using Shopping.BLL.Interfaces;
using Shopping.DAL;
using Shopping.VM;
using AutoMapper;

namespace Shopping.BLL.Repository_Classes
{
    internal class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(ShoppingDBContext context, IRepositoryWrapper repo, IMapper mapper) : base(context,repo,mapper) { }
    }
}
