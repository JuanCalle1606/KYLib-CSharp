using System;
using KYLib.Helpers;
using KYLib.Interfaces;

namespace KYLib.MathFn
{

	/// <summary>
	/// Wrapper a la estructura <see cref="Single"/>  implementando la interfaz <see cref="INumber"/>.
	/// Esta clase provee sobrecargas a todos los operadores sobrecargables.
	/// </summary>
	public
#if NETSTANDARD2_0
	partial
#endif
	struct Float
#if NETSTANDARD2_1
	: INumber<float>
#endif
	{

		#region General

		/// <summary>
		/// Representa el numero mas grande que puede tener un Float.
		/// </summary>
		public static readonly Float MaxValue = float.MaxValue;

		/// <summary>
		/// Representa el numero mas pequeño que puede tener un Float.
		/// </summary>
		public static readonly Float MinValue = float.MinValue;

		/// <summary>
		/// Valor interno de este numero.
		/// </summary>
		private float value;

		/// <summary>
		/// Constructor interno.
		/// </summary>
		private Float(float origin) =>
			value = origin;

		#endregion

		#region Estatico

		/// <summary>
		/// Convierte una representación en cadena de un numero a un Float.
		/// </summary>
		/// <param name="input">Cadena de entrada con formato numerico.</param>
		/// <returns>El entero parseado.</returns>
		public static Float Parse(string input) => new(float.Parse(input));

		#endregion

		#region Operadores Unarios

		/// <inheritdoc/>
		public static Float operator +(Float num) => num;

		/// <inheritdoc/>
		public static Float operator -(Float num) => new(-num.value);

		/*/// <inheritdoc/>
		public static Float operator ~(Float num) => new(~num.value);*/

		/// <inheritdoc/>
		public static Float operator ++(Float num) => new(num.value + 1);

		/// <inheritdoc/>
		public static Float operator --(Float num) => new(num.value - 1);

		#endregion

		#region Operadores Binarios Aritmeticos

		/// <inheritdoc/>
		public static Float operator +(Float num1, Float num2) => new(num1.value + num2.value);

		/// <inheritdoc/>
		public static Float operator -(Float num1, Float num2) => new(num1.value - num2.value);

		/// <inheritdoc/>
		public static Float operator *(Float num1, Float num2) => new(num1.value * num2.value);

		/// <inheritdoc/>
		public static Float operator /(Float num1, Float num2) => new(num1.value / num2.value);

		/// <inheritdoc/>
		public static Float operator %(Float num1, Float num2) => new(num1.value % num2.value);

		#endregion

		#region Operadores Binarios Logicos, Estos no aplican al tipo Float

		/*/// <inheritdoc/>
		public static Float operator &(Float num1, Float num2) => new(num1.value & num2.value);

		/// <inheritdoc/>
		public static Float operator |(Float num1, Float num2) => new(num1.value | num2.value);

		/// <inheritdoc/>
		public static Float operator ^(Float num1, Float num2) => new(num1.value ^ num2.value);

		/// <inheritdoc/>
		public static Float operator <<(Float num1, Int32 num2) => new(num1.value << num2);

		/// <inheritdoc/>
		public static Float operator >>(Float num1, Int32 num2) => new(num1.value >> num2);*/

		#endregion

		#region Operadores Binarios Comparativos

		/// <inheritdoc/>
		public static bool operator ==(Float num1, Float num2) => num1.value == num2.value;

		/// <inheritdoc/>
		public static bool operator !=(Float num1, Float num2) => num1.value != num2.value;

		/// <inheritdoc/>
		public static bool operator <(Float num1, Float num2) => num1.value < num2.value;

		/// <inheritdoc/>
		public static bool operator >(Float num1, Float num2) => num1.value > num2.value;

		/// <inheritdoc/>
		public static bool operator <=(Float num1, Float num2) => num1.value <= num2.value;

		/// <inheritdoc/>
		public static bool operator >=(Float num1, Float num2) => num1.value >= num2.value;

		#endregion

		#region conversiones
		/// <inheritdoc/>
		public static implicit operator Float(float value) => new(value);

		/// <inheritdoc/>
		public static implicit operator float(Float value) => value.value;

		/// <inheritdoc/>
		public static implicit operator Float(Small value) => new(value);

		/// <inheritdoc/>
		public static implicit operator Float(Int value) => new(value);

		/// <inheritdoc/>
		public static explicit operator Float(Real value) => new(((INumber)value).ToSingle(null));

		#endregion

		#region Interfaces
		/// <inheritdoc/>
		float INumber<float>.Value { get => value; set => this.value = value; }
#if NETSTANDARD2_1
		/// <inheritdoc/>
		float IConvertible.ToSingle(IFormatProvider provider) => value;
#endif
		/// <inheritdoc/>
		void INumber.UpdateValue(INumber source) =>
			value = source.ToSingle(null);

		/// <inheritdoc/>
		void INumber.UpdateValue(object source)
		{
			//primero vemos si es un IConvertible
			//primero vemos si es un IConvertible
			var n = (IConvertible)source;
			if (n != null)
			{
				value = ConvertHelper.ToSingle(n);
				return;
			}
			//si llegamos aqui es porque no se pudo leer el numero, en ese caso se produce una exepción
			throw new ArgumentException("El valor proporcionado no puede ser convertido en Float.", nameof(source));
		}

		/// <inheritdoc/>
		void INumber.Add(INumber num) => value += num.ToSingle(null);

		/// <inheritdoc/>
		public Int32 CompareTo(object obj) => value.CompareTo(obj);

		/// <inheritdoc/>
		public Int32 CompareTo(INumber other) => value.CompareTo(other.ToSingle(null));

		/// <inheritdoc/>
		void INumber.Div(INumber num) => value /= num.ToSingle(null);

		/// <inheritdoc/>
		public bool Equals(INumber other) => value.Equals(other.ToSingle(null));

		/// <inheritdoc/>
		public bool Equals(Float other) => value.Equals(other.value);

		/// <inheritdoc/>
		public TypeCode GetTypeCode() => value.GetTypeCode();

		/// <inheritdoc/>
		void INumber.Mul(INumber num) => value *= num.ToSingle(null);

		/// <inheritdoc/>
		void INumber.Rest(INumber num) => value %= num.ToSingle(null);

		/// <inheritdoc/>
		void INumber.Sub(INumber num) => value -= num.ToSingle(null);
		#endregion

		#region overrides

		/// <inheritdoc/>
		public override string ToString() => value.ToString();

		/// <inheritdoc/>
		public override Int32 GetHashCode() => value.GetHashCode();

		/// <inheritdoc/>
		public override bool Equals(object obj) => value.Equals(obj);

		#endregion
	}
}