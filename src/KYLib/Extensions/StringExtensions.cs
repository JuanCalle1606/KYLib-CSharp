using System;
using System.Security.Cryptography;
using System.Text;
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
	public static string Repeat(this string me, kint amount)
	{
		Ensure.NotLessThan(amount, 0, nameof(amount));
		switch (amount)
		{
			case 0:
				return string.Empty;
			case 1:
				return me;
		}

		//en caso de ser necesario repetir si se usa el string builder.
		var sb = new StringBuilder(me.Length * amount, me.Length * amount);
		for (kint i = 0; i < amount; i++)
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

	public static string ToMD5(this string input)
	{
		using var md5 = MD5.Create();
		
		byte[] inputBytes = Encoding.ASCII.GetBytes(input);
		byte[] hashBytes = md5.ComputeHash(inputBytes);

		return Convert.ToHexString(hashBytes);
	}
}