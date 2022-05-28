using KYLib.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace KYLib.Internal.Loopers;
#nullable disable
internal class WhileLooper : Looper
{
	EventWaitHandle handle;
	object sync = new();
	bool canrun;

	protected override void NewLoop()
	{
		handle.Set();
	}

	protected override void StartLoop()
	{
		Task.Run(Start);
	}

	private void Start()
	{
		handle?.Dispose();
		handle = new AutoResetEvent(false);
		canrun = true;
		while (CanRun())
		{
			LoopElapsed();
			handle.WaitOne();
		}
	}

	private bool CanRun()
	{
		lock (sync)
		{
			return canrun;
		}
	}

	protected override void StopLoop()
	{
		lock(sync)
		{
			canrun = false;
		}
	}
}
#nullable enable