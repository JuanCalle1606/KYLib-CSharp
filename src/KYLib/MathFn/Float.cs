using System;
using KYLib.Helpers;
using KYLib.Interfaces;
namespace KYLib.MathFn;

/// <summary>
/// Wrapper a la estructura <see cref="float"/>  implementando la interfaz <see cref="INumber"/>.
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
	float _value;

	/// <summary>
	/// Constructor interno.
	/// </summary>
	Float(float origin) =>
		_value = origin;

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
	public static Float operator -(Float num) => new(-num._value);

	/*/// <inheritdoc/>
	public static Float operator ~(Float num) => new(~num.value);*/

	/// <inheritdoc/>
	public static Float operator ++(Float num) => new(num._value + 1);

	/// <inheritdoc/>
	public static Float operator --(Float num) => new(num._value - 1);

	#endregion

	#region Operadores Binarios Aritmeticos

	/// <inheritdoc/>
	public static Float operator +(Float num1, Float num2) => new(num1._value + num2._value);

	/// <inheritdoc/>
	public static Float operator -(Float num1, Float num2) => new(num1._value - num2._value);

	/// <inheritdoc/>
	public static Float operator *(Float num1, Float num2) => new(num1._value * num2._value);

	/// <inheritdoc/>
	public static Float operator /(Float num1, Float num2) => new(num1._value / num2._value);

	/// <inheritdoc/>
	public static Float operator %(Float num1, Float num2) => new(num1._value % num2._value);

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
	public static bool operator ==(Float num1, Float num2) => num1._value == num2._value;

	/// <inheritdoc/>
	public static bool operator !=(Float num1, Float num2) => num1._value != num2._value;

	/// <inheritdoc/>
	public static bool operator <(Float num1, Float num2) => num1._value < num2._value;

	/// <inheritdoc/>
	public static bool operator >(Float num1, Float num2) => num1._value > num2._value;

	/// <inheritdoc/>
	public static bool operator <=(Float num1, Float num2) => num1._value <= num2._value;

	/// <inheritdoc/>
	public static bool operator >=(Float num1, Float num2) => num1._value >= num2._value;

	#endregion

	#region conversiones
	/// <inheritdoc/>
	public static implicit operator Float(float value) => new(value);

	/// <inheritdoc/>
	public static implicit operator float(Float value) => value._value;

	/// <inheritdoc/>
	public static implicit operator Float(Small value) => new(value);

	/// <inheritdoc/>
	public static implicit operator Float(Int value) => new(value);

	/// <inheritdoc/>
	public static explicit operator Float(Real value) => new(((INumber)value).ToSingle(null));

	#endregion

	#region Interfaces
	/// <inheritdoc/>
	float INumber<float>.Value { get => _value; set => _value = value; }
#if NETSTANDARD2_1
		/// <inheritdoc/>
		float IConvertible.ToSingle(IFormatProvider provider) => _value;
#endif
	/// <inheritdoc/>
	void INumber.UpdateValue(INumber source) =>
		_value = source.ToSingle(null);

	/// <inheritdoc/>
	void INumber.UpdateValue(object source)
	{
		//primero vemos si es un IConvertible
		//primero vemos si es un IConvertible
		var n = (IConvertible)source;
		if (n != null)
		{
			_value = ConvertHelper.ToSingle(n);
			return;
		}
		//si llegamos aqui es porque no se pudo leer el numero, en ese caso se produce una exepción
		throw new ArgumentException("El valor proporcionado no puede ser convertido en Float.", nameof(source));
	}

	/// <inheritdoc/>
	void INumber.Add(INumber num) => _value += num.ToSingle(null);

	/// <inheritdoc/>
	public Int32 CompareTo(object obj) => _value.CompareTo(obj);

	/// <inheritdoc/>
	public Int32 CompareTo(INumber other) => _value.CompareTo(other.ToSingle(null));

	/// <inheritdoc/>
	void INumber.Div(INumber num) => _value /= num.ToSingle(null);

	/// <inheritdoc/>
	public bool Equals(INumber other) => _value.Equals(other.ToSingle(null));

	/// <inheritdoc/>
	public bool Equals(Float other) => _value.Equals(other._value);

	/// <inheritdoc/>
	public TypeCode GetTypeCode() => _value.GetTypeCode();

	/// <inheritdoc/>
	void INumber.Mul(INumber num) => _value *= num.ToSingle(null);

	/// <inheritdoc/>
	void INumber.Rest(INumber num) => _value %= num.ToSingle(null);

	/// <inheritdoc/>
	void INumber.Sub(INumber num) => _value -= num.ToSingle(null);
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