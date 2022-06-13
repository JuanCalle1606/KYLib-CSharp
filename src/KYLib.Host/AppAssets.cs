using System.IO;
using KYLib.Utils;
namespace KYLib.Host;

public sealed class AppAssets
{
	public string FacturerName { get; }

	public string AppName { get; }

	public string ModuleName { get; }
	
	public AppAssets(string? facturerName, string appName, string? moduleName)
	{
		FacturerName = facturerName ?? string.Empty;
		AppName = appName;
		ModuleName = moduleName ?? string.Empty;
		var facturer = facturerName != null ? facturerName + Path.DirectorySeparatorChar : string.Empty;
		var app = facturer + appName + Path.DirectorySeparatorChar;
		var module = app + (moduleName != null ? moduleName + Path.DirectorySeparatorChar : string.Empty);
		
		var appconfig = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		AppPath = new(Path.Combine(appconfig, app), true) { ResolveAbsolute = true };
		ModulePath = new(Path.Combine(appconfig, module), true){ ResolveAbsolute = true };
		FacturerPath = facturerName == null ? AppPath : new (Path.Combine(appconfig, facturer), true){ ResolveAbsolute = true };
		
		var localappconfig = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		LocalAppPath = new (Path.Combine(localappconfig, app), true){ ResolveAbsolute = true };
		LocalModulePath = new(Path.Combine(localappconfig, module), true){ ResolveAbsolute = true };
		LocalFacturerPath = facturerName == null ? AppPath : new (Path.Combine(localappconfig, facturer), true){ ResolveAbsolute = true };
		
		
		var commonappconfig = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
		CommonAppPath = new (Path.Combine(commonappconfig, app), true){ ResolveAbsolute = true };
		CommonModulePath = new(Path.Combine(commonappconfig, module), true){ ResolveAbsolute = true };
		CommonFacturerPath = facturerName == null ? AppPath : new (Path.Combine(commonappconfig, facturer), true){ ResolveAbsolute = true };
	}

	public Assets CommonFacturerPath { get; set; }

	public Assets CommonModulePath { get; }

	public Assets CommonAppPath { get; }

	public Assets LocalFacturerPath { get; }

	public Assets LocalModulePath { get; }

	public Assets LocalAppPath { get; }

	public Assets FacturerPath { get; }

	public Assets ModulePath { get; }

	public Assets AppPath { get; }

}