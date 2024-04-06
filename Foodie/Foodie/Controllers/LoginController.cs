using Foodie.Models;
using Microsoft.AspNetCore.Mvc;
using Foodie.DataModels;

namespace Foodie.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataAccessModel _da;

        public LoginController(DataAccessModel dataAccessModel)
        {
            _da = dataAccessModel;
        }
        public IActionResult Index()
        {
            return View("~/Views/Login/Login_index.cshtml");
        }

        public JsonResult ValidateUser(RegisterModel userObj)
        {
            if(userObj != null)
            {
                if (userObj.UserName == "Admin" && userObj.Password == "123")
                {
                    HttpContext.Session.SetString("Usename", userObj.UserName);
                    HttpContext.Session.SetString("Password", userObj.Password);
                    return Json("admin");
                }
                else
                {
                    List<RegisterModel> user = _da.GetUserByUserName(userObj.UserName, userObj.Password);
                    if(user != null && user.Count > 0)
                    {
                        if (user[0].UserName == userObj.UserName && user[0].Password == userObj.Password)
                        {
                            HttpContext.Session.SetString("Usename", userObj.UserName);
                            HttpContext.Session.SetString("Password", userObj.Password);
                            HttpContext.Session.SetInt32("UserId", user[0].UserId);
                            return Json("user");
                        }
                        else
                        {
                            return Json("NotRegistered");
                        }
                    }
                }
                return Json(userObj.Password);
            }
            else
            {
                return Json("No User found");
            }
        }

        public IActionResult RegisterIndex()
        {
            return View("~/Views/Login/Register_index.cshtml");
        }

        public JsonResult RegisterUser(RegisterModel userObj)
        {
            if(userObj != null)
            {
                bool inserted = false;
                inserted = _da.registerUser(userObj);
                return Json(inserted);
            }
            return Json(null) ;
        }
    }
}
