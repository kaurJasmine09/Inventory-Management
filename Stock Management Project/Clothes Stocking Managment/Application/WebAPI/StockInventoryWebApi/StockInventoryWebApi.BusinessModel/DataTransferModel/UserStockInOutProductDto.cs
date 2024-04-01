using System;
using System.Collections.Generic;
using System.Text;

namespace StockInventoryWebApi.BusinessModel.DataTransferModel
{
    public class UserStockInOutProductDto
    {
        public int EmployeeId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset Date { get; set; }
        public int TransactionNumber { get; set; }
        public string TransactionId { get; set; }
        public string Comments { get; set; }
        public string Type { get; set; }
        public int? SupplierId { get; set; }
        public int? CustomerId { get; set; }
    }

    public class TransactionResponse
    {
        public bool IsSuccess { get; set; }
        public string TransctionId { get; set; }
    }
}
