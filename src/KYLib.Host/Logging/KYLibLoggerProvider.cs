using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;

namespace KYLib.Host.Logging;

internal class KYLibLoggerProvider : ILoggerProvider
{
	SerilogLoggerProvider provider;

	public KYLibLoggerProvider(Serilog.ILogger logger, bool dispose)
	{
		provider = new(logger, dispose);
	}

	public ILogger CreateLogger(string categoryName)
	{
		return provider.CreateLogger($"[{categoryName}] ");
	}

	public void Dispose()
	{
		provider.Dispose();
	}
}