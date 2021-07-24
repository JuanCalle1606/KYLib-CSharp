using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using KYLib.Utils;

namespace KYLib.System
{
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

		/// <summary>
		/// Lista de todos los mods, en cuestión contiene lo mismo que <see cref="AppDomain.GetAssemblies"/> pero en una lista.
		/// </summary>
		private static readonly List<Mod> AllMods = new();

		/// <summary>
		/// Obtiene un mod que ha sido cargado.
		/// </summary>
		/// <param name="identifier">Nombre simple del ensamblado con el que se quiere relacionar el mod.</param>
		public static Mod GetMod(string identifier)
		{
			if (string.IsNullOrWhiteSpace(identifier))
				return null;
			ValidateMods();
			return AllMods.Find(A => A.DLL.GetName().Name.Equals(identifier));
		}

#pragma warning disable CS1587

		/// <summary>
		/// Carga un mod desde un ensamblado de una ubicación arbitraria.
		/// </summary>
		/// <param name="path">Ubicación del ensamblado.</param>
		public static Mod Import(string path)
		{
			/// Al intentar cargar un assembly sera buscado en distintos directorios, dependiendo de si el argumento pasado es relativo o absoluto.
			/// Se intenta cargar desde estas rutas si path es relativo:
			/// Assets.BaseDir/path.dll
			/// Assets.BaseDir/path
			/// Assets.ModsDir/path.dll
			/// Assets.ModsDir/path
			/// path.dll
			/// path
			/// 
			/// Si path es absoluto solo se valida en estas ubicaciones:
			/// path.dll
			/// path
			var realpath = GetRealPath(path);

			string name = Assembly.LoadFrom(realpath)?.GetName().Name;

			return GetMod(name);
		}

		/// <summary>
		/// Busca entre los posibles archivos de ensamblado cual existe.
		/// </summary>
		private static string GetRealPath(string path)
		{
			var posiblePaths = new List<string>();
#if NS21
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
				posiblePaths.Add($"{Assets.BaseDir[path]}.dll");
				posiblePaths.Add(Assets.BaseDir[path]);
				posiblePaths.Add($"{Assets.ModsDir[path]}.dll");
				posiblePaths.Add(Assets.ModsDir[path]);
				posiblePaths.Add($"{path}.dll");
				posiblePaths.Add(path);
			}

			var existing = posiblePaths.Find(P => File.Exists(P));

			if (string.IsNullOrWhiteSpace(existing))
				return path;
			return existing;
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
			Assembly.Load(identifier);
			return GetMod(identifier);
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
			var mod = AllMods.Find(A => A.DLL.GetName().Name.Equals(identifier));
			return mod != null;
		}

		/// <summary>
		/// Valida que <see cref="AllMods"/> contenga todos los ensamblados.
		/// </summary>
		private static void ValidateMods()
		{
			var mods = AppDomain.CurrentDomain.GetAssemblies();
			if (mods.Length != AllMods.Count)
			{
				AllMods.Clear();
				AllMods.AddRange(mods.Select(A => new Mod(A)));
			}
		}

		private static bool autoloads = false;

		/// <summary>
		/// 
		/// </summary>
		public static void EnableAutoLoads()
		{
			if (Ensure.SetValue(ref autoloads, true))
				return;
#if DEBUG
			var sw = Stopwatch.StartNew();
#endif
			ValidateMods();
			foreach (var mod in AllMods.ToArray())
			{
				var load = mod.DLL.GetCustomAttribute<AutoLoadAttribute>();
				if (load != null)
				{
#if DEBUG
					var sw1 = Stopwatch.StartNew();
#endif
					AutoLoad(mod.DLL);
#if DEBUG
					sw1.Stop();
					Console.WriteLine($"Assembly {mod.DLL.GetName().Name} loaded in {sw1.ElapsedMilliseconds} ms");
#endif
				}
			}
#if DEBUG
			sw.Stop();
			Console.WriteLine($"Auto Loads Enabled in {sw.ElapsedMilliseconds} ms");
#endif
		}

		private static void AutoLoad(Assembly assembly)
		{
			foreach (var module in assembly.GetModules())
			{
				var load = module.GetCustomAttribute<AutoLoadAttribute>();
				if(load != null)
				{
#if DEBUG
					var sw = Stopwatch.StartNew();
#endif
					AutoLoad(module);
#if DEBUG
					sw.Stop();
					Console.WriteLine($"Module {module.Name} loaded in {sw.ElapsedMilliseconds} ms");
#endif
				}
			}
		}

		private static void AutoLoad(Module module)
		{
			foreach (var type in module.GetTypes())
			{
				var load = type.GetCustomAttribute<AutoLoadAttribute>();
				if (load != null)
				{
#if DEBUG
					var sw = Stopwatch.StartNew();
#endif
					AutoLoad(type);
#if DEBUG
					sw.Stop();
					Console.WriteLine($"Type {type.Name} loaded in {sw.ElapsedMilliseconds} ms");
#endif
				}
			}
		}

		private static void AutoLoad(Type type)
		{
			//check static  contructor first
			var staticconsFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Static;
			var cons = type.GetConstructor(staticconsFlags, null, Type.EmptyTypes, null);

			if (cons?.GetCustomAttribute<AutoLoadAttribute>() != null)
			{
				RuntimeHelpers.RunClassConstructor(type.TypeHandle);
				return;
			}
			// check normal constructor
			var consFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance;
			cons = type.GetConstructor(consFlags, null, Type.EmptyTypes, null);
			if (cons?.GetCustomAttribute<AutoLoadAttribute>() != null)
			{
				cons.Invoke(null);
				return;
			}

			// check static methods
			foreach (var staticmember in type.GetMethods(staticconsFlags))
			{
				if(staticmember?.GetCustomAttribute<AutoLoadAttribute>() != null && staticmember.GetParameters().Length == 0)
				{
					staticmember.Invoke(null, null);
					return;
				}
			}

			// check normal methods
			foreach (var member in type.GetMethods(consFlags))
			{
				if (member?.GetCustomAttribute<AutoLoadAttribute>() != null && member.GetParameters().Length == 0)
				{
					member.Invoke(Activator.CreateInstance(type, true), null);
					return;
				}
			}

			// implicit try to load the class
			RuntimeHelpers.RunClassConstructor(type.TypeHandle);
		}

		static Mod() =>
			AppDomain.CurrentDomain.AssemblyLoad += (o, a) =>
			{
				if (autoloads && a.LoadedAssembly.GetCustomAttribute<AutoLoadAttribute>() != null)
#if DEBUG
				{
					var sw = Stopwatch.StartNew();
#endif
					AutoLoad(a.LoadedAssembly);
#if DEBUG
					sw.Stop();
					Console.WriteLine($"Assembly {a.LoadedAssembly.GetName().Name} loaded in {sw.ElapsedMilliseconds} ms");
				}
				Console.WriteLine($"Ensamblado {a.LoadedAssembly.GetName().Name} cargado");
#endif
				AllMods.Add(new Mod(a.LoadedAssembly));
			};
	}
}