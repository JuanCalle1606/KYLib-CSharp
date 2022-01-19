using KYLib.Interfaces;
using KYLib.Utils;
using System;
using System.Reflection;

namespace KYLib.Modding
{
	/// <summary>
	/// Representa un ensamblado y provee metodos para facilitar el manejo.
	/// </summary>
	public partial class Mod : IEquatable<Mod>
	{
		/// <summary>
		/// Ensamblado relacionado a este objeto.
		/// </summary>
		public Assembly DLL { get; private set; }

		public IModInfo? ModInfo { get; internal set; }

		private Mod(Assembly assembly)
		{
			Ensure.NotNull(assembly, nameof(assembly));
			var att = assembly.GetCustomAttribute<ModInfoAttribute>();
			if (att != null)
				ModInfo = (IModInfo)Activator.CreateInstance(att.Type);
			DLL = assembly;
		}

		/// <inheritdoc/>
		public bool Equals(Mod other) =>
			DLL.Equals(other?.DLL);

		/// <inheritdoc/>
		public override bool Equals(object obj) => DLL.Equals(obj);

		/// <inheritdoc/>
		public override int GetHashCode() => DLL.GetHashCode();

		/// <inheritdoc/>
		public override string ToString() => DLL.ToString();
	}
}