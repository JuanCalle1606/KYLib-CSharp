#if NETSTANDARD2_0
using KYLib.Helpers;
using KYLib.Interfaces;
using System;

namespace KYLib.MathFn
{
	partial struct Real : INumber<double>
	{
		/// <inheritdoc/>
		bool IConvertible.ToBoolean(IFormatProvider provider) => ((IConvertible)_value).ToBoolean(provider);

		/// <inheritdoc/>
		byte IConvertible.ToByte(IFormatProvider provider) => ConvertHelper.ToByte(_value);

		/// <inheritdoc/>
		char IConvertible.ToChar(IFormatProvider provider) => ((IConvertible)_value).ToChar(provider);

		/// <inheritdoc/>
		DateTime IConvertible.ToDateTime(IFormatProvider provider) => ((IConvertible)_value).ToDateTime(provider);

		/// <inheritdoc/>
		decimal IConvertible.ToDecimal(IFormatProvider provider) => ((IConvertible)_value).ToDecimal(provider);

		/// <inheritdoc/>
		double IConvertible.ToDouble(IFormatProvider provider) => _value;

		/// <inheritdoc/>
		short IConvertible.ToInt16(IFormatProvider provider) => ((IConvertible)_value).ToInt16(provider);

		/// <inheritdoc/>
		int IConvertible.ToInt32(IFormatProvider provider) => ConvertHelper.ToInt32(_value);

		/// <inheritdoc/>
		long IConvertible.ToInt64(IFormatProvider provider) => ((IConvertible)_value).ToInt64(provider);

		/// <inheritdoc/>
		sbyte IConvertible.ToSByte(IFormatProvider provider) => ((IConvertible)_value).ToSByte(provider);

		/// <inheritdoc/>
		float IConvertible.ToSingle(IFormatProvider provider) => ConvertHelper.ToSingle(_value);

		/// <inheritdoc/>
		string IConvertible.ToString(IFormatProvider provider) => _value.ToString();

		/// <inheritdoc/>
		string IFormattable.ToString(string format, IFormatProvider formatProvider) =>
			_value.ToString(format, formatProvider);

		/// <inheritdoc/>
		object IConvertible.ToType(Type conversionType, IFormatProvider provider) =>
		((IConvertible)_value).ToType(conversionType, provider);

		/// <inheritdoc/>
		ushort IConvertible.ToUInt16(IFormatProvider provider) => ((IConvertible)_value).ToUInt16(provider);

		/// <inheritdoc/>
		uint IConvertible.ToUInt32(IFormatProvider provider) => ((IConvertible)_value).ToUInt32(provider);

		/// <inheritdoc/>
		ulong IConvertible.ToUInt64(IFormatProvider provider) => ((IConvertible)_value).ToUInt64(provider);
	}
}
#endif