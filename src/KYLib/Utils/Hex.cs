﻿using System.Linq;
using System.Threading.Tasks;
namespace KYLib.Utils;

/// <summary>
/// Provee metodos para convertir datos a hexadecimal y viceversa.
/// </summary>
public static class Hex
{
	static readonly uint[] _HexDict = Enumerable.Range(0, 256).Select(i =>
	{
		var s = i.ToString("X2");
		return s[0] + ((uint)s[1] << 16);
	}).ToArray();

	/// <summary>
	/// Convierte un conjunto de bytes en su representacion hexadecimal.
	/// </summary>
	/// <param name="arr">El arreglo que queremos converteir a hexadecimal.</param>
	/// <returns>La representacion hexadecimal del arreglo de bytes.</returns>
	public static string ConvertToHex(kbyte[] arr)
	{
		var dev = new char[arr.Length * 2];
		for (kint i = 0; i < arr.Length; i++)
		{
			var val = _HexDict[arr[i]];
			dev[2 * i] = (char)val;
			dev[2 * i + 1] = (char)(val >> 16);
		}
		return new string(dev);
	}

	/// <summary>
	/// Combierte un conjunto de bytes en su representacion hexadecimal de forma asincronica.
	/// </summary>
	/// <remarks>
	/// Use esta función cuando este trabajando dentro de un metodo asincronico y <paramref name="arr"/> sea muy grande.
	/// </remarks>
	/// <param name="arr">El arreglo que queremos convertir a hexadecimal.</param>
	/// <returns>La representacion hexadecimal del arreglo de bytes.</returns>
	public static async Task<string> ConvertToHexAsync(kbyte[] arr)
	{
		var dev = new char[arr.Length * 2];
		await Task.Run(() =>
		{
			for (kint i = 0; i < arr.Length; i++)
			{
				var val = _HexDict[arr[i]];
				dev[2 * i] = (char)val;
				dev[2 * i + 1] = (char)(val >> 16);
			}
		});
		return new string(dev);
	}

}
