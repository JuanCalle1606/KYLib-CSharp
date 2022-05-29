using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace KYLib.System.Sync;

internal class GlobalEventDispatcher : EventDispatcher
{
	private EventWaitHandle mutex;
	private EventWaitHandle controler;
	

	internal GlobalEventDispatcher(EventWaitHandle mutex, EventWaitHandle controler)
	{
		this.controler = controler;
		this.mutex = mutex;
		Task.Run(SyncThread);
	}

	private void SyncThread()
	{
		while(true)
		{
			controler.WaitOne();
			mutex.WaitOne();
			invoke();
		}
	}

	public override void Dispatch()
	{
		controler.Reset();
		mutex.Set();
		Thread.Sleep(0);
		mutex.Reset();
		controler.Set();
	}

	public override void Dispose()
	{
		mutex.Dispose();
		controler.Dispose();
	}
}