using System;
using System.Collections.Generic;
using System.Text;

namespace StockInventoryWebApi.BusinessModel.DataTransferModel
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
