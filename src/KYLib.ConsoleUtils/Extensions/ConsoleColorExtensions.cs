using System;
using KYLib.ConsoleUtils;
// ReSharper disable once CheckNamespace
namespace KYLib.Extensions;

/// <summary>
/// Metodos de extensiones para convertir entre colores de consola.
/// </summary>
public static class ConsoleColorExtensions
{
	/// <summary>
	/// Convierte esta instancia en un <see cref="ConsoleColor"/>
	/// </summary>
	/// <param name="color">Color de origen.</param>
	/// <returns><see cref="ConsoleColor"/> devuelto.</returns>
	public static ConsoleColor ToConsoleColor(this BackgroundColor color) =>
		(ConsoleColor)color;

	/// <summary>
	/// Convierte esta instancia en un <see cref="ConsoleColor"/>
	/// </summary>
	/// <param name="color">Color de origen.</param>
	/// <returns><see cref="ConsoleColor"/> devuelto.</returns>
	public static ConsoleColor ToConsoleColor(this ForegroundColor color) =>
		(ConsoleColor)color;

	/// <summary>
	/// Aplica este color como color de fondo de la consola.
	/// </summary>
	/// <param name="color">Color a aplicar.</param>
	public static void Apply(this BackgroundColor color) =>
		Console.BackgroundColor = color.ToConsoleColor();

	/// <summary>
	/// Aplica este color como color de letra de la consola.
	/// </summary>
	/// <param name="color">Color a aplicar.</param>
	public static void Apply(this ForegroundColor color) =>
		Console.ForegroundColor = color.ToConsoleColor();
}