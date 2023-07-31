using Microsoft.EntityFrameworkCore;

namespace CRUD_MVC.Models
{
    public class CustomerContext :DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }
        public DbSet<Customer> Tbl_Customer { get; set; }

        public DbSet<Product> Tbl_Product { get; set; }

    }
}
