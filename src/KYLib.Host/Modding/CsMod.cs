using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KYLib.Modding;

public abstract class CsMod : Mod
{
	public IServiceProvider Services { get; internal set; }

	public ILogger Logger { get; internal set; }

	public abstract void Load();

	public abstract void Unload();

	public abstract void Configure(HostBuilderContext context, IConfigurationBuilder builder);
}
 