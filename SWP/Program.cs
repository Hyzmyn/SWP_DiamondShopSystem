using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Models;
using Repository.Repositories;
using Service;
using Service.Interface;
using Service.Service;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Thêm dịch vụ vào container.
		builder.Services.AddControllersWithViews();

		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IUserRepository, UserRepository>();
		builder.Services.AddScoped<IProductService, ProductService>();
		builder.Services.AddScoped<IProductRepository, ProductRepository>();
		builder.Services.AddDbContext<DiamondShopContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

		builder.Services.AddDistributedMemoryCache();
		builder.Services.AddSession(options =>
		{
			options.IdleTimeout = TimeSpan.FromMinutes(30);
			options.Cookie.HttpOnly = true;
			options.Cookie.IsEssential = true;
		});

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

		app.UseSession(); // Đảm bảo session được sử dụng trước routing
		app.UseAuthentication(); // Nếu bạn sử dụng authentication
		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.Run();
	}
}
