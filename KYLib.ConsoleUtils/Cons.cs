using System;
using System.Collections.Generic;
using System.Linq;
using KYLib.Extensions;
using kint = KYLib.MathFn.Int;

namespace KYLib.ConsoleUtils
{
	/// <summary>
	/// Posee utilidades de la consola.
	/// </summary>
	public static class Cons
	{
		/// <summary>
		/// Inicia la ejecución de un programa de consola.
		/// </summary>
		/// <typeparam name="T">Tipo que herede de <see cref="ConsoleApp"/>.</typeparam>
		public static void Start<T>() where T : ConsoleApp, new() => new T().Start();

		#region Variables

		/// <summary>
		/// Mensaje que se le mostrara al usuario cuando deba ingresar un valor numerico.
		/// </summary>
		public static string ParseText { get; set; } = "Ingresa un numero:";

		/// <summary>
		/// Mensaje que se le mostrara al usuario cuando deba ingresar un valor numerico y ocurre un error.
		/// </summary>
		public static string ParseErrorText { get; set; } = "Valor no aceptado, ingresa otro:";

		/// <summary>
		/// Al solicitar un <see cref="bool"/> por consola cualquiera de estos valores puede ser ingresado para tomarlo como <c>true</c>.
		/// </summary>
		public static readonly List<string> AllowStrings = new List<string>(new string[] {
			"y",
			"s",
			"yes",
			"si",
			"allow",
			"1",
			"permitir",
			"aceptar",
			"true"
		});

		/// <summary>
		/// Tipo de color de fondo de consola.
		/// </summary>
		private static readonly Type BColor = typeof(BackgroundColor);

		/// <summary>
		/// Tipo de color de letra de consola.
		/// </summary>
		private static readonly Type FColor = typeof(ForegroundColor);

		#endregion

		#region Entradas

		/// <summary>
		/// Obtiene una cadena de la entrada del usuario sin mostrarle al usuario lo que ingresa.
		/// </summary>
		public static string SecretLine => GetSecretString();

		/// <summary>
		/// Obtiene una linea escrita por consola o escribe una en la salida estandar si se establece.
		/// </summary>
		/// <value>
		/// Su valor es cualquiera ingresado por el usuario.
		/// </value>
		public static string Line
		{ get => Console.ReadLine(); set => Trace(value); }

		/// <summary>
		/// Obtiene una linea escrita por consola o escribe en la linea actual de la salida estandar si se establece.
		/// </summary>
		/// <value>
		/// Su valor es cualquiera ingresado por el usuario.
		/// </value>
		public static string Inline { get => Console.ReadLine(); set => Console.Write(value); }

		/// <summary>
		/// Obtiene una linea escrita por consola o escribe una en la salida estandar si se establece.
		/// </summary>
		/// <value>
		/// Su valor es cualquiera ingresado por el usuario.
		/// </value>
		public static string Error { get => Console.ReadLine(); set => TraceError(value); }

		/// <summary>
		/// Esta propiedad se usa para obtener una key ingresada por el usuario, usea el desecho "_" o  establezcala en null para detener la consola hasta que el usuario pulse una tecla.
		/// </summary>
		/// <value>
		/// Su valor es cualquier tecla que el usuario pulse en consola.
		/// </value>
		public static ConsoleKeyInfo? Key { get => Console.ReadKey(); set => Console.ReadKey(); }

		/// <summary>
		/// Obtiene una cadena de la entrada del usuario sin mostrarle al usuario lo que ingresa.
		/// </summary>
		public static string GetSecretString()
		{
			string dev = "";
			ConsoleKeyInfo key = default;
			while (true)
			{
				key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter)
					break;
				if (key.Key == ConsoleKey.Backspace)
				{
					if (dev.Length > 0)
					{
						dev = dev[0..^1];
						Inline = "\b \b";
					}
				}
				else
				{
					dev += key.KeyChar;
					Inline = "*";
				}

			}
			Trace();
			return dev;
		}

		/// <summary>
		/// Obtiene un valor booleano de la entrada del usuario.
		/// </summary>
		/// <param name="text">Mensaje a mostrar al usario al solicitar una entrada.</param>
		/// <returns>Devuelve <c>true</c> si la cadena ingresada por el usuario se encuentra en el arreglo <see cref="AllowStrings"/> o <c>false</c> si no se encuentra.</returns>
		public static bool GetBool(string text)
		{
			Inline = text;
			return AllowStrings.Contains(Line.ToLower());
		}

		/// <summary>
		/// Obtiene un numero flotante de la entrada del usuario.
		/// </summary>
		/// <param name="text">Texto a mostrar al solicitar el numero.</param>
		/// <param name="errorText">Texto a mostrar al ingresar un validor invalido.</param>
		/// <returns>Devuelve el flotante ingresado por el usuario.</returns>
		public static float GetFloat(string text, string errorText)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(text))
					Line = text;
				return float.Parse(Line);
			}
			catch (Exception)
			{
				Line = errorText;
				return GetFloat(null, errorText);
			}
		}

		/// <summary>
		/// Obtiene un entero de la entrada del usuario.
		/// </summary>
		/// <remarks>
		/// Es esquivalente a <c>GetInt(<see cref="ParseText"/>, <see cref="ParseErrorText"/>)</c>
		/// </remarks>
		/// <value>
		/// Su valor es un un numero entero dado por el usuario.
		/// </value>
		public static kint Int => GetInt(ParseText, ParseErrorText);

		/// <summary>
		/// Obtiene un numero entero de la entrada del usuario.
		/// </summary>
		/// <remarks>
		/// Este metodo se llama a si mismo una y otra vez hasta que el usuario ingrese un valor numerico valido.
		/// </remarks>
		/// <param name="text">Texto que se le mostrara al usuario para que ingrese una opción.</param>
		/// <param name="errorText">Texto que se le mostrara al usuaro cuando no se pueda parsear el texto.</param>
		/// <returns>Un valor entero ingresado por el usuario.</returns>
		public static kint GetInt(string text, string errorText)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(text))
					Line = text;
				return kint.Parse(Line);
			}
			catch (Exception)
			{
				Line = errorText;
				return GetInt(null, errorText);
			}
		}

		/// <summary>
		///  Obtiene un numero entero de la entrada del usuario pero que se encuentra en un rango dado.
		/// </summary>
		/// <param name="min">Valor minimo que puede tener el numero.</param>
		/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
		/// <returns>Un valor entero ingresado por el usuario.</returns>
		public static kint GetInt(kint min, kint max) =>
		GetInt(min, max, ParseText, ParseErrorText);

		/// <summary>
		///  Obtiene un numero entero de la entrada del usuario pero que se encuentra en un rango dado.
		/// </summary>
		/// <param name="min">Valor minimo que puede tener el numero.</param>
		/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
		/// <param name="text">Texto que se le mostrara al usuario para que ingrese una opción.</param>
		/// <param name="errorText">Texto que se le mostrara al usuaro cuando no se pueda parsear el texto.</param>
		/// <returns>Un valor entero ingresado por el usuario.</returns>
		public static kint GetInt(kint min, kint max, string text, string errorText)
		{
			int dev = GetInt(text, errorText);
			while (dev < min || dev >= max)
				dev = GetInt(errorText, errorText);
			return dev;
		}


		/// <summary>
		/// Solicita por la entrada del usuario un indice para escoger un elemento de <paramref name="arr"/>.
		/// </summary>
		/// <param name="arr">Enumarable en el cual se escogeran los elementos.</param>
		/// <param name="print">Indica si se imprimen en la consola las opciones a escoger.</param>
		/// <param name="text">Indica el texto que se muestra en consola al solicitar la entrada.</param>
		/// <param name="errorText">Indica el texto que se muestra al ingresar un texto no valido.</param>
		/// <typeparam name="T">Cualquier tipo.</typeparam>
		/// <returns>Devuelve el elemento escogido.</returns>
		public static T Choose<T>(IEnumerable<T> arr, bool print, string text, string errorText)
		{
			Cons.Line = text;
			if (print)
				Cons.Line = arr.ToString(true, true);
			return arr.ElementAt(GetInt(0, arr.Count(), ParseText, errorText));
		}

		/// <summary>
		/// Escoge un elemento de una enumaración.
		/// </summary>
		/// <param name="print">Indica si se imprimen en la consola las opciones a escoger.</param>
		/// <param name="text">Indica el texto que se muestra en consola al solicitar la entrada.</param>
		/// <param name="errorText">Indica el texto que se muestra al ingresar un texto no valido.</param>
		/// <typeparam name="T">Alguna enumeración (<see cref="Enum"/>).</typeparam>
		/// <returns>Devuelve el elemento escogido.</returns>
		public static T ChooseEnum<T>(bool print, string text, string errorText) where T : struct, Enum
		{
			var arr = Enum.GetValues(typeof(T)).OfType<T>();
			return Choose(arr, print, text, errorText);
		}

		#endregion

		#region Utilidades

		/// <summary>
		/// Limpia la pantalla de la consola.
		/// </summary>
		public static void Clear() => Console.Clear();

		/// <summary>
		/// Añade un salto de linea a la consola.
		/// </summary>
		public static void Trace() => Console.WriteLine();

		/// <summary>
		/// Muestra un objeto por consola y añade un salto de linea.
		/// </summary>
		/// <remarks>
		/// Para poder mostrar los objetos en consola se llama a <c><paramref name="obj"/>.ToString()</c>.
		/// </remarks>
		/// <param name="obj">Cualquier objeto a mostrar.</param>
		public static void Trace(object obj) => Console.WriteLine(obj);

		/// <summary>
		/// Muestra un objeto por consola y añade un salto de linea.
		/// </summary>
		/// <param name="fontColor">Color de la letra a mostrar.</param>
		/// <param name="obj">Cualquier objeto a mostrar.</param>
		public static void Trace(object obj, ForegroundColor fontColor)
		{
			Console.ForegroundColor = fontColor.ToConsoleColor();
			Trace(obj);
			Console.ResetColor();
		}

		/// <summary>
		/// Muestra un objeto por consola y añade un salto de linea.
		/// </summary>
		/// <param name="backColor">Color del fondo a mostrar.</param>
		/// <param name="obj">Cualquier objeto a mostrar.</param>
		public static void Trace(object obj, BackgroundColor backColor)
		{
			Console.BackgroundColor = backColor.ToConsoleColor();
			Trace(obj);
			Console.ResetColor();
		}

		/// <summary>
		/// Muestra un objeto por consola y añade un salto de linea.
		/// </summary>
		/// <param name="fontColor">Color de la letra a mostrar.</param>
		/// <param name="obj">Cualquier objeto a mostrar.</param>
		/// <param name="backColor">Color del fondo a mostrar.</param>
		public static void Trace(object obj, ForegroundColor fontColor, BackgroundColor backColor)
		{
			Console.ForegroundColor = fontColor.ToConsoleColor();
			Console.BackgroundColor = backColor.ToConsoleColor();
			Trace(obj);
			Console.ResetColor();
		}

		/// <summary>
		/// Muestra un cojunto de objetos en la consola, uno por linea.
		/// </summary>
		/// <param name="obj">Lista de objetos a mostrar.</param>
		public static void Trace(params object[] obj)
		{
			bool colorchanged = false;
			foreach (var item in obj)
			{
				if (item.GetType().IsEquivalentTo(FColor))
				{
					Console.ForegroundColor = (ConsoleColor)item;
					colorchanged = true;
					continue;
				}
				else if (item.GetType().IsEquivalentTo(BColor))
				{
					Console.BackgroundColor = (ConsoleColor)item;
					colorchanged = true;
					continue;
				}

				if (colorchanged)
				{
					Console.Write(item);
					Console.ResetColor();
					colorchanged = false;
					Trace();
				}
				else
				{
					Trace(item);
				}

			}
		}

		/// <summary>
		/// Muestra un objeto en la salida de error estandar.
		/// </summary>
		/// <remarks>
		/// Para poder mostrar los objetos en consola se llama a <c><paramref name="obj"/>.ToString()</c>.
		/// </remarks>
		/// <param name="obj">Cualquier objeto.</param>
		public static void TraceError(object obj)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine(obj);
			Console.ResetColor();
		}

		#endregion
	}
}
