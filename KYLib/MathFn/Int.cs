using System;
using KYLib.Helpers;
using KYLib.Interfaces;

namespace KYLib.MathFn
{

	/// <summary>
	/// Wrapper a la estructura <see cref="Int32"/>  implementando la interfaz <see cref="INumber"/>.
	/// Esta clase provee sobrecargas a todos los operadores sobrecargables.
	/// </summary>
	public
#if NS20
	partial
#endif
	struct Int
#if NS21
	: INumber<int>
#endif
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
		public static Int Parse(string input) => new(int.Parse(input));

		#endregion

		#region Operadores Unarios

		/// <inheritdoc/>
		public static Int operator +(Int num) => num;

		/// <inheritdoc/>
		public static Int operator -(Int num) => new(-num.value);

		/// <inheritdoc/>
		public static Int operator ~(Int num) => new(~num.value);

		/// <inheritdoc/>
		public static Int operator ++(Int num) => new(num.value + 1);

		/// <inheritdoc/>
		public static Int operator --(Int num) => new(num.value - 1);

		#endregion

		#region Operadores Binarios Aritmeticos

		/// <inheritdoc/>
		public static Int operator +(Int num1, Int num2) => new(num1.value + num2.value);

		/// <inheritdoc/>
		public static Int operator -(Int num1, Int num2) => new(num1.value - num2.value);

		/// <inheritdoc/>
		public static Int operator *(Int num1, Int num2) => new(num1.value * num2.value);

		/// <inheritdoc/>
		public static Int operator /(Int num1, Int num2) => new(num1.value / num2.value);

		/// <inheritdoc/>
		public static Int operator %(Int num1, Int num2) => new(num1.value % num2.value);

		#endregion

		#region Operadores Binarios Logicos

		/// <inheritdoc/>
		public static Int operator &(Int num1, Int num2) => new(num1.value & num2.value);

		/// <inheritdoc/>
		public static Int operator |(Int num1, Int num2) => new(num1.value | num2.value);

		/// <inheritdoc/>
		public static Int operator ^(Int num1, Int num2) => new(num1.value ^ num2.value);

		/// <inheritdoc/>
		public static Int operator <<(Int num1, Int32 num2) => new(num1.value << num2);

		/// <inheritdoc/>
		public static Int operator >>(Int num1, Int32 num2) => new(num1.value >> num2);

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
		public static implicit operator Int(int value) => new(value);

		/// <inheritdoc/>
		public static implicit operator int(Int value) => value.value;

		/// <inheritdoc/>
		public static implicit operator Int(Small value) => new(value);

		/// <inheritdoc/>
		public static explicit operator Int(Float value) => new(((INumber)value).ToInt32(null));

		/// <inheritdoc/>
		public static explicit operator Int(Real value) => new(((INumber)value).ToInt32(null));

		#endregion

		#region Interfaces
		/// <inheritdoc/>
		int INumber<int>.Value { get => value; set => this.value = value; }
#if NS21
		/// <inheritdoc/>
		int IConvertible.ToInt32(IFormatProvider provider) => value;
#endif
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
				value = ConvertHelper.ToInt32(n);
				return;
			}
			//si llegamos aqui es porque no se pudo leer el numero, en ese caso se produce una exepción
			throw new ArgumentException("El valor proporcionado no puede ser convertido en Int.", nameof(source));
		}

		/// <inheritdoc/>
		void INumber.Add(INumber num) => value += num.ToInt32(null);

		/// <inheritdoc/>
		public int CompareTo(object obj) => value.CompareTo(obj);

		/// <inheritdoc/>
		public int CompareTo(INumber other) => value.CompareTo(other.ToInt32(null));

		/// <inheritdoc/>
		void INumber.Div(INumber num) => value /= num.ToInt32(null);

		/// <inheritdoc/>
		public bool Equals(INumber other) => value.Equals(other.ToInt32(null));

		/// <inheritdoc/>
		public bool Equals(Int other) => value.Equals(other.value);

		/// <inheritdoc/>
		public TypeCode GetTypeCode() => value.GetTypeCode();

		/// <inheritdoc/>
		void INumber.Mul(INumber num) => value *= num.ToInt32(null);

		/// <inheritdoc/>
		void INumber.Rest(INumber num) => value %= num.ToInt32(null);

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