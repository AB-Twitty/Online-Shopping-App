using System.Linq.Expressions;

namespace Shopping.BLL.Interfaces
{
    public interface IRepositoryBase<T>
    {
        public IQueryable<T> FindAll();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> exp);
        public Task Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
