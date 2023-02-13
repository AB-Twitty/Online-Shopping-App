using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shopping.BLL.Interfaces;
using Shopping.DAL;

namespace Shopping.BLL.Repository_Classes
{
    internal abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ShoppingDBContext _context;
        protected readonly IRepositoryWrapper _repo;
        protected readonly IMapper _mapper;

        public RepositoryBase(ShoppingDBContext context, IRepositoryWrapper repo, IMapper mapper)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
        }

        public IQueryable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T,bool>> exp)
        {
            return _context.Set<T>().Where(exp).AsNoTracking();
        }

        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
