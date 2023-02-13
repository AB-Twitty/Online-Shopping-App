using Microsoft.AspNetCore.Mvc;
using Shopping.BLL.Interfaces;
using Shopping.VM;
using Shopping.VM.CategoryVM;
using Shopping.API.Authorization;
using Shopping.VM.AccountVM;
using Shopping.BLL;
using Microsoft.Extensions.Options;
using Shopping.API.Helpers;
using ClosedXML.Excel;

namespace Shopping.API.Controllers
{
    public class CategoryController : APIBaseController
    {
        public CategoryController(IRepositoryWrapper repo, IJwtUtils utils, IOptions<AppSettings> app) : base(repo,utils,app) { }

        [HttpGet("Categories")]

        public async Task<ResponseModel<CategoryVM>> CategoriesList()
        {
            return await _repo.Category.CategoriesList();
        }

        [HttpGet("ExportToExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "categories.xlsx";
            try
            {
                var workBook = new XLWorkbook(); 
                IXLWorksheet workSheet = workBook.Worksheets.Add("List");
                IList<CategoryVM> categories = (await CategoriesList()).DataList;
                int currentCell = 1;
                foreach(var prop in categories[0].GetType().GetProperties())
                {
                    workSheet.Cell(1, currentCell++).Value = prop.Name;
                }

                for (int index=0; index<categories.Count; index++)
                {
                    currentCell = 1;
                    foreach (var prop in categories[index].GetType().GetProperties())
                    {
                        workSheet.Cell(index+2, currentCell++).Value = prop.GetValue(categories[index]);
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("AddCategory")]
        [Authorize(Role.Admin)]
        public async Task<ResponseModel<bool>> AddCategory([FromForm]CategoryVM categoryVM)
        {
            return await _repo.Category.AddCategory(new RequestModel<CategoryVM>
            {
                Token = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last(),
                User = (AccountInfoVM)HttpContext.Items["User"],
                Data = categoryVM
            });
        }

        [HttpDelete("DeleteCategory")]
        [Authorize(Role.Admin)]
        public async Task<ResponseModel<bool>> DeleteCategory(int categoryId)
        {
            return await _repo.Category.DeleteCategory(categoryId);
        }
    }
}
