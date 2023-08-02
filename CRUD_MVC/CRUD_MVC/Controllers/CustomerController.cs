using CRUD_MVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace CRUD_MVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerContext _context;
        //Customer Opration
        public CustomerController(CustomerContext context)
        {
            _context = context;
        }
        public IActionResult ActionName(string searchQuery)
        {

            var searchResults = _context.Tbl_Customer.ToList();
            return View(searchResults);
        }
        [HttpGet]
        public IActionResult Index()
        {
            var C = _context.Tbl_Customer.ToList();
            return View(C);
        }

        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAdmin(Customer cust)
        {
            _context.Tbl_Customer.Add(cust);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer cust)
        {
            _context.Tbl_Customer.Add(cust);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var customer = _context.Tbl_Customer.Where(x => x.CustomerID == id).FirstOrDefault();
            return View(customer);

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _context.Tbl_Customer.Where(x => x.CustomerID == id).FirstOrDefault();
            return View(customer);

        }
        [HttpPost]
        public IActionResult Edit(Customer cust)
        {
            _context.Tbl_Customer.Update(cust);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = _context.Tbl_Customer.Where(x => x.CustomerID == id).FirstOrDefault();
            return View(customer);

        }
        [HttpPost]
        public IActionResult Delete(Customer cust)
        {

            _context.Tbl_Customer.Remove(cust);
            _context.Entry(cust).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Logout()
        {
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //ViewBag.SysTime = DateTime.Now.ToLongTimeString();
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CallProductList()
        {
            TempData.Keep("Email"); // Keep TempData["Email"] during the redirect

            // Redirect to the ProductList action in the ProductController
            return RedirectToAction("ProductList", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            TempData["Email"] = "";
            if (ModelState.IsValid)
            {
                var User = from m in _context.Tbl_Customer select m;
                User = User.Where(s => s.CustomerEmailID.Contains(model.CustomerEmailID));
                if (User.Count() != 0)
                {

                    if (User.First().CustomerPassword == model.CustostomerPassword)
                    
                    {
                        TempData["Email"] = model.CustomerEmailID;
                        TempData.Keep("Email"); // Keep TempData["Email"] during the redirect

                        if (User.First().CustomerRole == "Admin")
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            if (User.First().CustomerEmailID == model.CustomerEmailID.ToString())
                            {
                                if (User.First().CustomerRole == "Customer")
                                    return RedirectToAction("CallProductList");
                            }
                            
                        }
                    }
                }
            }
            return RedirectToAction("Fail");
        }
    }
}


