using System;

namespace KYLib.Helpers;

/// <summary>
/// Provee los mismos metodos que <see cref="Convert"/> pero con la posibilidad de ignorar los errores.
/// </summary>
partial class ConvertHelper
{

	/// <summary>
	/// Convierte el valor especificado en un <see cref="int"/>.
	/// </summary>
	/// <param name="n">Valor a convertir.</param>
	/// <returns>El <see cref="int"/> equivalente a <paramref name="n"/> o 0 si <paramref name="n"/> es <c>null</c>.</returns>
	public static int ToInt32(IConvertible n) =>
		ConvertValue(n, Convert.ToInt32, v => (int)ToDouble(n));

	/// <summary>
	/// Convierte el valor especificado en un <see cref="byte"/>.
	/// </summary>
	/// <param name="n">Valor a convertir.</param>
	/// <returns>El <see cref="byte"/> equivalente a <paramref name="n"/> o 0 si <paramref name="n"/> es <c>null</c>.</returns>
	public static byte ToByte(IConvertible n) =>
		ConvertValue(n, Convert.ToByte, v => (byte)ToSingle(n));

}