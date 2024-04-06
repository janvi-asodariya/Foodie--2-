using Foodie.DataModels;
using Foodie.Models;
using Microsoft.AspNetCore.Mvc;

namespace Foodie.Controllers
{
    public class UserController : Controller
    {
        private readonly DataAccessModel _dmAdminDashboard;

        public UserController(DataAccessModel dmAdminDashboard)
        {
            _dmAdminDashboard = dmAdminDashboard;
        }
        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("Usename");
            string password = HttpContext.Session.GetString("Password");
            if (username == null && password == null)
            {
                return View("~/Views/Login/Login_index.cshtml");
            }
            else
            {
                List<CategoryModel> categories = _dmAdminDashboard.getCategories("ACTIVECAT");
                return View("~/Views/User/User_dashboard.cshtml",categories);
            }
        }
        public IActionResult Menu()
        {
            string username = HttpContext.Session.GetString("Usename");
            string password = HttpContext.Session.GetString("Password");
            if (username == null && password == null)
            {
                return View("~/Views/Login/Login_index.cshtml");
            }
            List<Products> products = _dmAdminDashboard.getProducts(0, "ACTIVEPRODUCT");
            return View("~/Views/User/User_menu.cshtml",products);
        }
        public IActionResult Cart()
        {
            string username = HttpContext.Session.GetString("Usename");
            string password = HttpContext.Session.GetString("Password");
            if (username == null && password == null)
            {
                return View("~/Views/Login/Login_index.cshtml");
            }
            int userid = HttpContext.Session.GetInt32("UserId") == null ? 0 : Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            List<Cart> cart = _dmAdminDashboard.GetCartItems(userid);
            if (cart != null)
            {
                int totalprice = Convert.ToInt32(ViewBag.TotalPrice);
                HttpContext.Session.SetInt32("totalprice", totalprice);
                return View("~/Views/User/User_cart.cshtml",cart);
            }
            return View("~/Views/User/User_cart.cshtml");
        }

        [HttpPost]
        public JsonResult AddToCart(Cart cartItems)
        {
            if (cartItems != null)
            {
                int userid = HttpContext.Session.GetInt32("UserId") == null ? 0 : Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                bool result = _dmAdminDashboard.AddToCart(cartItems, userid);
                return Json(result);
            }
            return Json(null);
        }
        public JsonResult RemoveItem(int id)
        {
            if (id != null && id >0)
            {
                int userid = HttpContext.Session.GetInt32("UserId") == null ? 0 : Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                bool result = _dmAdminDashboard.RemoveFromCart(id, userid);
                return Json(result);
            }
            return Json(null);
        }

        public IActionResult CheckOut()
        {
            return View("~/Views/User/User_checkout.cshtml");
        }
        public JsonResult Orderpayment(Payment payment)
        {
                bool isInserted = _dmAdminDashboard.Orderpayment(payment);
                if(isInserted.Equals(true))
                {
                    return Json("true");
                }
                return Json(null);
        }
    }
}
