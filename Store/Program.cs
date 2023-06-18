using TestStore.Models;
using Microsoft.EntityFrameworkCore;
using TestStore.Infrastructure;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(s => SessionCart.GetCart(s));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IProductRepository, EFProductRepository>();
builder.Services.AddTransient<IOrderRepository, EFOrderRepository>();
builder.Services.AddTransient<IImageRepository, EFImageRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), o=>o.EnableRetryOnFailure());
});
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), o => o.EnableRetryOnFailure());
});
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

builder.Services.AddSession();
builder.Services.AddMemoryCache();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseStatusCodePages();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
                   name: null,
                   pattern: "{category}/Page{productPage:int}",
                   defaults: new { controller = "Product", action = "List" }
               );

app.MapControllerRoute(
    name: null,
    pattern: "Page{productPage:int}",
    defaults: new { controller = "Product", action = "List", productPage = 1 }
);

app.MapControllerRoute(
    name: null,
    pattern: "{category}",
    defaults: new { controller = "Product", action = "List", productPage = 1 }
);

app.MapControllerRoute(
    name: null,
    pattern: "",
    defaults: new { controller = "Product", action = "List", productPage = 1 });
app.MapControllerRoute(
    name: null, pattern: "{controller}/{action}/{id?}"
    );
app.Run();


