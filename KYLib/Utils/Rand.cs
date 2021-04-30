using System;
using System.Collections.Generic;
using System.Linq;
using KYLib.MathFn;

namespace KYLib.Utils
{
	/// <summary>
	/// Clase que se usa para obtención de valores al azar.
	/// </summary>
	public static class Rand
	{
		/// <summary>
		/// Objeto que se usa para la obtención al azar de datos.
		/// </summary>
		private static readonly Random random = new();

		/// <summary>
		/// Obtiene un entero al azar.
		/// </summary>
		public static Int GetInt() => random.Next();

		/// <summary>
		/// Obtiene un entero al azar mayor o igual que <paramref name="min"/> y menor que <paramref name="max"/>.
		/// </summary>
		/// <param name="min">Valor minimo que puede tener el numero.</param>
		/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
		public static Int GetInt(Int min, Int max) => random.Next(min, max);

		/// <summary>
		/// Obtiene un entero mayor o igual a 0 y menor que <paramref name="max"/>. 
		/// </summary>
		/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
		public static Int GetInt(Int max) => random.Next(max);

		/// <summary>
		/// Obtiene un conjunto de <paramref name="amount"/> enteros al azar mayores o iguales que <paramref name="min"/> y menores que <paramref name="max"/>.
		/// </summary>
		/// <param name="min">Valor minimo que puede tener el numero.</param>
		/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
		/// <param name="amount">Cantidad de numeros a generar.</param>
		public static Int[] GetInt(Int min, Int max, Int amount)
		{
			if (!(amount > 0))
				throw new ArgumentException("No se puede generar un numero negativo de numeros.", nameof(amount));
			Int[] dev = new Int[amount];
			for (Int i = 0; i < amount; i++)
				dev[i] = GetInt(min, max);
			return dev;
		}

		/// <summary>
		/// Obtiene un arreglo de enteros al azar mayores o iguales que <paramref name="min"/> y menores que <paramref name="max"/>.
		/// </summary>
		/// <param name="min">Valor minimo que puede tener el numero.</param>
		/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
		/// <param name="minAmount">Cantidad minima de numeros que pueden ser generados.</param>
		/// <param name="maxAmout">Cantidad maxima excluyente de numeros que pueden ser generados.</param>
		/// <returns>Un arreglo cuya longitud es mayor o igual que <paramref name="minAmount"/> y menor que <paramref name="maxAmout"/> de numeros enteros al azar.</returns>
		public static Int[] GetInt(Int min, Int max, Int minAmount, Int maxAmout) =>
			GetInt(min, max, GetInt(minAmount, maxAmout));

		/// <summary>
		/// Obtiene un numero flotante al azar mayor o igual a 0 y menor a 1.
		/// </summary>
		public static float Get() => Convert.ToSingle(random.NextDouble());

		/// <summary>
		/// Obtiene un numero flotante al azar mayor o igual que 0 y menor que <paramref name="max"/>.
		/// </summary>
		/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
		public static float Get(float max) => Get() * max;

		/// <summary>
		/// Obtiene un numero flotante al azar mayor o igual que <paramref name="min"/> y menor que <paramref name="max"/>.
		/// </summary>
		/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
		/// <param name="min">Valor minimo que puede tener el numero.</param>
		public static float Get(float min, float max) => (Get() * (max - min)) + min;

		/// <summary>
		/// Obtiene un conjunto de <paramref name="amount"/> numeros flotantes al azar mayores o iguales que <paramref name="min"/> y menores que <paramref name="max"/>.
		/// </summary>
		/// <param name="min">Valor minimo que puede tener el numero.</param>
		/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
		/// <param name="amount">Cantidad de numeros a generar.</param>
		public static float[] Get(float min, float max, Int amount)
		{
			if (!(amount > 0))
				throw new ArgumentException();
			float[] dev = new float[amount];
			for (Int i = 0; i < amount; i++)
				dev[i] = Get(min, max);
			return dev;
		}

		/// <summary>
		/// Obtiene un arreglo de numeros flotantes al azar mayores o iguales que <paramref name="min"/> y menores que <paramref name="max"/>.
		/// </summary>
		/// <param name="min">Valor minimo que puede tener el numero.</param>
		/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
		/// <param name="minAmount">Cantidad minima de numeros que pueden ser generados.</param>
		/// <param name="maxAmout">Cantidad maxima excluyente de numeros que pueden ser generados.</param>
		/// <returns>Un arreglo cuya longitud es mayor o igual que <paramref name="minAmount"/> y menor que <paramref name="maxAmout"/> de numeros enteros al azar.</returns>
		public static float[] Get(float min, float max, Int minAmount, Int maxAmout) =>
			Get(min, max, GetInt(minAmount, maxAmout));
		/*
		public static float GetFixed(Int decimals) => Fix(Get(), decimals);

		public static float GetFixed(Int decimals, float max) => Fix(Get() * max, decimals);

		public static float GetFixed(Int decimals, float min, float max) => Fix((Get() * (max - min)) + min, decimals);

		public static float[] GetFixed(Int decimals, float min, float max, Int amount)
		{
			if (!(amount > 0))
				throw new ArgumentException();
			float[] dev = new float[amount];
			for (Int i = 0; i < amount; i++)
				dev[i] = GetFixed(decimals, min, max);
			return dev;
		}

		public static float[] GetFixed(Int decimals, float min, float max, Int minAmount, Int maxAmout) =>
			GetFixed(decimals, min, max, GetInt(minAmount, maxAmout + 1));
		*/
		/// <summary>
		/// Obtiene un numero de doble presición mayor o igual que 0 y menor que 1.
		/// </summary>
		public static Real GetDouble() => random.NextDouble();

		/// <summary>
		/// Escoge un elemento al azar de un enumerable de objetos.
		/// </summary>
		/// <param name="arr">Arreglo de origen.</param>
		/// <typeparam name="T">Cualquier tipo.</typeparam>
		/// <returns>Un elemento al azar escogido de <paramref name="arr"/>.</returns>
		public static T Choose<T>(IEnumerable<T> arr) => arr.ElementAt(GetInt(arr.Count()));

		//public static float Fix(float number, Int decimals) =>
		//	Convert.ToSingle(Math.Round(number, decimals));
	}
}
