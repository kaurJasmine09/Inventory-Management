using StockInventoryWebApi.BusinessModel.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockInventoryWebApi.Services.IService
{
    public interface ISupplierService
    {
        List<SupplierDto> GetAllSupplier();
        SupplierDto GetSupplierById(int supplierId);
        bool AddSupplier(SupplierDto supplier);
        bool UpdateSupplier(SupplierDto supplier);
        bool DeleteSupplier(int supplier);
    }
}
