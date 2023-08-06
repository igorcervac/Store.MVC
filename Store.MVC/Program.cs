using Store.MVC.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs/Store.MVC.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .WriteTo.Debug()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(op => op.UseMySql(builder.Configuration.GetConnectionString("StoreMvcDB"), ServerVersion.Parse("8.0.21-Vitess")));
builder.Services.AddScoped<IReadOnlyQueryableRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IReadOnlyRepository<ProductType>, ProductTypeRepository>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(sp => CartItemRepository.GetCart(sp));
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());    

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.MapControllerRoute(name: "default", pattern: "{controller=Products}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}

app.Run(); ;
