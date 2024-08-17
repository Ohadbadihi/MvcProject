using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IRepository, MyRepository>();
string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"]!;
builder.Services.AddDbContext<PetShopContext>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies()); 
builder.Services.AddControllersWithViews();
var app = builder.Build();

if (app.Environment.IsStaging() || app.Environment.IsProduction()) // need to create controller name Error Controller
{
    app.UseExceptionHandler("/Error/Index");
}

app.UseStaticFiles();


using (var scope = app.Services.CreateScope())
{
    var petShopContext = scope.ServiceProvider.GetRequiredService<PetShopContext>();
    petShopContext.Database.EnsureDeleted();
    petShopContext.Database.EnsureCreated();
}

app.UseRouting();
app.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");

app.Run();

