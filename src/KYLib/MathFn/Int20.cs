#if NETSTANDARD2_0
using KYLib.Helpers;
using KYLib.Interfaces;
using System;

namespace KYLib.MathFn
{
	partial struct Int : INumber<int>
	{
		/// <inheritdoc/>
		bool IConvertible.ToBoolean(IFormatProvider provider) => ((IConvertible)value).ToBoolean(provider);

		/// <inheritdoc/>
		byte IConvertible.ToByte(IFormatProvider provider) => ConvertHelper.ToByte(value);

		/// <inheritdoc/>
		char IConvertible.ToChar(IFormatProvider provider) => ((IConvertible)value).ToChar(provider);

		/// <inheritdoc/>
		DateTime IConvertible.ToDateTime(IFormatProvider provider) => ((IConvertible)value).ToDateTime(provider);

		/// <inheritdoc/>
		decimal IConvertible.ToDecimal(IFormatProvider provider) => value;

		/// <inheritdoc/>
		double IConvertible.ToDouble(IFormatProvider provider) => value;

		/// <inheritdoc/>
		short IConvertible.ToInt16(IFormatProvider provider) => ((IConvertible)value).ToInt16(provider);

		/// <inheritdoc/>
		int IConvertible.ToInt32(IFormatProvider provider) => value;

		/// <inheritdoc/>
		long IConvertible.ToInt64(IFormatProvider provider) => value;

		/// <inheritdoc/>
		sbyte IConvertible.ToSByte(IFormatProvider provider) => ((IConvertible)value).ToSByte(provider);

		/// <inheritdoc/>
		float IConvertible.ToSingle(IFormatProvider provider) => value;

		/// <inheritdoc/>
		string IConvertible.ToString(IFormatProvider provider) => value.ToString();

		/// <inheritdoc/>
		string IFormattable.ToString(string format, IFormatProvider formatProvider) =>
			value.ToString(format, formatProvider);

		/// <inheritdoc/>
		object IConvertible.ToType(Type conversionType, IFormatProvider provider) =>
		((IConvertible)value).ToType(conversionType, provider);

		/// <inheritdoc/>
		ushort IConvertible.ToUInt16(IFormatProvider provider) => ((IConvertible)value).ToUInt16(provider);

		/// <inheritdoc/>
		uint IConvertible.ToUInt32(IFormatProvider provider) => ((IConvertible)value).ToUInt32(provider);

		/// <inheritdoc/>
		ulong IConvertible.ToUInt64(IFormatProvider provider) => ((IConvertible)value).ToUInt64(provider);
	}
}
#endif