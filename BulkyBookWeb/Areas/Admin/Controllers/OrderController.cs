using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Utility;
using BulkyBooks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> orderHeaders;
            orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");

            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(x => x.PaymentIntentStatus == SD.PaymentStatusDelayedPayment);
                    break;

                case "inprocess":
                    orderHeaders = orderHeaders.Where(x => x.OrderStatus == SD.StatusInProcess);
                    break;

                case "completed":
                    orderHeaders = orderHeaders.Where(x => x.OrderStatus == SD.StatusShipped);
                    break;

                default:
                    break;
            }


            return Json(new { data = orderHeaders });
        }

        #endregion
    }
}
