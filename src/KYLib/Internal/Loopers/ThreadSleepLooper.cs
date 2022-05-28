using KYLib.Utils;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace KYLib.Internal.Loopers;
internal class ThreadSleepLooper : Looper
{
	object sync = new();
	bool canrun;
	private TimeSpan interval;

	public ThreadSleepLooper(TimeSpan timeSpan)
	{
		interval = timeSpan;
	}

	protected override void NewLoop()
	{
	}

	protected override void StartLoop()
	{
		var t = new Thread(Start);
		t.Priority = ThreadPriority.Highest;
		t.IsBackground = true;
		t.Start();
	}

	private void Start()
	{
		canrun = true;
		var sw = Stopwatch.StartNew();
		while (CanRun())
		{
			LoopElapsed();
			Thread.Sleep(interval);
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
		lock (sync)
		{
			canrun = false;
		}
	}
}
