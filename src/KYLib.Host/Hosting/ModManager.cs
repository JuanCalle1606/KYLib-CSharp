using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using KYLib.Host.Modding;

namespace KYLib.Host.Hosting;

public class ModManager : IDisposable
{
	IEnumerable<CsMod> mods;
	ILogger logger;

	public ModManager(HostBuilderContext context, IServiceProvider m, IEnumerable<CsMod> mods)
	{
		this.mods = mods;
		logger = m.GetRequiredService<ILogger<ModManager>>();
	}

	public void UnloadMods()
	{
		logger.LogInformation("Unloading mods");
	}

	public void LoadMods()
	{
		logger.LogInformation("Loading mods");
	}

	public void Dispose()
	{
		UnloadMods();
	}
}