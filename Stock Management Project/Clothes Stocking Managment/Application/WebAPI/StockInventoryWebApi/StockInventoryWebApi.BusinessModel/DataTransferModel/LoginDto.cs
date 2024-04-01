using System;
using System.Collections.Generic;
using System.Text;

namespace StockInventoryWebApi.BusinessModel.DataTransferModel
{
    public class LoginDto
    {
        public int EmployeeId { get; set; }
        public int Pin { get; set; }
    }
}
