using ElectroCommerce.DataAccess.Data;
using ElectroCommerce.DataAccess.Repository.IRepository;
using ElectroCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroCommerce.DataAccess.Repository
{
	public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
	{
		private ApplicationDbContext _db;
		public OrderDetailRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(OrderDetail orderDetail)
		{
			_db.OrderDetails.Update(orderDetail);
		}
	}
}
