using System;
using KYLib.Helpers;
using KYLib.Interfaces;

namespace KYLib.MathFn
{

	/// <summary>
	/// Wrapper a la estructura <see cref="Double"/> implementando la interfaz <see cref="INumber"/>.
	/// Esta clase provee sobrecargas a todos los operadores sobrecargables exceptuando los nos permitidos por <see cref="Double"/>.
	/// </summary>
	public struct Real : INumber<double>
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
		private double value;

		/// <summary>
		/// Constructor interno.
		/// </summary>
		private Real(double origin) =>
			value = origin;

		#endregion

		#region Estatico

		/// <summary>
		/// Convierte una representación en cadena de un numero a un Real.
		/// </summary>
		/// <param name="input">Cadena de entrada con formato numerico.</param>
		/// <returns>El entero parseado.</returns>
		public static Real Parse(string input) => new Real(double.Parse(input));

		#endregion

		#region Operadores Unarios

		/// <inheritdoc/>
		public static Real operator +(Real num) => num;

		/// <inheritdoc/>
		public static Real operator -(Real num) => new Real(-num.value);

		/*/// <inheritdoc/>
		public static Real operator ~(Real num) => new Real(~num.value);*/

		/// <inheritdoc/>
		public static Real operator ++(Real num) => new Real(num.value + 1);

		/// <inheritdoc/>
		public static Real operator --(Real num) => new Real(num.value - 1);

		#endregion

		#region Operadores Binarios Aritmeticos

		/// <inheritdoc/>
		public static Real operator +(Real num1, Real num2) => new Real(num1.value + num2.value);

		/// <inheritdoc/>
		public static Real operator -(Real num1, Real num2) => new Real(num1.value - num2.value);

		/// <inheritdoc/>
		public static Real operator *(Real num1, Real num2) => new Real(num1.value * num2.value);

		/// <inheritdoc/>
		public static Real operator /(Real num1, Real num2) => new Real(num1.value / num2.value);

		/// <inheritdoc/>
		public static Real operator %(Real num1, Real num2) => new Real(num1.value % num2.value);

		#endregion

		#region Operadores Binarios Logicos, Estos no aplican al tipo Double

		/*/// <inheritdoc/>
		public static Real operator &(Real num1, Real num2) => new Real(num1.value & num2.value);

		/// <inheritdoc/>
		public static Real operator &(Real num1, INumber num2) => new Real(num1.value & num2.ToDouble(null));

		/// <inheritdoc/>
		public static Real operator |(Real num1, Real num2) => new Real(num1.value | num2.value);

		/// <inheritdoc/>
		public static Real operator |(Real num1, INumber num2) => new Real(num1.value | num2.ToDouble(null));

		/// <inheritdoc/>
		public static Real operator ^(Real num1, Real num2) => new Real(num1.value ^ num2.value);

		/// <inheritdoc/>
		public static Real operator ^(Real num1, INumber num2) => new Real(num1.value ^ num2.ToDouble(null));

		/// <inheritdoc/>
		public static Real operator <<(Real num1, Int32 num2) => new Real(num1.value << num2);

		/// <inheritdoc/>
		public static Real operator >>(Real num1, Int32 num2) => new Real(num1.value >> num2);*/

		#endregion

		#region Operadores Binarios Comparativos

		/// <inheritdoc/>
		public static bool operator ==(Real num1, Real num2) => num1.value == num2.value;

		/// <inheritdoc/>
		public static bool operator !=(Real num1, Real num2) => num1.value != num2.value;

		/// <inheritdoc/>
		public static bool operator <(Real num1, Real num2) => num1.value < num2.value;

		/// <inheritdoc/>
		public static bool operator >(Real num1, Real num2) => num1.value > num2.value;

		/// <inheritdoc/>
		public static bool operator <=(Real num1, Real num2) => num1.value <= num2.value;

		/// <inheritdoc/>
		public static bool operator >=(Real num1, Real num2) => num1.value >= num2.value;

		#endregion

		#region conversiones
		/// <inheritdoc/>
		public static implicit operator Real(double value) => new Real(value);

		/// <inheritdoc/>
		public static implicit operator double(Real value) => value.value;

		/// <inheritdoc/>
		public static implicit operator Real(Int value) => new Real(value);

		/// <inheritdoc/>
		public static implicit operator Real(Float value) => new Real(value);

		/// <inheritdoc/>
		public static implicit operator Real(Small value) => new Real(value);

		#endregion

		#region Interfaces
		/// <inheritdoc/>
		double INumber<double>.Value { get => value; set => this.value = value; }

		/// <inheritdoc/>
		double IConvertible.ToDouble(IFormatProvider provider) => value;

		/// <inheritdoc/>
		void INumber.UpdateValue(INumber source) =>
			value = source.ToDouble(null);

		/// <inheritdoc/>
		void INumber.UpdateValue(object source)
		{
			//primero vemos si es un IConvertible
			var n = (IConvertible)source;
			if (n != null)
			{
				value = ConvertHelper.ToDouble(n);
				return;
			}
			//si llegamos aqui es porque no se pudo leer el numero, en ese caso se produce una exepción
			throw new ArgumentException("El valor proporcionado no puede ser convertido en Real.", nameof(source));
		}

		/// <inheritdoc/>
		void INumber.Add(INumber num) => value += num.ToDouble(null);

		/// <inheritdoc/>
		public int CompareTo(object obj) => value.CompareTo(obj);

		/// <inheritdoc/>
		public int CompareTo(INumber other) => value.CompareTo(other.ToDouble(null));

		/// <inheritdoc/>
		void INumber.Div(INumber num) => value /= num.ToDouble(null);

		/// <inheritdoc/>
		public bool Equals(INumber other) => value.Equals(other.ToDouble(null));

		/// <inheritdoc/>
		public bool Equals(Real other) => value.Equals(other.value);

		/// <inheritdoc/>
		public TypeCode GetTypeCode() => value.GetTypeCode();

		/// <inheritdoc/>
		void INumber.Mul(INumber num) => value *= num.ToDouble(null);

		/// <inheritdoc/>
		void INumber.Rest(INumber num) => value %= num.ToDouble(null);

		/// <inheritdoc/>
		void INumber.Sub(INumber num) => value -= num.ToDouble(null);
		#endregion

		#region overrides

		/// <inheritdoc/>
		public override string ToString() => value.ToString();

		/// <inheritdoc/>
		public override int GetHashCode() => value.GetHashCode();

		/// <inheritdoc/>
		public override bool Equals(object obj) => value.Equals(obj);

		#endregion
	}
}