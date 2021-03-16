using System;
using System.Text;
using KYLib.MathFn;

namespace KYLib.Extensions
{
	/// <summary>
	/// Extensiones para las cadenas.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Devuelve la cadena repetida <paramref name="amount"/> veces.
		/// </summary>
		/// <param name="me">La cadena de origen.</param>
		/// <param name="amount">Numero de veces a repetir</param>
		/// <returns>La cadena repetida <paramref name="amount"/> veces. </returns>
		public static string Repeat(this string me, Int amount)
		{
			if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), amount, "La cantidad de repeticiones no puede ser negativa.");
			if (amount == 0) return string.Empty;
			if (amount == 1) return me;
			//en caso de ser necesario repetir si se usa el string builder.
			StringBuilder sb = new StringBuilder(me.Length * amount, me.Length * amount);
			for (Int i = 0; i < amount; i++)
				sb.Append(me);
			return sb.ToString();
		}
	}
}