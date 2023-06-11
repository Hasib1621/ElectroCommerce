using ElectroCommerce.DataAccess.Repository.IRepository;
using ElectroCommerce.Models;
using ElectroCommerce.Models.ViewModels;
using ElectroCommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectroCommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DashboardController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            int totalProducts = _unitOfWork.Product.GetAll().Count();
            int totalOrderToday = _unitOfWork.OrderHeader.GetAll().Count(od => od.OrderDate.Date == DateTime.Today);
            int totalCustomers = _unitOfWork.ApplicationUser.GetAll().Count();
            int topProductId = _unitOfWork.OrderDetail.GetAll().GroupBy(od => od.ProductId).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();
            string topProductName = _unitOfWork.Product.Get(u=>u.Id == topProductId).Title;

            string topCustomerId = _unitOfWork.OrderHeader.GetAll().GroupBy(u=>u.ApplicationUserId).OrderByDescending(u=> u.Count()).Select(u=>u.Key).FirstOrDefault();

            string topCustName = _unitOfWork.ApplicationUser.Get(u=>u.Id == topCustomerId).Name;


            DashboardVM dashboardVM = new()
            {
                TotalCustomers = totalCustomers,
                TotalProducts = totalProducts,
                TotalOrderToday = totalOrderToday,
                TopCustomer = topCustName,
                TopProduct = topProductName
            };

            return View(dashboardVM);
        }

        public IActionResult ProductWiseChart()
        {
            var productIdCounts = _unitOfWork.OrderDetail
                .GetAll()
                .GroupBy(od => od.ProductId)
                .Select(g => new { ProductId = g.Key, Count = g.Count() })
                .ToDictionary(x => _unitOfWork.Product.Get(u=>u.Id == x.ProductId).Title, x => x.Count);
            var maxCountEntries = productIdCounts.OrderByDescending(x => x.Value)
                                    .Take(10)
                                    .ToDictionary(x => x.Key, x => x.Value);

            return View(maxCountEntries);
        }

        public IActionResult CategoryWiseChart()
        {
            


            var categoryCounts = _unitOfWork.OrderDetail
                .GetAll()
                .Join(_unitOfWork.Product.GetAll(), od => od.ProductId, p => p.Id, (od, p) => new { od, p })
                .GroupBy(x => x.p.CategoryId)
                .Select(g => new { CategoryId = g.Key, Count = g.Count() })
                .ToDictionary(x => _unitOfWork.Category.Get(u => u.Id == x.CategoryId).Name, x => x.Count);

            var maxCountEntries = categoryCounts.OrderByDescending(x => x.Value)
                                    .Take(10)
                                    .ToDictionary(x => x.Key, x => x.Value);
            return View(maxCountEntries);
        }

        public IActionResult EarningPerDay()
        {

            var thirtyDaysAgo = DateTime.Now.Date.AddDays(-30);
            var approvedPayments = _unitOfWork.OrderHeader
                .GetAll()
                .Where(o => o.PaymentStatus == "Approved" && o.PaymentDate.Date >= thirtyDaysAgo)
                .GroupBy(o => o.PaymentDate.Date)
                .ToDictionary(g => g.Key.ToString("yyyy-MM-dd"), g => g.Sum(o => o.OrderTotal));


            return View(approvedPayments);
        }

        public IActionResult SaleReport()
        {
            var orderDetails = _unitOfWork.OrderDetail.GetAll()
            .Join(
                _unitOfWork.Product.GetAll(),
                od => od.ProductId,
                p => p.Id,
                (od, p) => new
                {
                    ProductTitle = p.Title,
                    Count = od.Count,
                    Price = od.Price
                }
            )
            .GroupBy(d => d.ProductTitle)
            .Select(g => new
            {
                ProductTitle = g.Key,
                Count = g.Sum(d => d.Count),
                Price = g.First().Price
            });


            return View(orderDetails);
        }

    }
}
