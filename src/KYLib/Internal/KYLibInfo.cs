using KYLib.Abstractions;

namespace KYLib.Internal;

// ReSharper disable once InconsistentNaming
internal class KYLibInfo : IModInfo
{
	public string Name => Resources.ModName;

	public string Author => Resources.ModAuthor;

	public string Description => Resources.ModDescription;

	public string Title => Name;

	public string[] Dependencies => Array.Empty<string>();

	public string[] SoftDependencies => Dependencies;

	public Type? ModType => null;
}