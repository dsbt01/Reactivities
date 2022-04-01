using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Models;
using BulkyBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeader
    {
        private ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext Db) : base(Db)
        {
            _db = Db;

        }

        public void Update(OrderHeader orderHeader)
        {
            _db.OrderHeaders.Update(orderHeader);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderHeaderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);

            if (orderHeaderFromDb != null)
            {
                orderHeaderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderHeaderFromDb.PaymentStatus = paymentStatus; 
                }
                _db.SaveChanges();
            }
        }

        public void UpdateStripedPaymentid(int id, string sessionId, string? paymentIntentId)
        {
            var orderHeaderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);

            if (orderHeaderFromDb != null)
            {
                orderHeaderFromDb.PaymentDate = System.DateTime.Now;
                orderHeaderFromDb.SessionId = sessionId;
                orderHeaderFromDb.PaymentIntentId = paymentIntentId;

                _db.SaveChanges();
            }
        }
    }
}
