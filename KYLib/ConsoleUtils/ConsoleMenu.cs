using System;
using System.Collections.Generic;
using System.Text;
using KYLib.Extensions;

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
		protected List<ConsoleItem> Items = new List<ConsoleItem>();
		bool running = true;
		string String;

		/// <summary>
		/// Optiene el titulo del menu.
		/// </summary>
		public string Title { get; set; } = "Menu";

		/// <summary>
		/// Indica cual es el texto que se usa para indicar al usaurio que ingrese una opción.
		/// </summary>
		public string OptionText { get; set; } = null;

		/// <summary>
		/// Indica cual es el texto que se usa para indicar al usurio que la opción no es valida.
		/// </summary>
		public string OptionErrorText { get; set; } = null;

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
		#endregion

		#region Constructores
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
		/// Agrega un menu como submenu de este item.
		/// </summary>
		/// <param name="menu"></param>
		/// <returns></returns>
		public bool AddMenu(ConsoleMenu menu) => AddItem(menu.Title, menu.Start);

		/// <summary>
		/// Agrega un nuevo item al menu.
		/// </summary>
		/// <param name="item">Item a agregar.</param>
		/// <returns>Devuelve si el item se pudo agregar al menu.</returns>
		public bool AddItem(ConsoleItem item)
		{
			if (!Items.Contains(item))
			{
				Items.Add(item);
				UpdateString();
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
			int option;
			//bucle principal del menu.
			while (running)
			{
				Cons.Line = Title;
				BeforeRender?.Invoke();
				Cons.Line = String;
				AfterRender?.Invoke();

				option = Cons.GetInt(0, Items.Count,
					OptionText ?? DefaultOptionText,
					OptionErrorText ?? DefaultOptionErrorText);
				Cons.Clear();

				Items[option].Task?.Invoke();

				if (!Items[option].InstaOption)
					_ = Cons.Key;

				Cons.Clear();
			}
		}
		#endregion

		#region secundario
		void UpdateString()
		{
			String = Items.ToString(null, true, true);
		}
		#endregion
	}
}
