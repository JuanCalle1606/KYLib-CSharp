using KYLib.Abstractions;

namespace KYLib.Internal;

// ReSharper disable once InconsistentNaming
internal class KYLibInfo : IModInfo
{
	public string Name => "KYLib";

	public string Author => "Juan Pablo Calle";

	public string Description => "Libreria de utilidades para uso personal";

	public string Title => Name;

	public string[] Dependencies => Array.Empty<string>();

	public string[] SoftDependencies => Dependencies;

	public Type? ModType => null;
}