using Shopping.BLL.Interfaces;
using Shopping.VM;
using Shopping.DAL;
using AutoMapper;
using Shopping.VM.CategoryVM;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Shopping.BLL.Repository_Classes
{
    internal class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ShoppingDBContext context, IRepositoryWrapper repo, IMapper mapper) : base(context,repo,mapper) { }

        public async Task<ResponseModel<CategoryVM>> CategoriesList()
        {
            ResponseModel<CategoryVM> response;
            try
            {
                response = new ResponseModel<CategoryVM>
                {
                    Status = (int)HttpStatusCode.OK,
                    DataList = _mapper.Map<List<CategoryVM>>(await FindAll().ToListAsync()),
                    Message = "Categories List"
                };
                return response;
            }
            catch
            {
                response = new ResponseModel<CategoryVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "Internal Server Error"
                };
                return response;
            }
        }

        public async Task<ResponseModel<bool>> AddCategory(RequestModel<CategoryVM> request)
        {
            try
            {
                await Create(_mapper.Map<Category>(request.Data));
                await _repo.Save();
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.OK,
                    Token = request.Token,
                    Data = true,
                    Message = "Category Added Successfully"
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

        public async Task<ResponseModel<bool>> DeleteCategory(int categoryId)
        {
            ResponseModel<bool> response;
            try
            {
                if (await FindByCondition(x => x.Id==categoryId).AnyAsync())
                {
                    Delete(await FindByCondition(x => x.Id == categoryId).FirstAsync());
                    await _repo.Save();
                    response = new ResponseModel<bool>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Data = true,
                        Message = "Category Deleted Successfully"
                    };
                    return response;
                }
                response = new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Data = false,
                    Message = "Category Not Found"
                };
                return response;
            }
            catch
            {
                response = new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Data = false,
                    Message = "Internal Server Error"
                };
                return response;
            }
        }
    }
}
