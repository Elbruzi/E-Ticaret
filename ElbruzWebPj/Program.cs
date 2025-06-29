using ElbruzWebPj.Models;
using ElbruzWebPj.Models.CrudRepositery;
using ElbruzWebPj.Models.CrudRepository;
using ElbruzWebPj.Models.CrudRepository.Interfaces;
using ElbruzWebPj.Models.Mapper;
using ElbruzWebPj.Models.Middleware;
using ElbruzWebPj.Models.MVVM;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ElbruzWebPj")));

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(10);
});


builder.Services.AddScoped<Cls_VM_Mapping>();

builder.Services.AddHttpContextAccessor(); 



builder.Services.AddScoped<ICrudRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<ICrudRepository<Product>, ProductRepository>();
builder.Services.AddScoped<ICrudRepository<Supplier>, SupplierRepository>();
builder.Services.AddScoped<ICrudRepository<ProductColor>, ColorRepository>();


builder.Services.AddScoped<Cls_CategoryExcl>();

builder.Services.AddScoped<Cls_ProductExcl>();

builder.Services.AddScoped<Cls_User>();

builder.Services.AddScoped<Cls_Order>();

builder.Services.AddScoped<Cls_Fetch>();

builder.Services.AddScoped<Cls_DTO>();





var app = builder.Build();

app.UseSession();
app.UseMiddleware<MW_Sessions>();


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
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
