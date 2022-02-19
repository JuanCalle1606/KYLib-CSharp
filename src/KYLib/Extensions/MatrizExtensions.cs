﻿using KYLib.MathFn;

namespace KYLib.Extensions;

/// <summary>
/// Extensiones generales para las matrices.
/// </summary>
public static class MatrizExtensions
{
	#region Conversiones
	/// <summary>
	/// Converte la matriz en un arreglo de arreglos.
	/// </summary>
	/// <typeparam name="T">Cualquier tipo.</typeparam>
	/// <param name="mat">Matriz de origen.</param>
	/// <returns></returns>
	public static T[][] ToArray<T>(this T[,] mat)
	{
		(kint row, kint col) = (mat.GetLength(0), mat.GetLength(1));
		var dev = new T[row][];
		for (kint i = 0; i < row; i++)
		{
			dev[i] = new T[col];
			for (kint j = 0; j < col; j++)
				dev[i][j] = mat[i, j];
		}
		return dev;
	}

	/// <inheritdoc/>
	public static string ToString<T>(this T[,] arr, char separator, bool multiline, bool showindex)
	{
		var dev = "";
		for (kint i = 0; i < arr.GetLength(0); i++)
		{
			if (showindex)
				dev += $"{i}: ";
			for (kint j = 0; j < arr.GetLength(1); j++)
				dev += arr[i, j] + ",";
			if (multiline)
			{
				dev = dev.TrimEnd(',');
				dev += "\n";
			}
		}
		dev = dev.TrimEnd(separator);
		return dev;
	}
	#endregion
}