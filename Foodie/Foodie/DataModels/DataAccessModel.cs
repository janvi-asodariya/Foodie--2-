using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Xml.Linq;
using Foodie.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Foodie.DataModels
{
    public class DataAccessModel
    {
        private readonly IConfiguration _configuration;
        private readonly string _cs;

        public DataAccessModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _cs = _configuration.GetConnectionString("cs").Trim().ToString();
        }

        #region Category

        //Get all categories details
        public List<CategoryModel> getCategories(string operation)
        {
            DataTable dt;
            List<CategoryModel> categoryList = new List<CategoryModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    using (SqlCommand cmd = new SqlCommand("Category_Crud", con))
                    {
                        cmd.Parameters.AddWithValue("@Action", operation);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CategoryModel category = new CategoryModel();
                            category.CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"]);
                            category.Name = dt.Rows[i]["Name"].ToString();
                            category.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                            category.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                            category.CreatedDate = DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString());
                            categoryList.Add(category);
                        }
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
                return categoryList;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        //get categories by categoryId
        public List<CategoryModel> getCategoryById(int id, string command)
        {
            DataTable dt;
            List<CategoryModel> categoryList = new List<CategoryModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    using (SqlCommand cmd = new SqlCommand("Category_Crud", con))
                    {
                        cmd.Parameters.AddWithValue("@Action", command);
                        cmd.Parameters.AddWithValue("@CategoryId", id);
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (command == "GETBYID")
                        {
                            SqlDataAdapter sda = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            sda.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                CategoryModel category = new CategoryModel();
                                category.Name = dt.Rows[i]["Name"].ToString();
                                category.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                                category.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                                categoryList.Add(category);
                            }
                        }

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
                return categoryList;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        //Edit category by thier categoryId
        public bool EditCategoryById(CategoryModel category)
        {
            int i = 0;
            bool isUpdate = false;
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    using (SqlCommand cmd = new SqlCommand("Category_Crud", con))
                    {
                        cmd.Parameters.AddWithValue("@Action", category.CategoryId == 0 ? "INSERT" : "UPDATE");
                        cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                        cmd.Parameters.AddWithValue("@Name", category.Name);
                        cmd.Parameters.AddWithValue("@IsActive", category.IsActive);
                        if (category.ImageUrl != null && category.ImageUrl != string.Empty && category.CategoryId == 0)
                            cmd.Parameters.AddWithValue("@ImageUrl", category.ImageUrl);
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        i = cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
                if (i > 0) isUpdate = true;
                return isUpdate;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion Category

        #region Products
        public List<Products> getProducts(int id, string operation)
        {
            DataTable dt;
            List<Products> productList = new List<Products>();
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    using (SqlCommand cmd = new SqlCommand("Product_Crud", con))
                    {
                        cmd.Parameters.AddWithValue("@Action", operation);
                        if (operation == "GETBYID")
                        {
                            cmd.Parameters.AddWithValue("@ProductId", id);
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Products products = new Products();
                            if (operation != "GETBYID")
                            {

                                products.ProductId = Convert.ToInt32(dt.Rows[i]["ProductId"]);
                                products.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            }
                            products.Name = dt.Rows[i]["Name"].ToString();
                            products.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                            products.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                            products.CreatedDate = DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString());
                            products.Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                            products.Description = dt.Rows[i]["Description"].ToString();
                            products.CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"]);
                            //products.ProductId = id;
                            if (operation == "GETBYID" || operation == "SELECT")
                            {
                                products.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]);

                            }
                            if (operation == "GETBYID")
                            {
                                List<CategoryModel> categories = getCategoryById(products.CategoryId, "GETBYID");
                                products.CategoryName = categories[0].Name;
                            }
                            productList.Add(products);
                        }
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
                return productList;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public string UpdateOrInsertProduct(Products product)
        {
            int i = 0;
            bool isUpdate = false;
            string status = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    using (SqlCommand cmd = new SqlCommand("Product_Crud", con))
                    {
                        cmd.Parameters.AddWithValue("@Action", product.ProductId == 0 ? "INSERT" : "UPDATE");
                        cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                        cmd.Parameters.AddWithValue("@Name", product.Name.Trim());
                        cmd.Parameters.AddWithValue("@Description", product.Description.Trim());
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                        cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                        cmd.Parameters.AddWithValue("@IsActive", product.IsActive);
                        if (product.ImageUrl != null && product.ImageUrl != string.Empty && product.ProductId == 0)
                            cmd.Parameters.AddWithValue("@ImageUrl", product.ImageUrl);
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        i = cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
                if (i > 0 && product.ProductId == 0)
                {
                    status = "insert";
                }
                else if (i > 0 && product.ProductId != 0)
                {
                    status = "update";
                }
                return status;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        #endregion Products


        #region LoginRegister

        public List<RegisterModel> GetUserByUserName(string userName, string password)
        {
            DataTable dt;
            List<RegisterModel> user = new List<RegisterModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    using (SqlCommand cmd = new SqlCommand("User_Crud", con))
                    {
                        cmd.Parameters.AddWithValue("@Action", "SELECT4LOGIN");
                        cmd.Parameters.AddWithValue("@Username", userName);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RegisterModel registerUser = new RegisterModel();
                            registerUser.UserName = dt.Rows[i]["Username"].ToString();
                            registerUser.Password = dt.Rows[i]["Password"].ToString();
                            registerUser.UserId = Convert.ToInt32(dt.Rows[i]["UserId"]);

                            user.Add(registerUser);
                        }
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public bool registerUser(RegisterModel user)
        {
            int userid = user.UserId;
            bool inserted = false;
            int i;
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    using (SqlCommand cmd = new SqlCommand("User_Crud", con))
                    {
                        cmd.Parameters.AddWithValue("@Action", "INSERT");
                        cmd.Parameters.AddWithValue("@UserId", userid);
                        cmd.Parameters.AddWithValue("@Name", user.Name.Trim());
                        cmd.Parameters.AddWithValue("@Username", user.UserName.Trim());
                        cmd.Parameters.AddWithValue("@Mobile", user.Modile.Trim());
                        cmd.Parameters.AddWithValue("@Email", user.Email.Trim());
                        cmd.Parameters.AddWithValue("@Address", user.Address.Trim());
                        cmd.Parameters.AddWithValue("@Postcode", user.Postcode.Trim());
                        cmd.Parameters.AddWithValue("@Password", user.Password.Trim());
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        i = cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
                if (i > 0) inserted = true;
                return inserted;
            }
            catch (Exception ex)
            {
                return inserted;
            }
        }

        #endregion LoginRegister




        #region Cart

        public List<Cart> GetCartItems(int userid)
        {
            List<Cart> cartItems = new List<Cart>();
            DataTable dt;
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    using (SqlCommand cmd = new SqlCommand("Cart_Crud", con))
                    {
                        cmd.Parameters.AddWithValue("@Action", "SELECT");
                        cmd.Parameters.AddWithValue("@UserId", userid);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        sda.Fill(dt);
                        if (dt != null)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Cart carts = new Cart();
                                carts.ProductId = Convert.ToInt32(dt.Rows[i]["ProductId"]);
                                carts.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]);
                                carts.Qty = Convert.ToInt32(dt.Rows[i]["Qty"]);
                                carts.PrdQuantity = Convert.ToInt32(dt.Rows[i]["PrdQty"]);
                                carts.Price = Convert.ToInt32(dt.Rows[i]["Price"]);
                                carts.Name = dt.Rows[i]["Name"].ToString();
                                carts.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                                cartItems.Add(carts);
                            }
                        }
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return cartItems;
            }
            catch (Exception ex)
            {
                return cartItems;
            }

            #endregion Cart

        }

        public bool AddToCart(Cart items, int userid)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    using (SqlCommand cmd = new SqlCommand("Cart_Crud", con))
                    {
                        cmd.Parameters.AddWithValue("@Action", "INSERT");
                        cmd.Parameters.AddWithValue("@ProductId", items.ProductId);
                        cmd.Parameters.AddWithValue("@Quantity", items.Quantity);
                        cmd.Parameters.AddWithValue("@UserId", userid);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        i = cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
                if (i > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveFromCart(int id, int userid)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    using (SqlCommand cmd = new SqlCommand("Cart_Crud", con))
                    {
                        cmd.Parameters.AddWithValue("@Action", "DELETE");
                        cmd.Parameters.AddWithValue("@ProductId", id);
                        cmd.Parameters.AddWithValue("@UserId", userid);
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        i = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                if (i > 0) return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int AdminDashboardCounts(string tableName)
        {
            int count = 0; 
            using (SqlConnection con = new SqlConnection(_cs))
            {
                using(SqlCommand cmd = new SqlCommand("Dashboard", con))
                {
                    cmd.Parameters.AddWithValue("@Action", tableName);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        if (sdr[0] == DBNull.Value)
                        {
                            count = 0;     
                        }
                        else
                        {
                            count = Convert.ToInt32(sdr[0]);
                        }
                    }
                    sdr.Close();
                    con.Close();
                    
                }
            }
            return count;
        }


        public bool Orderpayment(Payment payment)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(_cs))
            {
                    //SqlTransaction transaction = con.BeginTransaction();
                    SqlCommand cmd = new SqlCommand("Save_Payment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", payment.Name);
                    cmd.Parameters.AddWithValue("@CardNo", payment.Cardno);
                    cmd.Parameters.AddWithValue("@ExpiryDate", payment.Expirydate);
                    cmd.Parameters.AddWithValue("@Cvv", payment.Cvv);
                    cmd.Parameters.AddWithValue("@Address", payment.Address);
                    cmd.Parameters.AddWithValue("@PaymentMode", payment.Paymentmode);
                    cmd.Parameters.Add("@InsertedId", SqlDbType.Int);
                    cmd.Parameters["@InsertedId"].Direction = ParameterDirection.Output;
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (i > 0)
                return true;
            else
                return false;
        }
    }
}
