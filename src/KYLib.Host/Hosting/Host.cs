using KYLib.Extensions;
using KYLib.Modding;
using KYLib.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Linq;
using KYLib.Host.Modding;

namespace KYLib.Host.Hosting;
public class Hosts
{
	public const string LoggerTemplate = "[{Timestamp:HH:mm:ss} {Level:u4}]: {Category}{Message:lj}{NewLine}{Exception}";

	public const string DefaultLogFile = "logs/log.txt";

	public static IHostBuilder CreateSimpleBuilder(bool isConsole = true, ILogger? logger = null, bool loadmods = false, string? modsDir = null)
	{
		var builder = new HostBuilder();
		if (loadmods)
		{
			var mods = Mod.LoadExtensionMods<CsMod>(modsDir ?? Assets.ModsDir).ToArray();
			builder.ConfigureAppConfiguration((c, b) => mods.ForEach(m => m.Configure(c, b)));
			builder.ConfigureServices((context, s)=> 
			{ 
				mods.ForEach(m => s.AddSingleton(m.GetType(), m));
				s.AddSingleton(m => new ModManager(context,m , mods));
			});
		}

		builder.UseConsoleLifetime(o => o.SuppressStatusMessages = !isConsole);
		builder.ConfigureAppConfiguration(c => c.AddYamlFile("config.yml"));
		builder.ConfigureLogging(l => { 
			l.AddSerilog(logger, true);
		});
		builder.ConfigureServices(s => {
			s.AddSingleton(s=>s);
			s.AddSingleton<Assets>(p => p.GetRequiredService<IHostEnvironment>().ContentRootPath);
		});
		return builder;
	}
}