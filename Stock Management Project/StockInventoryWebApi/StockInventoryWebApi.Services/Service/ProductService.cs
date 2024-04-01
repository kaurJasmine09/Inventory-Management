using StockInventoryWebApi.BusinessModel.DataTransferModel;
using StockInventoryWebApi.Entities.GenericRepo;
using StockInventoryWebApi.Services.IService;
using StockInventoryWebApi.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockInventoryWebApi.Services.Service
{
    public class ProductService : IProductService
    {
        #region Private Member Variables
        /// <summary>
        /// Initialise generic data context variable.
        /// </summary>
        private readonly GenericUnitOfWork<ClothingStock_DBContext> _unitOfWork;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the dependencies of services
        /// </summary>
        /// <param name="unitOfWork">unit of work for repository</param>
        public ProductService(GenericUnitOfWork<ClothingStock_DBContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public bool AddProduct(ProductDto product)
        {
            bool isSuccess = false;
            try
            {
                if(product != null)
                {
                    Product prod = new Product
                    {
                        Description = product.Description,
                        ProductName = product.ProductName,
                        Category = product.Category,
                        ProductId = product.ProductId,
                        Quantity = product.Quantity,
                        Size = product.Size
                    };
                    _unitOfWork.Repository<Product>().Insert(prod);
                    _unitOfWork.SaveChanges();
                    isSuccess = true;
                }

            }
            catch(Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public bool DeleteProduct(int prodId)
        {
            bool isSuccess = false;
            try
            {
                if (prodId > 0)
                {
                    Product prod = _unitOfWork.Context.Product.Where(x => x.ProductId == prodId).FirstOrDefault();
                    if (prod != null)
                    {
                        _unitOfWork.Repository<Web.Models.Customer>().Delete(prod);
                        _unitOfWork.SaveChanges();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;

        }

        public List<ProductDto> GetAllProduct()
        {
            List<ProductDto> prod = _unitOfWork.Context.Product
                                          .Select(x => new ProductDto
                                          {
                                              Description = x.Description,
                                              ProductName = x.ProductName,
                                              Category = x.Category,
                                              ProductId = x.ProductId,
                                              Quantity = x.Quantity,
                                              Size = x.Size
                                          }).ToList();

            return prod;
        }

        public ProductDto GetProductById(int prodId)
        {
            ProductDto productDto = null;
            if (prodId > 0)
            {
                productDto = _unitOfWork.Context.Product.Where(x => x.ProductId == prodId)
                                          .Select(x => new ProductDto
                                          {
                                              Description = x.Description,
                                              ProductName = x.ProductName,
                                              Category = x.Category,
                                              ProductId = x.ProductId,
                                              Quantity = x.Quantity,
                                              Size = x.Size
                                          }).FirstOrDefault();
            }

            return productDto;
        }

        public bool UpdateProduct(ProductDto product)
        {
            bool isSuccess = false;
            try
            {
                if (product != null)
                {
                    Product prod = _unitOfWork.Context.Product
                                                            .Where(x => x.ProductId == product.ProductId).FirstOrDefault();
                    if (prod != null)
                    {
                        if (product.Description != prod.Description)
                            prod.Description = product.Description ?? prod.Description;
                        if (product.ProductName != prod.ProductName)
                            prod.ProductName = product.ProductName;
                        if (product.Category != prod.Category)
                            prod.Category = product.Category ?? prod.Category;
                        if (product.Quantity != prod.Quantity)
                            prod.Quantity = product.Quantity;
                        if (product.Size != prod.Size && !String.IsNullOrWhiteSpace(product.Size))
                            prod.Size = product.Size;

                        _unitOfWork.Repository<Product>().Update(prod);
                        _unitOfWork.SaveChanges();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}
