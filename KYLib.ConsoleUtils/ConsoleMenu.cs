﻿using System;
using System.Collections.Generic;
using System.Text;
using KYLib.Extensions;
using KYLib.MathFn;

namespace KYLib.ConsoleUtils
{
	/// <summary>
	/// Representa un menu enlistado de items para mostrar por consola.
	/// </summary>
	public class ConsoleMenu
	{
		#region Variables
		/// <summary>
		/// Guarda todos los items del menu.
		/// </summary>
		protected List<ConsoleItem> Items = new();

		/// <summary>
		/// Indica si el menu esta corriendo
		/// </summary>
		private bool running = true;

		/// <summary>
		/// Optiene el titulo del menu.
		/// </summary>
		public string Title = "Menu";

		/// <summary>
		/// Indica cual es el texto que se usa para indicar al usaurio que ingrese una opción.
		/// </summary>
		public string OptionText = null;

		/// <summary>
		/// Indica cual es el texto que se usa para indicar al usurio que la opción no es valida.
		/// </summary>
		public string OptionErrorText = null;

		/// <summary>
		/// Indica si cuandos e agrega un nuevo item se debe agregar al final de la lista, en casod e ser false los items se agregan al inicio de la lista.
		/// </summary>
		public bool AddAtEnd = true;

		#endregion

		#region Acciones
		/// <summary>
		/// Define una función que sera llamada antes de dibujarse las opciones en pantalla.
		/// </summary>
		/// <remarks>
		/// Esta acción por defecto esta establecida en null por lo que no puede usar el operador += unicamente el =.
		/// </remarks>
		public Action BeforeRender;

		/// <summary>
		/// Define una función que sera llamada despues de dibujarse las opciones en pantalla.
		/// </summary>
		/// <remarks>
		/// Esta acción por defecto esta establecida en null por lo que no puede usar el operador += unicamente el =.
		/// </remarks>
		public Action AfterRender;

		#endregion

		#region Estaticas

		/// <summary>
		/// Indica cual es el texto por defecto que se usa para indicar al usaurio que ingrese una opción.
		/// </summary>
		public static string DefaultOptionText = "Ingresa una opción:";

		/// <summary>
		/// Indica cual es el texto por defecto que se usa para indicar al usurio que la opción no es valida.
		/// </summary>
		public static string DefaultOptionErrorText = "Opción no valida, ingresa otra:";

		/// <summary>
		/// Indica si la consola debe ser limpiada cada vez en cada iteración del menu.
		/// </summary>
		public static bool ClearConsole = true;

		#endregion

		#region Constructores

		/// <summary>
		/// Crea un nuevo menu de consola.
		/// </summary>
		public ConsoleMenu() : this(false)
		{ }

		/// <summary>
		/// Crea un nuevo menu de consola.
		/// </summary>
		/// <param name="addExit">Indica si se quiere agregar la opción de salir.</param>
		public ConsoleMenu(bool addExit)
		{
			if (addExit) Items.Add(new ConsoleItem("Salir", Stop) { InstaOption = true });
		}

		#endregion

		#region Interacciones

		/// <summary>
		/// Agrega un <see cref="ConsoleMenu"/> como submenu de este.
		/// </summary>
		/// <param name="menu">El menu que se quiere agregar.</param>
		/// <returns>Devuelve si se pudo agregar el submenu.</returns>
		public bool AddMenu(ConsoleMenu menu) =>
			AddMenu(menu, menu.Title);

		/// <summary>
		/// Agrega un <see cref="ConsoleMenu"/> como submenu de este.
		/// </summary>
		/// <param name="menu">El menu que se quiere agregar.</param>
		/// <param name="name">Indica si un nombre con el cual se mostrara el menu.</param>
		/// <returns>Devuelve si se pudo agregar el submenu.</returns>
		public bool AddMenu(ConsoleMenu menu, string name) =>
			AddMenu(menu, name, true);

		/// <summary>
		/// Agrega un <see cref="ConsoleMenu"/> como submenu de este.
		/// </summary>
		/// <param name="menu">El menu que se quiere agregar.</param>
		/// <param name="name">Indica si un nombre con el cual se mostrara el menu.</param>
		/// <param name="instaOption">Indica si una ves se salga de ese menu se volvera inmediatamente a este o si se espera por una pulsación del usuario.</param>
		/// <returns>Devuelve si se pudo agregar el submenu.</returns>
		public bool AddMenu(ConsoleMenu menu, string name, bool instaOption) =>
			AddItem(name, menu.Start, instaOption);


		/// <summary>
		/// Agrega un nuevo item al menu.
		/// </summary>
		/// <param name="item">Item a agregar.</param>
		/// <returns>Devuelve si el item se pudo agregar al menu.</returns>
		public bool AddItem(ConsoleItem item)
		{
			if (Items.FindByName(item.Name) == null)
			{
				if (AddAtEnd)
					Items.Add(item);
				else
					Items.Insert(0, item);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Agrega un nuevo item al menu.
		/// </summary>
		/// <param name="name">Nombre que tendra el item.</param>
		/// <param name="action"></param>
		/// <returns>Devuelve si el item se pudo agregar al menu.</returns>
		public bool AddItem(string name, Action action) => AddItem(new ConsoleItem(name, action));

		/// <summary>
		/// Agrega un nuevo item al menu.
		/// </summary>
		/// <param name="name">Nombre que tendra el item.</param>
		/// <param name="action"></param>
		/// <param name="instaOption">Indica la acción que efectuara este item.</param>
		/// <returns>Devuelve si el item se pudo agregar al menu.</returns>
		public bool AddItem(string name, Action action, bool instaOption) =>
			AddItem(new ConsoleItem(name, action) { InstaOption = instaOption });


		#endregion

		#region Logica principal
		/// <summary>
		/// Detiene la ejecución del menu en su proxima iteración.
		/// </summary>
		public void Stop() => running = false;

		/// <summary>
		/// Inicia la iteración del menu.
		/// </summary>
		public void Start()
		{
			running = true;
			Run();
		}

		/// <summary>
		/// Es el bucle principal del menu.
		/// </summary>
		private void Run()
		{
			Int option;
			//bucle principal del menu.
			while (running)
			{
				Cons.Line = Title;
				BeforeRender?.Invoke();
				Cons.Line = String();
				AfterRender?.Invoke();

				option = Cons.GetInt(0, Items.Count,
					OptionText ?? DefaultOptionText,
					OptionErrorText ?? DefaultOptionErrorText);
				if (ClearConsole) Cons.Clear();

				Items[option].Task?.Invoke();

				if (!Items[option].InstaOption)
					_ = Cons.Key;

				if (ClearConsole) Cons.Clear();
			}
		}
		#endregion

		#region Secundario

		string String() => Items.ToString(null, true, true);

		#endregion
	}
}
