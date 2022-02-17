using KYLib.System;
using KYLib.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace KYLib.Modding;

partial class Mod
{
	/// <summary>
	/// Obtiene el mod relacionado a <see cref="Assembly.GetExecutingAssembly"/>.
	/// </summary>
	public static readonly Mod Current = new(Assembly.GetExecutingAssembly());

	/// <summary>
	/// Obtiene el mod relacionado a <see cref="Assembly.GetEntryAssembly"/>.
	/// </summary>
	public static readonly Mod Entry = new(Assembly.GetEntryAssembly());

	
	static readonly List<Mod> _Allmods = new();

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
	/// <param name="identifier">Nombre simple del ensamblado con el que se quiere relacionar el mod.</param>
	public static Mod GetMod(string identifier)
	{
		if (string.IsNullOrWhiteSpace(identifier))
			return null;
		ValidateMods();
		return _Allmods.Find(a => a.Dll.GetName().Name == identifier);
	}

	public static Mod GetMod(Assembly assembly)
	{
		Ensure.NotNull(assembly, nameof(assembly));
		ValidateMods();
		return _Allmods.Find(m => m.Dll == assembly);
	}

	public static Mod GetMod(AssemblyName name)
	{
		Ensure.NotNull(name, nameof(name));
		ValidateMods();
		return _Allmods.Find(m => m.Dll.GetName() == name);
	}

#pragma warning disable CS1587

	/// <summary>
	/// Carga un mod desde un ensamblado de una ubicación arbitraria.
	/// </summary>
	/// <param name="name">Nombre del ensamblado.</param>
	public static Mod Import(string name)
	{
		return Import(name, Thread.CurrentThread.CurrentCulture);
	}

	public static Mod Import(string name, CultureInfo info)
	{
		var realpath = GetRealPath(name, info.Name + "/", info.Parent.Name + "/");

		var rname = Assembly.LoadFrom(realpath)?.GetName().Name;

		return GetMod(rname);
	}

	/// <summary>
	/// Busca entre los posibles archivos de ensamblado cual existe.
	/// </summary>
	static string GetRealPath(string path, string cci, string ncci)
	{
		var posiblePaths = new List<string>();
#if NETSTANDARD2_1
		var abs = Path.IsPathFullyQualified(path);
#else
			var abs = Path.GetFullPath(path) == path;
#endif
		if (abs)
		{
			posiblePaths.Add($"{path}.dll");
			posiblePaths.Add(path);
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

		var existing = posiblePaths.Find(p => File.Exists(p));

		return string.IsNullOrWhiteSpace(existing) ? path : existing;
	}

#pragma warning restore CS1587

	/// <summary>
	/// Intenta cargar un mod con <see cref="Import(string)"/>.
	/// </summary>
	/// <param name="path">Ubicación del ensamblado.</param>
	/// <param name="mod">Mod que ha sido cargado.</param>
	/// <returns>Devuelve si el mod se ha cargado o no.</returns>
	public static bool TryImport(string path, out Mod mod)
	{
		try
		{
			mod = Import(path);
			return mod != null;
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
	public static bool TryLoad(string identifier, out Mod mod)
	{
		try
		{
			mod = Load(identifier);
			return mod != null;
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

	public static List<Mod> LoadMods()
	{
		Directory.CreateDirectory(Assets.ModsDir);
		var files = Directory.GetFiles(Assets.ModsDir, "*.dll");
		var de = new List<Mod>();
		foreach (var file in files)
			de.Add(Import(file));
		return de;
	}

	static bool _autoloads = false;

	/// <summary>
	/// 
	/// </summary>
	public static void EnableAutoLoads()
	{
		if (Ensure.SetValue(ref _autoloads, true))
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
		AppDomain.CurrentDomain.AssemblyLoad += (o, a) =>
		{
			if (_autoloads && a.LoadedAssembly.GetCustomAttribute<AutoLoadAttribute>() != null)
				AutoLoadAttribute.AutoLoad(a.LoadedAssembly);
#if DEBUG
			Console.WriteLine($"Ensamblado {a.LoadedAssembly.GetName().Name} cargado");
#endif
			_Allmods.Add(new Mod(a.LoadedAssembly));
		};
}
