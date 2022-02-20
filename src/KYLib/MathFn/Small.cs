using System;
using KYLib.Abstractions;
using KYLib.Helpers;
namespace KYLib.MathFn;

/// <summary>
/// Wrapper a la estructura <see cref="byte"/>  implementando la interfaz <see cref="INumber"/>.
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

	/// <inheritdoc cref="Real.op_UnaryPlus"/>
	public static Small operator +(Small num) => num;

	/// <inheritdoc cref="Real.op_UnaryNegation"/>
	public static Small operator -(Small num) => new((byte)-num._value);

	/// <inheritdoc cref="Int.op_OnesComplement"/>
	public static Small operator ~(Small num) => new((byte)~num._value);

	/// <inheritdoc cref="Real.op_Increment"/>
	public static Small operator ++(Small num) => new(num._value + _One);

	/// <inheritdoc cref="Real.op_Decrement"/>
	public static Small operator --(Small num) => new(num._value - _One);

	#endregion

	#region Operadores Binarios Aritmeticos

	/// <inheritdoc cref="Real.op_Addition"/>
	public static Small operator +(Small num1, Small num2) => new((byte)(num1._value + num2._value));

	/// <inheritdoc cref="Real.op_Subtraction"/>
	public static Small operator -(Small num1, Small num2) => new((byte)(num1._value - num2._value));

	/// <inheritdoc cref="Real.op_Multiply"/>
	public static Small operator *(Small num1, Small num2) => new((byte)(num1._value * num2._value));

	/// <inheritdoc cref="Real.op_Division"/>
	public static Small operator /(Small num1, Small num2) => new((byte)(num1._value / num2._value));

	/// <inheritdoc cref="Real.op_Modulus"/>
	public static Small operator %(Small num1, Small num2) => new((byte)(num1._value % num2._value));

	#endregion

	#region Operadores Binarios Logicos
	
	// ReSharper disable InheritdocInvalidUsage
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

	// ReSharper restore InheritdocInvalidUsage
	#endregion

	#region conversiones
	
	/// <summary>
	/// Convierte un valor numerico en un <see cref="Small"/>.
	/// </summary>
	public static implicit operator Small(byte value) => new(value);

	/// <summary>
	/// COnvierte el valor en un <see cref="byte"/>.
	/// </summary>
	public static implicit operator byte(Small value) => value._value;

	/// <inheritdoc cref="op_Implicit(byte)"/>
	public static explicit operator Small(Real value) => new(((INumber)value).ToByte(null));

	/// <inheritdoc cref="op_Implicit(byte)"/>
	public static explicit operator Small(Float value) => new(((INumber)value).ToByte(null));

	/// <inheritdoc cref="op_Implicit(byte)"/>
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
	void INumber.UpdateValue(object? source)
	{
		if (source is not IConvertible n)
			throw new ArgumentException("El valor proporcionado no puede ser convertido en Small.", nameof(source));
		_value = ConvertHelper.ToByte(n);
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

	/// <inheritdoc cref="byte.Equals(byte)"/>
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