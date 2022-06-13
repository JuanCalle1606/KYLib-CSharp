using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using KYLib.Extensions;
using KYLib.System;
using KYLib.Utils;
#pragma warning disable CS1573
namespace KYLib.Modding;

partial class Mod
{
	static AssemblyLoadContext modLoadContext;

	static readonly List<Mod> _Allmods = new();

	/// <summary>
	/// Obtiene el mod relacionado a <see cref="Assembly.GetEntryAssembly"/>.
	/// </summary>
	public static readonly Mod? Entry = Info.HasEntry ? GetMod(Assembly.GetEntryAssembly()!) : null;
	
	/// <summary>
	/// Lista de todos los mods, en cuestión contiene lo mismo que <see cref="AppDomain.GetAssemblies"/> pero en una lista.
	/// </summary>
	public static Mod[] AllMods
	{
		get
		{
			ValidateMods(); 
			return _Allmods.ToArray();
		}
	}

	/// <summary>
	/// Obtiene un mod que ha sido cargado.
	/// </summary>
	/// <param name="mod">Identificador del ensamblado con el que se quiere relacionar el mod.</param>
	public static Mod? GetMod(string mod)
	{
		if (string.IsNullOrWhiteSpace(mod))
			return null;
		ValidateMods();
		return _Allmods.Find(a => a.Dll.GetName().Name == mod || a.ModInfo?.Name == mod);
	}

	/// <inheritdoc cref="GetMod(string)"/>
	public static Mod GetMod(Assembly mod)
	{
		Ensure.NotNull(mod, nameof(mod));
		ValidateMods();
		return _Allmods.Find(m => m.Dll == mod)!;
	}

	/// <inheritdoc cref="GetMod(string)"/>

	public static Mod? GetMod(AssemblyName mod)
	{
		Ensure.NotNull(mod, nameof(mod));
		ValidateMods();
		return _Allmods.Find(m => m.Dll.GetName().ToString() == mod.ToString());
	}

	/// <summary>
	/// Carga un mod desde un ensamblado de una ubicación arbitraria.
	/// </summary>
	/// <param name="path">Nombre del ensamblado.</param>
	public static Mod Import(string path) => Import(path, CultureInfo.CurrentCulture);

	/// <inheritdoc cref="Import(string)"/>
	/// <param name="info">Identificador de cultura del ensanmblado que se quiere cargar.</param>
	public static Mod Import(string path, CultureInfo info, AssemblyLoadContext? modLoadContext = null)
	{
		Ensure.NotNull(path, nameof(path));
		Ensure.NotNull(info, nameof(info));
		var realpath = GetRealPath(path, info.Name + "/", info.Parent.Name + "/");
		realpath = Path.GetFullPath(realpath);
		return modLoadContext == null ? GetMod(Assembly.LoadFrom(realpath)) : GetMod(modLoadContext.LoadFromAssemblyPath(realpath));
	}

	/// <summary>
	/// Busca entre los posibles archivos de ensamblado cual existe.
	/// </summary>
	static string GetRealPath(string path, string cci, string ncci)
	{
		var posiblePaths = new List<string>();
		var abs = Path.IsPathFullyQualified(path);

		if (abs)
		{
			var fileInfo = new FileInfo(path);
			var name = fileInfo.Name;
			var dirInfo = fileInfo.Directory;
			var dir = dirInfo is null ? null:  new Assets(dirInfo.FullName);
			
			if (dir is not null)
			{
				posiblePaths.Add($"{dir[name]}.dll");
				posiblePaths.Add(dir[name]);
				posiblePaths.Add($"{dir[cci + name]}.dll");
				posiblePaths.Add(dir[cci + name]);
				posiblePaths.Add($"{dir[ncci + name]}.dll");
				posiblePaths.Add(dir[ncci + name]);
			}
			else
			{
				posiblePaths.Add($"{path}.dll");
				posiblePaths.Add(path);
			}
		}
		else
		{
			// direct base dir
			posiblePaths.Add($"{Assets.BaseDir[path]}.dll");
			posiblePaths.Add(Assets.BaseDir[path]);
			posiblePaths.Add($"{Assets.BaseDir[cci + path]}.dll");
			posiblePaths.Add(Assets.BaseDir[cci + path]);
			posiblePaths.Add($"{Assets.BaseDir[ncci + path]}.dll");
			posiblePaths.Add(Assets.BaseDir[ncci + path]);
			// check in mods dir
			posiblePaths.Add($"{Assets.ModsDir[path]}.dll");
			posiblePaths.Add(Assets.ModsDir[path]);
			posiblePaths.Add($"{Assets.ModsDir[cci + path]}.dll");
			posiblePaths.Add(Assets.ModsDir[cci + path]);
			posiblePaths.Add($"{Assets.ModsDir[ncci + path]}.dll");
			posiblePaths.Add(Assets.ModsDir[ncci + path]);
			if (Assets.CurrentDir != Assets.BaseDir)
			{
				// directorio actual de ultimo
				posiblePaths.Add($"{path}.dll");
				posiblePaths.Add(path);
				posiblePaths.Add($"{cci + path}.dll");
				posiblePaths.Add(cci + path);
				posiblePaths.Add($"{ncci + path}.dll");
				posiblePaths.Add(ncci + path);
			}
		}

		var existing = posiblePaths.Find(File.Exists);

		return string.IsNullOrWhiteSpace(existing) ? path : existing;
	}

	/// <summary>
	/// Intenta cargar un mod con <see cref="Import(string)"/>.
	/// </summary>
	/// <param name="path">Ubicación del ensamblado.</param>
	/// <param name="mod">Mod que ha sido cargado.</param>
	/// <returns>Devuelve si el mod se ha cargado o no.</returns>
	public static bool TryImport(string path, out Mod? mod)
	{
		try
		{
			mod = Import(path);
			return true;
		}
		catch (Exception)
		{
			mod = null;
			return false;
		}
	}

	/// <summary>
	/// Carga un mod desde un ensamblado cuyo nombre simple es <paramref name="identifier"/>.
	/// </summary>
	/// <remarks>
	/// Es mejor usa este metodo en lugar de <see cref="Import(string)"/> a no ser que el ensambaldo se encuentre en una ubicación que no es por defecto.
	/// </remarks>
	/// <param name="identifier">Nombre simple del ensamblado a cargar.</param>
	public static Mod Load(string identifier)
	{
		return GetMod(Assembly.Load(identifier));
	}

	/// <summary>
	/// Intenta cargar un mod usando <see cref="Load(string)"/>.
	/// </summary>
	/// <param name="identifier">Nombre simple del ensamblado a cargar.</param>
	/// <param name="mod">Mod que ha sido cargado.</param>
	/// <returns>Devuelve si el mod se ha cargado o no.</returns>
	public static bool TryLoad(string identifier, out Mod? mod)
	{
		try
		{
			mod = Load(identifier);
			return true;
		}
		catch (Exception)
		{
			mod = null;
			return false;
		}
	}

	/// <summary>
	/// Indica si un mod cargado contiene un ensamblado llamado <paramref name="identifier"/>.
	/// </summary>
	/// <param name="identifier">Nombre simple del ensamblado a buscar.</param>
	/// <returns>Devuelve si el mod se ha cargado o no.</returns>
	public static bool IsLoaded(string identifier)
	{
		ValidateMods();
		var mod = _Allmods.Find(a => a.Dll.GetName().Name == identifier);
		return mod != null;
	}

	/// <summary>
	/// Valida que <see cref="_Allmods"/> contenga todos los ensamblados.
	/// </summary>
	static void ValidateMods()
	{
		var mods = AppDomain.CurrentDomain.GetAssemblies();
		if (mods.Length != _Allmods.Count)
		{
			_Allmods.Clear();
			_Allmods.AddRange(mods.Select(a => new Mod(a)));
		}
	}

	public static IEnumerable<Mod> LoadMods() => LoadMods(Assets.ModsDir);

	public static IEnumerable<Mod> LoadMods(Assets directory)
	{
		Directory.CreateDirectory(directory);
		var files = Directory.GetFiles(directory, "*.dll");
		modLoadContext ??= new AssemblyLoadContext("ExtensionMods");
		foreach (var file in files)
			if(Import(file, CultureInfo.CurrentCulture, modLoadContext) is {} mod)
				yield return mod;
	}

	public static IEnumerable<Tmod> LoadExtensionMods<Tmod>(Assets directory) where Tmod : Mod
	{
		Console.WriteLine("Will load mods xdd");
		foreach (var item in LoadMods(directory))
		{
			if (AnalizeExtensionMod<Tmod>(item))
				yield return ReplaceMod(item, item.ModInfo!.ModType!.RequiredInstance<Tmod>());
		}
	}

	static Tmod ReplaceMod<Tmod>(Mod item, Tmod replacement) where Tmod : Mod
	{
		//cuando un mod tiene un tipo definido se debe usar dicho tipo como mod en lugar de usar el tipo base
		var t = replacement;
		t.Dll = item.Dll;
		t.ModInfo = item.ModInfo;
		_Allmods.Remove(item);
		_Allmods.Add(t);
		return t;
	}

	static bool AnalizeExtensionMod<Tmod>(Mod mod)
	{
		return mod.ModInfo != null && mod.ModInfo.ModType != null && mod.ModInfo.ModType.IsAssignableTo(typeof(Tmod));
	}

	static bool _Autoloads;

	/// <summary>
	/// Habilita la carga automatica de clases que tengan el atributo <see cref="AutoLoadAttribute"/>.
	/// </summary>
	public static void EnableAutoLoads()
	{
		if (Ensure.SetValue(ref _Autoloads, true))
			return;
#if DEBUG
		var sw = Stopwatch.StartNew();
#endif
		ValidateMods();
		AutoLoadAttribute.AutoLoad(_Allmods.Select(m => m.Dll).ToArray());
#if DEBUG
		sw.Stop();
		Console.WriteLine($"Auto Loads Enabled in {sw.ElapsedMilliseconds} ms");
#endif
	}

	static Mod() =>
		AppDomain.CurrentDomain.AssemblyLoad += (_, a) =>
		{
			if (_Autoloads && a.LoadedAssembly.GetCustomAttribute<AutoLoadAttribute>() != null)
				AutoLoadAttribute.AutoLoad(a.LoadedAssembly);
#if DEBUGa
			Console.WriteLine($"Ensamblado {a.LoadedAssembly.GetName().Name} cargado de " + a.LoadedAssembly.Location);
#endif
			_Allmods.Add(new(a.LoadedAssembly));
		};
}
