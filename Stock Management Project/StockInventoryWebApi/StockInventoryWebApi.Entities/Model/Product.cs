using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace StockInventoryWebApi.Web.Models
{
    public partial class Product
    {
        public Product()
        {
            UserStockInOutProduct = new HashSet<UserStockInOutProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<UserStockInOutProduct> UserStockInOutProduct { get; set; }
    }
}
