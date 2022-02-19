using System;

namespace KYLib.Interfaces;

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