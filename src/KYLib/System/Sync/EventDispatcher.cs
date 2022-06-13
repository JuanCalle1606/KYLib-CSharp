using KYLib.Extensions;
using KYLib.Modding;
using KYLib.Utils;
using System.Runtime.Versioning;
using System.Threading;

namespace KYLib.System.Sync;

public abstract class EventDispatcher : IDisposable
{
	

	internal event Action Dispatcher;

	public abstract void Dispatch();

	protected void invoke() => Dispatcher?.Invoke();

	public static EventDispatcher CreateLocal() => new LocalEventDispatcher();

	[SupportedOSPlatform("windows")]
	public static EventDispatcher CreateGlobal<T>(string v, bool owner = true) => CreateGlobal(typeof(T).FullName + "." + v);

	[SupportedOSPlatform("windows")]
	public static EventDispatcher CreateGlobal(string v, bool owner = true) => CreateGlobal(Mod.Entry, v);

	[SupportedOSPlatform("windows")]
	public static EventDispatcher CreateGlobal(Mod? entry, string v)
	{
		Ensure.NotNull(entry, nameof(entry));
		var prefix = entry.Dll.GetName().FullName.ToMD5();
		var name = "{0}.{1}".Format(prefix, v).Replace('.', '_');
		EventWaitHandle mutex = openMutex(name, false);
		EventWaitHandle controler = openMutex(name+ "_controler", true);
		return new GlobalEventDispatcher(mutex, controler);
	}

	[SupportedOSPlatform("windows")]
	static EventWaitHandle openMutex(string name, bool signal)
	{
		EventWaitHandle dev;
		if (!EventWaitHandle.TryOpenExisting(name, out dev))
		{
			dev = new EventWaitHandle(signal, EventResetMode.ManualReset, name, out var cn);
		}
		return dev;
	}

	public abstract void Dispose();
}
