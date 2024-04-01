using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace StockInventoryWebApi.Web.Models
{
    public partial class SystemUser
    {
        public int EmployeeId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int Pin { get; set; }
        public string Phone { get; set; }

        public virtual UserStockInOutProduct UserStockInOutProduct { get; set; }
    }
}
