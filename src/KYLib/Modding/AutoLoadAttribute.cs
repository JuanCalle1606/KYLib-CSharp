using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
#if DEBUG
using System.Diagnostics;
#endif

namespace KYLib.Modding;

/// <summary>
/// Especifica que el elemento debe ser cargado cuando se llame a <see cref="Mod.EnableAutoLoads"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
public sealed class AutoLoadAttribute : Attribute
{
	/// <summary>
	/// Indica que el elemento se debe cargar de forma asincronica.
	/// </summary>
	public bool Async { get; set; }

	#region Assembly
	internal static void AutoLoad(Assembly[] assemblies)
	{
		foreach (var mod in assemblies)
			AutoLoad(mod);
	}

	internal static void AutoLoad(Assembly mod)
	{
		var load = mod.GetCustomAttribute<AutoLoadAttribute>();
		if (load != null)
			AutoLoad(mod, load);
	}

	static void AutoLoad(Assembly mod, AutoLoadAttribute load)
	{
		if (load.Async)
		{
			_ = AutoLoadAsync(mod);
			return;
		}
		AutoLoadSync(mod);
	}

	static void AutoLoadSync(Assembly mod)
#if DEBUG
	{
		var sw = Stopwatch.StartNew();
		AutoLoad(mod.GetModules());
		sw.Stop();
		Console.WriteLine(
			$"Assembly {mod.GetName().Name} loaded in {sw.ElapsedMilliseconds} ms"
		);
	}
#else
		=> AutoLoad(mod.GetModules());
#endif

	static async Task AutoLoadAsync(Assembly mod) =>
		await Task.Run(() => AutoLoadSync(mod));
	#endregion

	#region Module
	internal static void AutoLoad(Module[] assemblies)
	{
		foreach (var mod in assemblies)
			AutoLoad(mod);
	}

	internal static void AutoLoad(Module mod)
	{
		var load = mod.GetCustomAttribute<AutoLoadAttribute>();
		if (load != null)
			AutoLoad(mod, load);
	}

	static void AutoLoad(Module mod, AutoLoadAttribute load)
	{
		if (load.Async)
		{
			_ = AutoLoadAsync(mod);
			return;
		}
		AutoLoadSync(mod);
	}

	static void AutoLoadSync(Module mod)
#if DEBUG
	{
		var sw = Stopwatch.StartNew();
		AutoLoad(mod.GetTypes());
		sw.Stop();
		Console.WriteLine(
			$"Module {mod.Name} loaded in {sw.ElapsedMilliseconds} ms"
		);
	}
#else
		=> AutoLoad(mod.GetTypes());
#endif

	static async Task AutoLoadAsync(Module mod) =>
		await Task.Run(() => AutoLoadSync(mod));
	#endregion

	#region Type
	internal static void AutoLoad(Type[] assemblies)
	{
		foreach (var mod in assemblies)
			AutoLoad(mod);
	}

	internal static void AutoLoad(Type mod)
	{
		var load = mod.GetCustomAttribute<AutoLoadAttribute>();
		if (load != null)
			AutoLoad(mod, load);
	}

	static void AutoLoad(Type mod, AutoLoadAttribute load)
	{
		if (load.Async)
		{
			_ = AutoLoadAsync(mod);
			return;
		}
		AutoLoadSync(mod);
	}

	static void AutoLoadSync(Type type)
	{
		Mod.InternalTypeAutoLoaded(Mod.GetMod(type.Assembly), type);
#if DEBUG
		var sw = Stopwatch.StartNew();
#endif
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
#if DEBUG
			sw.Stop();
			Console.WriteLine(
				$"Type {type.Name} loaded in {sw.ElapsedMilliseconds} ms"
			);
#endif
			return;
		}

		// check static methods
		foreach (var staticmember in type.GetMethods(staticconsFlags))
		{
			if (staticmember.GetCustomAttribute<AutoLoadAttribute>() != null && staticmember.GetParameters().Length == 0)
			{
				staticmember.Invoke(null, null);
#if DEBUG
				sw.Stop();
				Console.WriteLine(
					$"Type {type.Name} loaded in {sw.ElapsedMilliseconds} ms"
				);
#endif
				return;
			}
		}

		// check normal methods
		foreach (var member in type.GetMethods(consFlags))
		{
			if (member.GetCustomAttribute<AutoLoadAttribute>() != null && member.GetParameters().Length == 0)
			{
				member.Invoke(Activator.CreateInstance(type, true), null);
#if DEBUG
				sw.Stop();
				Console.WriteLine(
					$"Type {type.Name} loaded in {sw.ElapsedMilliseconds} ms"
				);
#endif
				return;
			}
		}

		// implicit try to load the class
		RuntimeHelpers.RunClassConstructor(type.TypeHandle);
#if DEBUG
		sw.Stop();
		Console.WriteLine(
			$"Type {type.Name} loaded in {sw.ElapsedMilliseconds} ms"
		);
#endif
	}

	static async Task AutoLoadAsync(Type mod) =>
		await Task.Run(() => AutoLoadSync(mod));
	#endregion
}