using System;
using KYLib.Abstractions;
using KYLib.Helpers;
namespace KYLib.MathFn;

/// <summary>
/// Wrapper a la estructura <see cref="int"/>  implementando la interfaz <see cref="INumber"/>.
/// Esta clase provee sobrecargas a todos los operadores sobrecargables.
/// </summary>
public
#if NETSTANDARD2_0
	partial
#endif
	struct Int
#if NETSTANDARD2_1
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
	int _value;

	/// <summary>
	/// Constructor interno.
	/// </summary>
	Int(int origin) =>
		_value = origin;

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

	/// <inheritdoc cref="Real.op_UnaryPlus"/>
	public static Int operator +(Int num) => num;

	/// <inheritdoc cref="Real.op_UnaryNegation"/>
	public static Int operator -(Int num) => new(-num._value);

	/// <summary>
	/// Devuelve la negación bit a bit del numero.
	/// </summary>
	public static Int operator ~(Int num) => new(~num._value);

	/// <inheritdoc cref="Real.op_Increment"/>
	public static Int operator ++(Int num) => new(num._value + 1);

	/// <inheritdoc cref="Real.op_Decrement"/>
	public static Int operator --(Int num) => new(num._value - 1);

	#endregion

	#region Operadores Binarios Aritmeticos

	/// <inheritdoc cref="Real.op_Addition"/>
	public static Int operator +(Int num1, Int num2) => new(num1._value + num2._value);

	/// <inheritdoc cref="Real.op_Subtraction"/>
	public static Int operator -(Int num1, Int num2) => new(num1._value - num2._value);

	/// <inheritdoc cref="Real.op_Multiply"/>
	public static Int operator *(Int num1, Int num2) => new(num1._value * num2._value);

	/// <inheritdoc cref="Real.op_Division"/>
	public static Int operator /(Int num1, Int num2) => new(num1._value / num2._value);

	/// <inheritdoc cref="Real.op_Modulus"/>
	public static Int operator %(Int num1, Int num2) => new(num1._value % num2._value);

	#endregion

	#region Operadores Binarios Logicos

	// ReSharper disable InheritdocInvalidUsage
	/// <inheritdoc/>
	public static Int operator &(Int num1, Int num2) => new(num1._value & num2._value);

	/// <inheritdoc/>
	public static Int operator |(Int num1, Int num2) => new(num1._value | num2._value);

	/// <inheritdoc/>
	public static Int operator ^(Int num1, Int num2) => new(num1._value ^ num2._value);

	/// <inheritdoc/>
	public static Int operator <<(Int num1, Int32 num2) => new(num1._value << num2);

	/// <inheritdoc/>
	public static Int operator >>(Int num1, Int32 num2) => new(num1._value >> num2);

	#endregion

	#region Operadores Binarios Comparativos

	/// <inheritdoc/>
	public static bool operator ==(Int num1, Int num2) => num1._value == num2._value;

	/// <inheritdoc/>
	public static bool operator !=(Int num1, Int num2) => num1._value != num2._value;

	/// <inheritdoc/>
	public static bool operator <(Int num1, Int num2) => num1._value < num2._value;

	/// <inheritdoc/>
	public static bool operator >(Int num1, Int num2) => num1._value > num2._value;

	/// <inheritdoc/>
	public static bool operator <=(Int num1, Int num2) => num1._value <= num2._value;

	/// <inheritdoc/>
	public static bool operator >=(Int num1, Int num2) => num1._value >= num2._value;
	// ReSharper restore InheritdocInvalidUsage
	#endregion

	#region conversiones
	
	/// <summary>
	/// Convierte un valor numerico a un <see cref="Int"/>.
	/// </summary>
	public static implicit operator Int(int value) => new(value);

	/// <summary>
	/// Convierte el valor a un <see cref="int"/>.
	/// </summary>
	public static implicit operator int(Int value) => value._value;

	/// <inheritdoc cref="op_Implicit(int)"/>
	public static implicit operator Int(Small value) => new(value);

	/// <inheritdoc cref="op_Implicit(int)"/>
	public static explicit operator Int(Float value) => new(((INumber)value).ToInt32(null));

	/// <inheritdoc cref="op_Implicit(int)"/>
	public static explicit operator Int(Real value) => new(((INumber)value).ToInt32(null));

	#endregion

	#region Interfaces
	/// <inheritdoc/>
	int INumber<int>.Value { get => _value; set => _value = value; }
#if NETSTANDARD2_1
	/// <inheritdoc/>
	int IConvertible.ToInt32(IFormatProvider provider) => _value;
#endif
	/// <inheritdoc/>
	void INumber.UpdateValue(INumber source) =>
		_value = source.ToInt32(null);

	/// <inheritdoc/>
	void INumber.UpdateValue(object? source)
	{
		if (source is not IConvertible n)
			throw new ArgumentException("El valor proporcionado no puede ser convertido en Int.", nameof(source));
		_value = ConvertHelper.ToInt32(n);
	}

	/// <inheritdoc/>
	void INumber.Add(INumber num) => _value += num.ToInt32(null);

	/// <inheritdoc/>
	public int CompareTo(object obj) => _value.CompareTo(obj);

	/// <inheritdoc/>
	public int CompareTo(INumber other) => _value.CompareTo(other.ToInt32(null));

	/// <inheritdoc/>
	void INumber.Div(INumber num) => _value /= num.ToInt32(null);

	/// <inheritdoc/>
	public bool Equals(INumber other) => _value.Equals(other.ToInt32(null));

	/// <inheritdoc cref="int.Equals(int)"/>
	public bool Equals(Int other) => _value.Equals(other._value);

	/// <inheritdoc/>
	public TypeCode GetTypeCode() => _value.GetTypeCode();

	/// <inheritdoc/>
	void INumber.Mul(INumber num) => _value *= num.ToInt32(null);

	/// <inheritdoc/>
	void INumber.Rest(INumber num) => _value %= num.ToInt32(null);

	/// <inheritdoc/>
	void INumber.Sub(INumber num) => _value -= num.ToInt32(null);
	#endregion

	#region overrides

	/// <inheritdoc/>
	public override string ToString() => _value.ToString();

	/// <inheritdoc/>
	public override int GetHashCode() => _value.GetHashCode();

	/// <inheritdoc/>
	public override bool Equals(object obj) => _value.Equals(obj);

	#endregion
}