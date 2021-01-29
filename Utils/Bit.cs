using System;
using System.Collections.Generic;
using System.Text;

namespace KYLib.Utils
{
	/// <summary>
	/// Representa un entero de 1 bit.
	/// </summary>
	public struct Bit
	{
		// El valor del bit
		bool Value;

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
		public bool ResetRest{
			get{
				bool tmp = Rest;
				Rest = false;
				return tmp;
			}
		}
		/// <summary>
		/// Crea un nuevo bit con un valor dado.
		/// </summary>
		/// <param name="value">Es <c>true</c> si el bit debe vale 1 o <c>false</c> si debe valer 0.</param>
		Bit(bool value){
			Value = value;
			Rest = false;
		}
		/// <summary>
		/// Crea un nuevo bit con un valor dado.
		/// </summary>
		/// <param name="value">Es <c>true</c> si el bit debe vale 1 o <c>false</c> si debe valer 0.</param> 
		/// <param name="rest">Es <c>true</c> si el bit tiene resto o <c>false</c> si debe no.</param>
		Bit(bool value,bool rest){
			Value = value;
			Rest = rest;
		}

		#region Aritmetic Operator
		/// <summary>
		/// Operador que suma 2 bits.
		/// </summary>
		/// <param name="val1">Primer bit.</param>
		/// <param name="val2">Segundo bit.</param>
		/// <returns>El resultado de la suma.</returns>
		public static Bit operator +(Bit val1, Bit val2)
		{
			bool R = val1.Rest || val2.Rest;
			bool OutR =
				(val2 && R) ||
				(val1 && R) ||
				(val1 && val2);
			bool Out =
				(!val1 && !val2 && R) ||
				(!val1 && val2 && !R) ||
				(val1 && !val2 && !R) ||
				(val1 && val2 && R);
			return new Bit(Out, OutR);
		}
		/// <summary>
		/// Operador que resta 2 bits.
		/// </summary>
		/// <param name="val1">Primer bit.</param>
		/// <param name="val2">Segundo bit.</param>
		/// <returns>El resultado de la resta.</returns>
		public static Bit operator -(Bit val1, Bit val2)
		{
			bool R = val1.Rest || val2.Rest;
			bool OutR =
				(!val1 && R) ||
				(!val1 && val2) || 
				(val2 && R);
			bool Out =
				(!val1 && !val2 && R) ||
				(!val1 && val2 && !R) ||
				(val1 && !val2 && !R) ||
				(val1 && val2 && R);
			return new Bit(Out, OutR);
		}
		/// <summary>
		/// Operador que multiplica 2 bits.
		/// </summary>
		/// <param name="val1">Primer bit.</param>
		/// <param name="val2">Segundo bit.</param>
		/// <returns>El resultado de la multiplicación.</returns>
		public static Bit operator *(Bit val1, Bit val2) => val1 && val2;
		#endregion

		#region Boolean Operators
		/// <inheritdoc/>
		public static bool operator true(Bit value) => value.Value;
		/// <inheritdoc/>
		public static bool operator false(Bit value) => !value.Value;

		/// <summary>
		/// Compara si dos bis son iguales.
		/// </summary>
		public static bool operator ==(Bit val1, Bit val2) => val1.Value == val2.Value;
		/// <summary>
		/// Compara si dos bits son diferentes.
		/// </summary>
		public static bool operator !=(Bit val1, Bit val2) => val1.Value != val2.Value;
		#endregion

		/// <inheritdoc/>
		public override string ToString() => 
			Value ? "1" : "0";

		#region From Numbers
		private static Bit FromDouble(double value)
		{
			/*
			if (value != 0 && value != 1)
				throw new ArgumentOutOfRangeException(nameof(value), value, "El tipo bit solo acepta los valores 1 o 0");
			*/
			return new Bit(value > 0);
		}
		/// <inheritdoc/>
		public static implicit operator Bit(byte value) => FromDouble(value);
		/// <inheritdoc/>
		public static implicit operator Bit(short value) => FromDouble(value);
		/// <inheritdoc/>
		public static implicit operator Bit(ushort value) => FromDouble(value);
		/// <inheritdoc/>
		public static implicit operator Bit(int value) => FromDouble(value);
		/// <inheritdoc/>
		public static implicit operator Bit(uint value) => FromDouble(value);
		/// <inheritdoc/>
		public static implicit operator Bit(float value) => FromDouble(value);
		/// <inheritdoc/>
		public static implicit operator Bit(double value) => FromDouble(value);
		/// <inheritdoc/>
		public static implicit operator Bit(bool value) => new Bit(value);
		#endregion
		#region To Numbers
		int Toint() => Value ? 1 : 0;

		/// <inheritdoc/>
		public static implicit operator byte(Bit value) => (byte)value.Toint();
		/// <inheritdoc/>
		public static implicit operator short(Bit value) => (byte)value.Toint();
		/// <inheritdoc/>
		public static implicit operator ushort(Bit value) => (byte)value.Toint();
		/// <inheritdoc/>
		public static implicit operator int(Bit value) => value.Toint();
		/// <inheritdoc/>
		public static implicit operator uint(Bit value) => (byte)value.Toint();
		/// <inheritdoc/>
		public static implicit operator float(Bit value) => value.Toint();
		/// <inheritdoc/>
		public static implicit operator double(Bit value) => value.Toint();
		/// <inheritdoc/>
		public static implicit operator bool(Bit value) => value.Value;
		#endregion

		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
