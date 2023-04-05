using ItemLog.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddDbContext<DataContext>(options => {
//    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
//});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConnection"]);
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();




var app = builder.Build();

app.UseSession();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Items}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "items",
    pattern: "/items/{categorySlug?}",
    defaults: new { controller = "Items", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

