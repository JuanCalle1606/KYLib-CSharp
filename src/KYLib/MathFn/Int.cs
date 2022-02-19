using System;
using KYLib.Helpers;
using KYLib.Interfaces;

namespace KYLib.MathFn;

/// <summary>
/// Wrapper a la estructura <see cref="Int32"/>  implementando la interfaz <see cref="INumber"/>.
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

	/// <inheritdoc/>
	public static Int operator +(Int num) => num;

	/// <inheritdoc/>
	public static Int operator -(Int num) => new(-num._value);

	/// <inheritdoc/>
	public static Int operator ~(Int num) => new(~num._value);

	/// <inheritdoc/>
	public static Int operator ++(Int num) => new(num._value + 1);

	/// <inheritdoc/>
	public static Int operator --(Int num) => new(num._value - 1);

	#endregion

	#region Operadores Binarios Aritmeticos

	/// <inheritdoc/>
	public static Int operator +(Int num1, Int num2) => new(num1._value + num2._value);

	/// <inheritdoc/>
	public static Int operator -(Int num1, Int num2) => new(num1._value - num2._value);

	/// <inheritdoc/>
	public static Int operator *(Int num1, Int num2) => new(num1._value * num2._value);

	/// <inheritdoc/>
	public static Int operator /(Int num1, Int num2) => new(num1._value / num2._value);

	/// <inheritdoc/>
	public static Int operator %(Int num1, Int num2) => new(num1._value % num2._value);

	#endregion

	#region Operadores Binarios Logicos

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

	#endregion

	#region conversiones
	/// <inheritdoc/>
	public static implicit operator Int(int value) => new(value);

	/// <inheritdoc/>
	public static implicit operator int(Int value) => value._value;

	/// <inheritdoc/>
	public static implicit operator Int(Small value) => new(value);

	/// <inheritdoc/>
	public static explicit operator Int(Float value) => new(((INumber)value).ToInt32(null));

	/// <inheritdoc/>
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
	void INumber.UpdateValue(object source)
	{
		//primero vemos si es un IConvertible
		var n = (IConvertible)source;
		if (n != null)
		{
			_value = ConvertHelper.ToInt32(n);
			return;
		}
		//si llegamos aqui es porque no se pudo leer el numero, en ese caso se produce una exepción
		throw new ArgumentException("El valor proporcionado no puede ser convertido en Int.", nameof(source));
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

	/// <inheritdoc/>
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