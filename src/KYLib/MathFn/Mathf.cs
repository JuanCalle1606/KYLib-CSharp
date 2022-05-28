using System.Collections.Generic;
using System.Linq;
using KYLib.Utils;
namespace KYLib.MathFn;

/// <summary>
/// Provee funciones matematicas basicas.
/// </summary>
public static partial class Mathf
{
	#region INumber
	public static T SumOf<T>(IEnumerable<T> arr) where T : INumber<T>
	{
		var num = T.Zero;
		foreach (var item in arr)
			num += item;
		return num;
	}

	public static T MeanOf<T>(IEnumerable<T> arr) where T : INumber<T>
	{
		T num = T.Zero;
		T len = T.Zero;
		foreach (var item in arr)
		{
			num += item;
			len++;
		}
		return num / len;
	}

	#endregion

	#region  Trabajo con cadenas.
	/// <summary>
	/// Devuelve la <paramref name="power"/> potencia de 2.
	/// </summary>
	/// <param name="power">Potencia a la que se va a elevar el 2.</param>
	/// <returns>2 elevado a la <paramref name="power"/> en formato de cadenas.</returns>
	public static string Pow2To(kint power)
	{
		Ensure.NotLessThan(power, 0, nameof(power));
		var dev = "1";

		for (kint i = 0; i < power; i++)
			dev = Mult2(dev);
		return dev;
	}

	/// <summary>
	/// Multiplica un numero por dos.
	/// </summary>
	/// <remarks>
	/// Debe tener en cuenta que esta función no intenta convertir la cadena en un numero si no que directamente interpreta cada caracter como un numero por separado.
	/// </remarks>
	/// <param name="n">Una cadena de texto que representa un numero.</param>
	/// <returns>El numero <paramref name="n"/> multiplicado por dos.</returns>
	public static string Mult2(string n)
	{
		var chars = n.ToCharArray().ToList();
		List<char> output = new();
		kint count = chars.Count - 1;
		var res = '0';
		for (var i = count; i >= 0; i--)
			output.Insert(0, Mult2Core(chars[i], res, out res));
		if (res == '1')
			output.Insert(0, res);
		return new(output.ToArray());
	}

	static char Mult2Core(char n, char res, out char outRes)
	{
		var nr = kbyte.Parse($"{n}");
		kbyte temp = nr switch
		{
			0 or 5 => 0,
			1 or 6 => 2,
			2 or 7 => 4,
			3 or 8 => 6,
			_ => 8
		};
		temp += res == '1' ?
			// ReSharper disable once RedundantCast
			(kbyte)1 :
			// ReSharper disable once RedundantCast
			(kbyte)0;
		outRes = nr < 5 ? '0' : '1';
		return char.Parse($"{temp}");
	}
	#endregion
}