using System;

namespace KYLib.Helpers
{
	/// <summary>
	/// Provee los mismos metodos que <see cref="Convert"/> pero con la posibilidad de ignorar los errores.
	/// </summary>
	partial class ConvertHelper
	{
		/// <summary>
		/// Convierte el valor especificado en un <see cref="double"/>.
		/// </summary>
		/// <param name="n">Valor a convertir.</param>
		/// <returns>El <see cref="double"/> equivalente a <paramref name="n"/> o 0 si <paramref name="n"/> es <c>null</c>.</returns>
		public static double ToDouble(IConvertible n) =>
			ConvertValue(n, Convert.ToDouble, _ => double.NaN);

		/// <summary>
		/// Convierte el valor especificado en un <see cref="float"/>.
		/// </summary>
		/// <param name="n">Valor a convertir.</param>
		/// <returns>El <see cref="float"/> equivalente a <paramref name="n"/> o 0 si <paramref name="n"/> es <c>null</c>.</returns>
		public static float ToSingle(IConvertible n) =>
			ConvertValue(n, Convert.ToSingle, _ => float.NaN);
	}
}