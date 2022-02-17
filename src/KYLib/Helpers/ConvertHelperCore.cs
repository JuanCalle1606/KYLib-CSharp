using System;

namespace KYLib.Helpers
{
	/// <summary>
	/// Provee los mismos metodos que <see cref="Convert"/> pero con la posibilidad de ignorar los errores.
	/// </summary>
	public static partial class ConvertHelper
	{
		/// <summary>
		/// Indica si se deben prevenir los errores de conversion.
		/// </summary>
		public static bool IgnoreErrors = false;

		/// <summary>
		/// Solo si <see cref="IgnoreErrors"/> es <c>true</c>, indica si al ocurrir un error se debe devolver el valor por defecto o si se deria devolver otro (por ejempo: NaN).
		/// </summary>
		public static bool DefaultOnError = true;

		/// <summary>
		/// Convierte el valor.
		/// </summary>
		static T ConvertValue<T>(
			IConvertible n,
			Converter<object, T> converter,
			Converter<IConvertible, T> onfail)
		{
			try
			{
				return converter(n);
			}
			catch (Exception)
			{
				if (!IgnoreErrors)
					throw;
				if (DefaultOnError)
					return default;
				if (onfail != null)
					return onfail(n);
				return default;
			}
		}
	}
}