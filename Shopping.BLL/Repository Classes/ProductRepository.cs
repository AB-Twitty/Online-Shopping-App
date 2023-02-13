using Shopping.DAL;
using Shopping.BLL.Interfaces;
using Shopping.VM;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Shopping.VM.ProductVM;
using Shopping.VM.ImageVM;
using Shopping.VM.AccountVM;

namespace Shopping.BLL.Repository_Classes
{
    internal class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ShoppingDBContext context, IRepositoryWrapper repo, IMapper mapper) : base(context,repo,mapper) { }

        public async Task<ResponseModel<ProductInfoVM>> AddProductImages(RequestModel<ImageAddVM> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.Data.productId).AnyAsync())
                {
                    Product product = await FindByCondition(x => x.Id == request.Data.productId).FirstAsync();
                    product.LastModifiedDate = DateTime.Now;
                    int result = await _repo.Image.AddProductImages(request.Data.imageFiles, product.Id);
                    if (result == (int)HttpStatusCode.OK)
                    {
                        await _repo.Save();
                        return new ResponseModel<ProductInfoVM>
                        {
                            Status = (int)HttpStatusCode.OK,
                            Data = _mapper.Map<ProductInfoVM>(product),
                            Message = "Images Added Successfully"
                        };
                    }
                }
                return new ResponseModel<ProductInfoVM>
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "No product with that ID is found"
                };
            }
            catch
            {
                return new ResponseModel<ProductInfoVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<ProductInfoVM>> AddProduct(RequestModel<ProductAddVM> request)
        {
            try
            {
                Product product = new Product();
                product = _mapper.Map<Product>(request.Data);
                product.TraderId = request.User.id;
                product.CreationDate = product.LastModifiedDate = DateTime.Now;
                product.IsDeleted = false;
                await Create(product);
                int result = await _repo.Image.AddProductImages(request.Data.imageFiles, product.Id);
                if (result == (int)HttpStatusCode.OK)
                {
                    await _repo.Save();
                    return new ResponseModel<ProductInfoVM>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = _mapper.Map<ProductInfoVM>(await FindByCondition(x => x.Id == request.Data.id).FirstAsync()),
                        Message = "Product Added Successfully"
                    };
                }
                return new ResponseModel<ProductInfoVM>
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Token = request.Token,
                    Message = "Bad Request"
                };
            }
            catch
            {
                return new ResponseModel<ProductInfoVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<ProductInfoVM>> ProductInfo(RequestModel<int> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.Data).AnyAsync())
                {
                    return new ResponseModel<ProductInfoVM>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = _mapper.Map<ProductInfoVM>(await FindByCondition(x => x.Id == request.Data).Include(x => x.ProductImages).FirstAsync()),
                        Message = "The request has secceded",
                    };
                }
                return new ResponseModel<ProductInfoVM>
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Token = request.Token,
                    Message = "Not Found",
                };
            }
            catch
            {
                return new ResponseModel<ProductInfoVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<ProductInfoVM>> ProductsList()
        {
            try
            {
                return new ResponseModel<ProductInfoVM>
                {
                    Status = (int)HttpStatusCode.OK,
                    DataList = _mapper.Map<IList<ProductInfoVM>>(await FindByCondition(x => x.IsDeleted == false).ToListAsync()),
                    Message = "The request has secceded",
                };
            }
            catch
            {
                return new ResponseModel<ProductInfoVM>
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Not Found",
                };
            }
        }

        public async Task<ResponseModel<bool>> DeleteProduct(RequestModel<int> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.Data && x.TraderId == request.User.id).AnyAsync() || request.User.accountType == Role.Admin)
                {
                    Product product = await FindByCondition(x => x.Id == request.Data).FirstAsync();
                    product.IsDeleted = true;
                    product.LastModifiedDate = DateTime.Now;
                    Update(product);
                    await _repo.Save();
                    return new ResponseModel<bool>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = true,
                        Message = "Product deleted Successfully"
                    };
                }
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Token = request.Token,
                    Data = false,
                    Message = "Bad Request"
                };
            }
            catch
            {
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<bool>> RestoreProduct(RequestModel<int> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.Data && x.TraderId == request.User.id).AnyAsync())
                {
                    Product product = await FindByCondition(x => x.Id == request.Data).FirstAsync();
                    product.IsDeleted = false;
                    product.LastModifiedDate = DateTime.Now;
                    Update(product);
                    await _repo.Save();
                    return new ResponseModel<bool>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = true,
                        Message = "Product restored Successfully"
                    };
                }
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Token = request.Token,
                    Data = false,
                    Message = "Bad Request"
                };
            }
            catch
            {
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<bool>> DeleteProductImage(RequestModel<ImageVM> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.Data!.ProductId).AnyAsync())
                {
                    Product product = await FindByCondition(x => x.Id == request.Data!.ProductId).FirstAsync();
                    product.LastModifiedDate = DateTime.Now;
                    Update(product);
                    int result = await _repo.Image.DeleteProductImage(request.Data);
                    if (result == (int)HttpStatusCode.OK)
                    {
                        await _repo.Save();
                        return new ResponseModel<bool>
                        {
                            Status = (int)HttpStatusCode.OK,
                            Token = request.Token,
                            Data = true,
                            Message = "Image Deleted Successfully"
                        };
                    }
                }
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Token = request.Token,
                    Data = false,
                    Message = "Bad Request"
                };
            }
            catch
            {
                return new ResponseModel<bool>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel<ProductInfoVM>> UpdateProduct(RequestModel<ProductInfoVM> request)
        {
            try
            {
                if (await FindByCondition(x => x.Id == request.Data!.id && x.TraderId==request.User.id).AnyAsync())
                {
                    Product product = await FindByCondition(x => x.Id == request.Data.id).FirstAsync();
                    product = _mapper.Map<Product>(request.Data);
                    product.LastModifiedDate = DateTime.Now;
                    Update(product);
                    await _repo.Save();
                    return new ResponseModel<ProductInfoVM>
                    {
                        Status = (int)HttpStatusCode.OK,
                        Token = request.Token,
                        Data = request.Data,
                        Message = "Product Updated Successfully"
                    };
                }
                return new ResponseModel<ProductInfoVM>
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Token = request.Token,
                    Message = "Bad Request"
                };
            }
            catch
            {
                return new ResponseModel<ProductInfoVM>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Token = request.Token,
                    Message = "Internal Server Error"
                };
            }
        }

        public async Task<double> GetPrice(int productId)
        {
            if (await FindByCondition(x => x.Id == productId).AnyAsync())
            {
                Product product = await FindByCondition(x => x.Id == productId).FirstAsync();
                return (double)product.Price;
            }
            throw new Exception();
        }
    }
}
