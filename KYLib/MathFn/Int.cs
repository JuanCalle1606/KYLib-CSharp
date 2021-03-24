using System;
using KYLib.Interfaces;

namespace KYLib.MathFn
{

	/// <summary>
	/// Wrapper a la estructura System.Int32 implementando la interfaz INumber.
	/// Esta clase provee sobrecargas a todos los operadores sobrecargables.
	/// </summary>
	public struct Int : INumber<int>
	{

		#region General

		/// <summary>
		/// Representa el numero mas grande que puede tener un Int.
		/// </summary>
		public static readonly Int MaxValue = int.MaxValue;

		/// <summary>
		/// Representa el numero mas pequeño que puede tener un Int.
		/// </summary>
		public static readonly Int MinValue = int.MinValue;

		/// <summary>
		/// Valor interno de este numero.
		/// </summary>
		private int value;

		/// <summary>
		/// Constructor interno.
		/// </summary>
		private Int(int origin) =>
			value = origin;

		#endregion

		#region Estatico

		/// <summary>
		/// Convierte una representación en cadena de un numero a un Int.
		/// </summary>
		/// <param name="input">Cadena de entrada con formato numerico.</param>
		/// <returns>El entero parseado.</returns>
		public static Int Parse(string input) => new Int(int.Parse(input));

		#endregion

		#region Operadores Unarios

		/// <inheritdoc/>
		public static Int operator +(Int num) => num;

		/// <inheritdoc/>
		public static Int operator -(Int num) => new Int(-num.value);

		/// <inheritdoc/>
		public static Int operator ~(Int num) => new Int(~num.value);

		/// <inheritdoc/>
		public static Int operator ++(Int num) => new Int(num.value + 1);

		/// <inheritdoc/>
		public static Int operator --(Int num) => new Int(num.value - 1);

		#endregion

		#region Operadores Binarios Aritmeticos

		/// <inheritdoc/>
		public static Int operator +(Int num1, Int num2) => new Int(num1.value + num2.value);

		/// <inheritdoc/>
		public static Int operator -(Int num1, Int num2) => new Int(num1.value - num2.value);

		/// <inheritdoc/>
		public static Int operator *(Int num1, Int num2) => new Int(num1.value * num2.value);

		/// <inheritdoc/>
		public static Int operator /(Int num1, Int num2) => new Int(num1.value / num2.value);

		/// <inheritdoc/>
		public static Int operator %(Int num1, Int num2) => new Int(num1.value % num2.value);

		#endregion

		#region Operadores Binarios Logicos

		/// <inheritdoc/>
		public static Int operator &(Int num1, Int num2) => new Int(num1.value & num2.value);

		/// <inheritdoc/>
		public static Int operator |(Int num1, Int num2) => new Int(num1.value | num2.value);

		/// <inheritdoc/>
		public static Int operator ^(Int num1, Int num2) => new Int(num1.value ^ num2.value);

		/// <inheritdoc/>
		public static Int operator <<(Int num1, Int32 num2) => new Int(num1.value << num2);

		/// <inheritdoc/>
		public static Int operator >>(Int num1, Int32 num2) => new Int(num1.value >> num2);

		#endregion

		#region Operadores Binarios Comparativos

		/// <inheritdoc/>
		public static bool operator ==(Int num1, Int num2) => num1.value == num2.value;

		/// <inheritdoc/>
		public static bool operator !=(Int num1, Int num2) => num1.value != num2.value;

		/// <inheritdoc/>
		public static bool operator <(Int num1, Int num2) => num1.value < num2.value;

		/// <inheritdoc/>
		public static bool operator >(Int num1, Int num2) => num1.value > num2.value;

		/// <inheritdoc/>
		public static bool operator <=(Int num1, Int num2) => num1.value <= num2.value;

		/// <inheritdoc/>
		public static bool operator >=(Int num1, Int num2) => num1.value >= num2.value;

		#endregion

		#region conversiones
		/// <inheritdoc/>
		public static implicit operator Int(int value) => new Int(value);

		/// <inheritdoc/>
		public static implicit operator int(Int value) => value.value;

		/// <inheritdoc/>
		public static explicit operator Int(Real value) => new Int(((INumber)value).ToInt32(null));

		#endregion

		#region Interfaces
		/// <inheritdoc/>
		int INumber<int>.Value { get => value; set => this.value = value; }

		/// <inheritdoc/>
		void INumber.UpdateValue(INumber source) =>
			value = source.ToInt32(null);

		/// <inheritdoc/>
		void INumber.UpdateValue(object source)
		{
			//primero vemos si es un IConvertible
			var n = (IConvertible)source;
			if (n != null)
			{
				try
				{
					value = n.ToInt32(null);
					return;
				}
				catch (Exception)
				{
					//En caso de un error lo ignoramos he intentamos otro metodo de conversión.
				}
			}
			//AHora obtenemos el string del objeto
			string s = source.ToString();
			if (!string.IsNullOrWhiteSpace(s))
			{
				int tempval = 0;
				//vemos si se puede parsear el string
				if (int.TryParse(s, out tempval))
				{
					value = tempval;
					return;
				}
			}
			//si llegamos aqui es porque no se pudo leer el numero, en ese caso se produce una exepción
			throw new ArgumentException("El valor proporcionado no puede ser convertido en Int.", nameof(source));
		}

		/// <inheritdoc/>
		void INumber<int>.Add(int num) => value += num;

		/// <inheritdoc/>
		void INumber.Add(INumber num) => value += num.ToInt32(null);

		/// <inheritdoc/>
		public int CompareTo(object obj) => value.CompareTo(obj);

		/// <inheritdoc/>
		public int CompareTo(INumber other) => value.CompareTo(other.ToInt32(null));

		/// <inheritdoc/>
		void INumber<int>.Div(int num) => value /= num;

		/// <inheritdoc/>
		void INumber.Div(INumber num) => value /= num.ToInt32(null);

		/// <inheritdoc/>
		public bool Equals(INumber other) => value.Equals(other.ToInt32(null));

		/// <inheritdoc/>
		public bool Equals(int other) => value.Equals(other);

		/// <inheritdoc/>
		public TypeCode GetTypeCode() => value.GetTypeCode();

		/// <inheritdoc/>
		void INumber<int>.Mul(int num) => value *= num;

		/// <inheritdoc/>
		void INumber.Mul(INumber num) => value *= num.ToInt32(null);

		/// <inheritdoc/>
		void INumber<int>.Rest(int num) => value %= num;

		/// <inheritdoc/>
		void INumber.Rest(INumber num) => value %= num.ToInt32(null);

		/// <inheritdoc/>
		void INumber<int>.Sub(int num) => value -= num;

		/// <inheritdoc/>
		void INumber.Sub(INumber num) => value -= num.ToInt32(null);
		#endregion

		#region overrides

		/// <inheritdoc/>
		public override string ToString() => value.ToString();

		/// <inheritdoc/>
		public override int GetHashCode() => value.GetHashCode();

		/// <inheritdoc/>
		public override bool Equals(object obj) => value.Equals(obj);

		#endregion
	}
}