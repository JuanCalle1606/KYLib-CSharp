using System;
using System.Text;
using KYLib.MathFn;
using KYLib.System;
using KYLib.Utils;

namespace KYLib.Extensions;

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
		Ensure.NotLessThan<int>(amount, 0, nameof(amount));
		if (amount == 0) return string.Empty;
		if (amount == 1) return me;
		//en caso de ser necesario repetir si se usa el string builder.
		StringBuilder sb = new(me.Length * amount, me.Length * amount);
		for (Int i = 0; i < amount; i++)
			sb.Append(me);
		return sb.ToString();
	}

	/// <summary>
	/// Formatea una cadena con los elementos pasados.
	/// </summary>
	/// <param name="me">Cadena a formatear</param>
	/// <param name="objs">Arreglo de objetos a formatear.</param>
	/// <returns>La cadena formateada.</returns>
	public static string Format(this string me, params object[] objs) =>
		string.Format(me, objs);

	/// <summary>
	/// Ejecuta esta cadena como un comando de terminal y devuelve la salida de dicho comando.
	/// </summary>
	/// <param name="me">Cadena que contiene el comando.</param>
	/// <returns>Salida del proceso.</returns>
	public static string RunInBash(this string me) => Bash.GetCommand(me);
}