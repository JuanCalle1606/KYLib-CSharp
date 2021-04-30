using System;
using KYLib.Helpers;
using KYLib.Interfaces;

namespace KYLib.MathFn
{

	/// <summary>
	/// Wrapper a la estructura <see cref="Byte"/>  implementando la interfaz <see cref="INumber"/>.
	/// Esta clase provee sobrecargas a todos los operadores sobrecargables.
	/// </summary>
	public
#if NS20
	partial
#endif
	struct Small
#if NS21
	: INumber<byte>
#endif
	{

		#region General

		/// <summary>
		/// Representa el numero mas grande que puede tener un Small.
		/// </summary>
		public static readonly Small MaxValue = byte.MaxValue;

		/// <summary>
		/// Representa el numero mas pequeño que puede tener un Small.
		/// </summary>
		public static readonly Small MinValue = byte.MinValue;

		/// <summary>
		/// Guarda el valor 1 en tipo <see cref="Small"/>.
		/// </summary>
		private static readonly Small One = 1;

		/// <summary>
		/// Guarda el valor 0 en tipo <see cref="Small"/>.
		/// </summary>
		private static readonly Small Zero = 0;

		/// <summary>
		/// Valor interno de este numero.
		/// </summary>
		private byte value;

		/// <summary>
		/// Constructor interno.
		/// </summary>
		private Small(byte origin) =>
			value = origin;

		#endregion

		#region Estatico

		/// <summary>
		/// Convierte una representación en cadena de un numero a un Small.
		/// </summary>
		/// <param name="input">Cadena de entrada con formato numerico.</param>
		/// <returns>El entero parseado.</returns>
		public static Small Parse(string input) => new Small(byte.Parse(input));

		#endregion

		#region Operadores Unarios

		/// <inheritdoc/>
		public static Small operator +(Small num) => num;

		/// <inheritdoc/>
		public static Small operator -(Small num) => new Small((byte)(-num.value));

		/// <inheritdoc/>
		public static Small operator ~(Small num) => new Small((byte)(~num.value));

		/// <inheritdoc/>
		public static Small operator ++(Small num) => new Small(num.value + One);

		/// <inheritdoc/>
		public static Small operator --(Small num) => new Small(num.value - One);

		#endregion

		#region Operadores Binarios Aritmeticos

		/// <inheritdoc/>
		public static Small operator +(Small num1, Small num2) => new Small((byte)(num1.value + num2.value));

		/// <inheritdoc/>
		public static Small operator -(Small num1, Small num2) => new Small((byte)(num1.value - num2.value));

		/// <inheritdoc/>
		public static Small operator *(Small num1, Small num2) => new Small((byte)(num1.value * num2.value));

		/// <inheritdoc/>
		public static Small operator /(Small num1, Small num2) => new Small((byte)(num1.value / num2.value));

		/// <inheritdoc/>
		public static Small operator %(Small num1, Small num2) => new Small((byte)(num1.value % num2.value));

		#endregion

		#region Operadores Binarios Logicos

		/// <inheritdoc/>
		public static Small operator &(Small num1, Small num2) => new Small((byte)(num1.value & num2.value));

		/// <inheritdoc/>
		public static Small operator |(Small num1, Small num2) => new Small((byte)(num1.value | num2.value));

		/// <inheritdoc/>
		public static Small operator ^(Small num1, Small num2) => new Small((byte)(num1.value ^ num2.value));

		/// <inheritdoc/>
		public static Small operator <<(Small num1, Int32 num2) => new Small((byte)(num1.value << num2));

		/// <inheritdoc/>
		public static Small operator >>(Small num1, Int32 num2) => new Small((byte)(num1.value >> num2));

		#endregion

		#region Operadores Binarios Comparativos

		/// <inheritdoc/>
		public static bool operator ==(Small num1, Small num2) => num1.value == num2.value;

		/// <inheritdoc/>
		public static bool operator !=(Small num1, Small num2) => num1.value != num2.value;

		/// <inheritdoc/>
		public static bool operator <(Small num1, Small num2) => num1.value < num2.value;

		/// <inheritdoc/>
		public static bool operator >(Small num1, Small num2) => num1.value > num2.value;

		/// <inheritdoc/>
		public static bool operator <=(Small num1, Small num2) => num1.value <= num2.value;

		/// <inheritdoc/>
		public static bool operator >=(Small num1, Small num2) => num1.value >= num2.value;

		#endregion

		#region conversiones
		/// <inheritdoc/>
		public static implicit operator Small(byte value) => new Small(value);

		/// <inheritdoc/>
		public static implicit operator byte(Small value) => value.value;

		/// <inheritdoc/>
		public static explicit operator Small(Real value) => new Small(((INumber)value).ToByte(null));

		/// <inheritdoc/>
		public static explicit operator Small(Float value) => new Small(((INumber)value).ToByte(null));

		/// <inheritdoc/>
		public static explicit operator Small(Int value) => new Small(((INumber)value).ToByte(null));

		#endregion

		#region Interfaces
		/// <inheritdoc/>
		byte INumber<byte>.Value { get => value; set => this.value = value; }
#if NS21
		/// <inheritdoc/>
		byte IConvertible.ToByte(IFormatProvider provider) => value;
#endif
		/// <inheritdoc/>
		void INumber.UpdateValue(INumber source) =>
			value = source.ToByte(null);

		/// <inheritdoc/>
		void INumber.UpdateValue(object source)
		{
			//primero vemos si es un IConvertible
			var n = (IConvertible)source;
			if (n != null)
			{
				value = ConvertHelper.ToByte(n);
				return;
			}
			//si llegamos aqui es porque no se pudo leer el numero, en ese caso se produce una exepción
			throw new ArgumentException("El valor proporcionado no puede ser convertido en Small.", nameof(source));
		}

		/// <inheritdoc/>
		void INumber.Add(INumber num) => value += num.ToByte(null);

		/// <inheritdoc/>
		public Int32 CompareTo(object obj) => value.CompareTo(obj);

		/// <inheritdoc/>
		public Int32 CompareTo(INumber other) => value.CompareTo(other.ToByte(null));

		/// <inheritdoc/>
		void INumber.Div(INumber num) => value /= num.ToByte(null);

		/// <inheritdoc/>
		public bool Equals(INumber other) => value.Equals(other.ToByte(null));

		/// <inheritdoc/>
		public bool Equals(Small other) => value.Equals(other.value);

		/// <inheritdoc/>
		public TypeCode GetTypeCode() => value.GetTypeCode();

		/// <inheritdoc/>
		void INumber.Mul(INumber num) => value *= num.ToByte(null);

		/// <inheritdoc/>
		void INumber.Rest(INumber num) => value %= num.ToByte(null);

		/// <inheritdoc/>
		void INumber.Sub(INumber num) => value -= num.ToByte(null);
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