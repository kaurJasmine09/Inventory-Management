using StockInventoryWebApi.BusinessModel.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockInventoryWebApi.Services.IService
{
    public interface IStockService
    {
        List<UserStockInOutProductDto> GetAllStocks();
        UserStockInOutProductDto GetStockById(int empId, int prodId, int transNum);
        bool AddStockRecord(UserStockInOutProductDto userStock);
        bool UpdateStockRecord(UserStockInOutProductDto userStock);
        bool DeleteStockRecord(int empId, int prodId, int transNum);
    }
}
