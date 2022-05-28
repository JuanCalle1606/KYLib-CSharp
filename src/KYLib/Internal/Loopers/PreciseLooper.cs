using KYLib.Utils;
using System.Threading.Tasks;
using System.Threading;

namespace KYLib.Internal.Loopers;

internal class PreciseLooper : Looper
{
	private TimeSpan interval;
	private CancellationTokenSource source;

	public PreciseLooper(TimeSpan targetInterval)
	{
		interval = targetInterval;
	}

	protected override void NewLoop()
	{
		
	}

	protected override void StartLoop()
	{
		source = new CancellationTokenSource();
		_=PrecisionRepeatActionOnIntervalAsync(LoopElapsed, interval, source.Token);
	}

	protected override void StopLoop()
	{
		source.Cancel();
	}

	public static async Task PrecisionRepeatActionOnIntervalAsync(Action action, TimeSpan interval, CancellationToken ct = default)
	{
		long stage1Delay = 16;
		long stage2Delay = 8 * TimeSpan.TicksPerMillisecond;
		bool USE_SLEEP0 = true;

		DateTime target = DateTime.Now + new TimeSpan(0, 0, 0, 0, (int)stage1Delay + 2);
		bool warmup = true;
		while (!ct.IsCancellationRequested)
		{
			// Getting closer to 'target' - Lets do the less precise but least cpu intensive wait
			var timeLeft = target - DateTime.Now;
			if (timeLeft.TotalMilliseconds >= stage1Delay)
			{
				try
				{
					await Task.Delay((int)(timeLeft.TotalMilliseconds - stage1Delay), ct);
				}
				catch (TaskCanceledException)
				{
					return;
				}
			}

			// Getting closer to 'target' - Lets do the semi-precise but mild cpu intesive wait - Task.Yield()
			while (DateTime.Now < target - new TimeSpan(stage2Delay))
			{
				await Task.Yield();
			}

			// Getting closer to 'target' - Lets do the semi-precise but mild cpu intesive wait - Thread.Sleep(0)
			// Note: Thread.Sleep(0) is removed below because it is sometimes looked down on and also said not good to mix 'Thread.Sleep(0)' with Tasks.
			//       However, Thread.Sleep(0) does have a quicker and more reliable turn around time then Task.Yield() so to 
			//       make up for this a longer (and more expensive) Thread.SpinWait(1) would be needed.
			if (USE_SLEEP0)
			{
				while (DateTime.Now < target - new TimeSpan(stage2Delay / 8))
				{
					Thread.Sleep(0);
				}
			}

			// Extreamlly close to 'target' - Lets do the most precise but very cpu/battery intensive 
			while (DateTime.Now < target)
			{
				Thread.SpinWait(64);
			}

			if (!warmup)
			{
				action(); // or your code here
				target += interval;
			}
			else
			{
				long start1 = DateTime.Now.Ticks + ((long)interval.TotalMilliseconds * TimeSpan.TicksPerMillisecond);
				long alignVal = start1 - (start1 % ((long)interval.TotalMilliseconds * TimeSpan.TicksPerMillisecond));
				target = new DateTime(alignVal);
				warmup = false;
			}
		}
	}
}