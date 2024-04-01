using StockInventoryWebApi.BusinessModel.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockInventoryWebApi.Services.IService
{
    public interface IProductService
    {
        List<ProductDto> GetAllProduct();
        ProductDto GetProductById(int prodId);
        bool AddProduct(ProductDto product);
        bool UpdateProduct(ProductDto product);
        bool DeleteProduct(int prodId);
    }
}
