using Microsoft.AspNetCore.Mvc;
using Foodie.DataModels;
using System.Data;
using Foodie.Models;
using Microsoft.Build.Evaluation;

namespace Foodie.Controllers
{
    public class AdminController : Controller
    {

        private readonly DataAccessModel _da;

        public AdminController(DataAccessModel dataAccessModel)
        {
            _da = dataAccessModel;
        }

        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("Usename");
            string password = HttpContext.Session.GetString("Password");
            if ((username == null && password == null) || (username != "Admin" && username != "123"))
            {
                return View("~/Views/Login/Login_index.cshtml");
            }
            HttpContext.Session.SetInt32("CategoryCount", _da.AdminDashboardCounts("CATEGORY"));
            HttpContext.Session.SetInt32("ProductCount", _da.AdminDashboardCounts("PRODUCT"));
            return View();
        }

        #region Category

        //Function that returns list of category and shows that category into table format with required fields
        public IActionResult Category()
        {
            string username = HttpContext.Session.GetString("Usename");
            string password = HttpContext.Session.GetString("Password");
            if (username == null && password == null)
            {
                return View("~/Views/Login/Login_index.cshtml");
            }
            List<CategoryModel> categories  = _da.getCategories("SELECT");

            return View(categories);
        }

        [HttpPost]
        public JsonResult getCategory(string operation)
        {
            List<CategoryModel> categories = _da.getCategories(operation);
            return Json(categories);
        }

        [HttpPost]
        public JsonResult GetCategoryDetail(int id,string operation)
        {
            List<CategoryModel> categories = _da.getCategoryById(id, operation);
            ViewBag.Name = string.Empty;
            ViewBag.IsActive = 0;
            if (categories.Count ==0 || categories == null)
            {
                return Json(1);
            }
            return Json(categories);
        }

        //EditCategory 
        [HttpPost]
        public JsonResult EditCategory(CategoryModel category)
        {
            bool isUpdate = false;
            if (category != null)
            {
                if (category.Image != null && category.Image.Length > 0)
                {
                    // Generate a unique filename using GUID
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + category.Image.FileName;

                    // Define the path where the image will be saved
                    //string imagePath = Path.Combine("~/Images/Category/", uniqueFileName);
                    string imagePath = Path.Combine("Images", "Category", uniqueFileName);

                    // Save the image to the specified path
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath), FileMode.Create))
                    {
                        category.Image.CopyTo(stream);
                        category.ImageUrl = imagePath;
                    }
                }
                isUpdate = _da.EditCategoryById(category);
            }

            return Json(isUpdate);
        }

        #endregion Category

        #region Product
        public IActionResult Product()
        {
            string username = HttpContext.Session.GetString("Usename");
            string password = HttpContext.Session.GetString("Password");
            if (username == null && password == null)
            {
                return View("~/Views/Login/Login_index.cshtml");
            }
            List<Products> products = _da.getProducts(0,"SELECT");
            return View(products);
        }
        [HttpPost]
        public JsonResult EditProduct(int id,string operation)
        {
            List<Products> products = _da.getProducts(id, operation);
            return Json(products);
        }

        [HttpPost]
        public JsonResult UpdateOrInsertProduct(Products product)
        {
            string status = string.Empty;
            if(product != null)
            {
                if (product.Image != null && product.Image.Length > 0)
                {
                    // Generate a unique filename using GUID
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.Image.FileName;

                    // Define the path where the image will be saved
                    //string imagePath = Path.Combine("~/Images/Category/", uniqueFileName);
                    string imagePath = Path.Combine("Images", "Product", uniqueFileName);

                    // Save the image to the specified path
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath), FileMode.Create))
                    {
                        product.Image.CopyTo(stream);
                        product.ImageUrl = imagePath;
                    }
                }
                status = _da.UpdateOrInsertProduct(product);
                return Json(status);
            }
            return Json(status);  
        }
        #endregion Product


        public IActionResult OrderStatus()
        {
            string username = HttpContext.Session.GetString("Usename");
            string password = HttpContext.Session.GetString("Password");
            if ((username != "Admin" && password != "123") || (username == null && password == null))
            {
                return View("~/Views/Login/Login_index.cshtml");
            }
            return View();
        }
        public IActionResult Users()
        {
            string username = HttpContext.Session.GetString("Usename");
            string password = HttpContext.Session.GetString("Password");
            if (username == null && password == null)
            {
                return View("~/Views/Login/Login_index.cshtml");
            }
            return View();
        }
    }
}
