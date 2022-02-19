using System;
using KYLib.Helpers;

namespace KYLib.Interfaces;

/// <summary>
/// Representa un numero que esta basado en un tipo numerico nativo de C#.
/// </summary>
/// <typeparam name="TBase">Cualquier structura que pueda actuar como numero.</typeparam>
public interface INumber<TBase> : INumber
where TBase : struct, IComparable, IComparable<TBase>, IConvertible, IEquatable<TBase>, IFormattable
{
	/// <summary>
	/// Almacena el valor verdadero de este numero.
	/// </summary>
	TBase Value { get; set; }
#if NETSTANDARD2_1
	/// <inheritdoc/>
	bool IConvertible.ToBoolean(IFormatProvider provider) => Value.ToBoolean(provider);

	/// <inheritdoc/>
	byte IConvertible.ToByte(IFormatProvider provider) => ConvertHelper.ToByte(Value);

	/// <inheritdoc/>
	char IConvertible.ToChar(IFormatProvider provider) => Value.ToChar(provider);

	/// <inheritdoc/>
	DateTime IConvertible.ToDateTime(IFormatProvider provider) => Value.ToDateTime(provider);

	/// <inheritdoc/>
	decimal IConvertible.ToDecimal(IFormatProvider provider) => Value.ToDecimal(provider);

	/// <inheritdoc/>
	double IConvertible.ToDouble(IFormatProvider provider) => ConvertHelper.ToDouble(Value);

	/// <inheritdoc/>
	short IConvertible.ToInt16(IFormatProvider provider) => Value.ToInt16(provider);

	/// <inheritdoc/>
	int IConvertible.ToInt32(IFormatProvider provider) => ConvertHelper.ToInt32(Value);

	/// <inheritdoc/>
	long IConvertible.ToInt64(IFormatProvider provider) => Value.ToInt64(provider);

	/// <inheritdoc/>
	sbyte IConvertible.ToSByte(IFormatProvider provider) => Value.ToSByte(provider);

	/// <inheritdoc/>
	float IConvertible.ToSingle(IFormatProvider provider) => ConvertHelper.ToSingle(Value);

	/// <inheritdoc/>
	string IConvertible.ToString(IFormatProvider provider) => Value.ToString();

	/// <inheritdoc/>
	string IFormattable.ToString(string format, IFormatProvider formatProvider) =>
		((IFormattable)Value).ToString(format, formatProvider);

	/// <inheritdoc/>
	object IConvertible.ToType(Type conversionType, IFormatProvider provider) =>
		Value.ToType(conversionType, provider);

	/// <inheritdoc/>
	ushort IConvertible.ToUInt16(IFormatProvider provider) => Value.ToUInt16(provider);

	/// <inheritdoc/>
	uint IConvertible.ToUInt32(IFormatProvider provider) => Value.ToUInt32(provider);

	/// <inheritdoc/>
	ulong IConvertible.ToUInt64(IFormatProvider provider) => Value.ToUInt64(provider);
#endif
}