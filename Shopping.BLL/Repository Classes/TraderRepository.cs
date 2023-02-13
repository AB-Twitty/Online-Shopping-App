using Shopping.BLL.Interfaces;
using Shopping.DAL;
using Shopping.VM;
using AutoMapper;

namespace Shopping.BLL.Repository_Classes
{
    internal class TraderRepository : RepositoryBase<Trader>, ITraderRepository
    {
        public TraderRepository(ShoppingDBContext context, IRepositoryWrapper repo, IMapper mapper) : base(context,repo,mapper) { }
    }
}
