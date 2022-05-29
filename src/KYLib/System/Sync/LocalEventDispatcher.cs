using System.Threading;

namespace KYLib.System.Sync;
internal class LocalEventDispatcher : EventDispatcher
{
	public override void Dispatch()
	{
		invoke();
	}

	public override void Dispose()
	{
	}
}