using System;

namespace KYLib.Interfaces
{
	/// <summary>
	/// Representa un numero en el cual se pueden operar las operacione sbasicas como suma, resta, multiplicación y división.
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
		/// <returns>El resto de la división</returns>
		INumber Rest(INumber num);
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

		/// <summary>
		/// Le suma un numero a este numero.
		/// </summary>
		/// <param name="num">Numero a sumar.</param>
		void Add(TBase num);

		/// <summary>
		/// Le resta un numero a este numero.
		/// </summary>
		/// <param name="num">Numero a restar.</param>
		void Sub(TBase num);

		/// <summary>
		/// Le multiplica un numero a este numero.
		/// </summary>
		/// <param name="num">Numero a multiplicar.</param>
		void Mul(TBase num);

		/// <summary>
		/// Le divide un numero a este numero.
		/// </summary>
		/// <param name="num">Numero a dividir.</param>
		void Div(TBase num);

		/// <summary>
		/// Calcula el resto entre este numero y otro.
		/// </summary>
		/// <param name="num">El numero por el que se va a dividir.</param>
		/// <returns>El resto de la división</returns>
		INumber Rest(TBase num);
	}
}