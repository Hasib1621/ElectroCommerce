using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroCommerce.Models.ViewModels
{
    public class DashboardVM
    {
        public int TotalOrderToday { get; set; }
        public int TotalProducts { get; set; }
        public int TotalCustomers { get; set; }
        public string TopProduct { get; set; }
        public string TopCustomer{ get; set; }
    }
}
