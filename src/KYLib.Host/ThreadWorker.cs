using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace KYLib.Host;

public abstract class ThreadWorker : IHostedService
{
	Task task;

	CancellationTokenSource token;

	Task IHostedService.StartAsync(CancellationToken cancellationToken)
	{
		token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
		task = Task.Run(() => Run(token.Token), token.Token);
		return Task.CompletedTask;
	}

	public abstract void Run(CancellationToken cancellationToken);

	public abstract void Stop();

	Task IHostedService.StopAsync(CancellationToken cancellationToken)
	{
		if(task != null && !task.IsCompleted)
		{
			token.Cancel();
		}
		Stop();
		return task;
	}
}
