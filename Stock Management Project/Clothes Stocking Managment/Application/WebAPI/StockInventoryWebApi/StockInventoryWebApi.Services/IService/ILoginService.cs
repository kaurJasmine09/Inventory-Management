using StockInventoryWebApi.BusinessModel.DataTransferModel;
using StockInventoryWebApi.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockInventoryWebApi.Services.IService
{
    public interface ILoginService
    {
        EmployeeInfoDto AuthenticateUser(int employeeId, int pin); 
    }
}
