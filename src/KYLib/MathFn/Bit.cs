using System;
namespace KYLib.MathFn;

/// <summary>
/// Representa un entero de 1 bit.
/// </summary>
public struct Bit : IEquatable<Bit>
{
	/// <summary>
	/// Representa el bit 1.
	/// </summary>
	public static readonly Bit One = 1;
	
	/// <summary>
	/// Representa el bit 0.
	/// </summary>
	public static readonly Bit Zero = 0;

	// El valor del bit
	readonly bool _value;

	/// <summary>
	/// Indica si en la ultima operación que se realizo con este bit sobro(para suma) o falto(para resta) otro <c>bit</c>.
	/// </summary>
	/// <remarks>
	/// Esto se usa solo para operaciónes aritmeticas.
	/// </remarks>
	public bool Rest { get; private set; }

	/// <summary>
	/// Obtiene el resto actual de este bit y luego lo establece en 0.
	/// </summary>
	/// <see cref="Rest"/>
	/// <seealso cref="Rest"/>
	public bool ResetRest
	{
		get
		{
			var tmp = Rest;
			Rest = false;
			return tmp;
		}
	}
	
	/// <summary>
	/// Crea un nuevo bit con un valor dado.
	/// </summary>
	/// <param name="value">Es <c>true</c> si el bit debe vale 1 o <c>false</c> si debe valer 0.</param>
	Bit(bool value)
	{
		_value = value;
		Rest = false;
	}
	
	/// <summary>
	/// Crea un nuevo bit con un valor dado.
	/// </summary>
	/// <param name="value">Es <c>true</c> si el bit debe vale 1 o <c>false</c> si debe valer 0.</param> 
	/// <param name="rest">Es <c>true</c> si el bit tiene resto o <c>false</c> si no.</param>
	Bit(bool value, bool rest)
	{
		_value = value;
		Rest = rest;
	}

	#region Aritmetic Operator
	/// <summary>
	/// Suma dos bits.
	/// </summary>
	/// <param name="val1">Primer bit a sumar.</param>
	/// <param name="val2">Segundo bit a sumar.</param>
	/// <param name="res">La suma lleva o no resto.</param>
	/// <returns>La suma de los bits, si la suma tiene resto el bit resultante tendra la propiedad <c>Rest</c> en <c>true</c>.</returns>
	public static Bit Add(Bit val1, Bit val2, bool res)
	{
		var outR =
			val2 && res ||
			val1 && res ||
			val1 && val2;
		var @out =
			!val1 && !val2 && res ||
			!val1 && val2 && !res ||
			val1 && !val2 && !res ||
			val1 && val2 && res;
		return new Bit(@out, outR);
	}

	/// <summary>
	/// Operador que suma 2 bits.
	/// </summary>
	/// <param name="val1">Primer bit.</param>
	/// <param name="val2">Segundo bit.</param>
	/// <returns>El resultado de la suma.</returns>
	public static Bit operator +(Bit val1, Bit val2) => Add(val1, val2, val1.Rest || val2.Rest);

	/// <summary>
	/// Resta 2 bits.
	/// </summary>
	/// <param name="val1">Primer bit.</param>
	/// <param name="val2">Segundo bit.</param>
	/// <param name="res">Si la suma tiene resto o no.</param>
	/// <returns>El resultado de la resta.</returns>
	public static Bit Subtract(Bit val1, Bit val2, bool res)
	{
		var outR =
			!val1 && res ||
			!val1 && val2 ||
			val2 && res;
		var @out =
			!val1 && !val2 && res ||
			!val1 && val2 && !res ||
			val1 && !val2 && !res ||
			val1 && val2 && res;
		return new Bit(@out, outR);
	}

	/// <summary>
	/// Operador que resta 2 bits.
	/// </summary>
	/// <param name="val1">Primer bit.</param>
	/// <param name="val2">Segundo bit.</param>
	/// <returns>El resultado de la resta.</returns>
	public static Bit operator -(Bit val1, Bit val2) => Subtract(val1, val2, val1.Rest || val2.Rest);
	/// <summary>
	/// Operador que multiplica 2 bits.
	/// </summary>
	/// <param name="val1">Primer bit.</param>
	/// <param name="val2">Segundo bit.</param>
	/// <returns>El resultado de la multiplicación.</returns>
	public static Bit operator *(Bit val1, Bit val2) => val1 && val2;
	#endregion

	#region Boolean Operators
	/// <summary>
	/// Devuelve si el bit esta encendido.
	/// </summary>
	public static bool operator true(Bit value) => value._value;
	
	/// <summary>
	/// Devuelve si el bit esta apagado.
	/// </summary>
	public static bool operator false(Bit value) => !value._value;

	/// <summary>
	/// Compara si dos bis son iguales.
	/// </summary>
	public static bool operator ==(Bit val1, Bit val2) => val1._value == val2._value;
	
	/// <summary>
	/// Compara si dos bits son diferentes.
	/// </summary>
	public static bool operator !=(Bit val1, Bit val2) => val1._value != val2._value;
	
	/// <summary>
	/// Compara si un bit es mayor que otro.
	/// </summary>
	public static bool operator >(Bit val1, Bit val2) => val1 && !val2;
	/// <summary>
	/// Compara si un bit es menor que otro.
	/// </summary>
	public static bool operator <(Bit val1, Bit val2) => !val1 && val2;
	#endregion

	#region From Numbers

	static Bit FromDouble(kdouble value) => new(value != 0);
	
	/// <inheritdoc cref="op_Implicit(bool)"/>
	public static implicit operator Bit(kbyte value) => FromDouble(value);

	/// <inheritdoc cref="op_Implicit(bool)"/>
	public static implicit operator Bit(short value) => FromDouble(value);

	/// <inheritdoc cref="op_Implicit(bool)"/>
	public static implicit operator Bit(ushort value) => FromDouble(value);

	/// <inheritdoc cref="op_Implicit(bool)"/>
	public static implicit operator Bit(kint value) => FromDouble(value);

	/// <inheritdoc cref="op_Implicit(bool)"/>
	public static implicit operator Bit(uint value) => FromDouble(value);

	/// <inheritdoc cref="op_Implicit(bool)"/>
	public static implicit operator Bit(kfloat value) => FromDouble(value);
	
	/// <inheritdoc cref="op_Implicit(bool)"/>
	public static implicit operator Bit(kdouble value) => FromDouble(value);
	
	/// <summary>
	/// Crea un nuevo bit a partir de un valor.
	/// </summary>
	public static implicit operator Bit(bool value) => new(value);
	#endregion
	
	#region To Numbers

	/// <summary>
	/// Convierte el bit a un valor de otro tipo.
	/// </summary>
	int Toint() => _value ? 1 : 0;

	/// <inheritdoc cref="Toint"/>
	public static implicit operator byte(Bit value) => (byte)value.Toint();

	/// <inheritdoc cref="Toint"/>
	public static implicit operator short(Bit value) => (byte)value.Toint();

	/// <inheritdoc cref="Toint"/>
	public static implicit operator ushort(Bit value) => (byte)value.Toint();

	/// <inheritdoc cref="Toint"/>
	public static implicit operator int(Bit value) => value.Toint();

	/// <inheritdoc cref="Toint"/>
	public static implicit operator uint(Bit value) => (byte)value.Toint();

	/// <inheritdoc cref="Toint"/>
	public static implicit operator float(Bit value) => value.Toint();

	/// <inheritdoc cref="Toint"/>
	public static implicit operator double(Bit value) => value.Toint();

	/// <inheritdoc cref="Toint"/>
	public static implicit operator bool(Bit value) => value._value;
	#endregion

	/// <inheritdoc/>
	public override string ToString() =>
		_value ? "1" : "0";

	/// <inheritdoc/>
	public override bool Equals(object? obj) => _value.Equals(obj);

	/// <inheritdoc/>
	public bool Equals(Bit other) => _value == other._value;

	/// <inheritdoc/>
	public override int GetHashCode() => _value.GetHashCode();

}