using System;
using System.Collections.Generic;

namespace KYLib.Utils
{
	public static class Rand
	{
		private static Random random = new Random();

		public static int GetInt() => random.Next();

		public static int GetInt(int min, int max) => random.Next(min, max);

		public static int GetInt(int max) => random.Next(max);

		public static int[] GetInt(int min, int max, int amount)
		{
			if (!(amount > 0))
				throw new ArgumentException();
			int[] dev = new int[amount];
			for (int i = 0; i < amount; i++)
				dev[i] = GetInt(min, max);
			return dev;
		}

		public static int[] GetInt(int min, int max, int minAmount, int maxAmout) => 
			GetInt(min, max, GetInt(minAmount, maxAmout + 1));

		public static float Get() => Convert.ToSingle(random.NextDouble());

		public static float Get(float max) => Get() * max;

		public static float Get(float min, float max) => (Get() * (max - min)) + min;

		public static float[] Get(float min, float max, int amount)
		{
			if (!(amount > 0))
				throw new ArgumentException();
			float[] dev = new float[amount];
			for (int i = 0; i < amount; i++)
				dev[i] = Get(min, max);
			return dev;
		}

		public static float[] Get(float min, float max, int minAmount, int maxAmout) =>
			Get(min, max, GetInt(minAmount, maxAmout + 1));

		public static float GetFixed(int decimals) => Fix(Get(), decimals);

		public static float GetFixed(int decimals, float max) => Fix(Get() * max, decimals);

		public static float GetFixed(int decimals, float min, float max) => Fix((Get() * (max - min)) + min, decimals);

		public static float[] GetFixed(int decimals, float min, float max, int amount)
		{
			if (!(amount > 0))
				throw new ArgumentException();
			float[] dev = new float[amount];
			for (int i = 0; i < amount; i++)
				dev[i] = GetFixed(decimals, min, max);
			return dev;
		}

		public static float[] GetFixed(int decimals, float min, float max, int minAmount, int maxAmout) =>
			GetFixed(decimals, min, max, GetInt(minAmount, maxAmout + 1));

		public static double GetDouble() => random.NextDouble();

		public static T Choise<T>(T[] arr) => arr[random.Next(arr.Length)];

		public static T Choise<T>(List<T> arr) => arr[random.Next(arr.Count)];

		public static float Fix(float number, int decimals) => 
			Convert.ToSingle(Math.Round(number, decimals));
	}
}
