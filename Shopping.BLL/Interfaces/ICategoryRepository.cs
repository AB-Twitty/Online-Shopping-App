using Shopping.VM.CategoryVM;
using Shopping.VM;
using Shopping.DAL;

namespace Shopping.BLL.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        public Task<ResponseModel<CategoryVM>> CategoriesList();
        public Task<ResponseModel<bool>> AddCategory(RequestModel<CategoryVM> request);
        public Task<ResponseModel<bool>> DeleteCategory(int categoryId);
    }
}
