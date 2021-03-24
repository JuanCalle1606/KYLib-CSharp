using System;
using System.ComponentModel;
using System.Diagnostics;

namespace KYLib.System.Components
{
	/// <summary>
	/// Crea un icono en el area de notificaciones creado mediante yad, esto solo funciona en linux y unicamente ha sido probado en debian.
	/// </summary>
	/// <remarks>
	/// Este tipo de icono no puede ser creado si no se tiene instalado yad en el sistema, deberia instalarlo con: <code>sudo apt install yad</code> y para los clientes deberia ser agregado como una dependencia.
	/// </remarks>
	public class YadTrayIcon : TrayIcon
	{
		#region Override
		private string tooltip;

		/// <inheritdoc/>
		public override string Tooltip
		{
			get => tooltip;
			set
			{
				CheckState();
				tooltip = value;
				ToInput($"tooltip:{tooltip}");
			}
		}

		private string icon;

		/// <inheritdoc/>
		public override string Icon
		{
			get => icon;
			set
			{
				CheckState();
				icon = value;
				ToInput($"icon:{icon}");
			}
		}

		private bool visible = true;

		/// <inheritdoc/>
		public override bool Visible
		{
			get => visible;
			set
			{
				CheckState();
				visible = value;
				ToInput($"visible:{visible}");
			}
		}

		/// <inheritdoc/>
		public override event EventHandler Command;

		#endregion

		#region General

		/// <summary>
		/// Crea un nuevo icono por medio del programa yad
		/// </summary>
		public YadTrayIcon()
		{
			//se crea la conexion con yad, en caso de no existir el programa yad porque no esta instalado entonces se producira un error.
			try
			{
				process = Bash.Start("yad", "--notification --listen --no-middle", OnOut, OnError, out ToInput);
			}
			catch (Win32Exception e)//ocurre cuando el archivo no es encontrado
			{
				throw new NotSupportedException($"El sistema \"{Info.CurrentSystem}\" actual no tiene instalado yad por lo que no puede usar YadTrayIcon", e);
			}
			// en caso de cualquier otra exepcion se dispara normal sin interceptarla

			//Configuracion por defecto
			ToInput("action:echo 'IconClicked'");
			Tooltip = "YadTrayIcon";
			Icon = "notification";

			//agregamos el evento de Disposing
			Disposed += AfterDisposed;
		}

		/// <summary>
		/// Se llama despues de usar Dispose()
		/// </summary>
		private void AfterDisposed(object sender, EventArgs e)
		{
			//liberamos los recursos del proceso.
			using (process)
				ToInput("quit");
		}

		/// <summary>
		/// Verifica que se puedan hacer llamadas al icono.
		/// </summary>
		private void CheckState()
		{
			if (IsDisposed || process.HasExited)
				throw new ObjectDisposedException(nameof(process), "No se pueden realizar acciones sobre un icono que ya ha sido cerrado");
		}

		#endregion

		#region Comunicación con el proceso

		Process process;

		/// <summary>
		/// Accion usada para enviar entradas
		/// </summary>
		Action<string> ToInput;

		/// <summary>
		/// Guarda la cadena del ultimo error que ocurrio.
		/// </summary>
		private string lastError;

		/// <summary>
		/// Recibe y guarda la cadena del ultimo error producido
		/// </summary>
		private void OnError(string str)
		{
			if (!string.IsNullOrWhiteSpace(str))
				lastError = str;
		}

		/// <summary>
		/// Recibe todas las salidas estandar del programa y las procesa
		/// </summary>
		private void OnOut(string str)
		{
			if (string.IsNullOrWhiteSpace(str))
				return;
			if (str.Contains("IconClicked"))
				Command?.Invoke(this, null);
		}

		/// <summary>
		/// Cierra este TrayIcon lo cual hace que ya no se pueda utilizar.
		/// </summary>
		public void Quit()
		{
			CheckState();
			//esto llama a AfterDisposed
			Dispose(true);
		}
		#endregion
	}
}