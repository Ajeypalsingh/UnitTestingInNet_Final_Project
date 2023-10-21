using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EcommerceApp.Data;
using EcommerceApp.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EcommerceAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceAppContext") ?? throw new InvalidOperationException("Connection string 'EcommerceAppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

 
builder.Services.AddScoped(typeof(IProductRepository<Product>), typeof(ProductRepository));
builder.Services.AddScoped(typeof(ICartRepository<Cart>), typeof(CartRepository));

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider provider = scope.ServiceProvider;

    await SeedData.Initialize(provider);
}
// Configure the HTTP request pipeline.
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
    pattern: "{controller=Catalogue}/{action=Index}/{id?}");

app.Run();
