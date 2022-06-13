using Microsoft.Extensions.DependencyInjection;
namespace KYLib.Host.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAssets(this IServiceCollection services, string appName, string? facturerName = null, string? moduleName = null)
	{
		return services.AddAssets(out _, appName, facturerName, moduleName);
	}

	public static IServiceCollection AddAssets(this IServiceCollection services,out AppAssets appAssets, string appName, string? facturerName = null, string? moduleName = null)
	{
		var aa = new AppAssets(facturerName, appName, moduleName);
		appAssets = aa;
		services.AddSingleton(aa);
		return services;
	}
}