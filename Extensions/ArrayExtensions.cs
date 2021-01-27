using System;
using System.Collections.Generic;
using System.Linq;
using KYLib.Utils;

namespace KYLib.Extensions
{
	public static class ArrayExtension
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
		{
			T[] elements = source.ToArray();
			for (int i = elements.Length - 1; i > 0; i--)
			{
				int swapIndex = Rand.GetInt(i + 1);
				yield return elements[swapIndex];
				elements[swapIndex] = elements[i];
			}
			yield return elements[0];
		}

		public static int IndexOf<T>(this T[] arr, T target)
		{
			int dev = -1;
			for (int i = 0; i < arr.Length; i++)
				if (arr[i].Equals(target))
					return i;
			return dev;
		}

		public static void Sort<T>(this T[] arr, Comparison<T> comparison)
		{
			int len = arr.Length;
			int sorted = 0;
			int limit;
			T aux;
			while (sorted < len)
			{
				limit = len - 1 - sorted;
				for (int i = 0; i < limit; i++)
				{
					if (comparison(arr[i], arr[i + 1]) > 0)
					{
						aux = arr[i];
						arr[i] = arr[i + 1];
						arr[i + 1] = aux;
					}
				}
				sorted++;
			}
		}

		public static void SortAsc<T>(this T[] arr) where T : IComparable<T> =>
			arr.Sort((T1, T2) => T1.CompareTo(T2));

		public static void SortDesc<T>(this T[] arr) where T : IComparable<T> =>
			arr.Sort((T1, T2) => T1.CompareTo(T2) * -1);

		public static int sum(this int[] arr)
		{
			int dev = 0;
			foreach (int item in arr)
				dev += item;
			return dev;
		}

		public static int max(this int[] arr)
		{
			int dev = 0;
			for (int i = 1; i < arr.Length; i++)
				if (arr[i] > arr[dev])
					dev = i;
			return dev;
		}

		public static int min(this int[] arr)
		{
			int dev = 0;
			for (int i = 1; i < arr.Length; i++)
				if (arr[i] < arr[dev])
					dev = i;
			return dev;
		}

		public static T[][] ToBidimensionalArray<T>(this T[,] mat)
		{

			return null;
		}

		public static T[,] ToMatriz<T>(this T[][] arr)
		{

			return null;
		}

		public static int[] ToIntArray(this string[] arr)
		{
			int len = arr.Length;
			int[] dev = new int[len];
			for (int i = 0; i < len; i++)
				dev[i] = int.Parse(arr[i]);

			return dev;
		}

		public static float[] ToFloatArray(this string[] arr)
		{
			int len = arr.Length;
			float[] dev = new float[len];
			for (int i = 0; i < len; i++)
				dev[i] = float.Parse(arr[i]);

			return dev;
		}

		public static string ToString<T>(this T[] arr, char separator, bool ShowIndex)
		{
			string dev = "";
			for (int i = 0; i < arr.Length; i++)
			{
				if (ShowIndex)
					dev += $"{i}: ";
				dev += arr[i].ToString() + separator;
			}
			dev = dev.TrimEnd(separator);
			return dev;
		}

		public static string ToString<T>(this T[] arr, char separator) => arr.ToString(separator, false);

		public static string ToString<T>(this T[][] arr, char separator, bool multiline, bool Showindex)
		{
			string dev = "";
			for (int i = 0; i < arr.Length; i++)
			{
				if (Showindex)
					dev += $"{i}: ";
				dev += arr[i].ToString(separator);
				if (multiline)
					dev += "\n";
			}
			dev = dev.TrimEnd(separator);
			return dev;
		}

		public static string ToString<T>(this T[][] arr, char separator, bool multiline) =>
			arr.ToString(separator, multiline, false);

		public static string ToString<T>(this T[,] arr, char separator, bool multiline, bool Showindex)
		{
			string dev = "";
			for (int i = 0; i < arr.GetLength(0); i++)
			{
				if (Showindex)
					dev += $"{i}: ";
				for (int j = 0; j < arr.GetLength(1); j++)
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

		public static string ToString<T>(this List<T> arr, char separator) =>
			arr.ToArray().ToString(separator);
	}
}
