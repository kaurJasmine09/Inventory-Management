using System;
using System.Collections.Generic;
using System.Text;

namespace StockInventoryWebApi.BusinessModel.DataTransferModel
{
    public class EmployeeInfoDto
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int Pin { get; set; }
        public string Phone { get; set; }
    }
}
