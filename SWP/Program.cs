using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Models;
using Repository.Repositories;
using Service.Services;
using Repository;
using Service.Services.VNPay;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DiamondShopContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<ICartService, CartService>();

		// Thêm dịch vụ vào container.
		builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IDiscountService, DiscountService>();
        builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IUserRepository, UserRepository>();
		builder.Services.AddScoped<IProductService, ProductService>();
		builder.Services.AddScoped<IProductRepository, ProductRepository>();
		builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
		builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IWalletPointRepository, WalletPointRepository>();
        builder.Services.AddScoped<IWalletService, WalletPointService>();
        builder.Services.AddScoped<IWarrantyRepository, WarrantyRepository>();
        builder.Services.AddScoped<IWarrantyService, WarrantyService>();


        builder.Services.AddDistributedMemoryCache();
		builder.Services.AddSession(options =>
		{
			options.IdleTimeout = TimeSpan.FromMinutes(30);
			options.Cookie.HttpOnly = true;
			options.Cookie.IsEssential = true;
		});
		builder.Services.AddHttpClient();
		builder.Services.AddSingleton<IVnPayService, VnPayService>();

        var app = builder.Build();

		// Cấu hình pipeline yêu cầu HTTP.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			app.UseHsts();
		}
		
		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseSession(); 
		app.UseAuthentication(); 
		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");


        app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            
        

        app.Run();
	}
}
