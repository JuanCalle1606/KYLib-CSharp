using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using KYLib.Utils;

namespace KYLib.System
{
	partial class Mod
	{
		/// <summary>
		/// Obtiene el mod relacionado a <see cref="Assembly.GetExecutingAssembly"/>.
		/// </summary>
		public static readonly Mod Current = new Mod(Assembly.GetExecutingAssembly());

		/// <summary>
		/// Obtiene el mod relacionado a <see cref="Assembly.GetEntryAssembly"/>.
		/// </summary>
		public static readonly Mod Entry = new Mod(Assembly.GetEntryAssembly());

		/// <summary>
		/// Lista de todos los mods, en cuestión contiene lo mismo que <see cref="AppDomain.GetAssemblies"/> pero en una lista.
		/// </summary>
		private static List<Mod> AllMods = new List<Mod>();

		/// <summary>
		/// Obtiene un mod que ha sido cargado.
		/// </summary>
		/// <param name="identifier">Nombre simple del ensamblado con el que se quiere relacionar el mod.</param>
		public static Mod GetMod(string identifier)
		{
			if (string.IsNullOrWhiteSpace(identifier)) return null;
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
			var abs = Path.IsPathFullyQualified(path);
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

			if (string.IsNullOrWhiteSpace(existing)) return path;
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

		/// <summary>
		/// Metodo en blanco, usado solo para que se llame al contructor de la clase.
		/// </summary>
		public static void Init() { }

		static Mod() =>
			AppDomain.CurrentDomain.AssemblyLoad += (o, a) =>
			{
#if DEBUG
				Console.WriteLine($"Ensamblado {a.LoadedAssembly.GetName().Name} cargado");
#endif
				AllMods.Add(new Mod(a.LoadedAssembly));
			};
	}
}