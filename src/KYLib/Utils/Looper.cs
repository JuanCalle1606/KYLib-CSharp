using KYLib.Internal.Loopers;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace KYLib.Utils;

public delegate bool LoopRunner(TickData data);

public abstract class Looper : IDisposable
{
	protected bool Disposed { get; private set; }

	EventWaitHandle handle;

	int loopCount;

	public Looper()
	{
		handle = new AutoResetEvent(false);
	}

	protected void LoopElapsed()
	{
		handle.Set();
	}

	public LooperInfo RunLoop(LoopRunner action, Action? load = null, Action? unload = null, CancellationToken token = default)
	{
		Ensure.NotNull(action, nameof(action));
		var thread = CreateThread(() =>
		{
			StartLoop();
			load?.Invoke();
			var frameSW = new Stopwatch();
			var tickSW = Stopwatch.StartNew();
			int ticks = 0;
			int lastticks = 0;
			TimeSpan et = default;
			while (!token.IsCancellationRequested)
			{
				handle.WaitOne();
				var tick = new TickData
				{
					Ticks = lastticks,
					LoopTime = frameSW.Elapsed,
					ExecutionTime = et,
					TotalTime = et + frameSW.Elapsed
				};
				frameSW.Restart();
				if (!action(tick)) break;
				ticks++;
				et = frameSW.Elapsed;
				frameSW.Restart();
				NewLoop();
				if(tickSW.ElapsedMilliseconds > 1000)
				{
					lastticks = ticks;
					ticks = 0;
					tickSW.Restart();
				}
			}
			unload?.Invoke();
			StopLoop();
			Interlocked.Decrement(ref loopCount);
		});
		thread.Start();
		return new LooperInfo(load, unload, action, token, thread);
	}

	public static Looper ThreadSleep(TimeSpan timeSpan) => new ThreadSleepLooper(timeSpan);

	public static Looper Precise(TimeSpan timeSpan) => new PreciseLooper(timeSpan);

	protected abstract void NewLoop();

	protected abstract void StartLoop();

	public static Looper While() => new WhileLooper();

	public static Looper Timer(byte Frequency = 60) => new TimerLooper(Frequency);

	public static Looper Adaptive(short Frequency, bool useThread = false)
	{
		Ensure.NotGreaterThan(Frequency, 1000, nameof(Frequency));
		Ensure.NotLessThan(Frequency, 0, nameof(Frequency));
		return Frequency switch
		{
			0 => While(),
			<= 60 => Timer((byte)Frequency),
			<= 1000 => useThread ? ThreadSleep(TimeSpan.FromMilliseconds(1000f / Frequency)) : Precise(TimeSpan.FromMilliseconds(1000f / Frequency)),
			_ => throw new NotImplementedException()
		};
	}

	internal static Thread CreateThread(ThreadStart action)
	{
		var t = new Thread(action);
		t.IsBackground = true;
		t.Name = "Looper Thread";
		return t;
	}

	public void Dispose()
	{
		handle.Dispose();
	}

	protected abstract void StopLoop();
}