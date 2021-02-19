﻿using System;
using System.Collections.Generic;
using System.Text;

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
		public static string ParseText { get; set; } = "";

		/// <summary>
		/// Mensaje que se le mostrara al usuario cuando deba ingresar un valor numerico y ocurre un error.
		/// </summary>
		public static string ParseErrorText { get; set; } = "";
		#endregion

		#region Entradas

		/// <summary>
		/// Obtiene una linea escrita por consola o escribe una nueva si se establece.
		/// </summary>
		/// <value>
		/// Su valor es cualquiera ingresado por el usuario.
		/// </value>
		public static string Line { get => Console.ReadLine(); set => Console.WriteLine(value); }

		/// <summary>
		/// Esta propiedad se usa para obtener una key ingresada por el usuario, usea el desecho "_" o  establezcala en null para detener la consola hasta que el usuario pulse una tecla.
		/// </summary>
		/// <value>
		/// Su valor es cualquier tecla que el usuario pulse en consola.
		/// </value>
		public static ConsoleKeyInfo? Key { get => Console.ReadKey(); set => Console.ReadKey(); }

		/// <summary>
		/// Obtiene un entero de la entrada del usuario.
		/// </summary>
		/// <remarks>
		/// Es esquivalente a <c>GetInt(<see cref="ParseText"/>, <see cref="ParseErrorText"/>)</c>
		/// </remarks>
		/// <value>
		/// Su valor es un un numero entero dado por el usuario.
		/// </value>
		public static int Int => GetInt(ParseText, ParseErrorText);

		/// <summary>
		/// Obtiene un numero entero de la entrada del usuario.
		/// </summary>
		/// <remarks>
		/// Este metodo se llama a si mismo una y otra vez hasta que el usuario ingrese un valor numerico valido.
		/// </remarks>
		/// <param name="text">Texto que se le mostrara al usuario para que ingrese una opción.</param>
		/// <param name="errorText">Texto que se le mostrara al usuaro cuando no se pueda parsear el texto.</param>
		/// <returns>Un valor entero ingresado por el usuario.</returns>
		public static int GetInt(string text,string errorText){
			try{
				if (!string.IsNullOrWhiteSpace(text))
					Line = text;
				return int.Parse(Line);
			}
			catch (Exception){
				Line = errorText;
				return GetInt(null, errorText);
			}
		}

		/// <summary>
		///  Obtiene un numero entero de la entrada del usuario pero que se encuentra en un rango dado..
		/// </summary>
		/// <param name="min">Valor minimo que puede tener el numero.</param>
		/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
		/// <param name="text">Texto que se le mostrara al usuario para que ingrese una opción.</param>
		/// <param name="errorText">Texto que se le mostrara al usuaro cuando no se pueda parsear el texto.</param>
		/// <returns>Un valor entero ingresado por el usuario.</returns>
		public static int GetInt(int min, int max, string text, string errorText)
		{
			int dev = GetInt(text, errorText);
			while (dev < min || dev >= max)
				dev = GetInt(errorText, errorText);
			return dev;
		}

		#endregion

		#region Utilidades

		/// <summary>
		/// Limpia la pantalla de la consola.
		/// </summary>
		public static void Clear() => Console.Clear();

		/// <summary>
		/// Muestra un objeto por consola.
		/// </summary>
		/// <remarks>
		/// Para poder mostrar los objetos en consola se llama a <c><paramref name="obj"/>.ToString()</c>.
		/// </remarks>
		/// <param name="obj">Cualquier objeto.</param>
		public static void Trace(object obj) => Console.WriteLine(obj);

		/// <summary>
		/// Muestra un cojunto de objetos en la consola, uno por linea.
		/// </summary>
		/// <param name="obj">Lista de objetos a mostrar.</param>
		public static void Trace(params object[] obj)
		{
			foreach(var item in obj)
				Trace(item);
		}

		/// <summary>
		/// Añade un salto de linea a la consola.
		/// </summary>
		public static void Trace()=>Console.WriteLine();

		#endregion
	}
}
