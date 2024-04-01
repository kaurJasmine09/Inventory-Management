using StockInventoryWebApi.BusinessModel.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockInventoryWebApi.Services.IService
{
    public interface ICustomerService
    {
        List<CustomerDto> GetAllCustomers();
        CustomerDto GetCustomerById(int custId);
        bool AddCustomer(CustomerDto customer);
        bool UpdateCustomer(CustomerDto customer);
        bool DeleteCustomer(int custId);
    }
}
