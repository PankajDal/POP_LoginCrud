
using CRUD_MVC.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CustomerContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AppCon") ?? throw new
InvalidOperationException("Connection string 'CRUDDotnet_6_EntityContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Login}/{id?}");

app.Run();


