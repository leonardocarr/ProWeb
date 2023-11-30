using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProWebApp.Areas.Identity.Data;
using ProWebApp.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProWebDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ProWebDbContextConnection' not found.");

builder.Services.AddDbContext<ProWebDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ProWebDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews(); 
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
