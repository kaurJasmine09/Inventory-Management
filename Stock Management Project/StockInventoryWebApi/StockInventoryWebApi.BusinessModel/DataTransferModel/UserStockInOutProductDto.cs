using System;
using System.Collections.Generic;
using System.Text;

namespace StockInventoryWebApi.BusinessModel.DataTransferModel
{
    public class UserStockInOutProductDto
    {
        public int EmployeeId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int TransactionNumber { get; set; }
        public string Comments { get; set; }
        public string Type { get; set; }
    }
}
