using Serilog.Core;
using Serilog.Events;
namespace KYLib.Host.Logging.Enrichers;

internal class CategoryEnrich : ILogEventEnricher
{

	public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
	{
		if (logEvent.Properties.ContainsKey("SourceContext"))
		{
			var source = logEvent.Properties["SourceContext"];
			var last = source.ToString().Trim('"').Split(".")[^1];
			logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Category", $"[{last}] "));
		}
	}
}