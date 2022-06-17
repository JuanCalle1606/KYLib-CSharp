using KYLib.Host.Logging;
using KYLib.Host.Logging.Enrichers;
using KYLib.Utils;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Configuration;
using Serilog.Extensions.Logging;
namespace KYLib.Host.Extensions;

public static class LoggingBuilderExtensions
{
	public static  string LogTemplate = "[{Timestamp:HH:mm:ss} {Level:u4}]: {Category}{Message:lj}{NewLine}{Exception}";
	
	public static ILoggingBuilder AddSerilogWith(this ILoggingBuilder builder, Action<LoggerSinkConfiguration, string> writeTo, bool dispose = false, bool overrireLogger = false)
	{
		Ensure.NotNull(writeTo, nameof(writeTo));
		builder.ClearProviders();
		var lb = new LoggerConfiguration();
		writeTo(lb.WriteTo, LogTemplate);
		lb.Enrich.With<CategoryEnrich>();
		var log = lb.CreateLogger();
		if (overrireLogger)
			Log.Logger = log;
		return builder.AddProvider(new SerilogLoggerProvider(log, dispose));
	}
}