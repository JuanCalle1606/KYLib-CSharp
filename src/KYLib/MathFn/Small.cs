using System;
using KYLib.Helpers;
using KYLib.Interfaces;

namespace KYLib.MathFn;

/// <summary>
/// Wrapper a la estructura <see cref="Byte"/>  implementando la interfaz <see cref="INumber"/>.
/// Esta clase provee sobrecargas a todos los operadores sobrecargables.
/// </summary>
public
#if NETSTANDARD2_0
	partial
#endif
	struct Small
#if NETSTANDARD2_1
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
	static readonly Small _One = 1;

	/// <summary>
	/// Valor interno de este numero.
	/// </summary>
	byte _value;

	/// <summary>
	/// Constructor interno.
	/// </summary>
	Small(byte origin) =>
		_value = origin;

	#endregion

	#region Estatico

	/// <summary>
	/// Convierte una representación en cadena de un numero a un Small.
	/// </summary>
	/// <param name="input">Cadena de entrada con formato numerico.</param>
	/// <returns>El entero parseado.</returns>
	public static Small Parse(string input) => new(byte.Parse(input));

	#endregion

	#region Operadores Unarios

	/// <inheritdoc/>
	public static Small operator +(Small num) => num;

	/// <inheritdoc/>
	public static Small operator -(Small num) => new((byte)-num._value);

	/// <inheritdoc/>
	public static Small operator ~(Small num) => new((byte)~num._value);

	/// <inheritdoc/>
	public static Small operator ++(Small num) => new(num._value + _One);

	/// <inheritdoc/>
	public static Small operator --(Small num) => new(num._value - _One);

	#endregion

	#region Operadores Binarios Aritmeticos

	/// <inheritdoc/>
	public static Small operator +(Small num1, Small num2) => new((byte)(num1._value + num2._value));

	/// <inheritdoc/>
	public static Small operator -(Small num1, Small num2) => new((byte)(num1._value - num2._value));

	/// <inheritdoc/>
	public static Small operator *(Small num1, Small num2) => new((byte)(num1._value * num2._value));

	/// <inheritdoc/>
	public static Small operator /(Small num1, Small num2) => new((byte)(num1._value / num2._value));

	/// <inheritdoc/>
	public static Small operator %(Small num1, Small num2) => new((byte)(num1._value % num2._value));

	#endregion

	#region Operadores Binarios Logicos

	/// <inheritdoc/>
	public static Small operator &(Small num1, Small num2) => new((byte)(num1._value & num2._value));

	/// <inheritdoc/>
	public static Small operator |(Small num1, Small num2) => new((byte)(num1._value | num2._value));

	/// <inheritdoc/>
	public static Small operator ^(Small num1, Small num2) => new((byte)(num1._value ^ num2._value));

	/// <inheritdoc/>
	public static Small operator <<(Small num1, Int32 num2) => new((byte)(num1._value << num2));

	/// <inheritdoc/>
	public static Small operator >>(Small num1, Int32 num2) => new((byte)(num1._value >> num2));

	#endregion

	#region Operadores Binarios Comparativos

	/// <inheritdoc/>
	public static bool operator ==(Small num1, Small num2) => num1._value == num2._value;

	/// <inheritdoc/>
	public static bool operator !=(Small num1, Small num2) => num1._value != num2._value;

	/// <inheritdoc/>
	public static bool operator <(Small num1, Small num2) => num1._value < num2._value;

	/// <inheritdoc/>
	public static bool operator >(Small num1, Small num2) => num1._value > num2._value;

	/// <inheritdoc/>
	public static bool operator <=(Small num1, Small num2) => num1._value <= num2._value;

	/// <inheritdoc/>
	public static bool operator >=(Small num1, Small num2) => num1._value >= num2._value;

	#endregion

	#region conversiones
	/// <inheritdoc/>
	public static implicit operator Small(byte value) => new(value);

	/// <inheritdoc/>
	public static implicit operator byte(Small value) => value._value;

	/// <inheritdoc/>
	public static explicit operator Small(Real value) => new(((INumber)value).ToByte(null));

	/// <inheritdoc/>
	public static explicit operator Small(Float value) => new(((INumber)value).ToByte(null));

	/// <inheritdoc/>
	public static explicit operator Small(Int value) => new(((INumber)value).ToByte(null));

	#endregion

	#region Interfaces
	/// <inheritdoc/>
	byte INumber<byte>.Value { get => _value; set => _value = value; }
#if NETSTANDARD2_1
	/// <inheritdoc/>
	byte IConvertible.ToByte(IFormatProvider provider) => _value;
#endif
	/// <inheritdoc/>
	void INumber.UpdateValue(INumber source) =>
		_value = source.ToByte(null);

	/// <inheritdoc/>
	void INumber.UpdateValue(object source)
	{
		//primero vemos si es un IConvertible
		var n = (IConvertible)source;
		if (n != null)
		{
			_value = ConvertHelper.ToByte(n);
			return;
		}
		//si llegamos aqui es porque no se pudo leer el numero, en ese caso se produce una exepción
		throw new ArgumentException("El valor proporcionado no puede ser convertido en Small.", nameof(source));
	}

	/// <inheritdoc/>
	void INumber.Add(INumber num) => _value += num.ToByte(null);

	/// <inheritdoc/>
	public Int32 CompareTo(object obj) => _value.CompareTo(obj);

	/// <inheritdoc/>
	public Int32 CompareTo(INumber other) => _value.CompareTo(other.ToByte(null));

	/// <inheritdoc/>
	void INumber.Div(INumber num) => _value /= num.ToByte(null);

	/// <inheritdoc/>
	public bool Equals(INumber other) => _value.Equals(other.ToByte(null));

	/// <inheritdoc/>
	public bool Equals(Small other) => _value.Equals(other._value);

	/// <inheritdoc/>
	public TypeCode GetTypeCode() => _value.GetTypeCode();

	/// <inheritdoc/>
	void INumber.Mul(INumber num) => _value *= num.ToByte(null);

	/// <inheritdoc/>
	void INumber.Rest(INumber num) => _value %= num.ToByte(null);

	/// <inheritdoc/>
	void INumber.Sub(INumber num) => _value -= num.ToByte(null);
	#endregion

	#region overrides

	/// <inheritdoc/>
	public override string ToString() => _value.ToString();

	/// <inheritdoc/>
	public override Int32 GetHashCode() => _value.GetHashCode();

	/// <inheritdoc/>
	public override bool Equals(object obj) => _value.Equals(obj);

	#endregion
}