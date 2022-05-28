using KYLib.Utils;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Diagnostics;

namespace KYLib.Internal.Loopers;

internal class TimerLooper : Looper
{
	Timer timer;

	public TimerLooper(byte Frequency = 60)
	{
		Ensure.NotGreaterThan(Frequency, 65, nameof(Frequency));
		Ensure.NotLessThan(Frequency, 0, nameof(Frequency));
		timer = new Timer(1000d / Frequency);
	}

	private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
	{
		LoopElapsed();
	}

	protected override void StartLoop()
	{
		timer.AutoReset = true;
		timer.Elapsed += Timer_Elapsed;
		timer.Start();
	}

	protected override void NewLoop()
	{
		
	}

	protected override void StopLoop()
	{
		timer.Stop();
		timer.Dispose();
	}
}
