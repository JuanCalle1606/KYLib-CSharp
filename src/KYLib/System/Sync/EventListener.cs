using KYLib.Modding;
using KYLib.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Threading;

namespace KYLib.System.Sync;

public class EventListener : IDisposable
{
	public event Action Listener;
	List<EventWaitHandle> Handles = new();
	Mutex mutexHandle = new Mutex(false);
	private EventDispatcher dispatcher;
	bool disposed = false;

	public bool ListenOnBackground { get; set; }

	private EventListener(EventDispatcher dispatcher)
	{
		this.dispatcher = dispatcher;
		dispatcher.Dispatcher += Invoke; 
	}

	[SupportedOSPlatform("windows")]
	public static EventListener ListenGlobal<T>(string v)
	{
		var t = typeof(T);
		return ListenGlobal(Mod.GetMod(t.Assembly), t.FullName + "." + v);
	}

	[SupportedOSPlatform("windows")]
	private static EventListener ListenGlobal(Mod mod, string v)
	{
		var temp = EventDispatcher.CreateGlobal(mod, v);
		return ListenDispatcher(temp);
	}

	public static EventListener ListenDispatcher(EventDispatcher dispatcher)
	{
		Ensure.NotNull(dispatcher, nameof(dispatcher));
		return new EventListener(dispatcher);
	}

	public void Listen()
	{
		if (disposed) throw new ObjectDisposedException(null);
		mutexHandle.WaitOne();
		using var handler = new AutoResetEvent(false);
		Handles.Add(handler);
		mutexHandle.ReleaseMutex();
		handler.WaitOne();
	}

	internal void Invoke()
	{
		if (disposed) return;
		mutexHandle.WaitOne();
		if (ListenOnBackground)
		{
			if (Listener != null)
			{
				var thread = new Thread(() => Listener());
				thread.IsBackground = true;
				thread.Start();
			}
		}
		else
		{
			Listener?.Invoke();
		}
		foreach (var item in Handles)
			item.Set();
		Handles.Clear();
		mutexHandle.ReleaseMutex();
	}

	public void Dispose()
	{
		if (disposed) return;
		disposed = true;
		dispatcher.Dispatcher -= Invoke;
		mutexHandle.WaitOne();
		mutexHandle.Dispose();
	}

	~EventListener()
	{
		Dispose();
	}
}
