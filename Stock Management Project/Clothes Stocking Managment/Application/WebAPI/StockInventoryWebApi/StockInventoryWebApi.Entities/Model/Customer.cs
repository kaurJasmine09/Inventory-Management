using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace StockInventoryWebApi.Web.Models
{
    public partial class Customer
    {
        public Customer()
        {
            UserStockInOutProduct = new HashSet<UserStockInOutProduct>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<UserStockInOutProduct> UserStockInOutProduct { get; set; }
    }
}
