using System;
using System.Globalization;
using KYLib.Helpers;
using KYLib.Interfaces;
// ReSharper disable CompareOfFloatsByEqualityOperator
namespace KYLib.MathFn;

/// <summary>
/// Wrapper a la estructura <see cref="double"/> implementando la interfaz <see cref="INumber"/>.
/// Esta clase provee sobrecargas a todos los operadores sobrecargables exceptuando los nos permitidos por <see cref="double"/>.
/// </summary>
public
#if NETSTANDARD2_0
	partial
#endif
	struct Real
#if NETSTANDARD2_1
	: INumber<double>
#endif
{

	#region General

	/// <summary>
	/// Representa el numero mas grande que puede tener un Real.
	/// </summary>
	public static readonly Real MaxValue = double.MaxValue;

	/// <summary>
	/// Representa el numero mas pequeño que puede tener un Real.
	/// </summary>
	public static readonly Real MinValue = double.MinValue;

	/// <summary>
	/// Valor interno de este numero.
	/// </summary>
	double _value;

	/// <summary>
	/// Constructor interno.
	/// </summary>
	Real(double origin) =>
		_value = origin;

	#endregion

	#region Estatico

	/// <summary>
	/// Convierte una representación en cadena de un numero a un Real.
	/// </summary>
	/// <param name="input">Cadena de entrada con formato numerico.</param>
	/// <returns>El entero parseado.</returns>
	public static Real Parse(string input) => new(double.Parse(input));

	#endregion

	#region Operadores Unarios

	/// <summary>
	/// Duelve el mismo valor.
	/// </summary>
	public static Real operator +(Real num) => num;

	/// <summary>
	/// Niega el valor de <paramref name="num"/>.
	/// </summary>
	public static Real operator -(Real num) => new(-num._value);

	/// <summary>
	/// Duelve <paramref name="num"/> incrementado en 1.
	/// </summary>
	public static Real operator ++(Real num) => new(num._value + 1);

	/// <summary>
	/// Duelve <paramref name="num"/> decrementado en 1.
	/// </summary>
	public static Real operator --(Real num) => new(num._value - 1);

	#endregion

	#region Operadores Binarios Aritmeticos

	/// <summary>
	/// Suma dos numeros.
	/// </summary>
	/// <param name="num1">Primer operador.</param>
	/// <param name="num2">Segundo operador</param>
	/// <returns>El resutado de la operación con <paramref name="num1"/> y <paramref name="num2"/>.</returns>
	public static Real operator +(Real num1, Real num2) => new(num1._value + num2._value);

	/// <inheritdoc cref="op_Addition"/>
	/// <summary>
	///	Resta dos numeros.
	/// </summary>
	public static Real operator -(Real num1, Real num2) => new(num1._value - num2._value);

	/// <inheritdoc cref="op_Addition"/>
	/// <summary>
	///	Multiplica dos numeros.
	/// </summary>
	public static Real operator *(Real num1, Real num2) => new(num1._value * num2._value);

	/// <inheritdoc cref="op_Addition"/>
	/// <summary>
	///	Divide dos numeros.
	/// </summary>
	public static Real operator /(Real num1, Real num2) => new(num1._value / num2._value);

	/// <inheritdoc cref="op_Addition"/>
	/// <summary>
	///	Modula dos numeros.
	/// </summary>
	public static Real operator %(Real num1, Real num2) => new(num1._value % num2._value);

	#endregion

	#region Operadores Binarios Logicos, Estos no aplican al tipo Double

	/*/// <inheritdoc/>
	public static Real operator &(Real num1, Real num2) => new(num1.value & num2.value);

	/// <inheritdoc/>
	public static Real operator &(Real num1, INumber num2) => new(num1.value & num2.ToDouble(null));

	/// <inheritdoc/>
	public static Real operator |(Real num1, Real num2) => new(num1.value | num2.value);

	/// <inheritdoc/>
	public static Real operator |(Real num1, INumber num2) => new(num1.value | num2.ToDouble(null));

	/// <inheritdoc/>
	public static Real operator ^(Real num1, Real num2) => new(num1.value ^ num2.value);

	/// <inheritdoc/>
	public static Real operator ^(Real num1, INumber num2) => new(num1.value ^ num2.ToDouble(null));

	/// <inheritdoc/>
	public static Real operator <<(Real num1, Int32 num2) => new(num1.value << num2);

	/// <inheritdoc/>
	public static Real operator >>(Real num1, Int32 num2) => new(num1.value >> num2);*/

	#endregion

	#region Operadores Binarios Comparativos

	/// <inheritdoc cref="double.op_Equality"/>
	public static bool operator ==(Real num1, Real num2) => num1._value == num2._value;

	/// <inheritdoc cref="double.op_Inequality"/>
	public static bool operator !=(Real num1, Real num2) => num1._value != num2._value;

	/// <inheritdoc cref="double.op_LessThan"/>
	public static bool operator <(Real num1, Real num2) => num1._value < num2._value;

	/// <inheritdoc cref="double.op_GreaterThan"/>
	public static bool operator >(Real num1, Real num2) => num1._value > num2._value;

	/// <inheritdoc cref="double.op_LessThanOrEqual"/>
	public static bool operator <=(Real num1, Real num2) => num1._value <= num2._value;

	/// <inheritdoc cref="double.op_GreaterThanOrEqual"/>
	public static bool operator >=(Real num1, Real num2) => num1._value >= num2._value;

	#endregion

	#region conversiones
	
	/// <summary>
	/// Convierte un valor numerico a un <see cref="Real"/>.
	/// </summary>
	public static implicit operator Real(double value) => new(value);

	/// <summary>
	/// Convierte el valor a un <see cref="double"/>.
	/// </summary>
	public static implicit operator double(Real value) => value._value;

	/// <inheritdoc cref="op_Implicit(double)"/>
	public static implicit operator Real(Int value) => new(value);

	/// <inheritdoc cref="op_Implicit(double)"/>
	public static implicit operator Real(Float value) => new(value);

	/// <inheritdoc cref="op_Implicit(double)"/>
	public static implicit operator Real(Small value) => new(value);

	#endregion

	#region Interfaces
	/// <inheritdoc/>
	double INumber<double>.Value { get => _value; set => _value = value; }
#if NETSTANDARD2_1
		/// <inheritdoc/>
		double IConvertible.ToDouble(IFormatProvider provider) => _value;
#endif
	/// <inheritdoc/>
	void INumber.UpdateValue(INumber source) =>
		_value = source.ToDouble(null);

	/// <inheritdoc/>
	void INumber.UpdateValue(object? source)
	{
		if (source is not IConvertible n)
			throw new ArgumentException("El valor proporcionado no puede ser convertido en Real.", nameof(source));
		_value = ConvertHelper.ToDouble(n);
	}

	/// <inheritdoc/>
	void INumber.Add(INumber num) => _value += num.ToDouble(null);

	/// <inheritdoc/>
	public int CompareTo(object obj) => _value.CompareTo(obj);

	/// <inheritdoc/>
	public int CompareTo(INumber other) => _value.CompareTo(other.ToDouble(null));

	/// <inheritdoc/>
	void INumber.Div(INumber num) => _value /= num.ToDouble(null);

	/// <inheritdoc/>
	public bool Equals(INumber other) => _value.Equals(other.ToDouble(null));

	/// <inheritdoc cref="double.Equals(double)"/>
	public bool Equals(Real other) => _value.Equals(other._value);

	/// <inheritdoc/>
	public TypeCode GetTypeCode() => _value.GetTypeCode();

	/// <inheritdoc/>
	void INumber.Mul(INumber num) => _value *= num.ToDouble(null);

	/// <inheritdoc/>
	void INumber.Rest(INumber num) => _value %= num.ToDouble(null);

	/// <inheritdoc/>
	void INumber.Sub(INumber num) => _value -= num.ToDouble(null);
	#endregion

	#region overrides

	/// <inheritdoc/>
	public override string ToString() => _value.ToString(CultureInfo.CurrentCulture);

	/// <inheritdoc/>
	public override int GetHashCode() => _value.GetHashCode();

	/// <inheritdoc/>
	public override bool Equals(object obj) => _value.Equals(obj);

	#endregion
}