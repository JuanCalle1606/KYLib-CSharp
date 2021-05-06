using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using KYLib.Extensions;
using bit = KYLib.MathFn.Bit;

/*
 * TODO:
 * -Implementar la división.
 * -Implementar el modulo.
 * -Cambiar la obtención del String.
 */

namespace KYLib.MathFn
{
	/// <summary>
	/// Representa un entero de un numero variable de bits.
	/// </summary>
	[Obsolete("La clase BitArray tiene problemas de rendimiento por lo que sera eliminada", true)]
	public struct BitArray : IComparable<BitArray>, IEnumerable<bit>, IEquatable<BitArray>
	{
		#region Base
		//Guarda la secuencia de bits.
		private readonly List<bit> bits;
		//Guarda la representacion en cadena de este nuemro.
		private string String { get; set; }
		//indica si hay que actualizar la cadena o no.
		private bool ChangeString;

		/// <summary>
		/// Indica el numero maximo de bits que puede tener un numero, este valor puede ser aumentado hasta la cantidad de int.MaxValue lo que implicaria un numero de 268Mb.
		/// </summary>
		public static int MaxBits { get; set; } = 128;

		BitArray(List<bit> source)
		{
			if (source.Count == 0)
				source.Add(bit.Zero);
			while (source.Count > MaxBits)
				source.RemoveAt(0);
			bits = source;
			ChangeString = true;
			String = null;
			RemoveZeros();
		}

		private void RemoveZeros()
		{
			while (bits.Count > 1 && bits[0] == bit.Zero)
				bits.RemoveAt(0);
		}

		/// <summary>
		/// Obtiene el tamaño en bits del numero actual.
		/// </summary>
		public int Length => bits.Count;
		#endregion

		#region Metodos Estaticos
		private static Regex IsPow2Reg = new Regex("^10*$");

		/// <summary>
		/// Indica si un numero es una potencia de 2.
		/// </summary>
		/// <param name="num">El numero a verificar.</param>
		/// <returns>true si <paramref name="num"/> es una potencia de 2 o false en caso contrario.</returns>
		public static bool IsPow2(BitArray num) => IsPow2Reg.IsMatch(num.bits.ToString(null));

		/// <summary>
		/// Indica si un numero es una potencia de 2.
		/// </summary>
		/// <param name="num">El numero a verificar.</param>
		/// <param name="power">SE establece en la potencia de 2 a la que esta elevado <paramref name="num"/> o -1 si no es potencia de 2.</param>
		/// <returns>true si <paramref name="num"/> es una potencia de 2 o false en caso contrario.</returns>
		public static bool IsPow2(BitArray num, out int power)
		{
			if (IsPow2(num))
			{
				power = num.bits.ToString(null).Replace('1', '0').LastIndexOf('0');
				return true;
			}
			power = -1;
			return false;
		}
		#endregion

		#region Metodos generales
		/// <summary>
		/// Desplaza el numero 1 bit hacia la izquierda.
		/// </summary>
		public void Shift()
		{
			bits.Add(bit.Zero);
			ChangeString = true;
		}
		/// <summary>
		/// Desplaza el numero <paramref name="amount"/> bits hacia la izquierda.
		/// </summary>
		/// <param name="amount">La cantidad de bits que se va a desplazar este numero.</param>
		public void Shift(int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				bits.Add(bit.Zero);
			}

			ChangeString = true;
		}

		/// <summary>
		/// Desplaza el numero 1 bit hacia la derecha.
		/// </summary>
		public void Unshift()
		{
			bits.RemoveAt(bits.Count - 1);
			ChangeString = true;
		}
		/// <summary>
		/// Desplaza el numero <paramref name="amount"/> bits hacia la derecha.
		/// </summary>
		/// <param name="amount">La cantidad de bits que se va a desplazar este numero.</param>
		public void Unshift(int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				bits.RemoveAt(bits.Count - 1);
			}

			ChangeString = true;
		}
		#endregion

		#region Operadores Aritmeticos
		/// <inheritdoc/>
		public static BitArray operator ++(BitArray num)
		{
			List<bit> dev;
			//Con este if aceleremanos un 200% el proceso
			if (num[^1] == bit.Zero)
			{
				dev = num.bits.ToList();
				dev[^1] = 1;
				return new BitArray(dev);
			}
			else if (num.bits.IndexOf(bit.Zero) == -1)
			{
				dev = new List<bit> { bit.One };
				dev.FillEnd(num.Length + 1, bit.Zero);
				return new BitArray(dev);
			}
			//Actualmente es menos optimo que hacer una suma normal
			/*else
			{
				dev = num.bits.ToList();
				int lc = dev.LastIndexOf(bit.Zero);
				dev.RemoveRange(lc, dev.Count - lc);
				dev.FillEnd(num.Length,bit.Zero);
				dev[lc] = bit.One;
				return new BitArray(dev);
			}*/
			return num + 1;
		}

		/// <inheritdoc/>
		public static BitArray operator +(BitArray num1, BitArray num2)
		{
			List<bit> dev = new List<bit>();
			int count = Math.Max(Math.Max(num1.bits.Count, num2.bits.Count) + 1, 2);
			num1.bits.FillStart(count, 0);
			num2.bits.FillStart(count, 0);

			bool res = false;

			for (int i = count - 1; i >= 0; i--)
			{
				dev.Insert(0, bit.Add(num1[i], num2[i], res));
				res = dev[0].ResetRest;
			}

			return new BitArray(dev);
		}
		/// <inheritdoc/>
		public static BitArray operator -(BitArray num1, BitArray num2)
		{
			List<bit> dev = new List<bit>();
			//si el segundo numero es mayor la resta dara 0.
			if (num1 < num2)
			{
				return new BitArray(dev);
			}

			int count = Math.Max(Math.Max(num1.bits.Count, num2.bits.Count) + 1, 2);
			num1.bits.FillStart(count, 0);
			num2.bits.FillStart(count, 0);

			bool res = false;

			for (int i = count - 1; i >= 0; i--)
			{
				dev.Insert(0, bit.Subtract(num1[i], num2[i], res));
				res = dev[0].ResetRest;
			}

			num1.RemoveZeros();
			num2.RemoveZeros();

			return new BitArray(dev);
		}
		/// <inheritdoc/>
		public static BitArray operator *(BitArray num1, BitArray num2)
		{
			int pow;
			List<bit> devbits;
			BitArray dev;
			//Si son potencias de dos uno de los multiplos solo es necesarios desplazar el otro numero.
			if (IsPow2(num2, out pow))
			{
				devbits = num1.bits.ToList();
				dev = new BitArray(devbits);
				dev.Shift(pow);
				return dev;
			}
			else if (IsPow2(num1, out pow))
			{
				devbits = num2.bits.ToList();
				dev = new BitArray(devbits);
				dev.Shift(pow);
				return dev;
			}

			//si no son potencias de dos se hace una multiplicacion normal
			devbits = new List<bit>();
			dev = new BitArray(devbits);
			BitArray copy = num1;

			for (int i = num2.bits.Count - 1; i >= 0; i--)
			{
				if (num2[i] == bit.One)
				{
					dev += copy;
				}

				copy.Shift();
			}
			return dev;

		}

		#endregion

		#region Operadores Comparativos
		/// <inheritdoc/>
		public static bool operator ==(BitArray num1, BitArray num2)
		{
			if (num1.bits.Count != num2.bits.Count)
			{
				return false;
			}

			return num1.String == num2.String;
		}
		/// <inheritdoc/>
		public static bool operator !=(BitArray num1, BitArray num2)
		{
			if (num1.bits.Count != num2.bits.Count)
			{
				return true;
			}

			return num1.String != num2.String;
		}
		/// <inheritdoc/>
		public static bool operator >(BitArray num1, BitArray num2)
		{
			if (num1.Length > num2.Length)
			{
				return true;
			}

			if (num1.Length < num2.Length)
			{
				return false;
			}

			for (int i = 0; i < num1.Length; i++)
			{
				if (num1[i] > num2[i])
				{
					return true;
				}
				else if (num1[i] < num2[i])
				{
					return false;
				}
			}

			return false;
		}
		/// <inheritdoc/>
		public static bool operator <(BitArray num1, BitArray num2)
		{
			if (num1.Length < num2.Length)
			{
				return true;
			}

			if (num1.Length > num2.Length)
			{
				return false;
			}

			for (int i = 0; i < num1.Length; i++)
			{
				if (num1[i] < num2[i])
				{
					return true;
				}
				else if (num1[i] > num2[i])
				{
					return false;
				}
			}

			return false;
		}

		/// <inheritdoc/>
		public static bool operator >=(BitArray num1, BitArray num2) => !(num1 < num2);
		/// <inheritdoc/>
		public static bool operator <=(BitArray num1, BitArray num2) => !(num1 > num2);

		#endregion

		#region Indexable
		/// <inheritdoc/>
		public bit this[int index] => bits[index];
		#endregion

		#region Conversiones
		// Cambia un numero decimal por su respectivo binario.
		private static BitArray ChangeBase(double n)
		{
			List<bit> dev = new List<bit>();
			n = Math.Round(n);
			while (n != 1)
			{
				if (n % 2 == 0)
				{
					dev.Insert(0, bit.Zero);
					n /= 2;
				}
				else
				{
					dev.Insert(0, bit.One);
					n--;
					n /= 2;
				}
			}
			dev.Insert(0, bit.One);

			return new BitArray(dev);
		}
		private static BitArray ChangeBase(ulong n)
		{
			List<bit> dev = new List<bit>();
			if (n == 0)
			{
				return new BitArray(dev);
			}

			while (n != 1)
			{
				if (n % 2 == 0)
				{
					dev.Insert(0, bit.Zero);
					n /= 2;
				}
				else
				{
					dev.Insert(0, bit.One);
					n--;
					n /= 2;
				}
			}
			dev.Insert(0, bit.One);

			return new BitArray(dev);
		}

		/// <inheritdoc/>
		public static implicit operator BitArray(ulong n) => ChangeBase(n);
		/// <inheritdoc/>
		public static implicit operator BitArray(long n) => ChangeBase((ulong)n);
		/// <inheritdoc/>
		public static implicit operator BitArray(uint n) => ChangeBase(n);
		/// <inheritdoc/>
		public static implicit operator BitArray(int n) => ChangeBase((uint)n);
		/// <inheritdoc/>
		public static implicit operator BitArray(ushort n) => ChangeBase(n);
		/// <inheritdoc/>
		public static implicit operator BitArray(short n) => ChangeBase((ushort)n);
		/// <inheritdoc/>
		public static implicit operator BitArray(byte n) => ChangeBase(n);
		/// <inheritdoc/>
		public static implicit operator BitArray(sbyte n) => ChangeBase((byte)n);
		/// <inheritdoc/>
		public static implicit operator BitArray(bit n) => ChangeBase(n);
		/// <inheritdoc/>
		public static explicit operator BitArray(float n) => ChangeBase(n);
		/// <inheritdoc/>
		public static explicit operator BitArray(double n) => ChangeBase(n);
		/// <inheritdoc/>
		public static explicit operator BitArray(decimal n) => ChangeBase((double)n);

		/// <inheritdoc/>
		public static implicit operator BitArray(bit[] SourceBits) =>
			new BitArray(SourceBits.ToList());
		/// <inheritdoc/>
		public static implicit operator BitArray(List<bit> SourceBits) =>
			new BitArray(SourceBits);
		#endregion

		#region IEnumerable
		/// <inheritdoc/>
		public IEnumerator<bit> GetEnumerator() =>
			bits.GetEnumerator();
		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator() =>
			bits.GetEnumerator();
		#endregion

		#region IComparable
		/// <inheritdoc/>
		public int CompareTo(BitArray other) => this <= other ? -1 : 1;
		#endregion

		#region overrides
		/// <inheritdoc/>
		public override string ToString()
		{
			if (ChangeString)
			{
				String = GetString();
			}

			return String;
		}
		private string GetString()
		{
			decimal n = 0;
			int count = bits.Count - 1;
			for (int i = count; i >= 0; i--)
			{
				n += bits[i] == bit.One ? (decimal)Math.Pow(2, count - i) : 0;
			}

			List<char> nl = n.ToString().ToCharArray().ToList();
			int steps = 1;
			for (int i = nl.Count - 1; i >= 0; i--)
			{
				if (steps == 3)
				{
					steps = 0;
					nl.Insert(i, ',');
				}
				steps++;
			}

			if (nl[0] == ',')
			{
				nl.RemoveAt(0);
			}

			return $"{nl.ToString(null)}";
		}

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj.Equals(this);
		/// <inheritdoc/>
		public bool Equals(BitArray other) =>
			EqualityComparer<List<bit>>.Default.Equals(bits, other.bits);
		/// <inheritdoc/>
		public override int GetHashCode() => 1537853281 + EqualityComparer<List<bit>>.Default.GetHashCode(bits);
		#endregion
	}
}
