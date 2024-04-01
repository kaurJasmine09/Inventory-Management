using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace StockInventoryWebApi.Web.Models
{
    public partial class UserStockInOutProduct
    {
        public int EmployeeId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int TransactionNumber { get; set; }
        public string Comments { get; set; }
        public string Type { get; set; }
        public int? SupplierId { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual SystemUser Employee { get; set; }
        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
