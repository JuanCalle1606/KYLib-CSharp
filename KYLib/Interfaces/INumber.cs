using System;
using KYLib.Helpers;

namespace KYLib.Interfaces
{
	/// <summary>
	/// Representa un numero en el cual se pueden operar las operaciones basicas como suma, resta, multiplicación y división.
	/// </summary>
	public interface INumber : IComparable, IComparable<INumber>, IConvertible, IEquatable<INumber>, IFormattable
	{
		/// <summary>
		/// Le suma un numero a este numero.
		/// </summary>
		/// <param name="num">Numero a sumar.</param>
		void Add(INumber num);

		/// <summary>
		/// Le resta un numero a este numero.
		/// </summary>
		/// <param name="num">Numero a restar.</param>
		void Sub(INumber num);

		/// <summary>
		/// Le multiplica un numero a este numero.
		/// </summary>
		/// <param name="num">Numero a multiplicar.</param>
		void Mul(INumber num);

		/// <summary>
		/// Le divide un numero a este numero.
		/// </summary>
		/// <param name="num">Numero a dividir.</param>
		void Div(INumber num);

		/// <summary>
		/// Calcula el resto entre este numero y otro.
		/// </summary>
		/// <param name="num">El numero por el que se va a dividir.</param>
		void Rest(INumber num);

		/// <summary>
		/// Actualiza el valor interno de este numero basado en otro numero.
		/// </summary>
		/// <param name="source">Otro numero o algun objeto que sea convertible a numero del cual queremos copiar el valor a este.</param>
		void UpdateValue(object source);

		/// <summary>
		/// Actualiza el valor interno de este numero basado en otro numero.
		/// </summary>
		/// <param name="source">Otro numero del cual queremos copiar el valor a este.</param>
		void UpdateValue(INumber source);
	}

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

		/// <inheritdoc/>
		bool IConvertible.ToBoolean(IFormatProvider provider) => ((IConvertible)Value).ToBoolean(provider);

		/// <inheritdoc/>
		byte IConvertible.ToByte(IFormatProvider provider) => ConvertHelper.ToByte(Value);

		/// <inheritdoc/>
		char IConvertible.ToChar(IFormatProvider provider) => ((IConvertible)Value).ToChar(provider);

		/// <inheritdoc/>
		DateTime IConvertible.ToDateTime(IFormatProvider provider) => ((IConvertible)Value).ToDateTime(provider);

		/// <inheritdoc/>
		decimal IConvertible.ToDecimal(IFormatProvider provider) => ((IConvertible)Value).ToDecimal(provider);

		/// <inheritdoc/>
		double IConvertible.ToDouble(IFormatProvider provider) => ConvertHelper.ToDouble(Value);

		/// <inheritdoc/>
		short IConvertible.ToInt16(IFormatProvider provider) => ((IConvertible)Value).ToInt16(provider);

		/// <inheritdoc/>
		int IConvertible.ToInt32(IFormatProvider provider) => ConvertHelper.ToInt32(Value);

		/// <inheritdoc/>
		long IConvertible.ToInt64(IFormatProvider provider) => ((IConvertible)Value).ToInt64(provider);

		/// <inheritdoc/>
		sbyte IConvertible.ToSByte(IFormatProvider provider) => ((IConvertible)Value).ToSByte(provider);

		/// <inheritdoc/>
		float IConvertible.ToSingle(IFormatProvider provider) => ConvertHelper.ToSingle(Value);

		/// <inheritdoc/>
		string IConvertible.ToString(IFormatProvider provider) => Value.ToString();

		/// <inheritdoc/>
		string IFormattable.ToString(string format, IFormatProvider formatProvider) =>
			((IFormattable)Value).ToString(format, formatProvider);

		/// <inheritdoc/>
		object IConvertible.ToType(Type conversionType, IFormatProvider provider) =>
		((IConvertible)Value).ToType(conversionType, provider);

		/// <inheritdoc/>
		ushort IConvertible.ToUInt16(IFormatProvider provider) => ((IConvertible)Value).ToUInt16(provider);

		/// <inheritdoc/>
		uint IConvertible.ToUInt32(IFormatProvider provider) => ((IConvertible)Value).ToUInt32(provider);

		/// <inheritdoc/>
		ulong IConvertible.ToUInt64(IFormatProvider provider) => ((IConvertible)Value).ToUInt64(provider);
	}
}