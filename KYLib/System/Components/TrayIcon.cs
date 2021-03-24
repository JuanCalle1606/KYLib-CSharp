using System;
using System.ComponentModel;

namespace KYLib.System.Components
{
	/// <summary>
	/// Representa un icono en el area de notificaciones, esta clase es abstracta y distintas implementaciones sera creadas para cada plataforma.
	/// </summary>
	public abstract class TrayIcon : Component
	{
		/// <summary>
		/// Define el mensaje que se muestra en este icono al pasar el mouse por encima.
		/// </summary>
		public abstract string Tooltip { get; set; }

		/// <summary>
		/// Define el icono que se usara, todas las clases hijas deben poder crear el icono apartir de una cadena.
		/// </summary>
		public abstract string Icon { get; set; }

		/// <summary>
		/// Evento que ocurre cuando se le da click izquierdo al icono.
		/// </summary>
		public abstract event EventHandler Command;

		/// <summary>
		/// Indica si el icono es visible o no.
		/// </summary>
		public abstract bool Visible { get; set; }

		/// <summary>
		/// Indica si este recurso ha llamado al metodo <see cref="IDisposable.Dispose"/>
		/// </summary>
		public bool IsDisposed { get; private set; }

		/// <summary>
		/// Constructor que se llama al instanciar una clase hija
		/// </summary>
		protected TrayIcon()
		{
			//cuando se liberan recursos ponemos esta variable en true
			Disposed += (o, s) => IsDisposed = true;
		}
	}
}