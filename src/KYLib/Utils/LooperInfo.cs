using System.Threading;

namespace KYLib.Utils;

public struct LooperInfo
{
	public readonly Action? Load;

	public readonly Action? Unload;

	public readonly LoopRunner Runner;

	public readonly CancellationToken? Token;

	public readonly Thread LoopThread;

	public LooperInfo(Action? load, Action? unload, LoopRunner runner, CancellationToken? token, Thread loopThread)
	{
		Load = load;
		Unload = unload;
		Runner = runner;
		Token = token;
		LoopThread = loopThread;
	}
}