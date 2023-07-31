using CRUD_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly CustomerContext _Pro;

        public ProductController(CustomerContext pro) 
        {
            _Pro = pro;  
        }
        public IActionResult ProductList(string searchTerm)
        {
            
            var products = _Pro.Tbl_Product.ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.ProductName.Contains(searchTerm)).ToList();
            }

            ViewBag.SearchTerm = searchTerm; // Pass the search term to the view

            return View(products);
        }

        [HttpPost]
        public IActionResult modelPopup(List<int> selectedProducts)
        {
            // Process the selected product IDs here
            var selectedProductList = _Pro.Tbl_Product
                .Where(p => selectedProducts.Contains(p.ProductID))
                .ToList();
            return View(selectedProducts);
            // Pass the selected products to the Invoice view
           // return RedirectToAction("Invoice", selectedProductList);
        }

        [HttpPost]
        public IActionResult ProcessSelectedProducts(List<int> selectedProducts)
        {
            TempData.Keep("Email"); // Keep TempData["Email"] during the redirect

            var obj =TempData["SelectedProducts"] = selectedProducts;
            
            return View("ProcessSelectedProducts", obj);
      
        }
        public IActionResult InvoiceCostList(List<int> selectedProducts)
        {
           // var CurrentCustomer = _Pro.Tbl_Customer.Where(q => selectedProducts.Contains(q.CustomerID)).ToList();
            var custEmail = TempData["Email"]; // Keep TempData["Email"] during the redirect
            var CurrentCustomer = _Pro.Tbl_Customer.Select(s => new { s.CustomerName,s.CustomerEmailID,s.CustomerPhoneNo}).Where(q => q.CustomerEmailID==custEmail.ToString()).ToList();

            if (CurrentCustomer.Count > 0)
            {
                foreach (var item in CurrentCustomer)
                {
                    ViewBag.Name = item.CustomerName;
                    ViewBag.PhoneNo = item.CustomerPhoneNo;
                    ViewBag.Email = item.CustomerEmailID;
                }
              
            }
            
            var SelectedProducts = TempData["SelectedProducts"] = selectedProducts;
            List<Product> selectedProductList = _Pro.Tbl_Product
                .Where(p => selectedProducts.Contains(p.ProductID))
                .ToList();
            return View(selectedProductList);
            //sreturn View(CurrentCustomer);

        }

        [HttpGet]
        public IActionResult Invoice()
        {
            // Retrieve the selected products and their quantities from the TempData
            var selectedProducts = TempData["SelectedProducts"] as List<int>;
            var quantities = TempData["Quantities"] as Dictionary<int, int>;

            if (selectedProducts == null || quantities == null)
            {
                // Handle the case where the selected products or quantities are not found
                return RedirectToAction("ProcessSelectedProducts");
            }

            var selectedProductList = _Pro.Tbl_Product
                .Where(p => selectedProducts.Contains(p.ProductID))
                .ToList();

            // Calculate the total quantity for each selected product
            foreach (var product in selectedProductList)
            {
                if (quantities.TryGetValue(product.ProductID, out int quantity))
                {
                    product.Quantity = quantity;
                }
            }

            // Calculate the total price of the selected products
            decimal totalPrice = selectedProductList.Sum(p => p.ProductPrice * p.Quantity);

            // Pass the selected products and total price to the Invoice view
            var invoiceViewModel = new InvoiceViewModel
            {
                SelectedProducts = selectedProductList,
                TotalPrice = totalPrice
            };

            return View(invoiceViewModel);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]  
        //public IActionResult AddProduct(Product P)
        public IActionResult AddProduct(ProductViewModel P, Product pro)
        {
            Product Product = new Product();
            Product.ProductID = pro.ProductID;
            Product.ProductPrice = pro.ProductPrice;
            Product.Quantity = pro.Quantity;
             Product.ProductImage = pro.ProductImage;
            P.Product = Product;
           
            if (ModelState.IsValid)
            {
                // Save the image to the ProductImage property
                if (P.Image != null && P.Image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        P.Image.CopyTo(memoryStream);
                        P.Product.ProductImage = memoryStream.ToArray();
                    }
                }

                _Pro.Tbl_Product.Add(P.Product);

                _Pro.SaveChanges();
                return RedirectToAction("ProductList");
            }

            ////////////////////////////////////


            //_Pro.Tbl_Product.Add(P);
            //_Pro.SaveChanges();
            return RedirectToAction("ProductList");

        }
           



































        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var customer = _Pro.Tbl_Product.Where(x => x.ProductID == id).FirstOrDefault();
            return View(customer);
        }
        [HttpPost]
        public IActionResult EditProduct(Product P) 
        {
            _Pro.Tbl_Product.Update(P);
            _Pro.SaveChanges();
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var customer = _Pro.Tbl_Product.Where(x => x.ProductID == id).FirstOrDefault();
            return View(customer);
        }
        [HttpPost]
        public IActionResult DeleteProduct(Product P)
        {
            _Pro.Tbl_Product.Remove(P);
            _Pro.Entry(P).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _Pro.SaveChanges();
            return RedirectToAction("ProductList");
        }
    }
}
