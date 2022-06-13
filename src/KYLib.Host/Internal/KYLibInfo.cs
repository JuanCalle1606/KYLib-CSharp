using KYLib.Abstractions;

namespace KYLib.Internal;

// ReSharper disable once InconsistentNaming
internal class KYLibHostInfo : IModInfo
{
	public string Name => Resources.ModName;

	public string Author => Resources.ModAuthor;

	public string Description => Resources.ModDescription;

	public string Title => Name;

	public string[] Dependencies => new string[] { Resources.ModName };

	public string[] SoftDependencies => Array.Empty<string>();

	public Type? ModType => null;
}