#if NETSTANDARD2_0
using System;
using KYLib.Abstractions;
namespace KYLib.MathFn
{
	partial struct Small : INumber<byte>
	{
		/// <inheritdoc/>
		bool IConvertible.ToBoolean(IFormatProvider provider) => ((IConvertible)_value).ToBoolean(provider);

		/// <inheritdoc/>
		byte IConvertible.ToByte(IFormatProvider provider) => _value;

		/// <inheritdoc/>
		char IConvertible.ToChar(IFormatProvider provider) => ((IConvertible)_value).ToChar(provider);

		/// <inheritdoc/>
		DateTime IConvertible.ToDateTime(IFormatProvider provider) => ((IConvertible)_value).ToDateTime(provider);

		/// <inheritdoc/>
		decimal IConvertible.ToDecimal(IFormatProvider provider) => _value;

		/// <inheritdoc/>
		double IConvertible.ToDouble(IFormatProvider provider) => _value;

		/// <inheritdoc/>
		short IConvertible.ToInt16(IFormatProvider provider) => _value;

		/// <inheritdoc/>
		int IConvertible.ToInt32(IFormatProvider provider) => _value;

		/// <inheritdoc/>
		long IConvertible.ToInt64(IFormatProvider provider) => _value;

		/// <inheritdoc/>
		sbyte IConvertible.ToSByte(IFormatProvider provider) => ((IConvertible)_value).ToSByte(provider);

		/// <inheritdoc/>
		float IConvertible.ToSingle(IFormatProvider provider) => _value;

		/// <inheritdoc/>
		string IConvertible.ToString(IFormatProvider provider) => _value.ToString();

		/// <inheritdoc/>
		string IFormattable.ToString(string format, IFormatProvider formatProvider) =>
			_value.ToString(format, formatProvider);

		/// <inheritdoc/>
		object IConvertible.ToType(Type conversionType, IFormatProvider provider) =>
		((IConvertible)_value).ToType(conversionType, provider);

		/// <inheritdoc/>
		ushort IConvertible.ToUInt16(IFormatProvider provider) => _value;

		/// <inheritdoc/>
		uint IConvertible.ToUInt32(IFormatProvider provider) => _value;

		/// <inheritdoc/>
		ulong IConvertible.ToUInt64(IFormatProvider provider) => _value;
	}
}
#endif