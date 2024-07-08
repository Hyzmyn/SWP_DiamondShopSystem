using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Repository.Models;
using Service.Services;
using Service.Services.VNPay;
using service.Services;
using Service;
using Repository.Repositories;
using Microsoft.AspNetCore.Identity;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DiamondShopContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



		// Thêm dịch vụ vào container.
		builder.Services.AddControllersWithViews().AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
		});
		builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<IDiscountService, DiscountService>();
        builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
        builder.Services.AddScoped<IGemPriceListService, GemPriceListService>();
        builder.Services.AddScoped<IGemPriceListRepository, GemPriceListRepository>();
        builder.Services.AddScoped<IGemService, GemService>();
        builder.Services.AddScoped<IGemRepository, GemRepository>();
        builder.Services.AddScoped<IMaterialPriceListService, MaterialPriceListService>();
        builder.Services.AddScoped<IMaterialPriceListRepository, MaterialPriceListRepository>();
        builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
        builder.Services.AddScoped<IPriceRateListService, PriceRateListService>();
        builder.Services.AddScoped<IPriceRateListRepository, PriceRateListRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IUserRepository, UserRepository>();
		builder.Services.AddScoped<IProductService, ProductService>();
		builder.Services.AddScoped<IProductRepository, ProductRepository>();
		builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IWalletPointRepository, WalletPointRepository>();
        builder.Services.AddScoped<IWalletService, WalletPointService>();
        builder.Services.AddScoped<IWarrantyRepository, WarrantyRepository>();
        builder.Services.AddScoped<IWarrantyService, WarrantyService>();

		builder.Services.AddHostedService<PriceCalculationHostedService>();

		builder.Services.AddDistributedMemoryCache();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        builder.Services.AddSession(options =>
		{
			options.IdleTimeout = TimeSpan.FromMinutes(30);
			options.Cookie.HttpOnly = true;
			options.Cookie.IsEssential = true;
		});
		builder.Services.AddHttpClient();
		builder.Services.AddSingleton<IVnPayService, VnPayService>();
		builder.Services.AddSingleton<EmailService>();

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
