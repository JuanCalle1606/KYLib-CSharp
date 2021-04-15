using System;
using KYLib.Interfaces;

namespace KYLib.ConsoleUtils
{

	/// <summary>
	/// Representa un item de un menu de consola.
	/// </summary>
	public class ConsoleItem : INameable
	{
		/// <summary>
		/// Acción que ejecutara este item.
		/// </summary>
		public Action Task = null;

		/// <summary>
		/// Indica si esta opción se devuelve al menu contenedor inmediatamente despues de ejecutarse.
		/// </summary>
		public bool InstaOption = false;

		/// <summary>
		/// Nombre del item.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Crea un nuevo item con un nombre por defecto y que no hace nada.
		/// </summary>
		public ConsoleItem()
		{
			Name = "MenuItem";
		}
		/// <summary>
		/// Crea un nuevo item con un nombre y que no hace nada.
		/// </summary>
		/// <param name="name">Nombre del item.</param>
		public ConsoleItem(string name)
		{
			Name = name;
		}
		/// <summary>
		/// Crea un nuevo item con un nombre por defecto y que hara una acción dada.
		/// </summary>
		/// <param name="action">La acción que hara este item.</param>
		public ConsoleItem(Action action)
		{
			Name = "MenuItem";
			Task = action;
		}
		/// <summary>
		/// Crea un nuevo item con un nombre y una acción dadas.
		/// </summary>
		/// <param name="name">Nombre del item.</param>
		/// <param name="action">La acción que hara este item.</param>
		public ConsoleItem(string name, Action action)
		{
			Name = name;
			Task = action;
		}
		/// <inheritdoc/>
		public override string ToString() => Name;
		/// <summary>
		/// Crea un nuevo item que hara una accion dada.
		/// </summary>
		/// <param name="action">La acción que hara este item.</param>
		public static implicit operator ConsoleItem(Action action) => new ConsoleItem(action);
		/// <summary>
		/// Crea un nuevo item con un nombre especifico.
		/// </summary>
		/// <param name="name">Nombre del item.</param>
		public static implicit operator ConsoleItem(string name) => new ConsoleItem(name);
	}
}
