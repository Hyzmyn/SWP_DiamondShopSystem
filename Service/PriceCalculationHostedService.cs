using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Services;
using System.Threading;
using System.Threading.Tasks;

public class PriceCalculationHostedService : IHostedService
{
	private readonly IServiceProvider _serviceProvider;

	public PriceCalculationHostedService(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		using (var scope = _serviceProvider.CreateScope())
		{
			var gemPriceListService = scope.ServiceProvider.GetRequiredService<IGemPriceListService>();
			await gemPriceListService.CalculateAndSavePricesAsync();
		}
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
