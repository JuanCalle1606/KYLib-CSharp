using System;
using System.Diagnostics;

namespace KYLib.System
{
	//en este archivo van los metodos que ofrecen control total.
	/// <summary>
	/// Clase que provee metodos para ejecutar comandos por medio de la terminal o para abrir otros programas.
	/// </summary>
	/// <remarks>
	/// Esta clase provee metodos para invocar procesos de forma controlada y de forma no controlada asi como metodos para crear los procesos.
	/// <para>
	/// Metodos controlados: todas las sobrecargas del metodo <see cref="CreateBashProcess(string)"/> y de <see cref="Start(string)"/> devuelven un proceso que el usuario puede editar despues de haberse creado y decidir como administrarlo, el metodo <see cref="Start(string)"/> ademas inicia de forma automatica el proceso y provee sobrecargas para obtener callbacks que son llamados cuando se reciben salidas estandar.
	/// </para>
	/// <para>
	/// Metodos no controlados: todos los demas metodos de la clase son no controlados, esto significa que cuando se intenta lanzar un ejecutable que no existe no se producira un error porque la invocación se hace por medio de un programa de terminal intermedio, ademas de que exceptuando los metodos <see cref="CreateBashProcess(string)"/> los demas no pueden ser detenidos una vez iniciados por lo que provee un menor control, se recomienda usar estos metodos solo para ejecutar herramientas por linea de comandos o programa pequeños con los que no sea necesario comunicarse.
	/// </para>
	/// </remarks>
	public static partial class Bash
	{
		#region WithCallback

		/// <summary>
		/// Inicia un proceso de un programa y lo controla.
		/// </summary>
		/// <remarks>
		/// A diferencia de los otros metodos de la clase <see cref="Bash"/> este metodo no ejecuta un comando por medio del terminal sino que directamente ejecuta el programa por su archivo ejecutable esto permite manejar el error de archivo no encontrado directamente una vez llamado a este metodo, igual a diferencia de todos los otros metodo este si devuelve el proceso que crea por lo que permite un mayor control sobre lo que ocurre con el proceso.
		/// </remarks>
		/// <param name="file">Nombre del archivo de programa a ejecutar.</param>
		/// <param name="stdout">Opcionalemente se puede pasar una acción que se llame cada vez que se reciba una salida estandar, en caso de no pasarse una acción la salida estandar puede seguir siendo administrada por medio del objeto de proceso devuelto.</param>
		/// <param name="stderr">Opcionalemente se puede pasar una acción que se llame cada vez que se reciba una salida del error estandar, en caso de no pasarse una acción el error estandar puede seguir siendo administrado por medio del objeto de proceso devuelto.</param>
		/// <param name="stdin">Este metodo crea una acción que devuelve por medio de este parametro, llame a esta acción cada ves que quiera escribir algo en la entrada estandar del programa, en caso de ignorar esta acción aun puede administrar la entrada estandar por dmdio del objeto de proceso devuelto.</param>
		/// <returns>Devuelve un objeto de proceso que representa al programa invocado corriendo.</returns>
		public static Process Start(string file, Action<string> stdout, Action<string> stderr, out Action<string> stdin) =>
			Start(file, string.Empty, null, stdout, stderr, out stdin);

		/// <summary>
		/// Inicia un proceso de un programa y lo controla.
		/// </summary>
		/// <remarks>
		/// A diferencia de los otros metodos de la clase <see cref="Bash"/> este metodo no ejecuta un comando por medio del terminal sino que directamente ejecuta el programa por su archivo ejecutable esto permite manejar el error de archivo no encontrado directamente una vez llamado a este metodo, igual a diferencia de todos los otros metodo este si devuelve el proceso que crea por lo que permite un mayor control sobre lo que ocurre con el proceso.
		/// </remarks>
		/// <param name="file">Nombre del archivo de programa a ejecutar.</param>
		/// <param name="args">Argumentos opcionales para pasar al programa.</param>
		/// <param name="stdout">Opcionalemente se puede pasar una acción que se llame cada vez que se reciba una salida estandar, en caso de no pasarse una acción la salida estandar puede seguir siendo administrada por medio del objeto de proceso devuelto.</param>
		/// <param name="stderr">Opcionalemente se puede pasar una acción que se llame cada vez que se reciba una salida del error estandar, en caso de no pasarse una acción el error estandar puede seguir siendo administrado por medio del objeto de proceso devuelto.</param>
		/// <param name="stdin">Este metodo crea una acción que devuelve por medio de este parametro, llame a esta acción cada ves que quiera escribir algo en la entrada estandar del programa, en caso de ignorar esta acción aun puede administrar la entrada estandar por dmdio del objeto de proceso devuelto.</param>
		/// <returns>Devuelve un objeto de proceso que representa al programa invocado corriendo.</returns>
		public static Process Start(string file, string args, Action<string> stdout, Action<string> stderr, out Action<string> stdin) =>
			Start(file, args, null, stdout, stderr, out stdin);


		/// <summary>
		/// Inicia un proceso de un programa y lo controla.
		/// </summary>
		/// <remarks>
		/// A diferencia de los otros metodos de la clase <see cref="Bash"/> este metodo no ejecuta un comando por medio del terminal sino que directamente ejecuta el programa por su archivo ejecutable esto permite manejar el error de archivo no encontrado directamente una vez llamado a este metodo, igual a diferencia de todos los otros metodo este si devuelve el proceso que crea por lo que permite un mayor control sobre lo que ocurre con el proceso.
		/// </remarks>
		/// <param name="file">Nombre del archivo de programa a ejecutar.</param>
		/// <param name="args">Argumentos opcionales para pasar al programa.</param>
		/// <param name="runin">Directorio en el que se ejecutara el proceso.</param>
		/// <param name="stdout">Opcionalemente se puede pasar una acción que se llame cada vez que se reciba una salida estandar, en caso de no pasarse una acción la salida estandar puede seguir siendo administrada por medio del objeto de proceso devuelto.</param>
		/// <param name="stderr">Opcionalemente se puede pasar una acción que se llame cada vez que se reciba una salida del error estandar, en caso de no pasarse una acción el error estandar puede seguir siendo administrado por medio del objeto de proceso devuelto.</param>
		/// <param name="stdin">Este metodo crea una acción que devuelve por medio de este parametro, llame a esta acción cada ves que quiera escribir algo en la entrada estandar del programa, en caso de ignorar esta acción aun puede administrar la entrada estandar por dmdio del objeto de proceso devuelto.</param>
		/// <returns>Devuelve un objeto de proceso que representa al programa invocado corriendo.</returns>
		public static Process Start(string file, string args, string runin, Action<string> stdout, Action<string> stderr, out Action<string> stdin)
		{
			var process = CreateProcess(file, args, runin, true);
			process.OutputDataReceived += (o, e) => stdout?.Invoke(e.Data);
			process.ErrorDataReceived += (o, e) => stderr?.Invoke(e.Data);
			process.Start();
			process.BeginOutputReadLine();
			process.BeginErrorReadLine();
			stdin = s =>
			{
				process.StandardInput.WriteLine(s);
				process.StandardInput.Flush();
			};
			return process;
		}

		#endregion

		#region WithoutCallback

		/// <summary>
		/// Inicia un proceso de un programa y lo controla.
		/// </summary>
		/// <remarks>
		/// A diferencia de los otros metodos de la clase <see cref="Bash"/> este metodo no ejecuta un comando por medio del terminal sino que directamente ejecuta el programa por su archivo ejecutable esto permite manejar el error de archivo no encontrado directamente una vez llamado a este metodo, igual a diferencia de todos los otros metodo este si devuelve el proceso que crea por lo que permite un mayor control sobre lo que ocurre con el proceso.
		/// </remarks>
		/// <param name="file">Nombre del archivo de programa a ejecutar.</param>
		/// <returns>Devuelve un objeto de proceso que representa al programa invocado corriendo.</returns>
		public static Process Start(string file) =>
			Start(file, string.Empty);

		/// <summary>
		/// Inicia un proceso de un programa y lo controla.
		/// </summary>
		/// <remarks>
		/// A diferencia de los otros metodos de la clase <see cref="Bash"/> este metodo no ejecuta un comando por medio del terminal sino que directamente ejecuta el programa por su archivo ejecutable esto permite manejar el error de archivo no encontrado directamente una vez llamado a este metodo, igual a diferencia de todos los otros metodo este si devuelve el proceso que crea por lo que permite un mayor control sobre lo que ocurre con el proceso.
		/// </remarks>
		/// <param name="file">Nombre del archivo de programa a ejecutar.</param>
		/// <param name="args">Argumentos opcionales para pasar al programa.</param>
		/// <returns>Devuelve un objeto de proceso que representa al programa invocado corriendo.</returns>
		public static Process Start(string file, string args) =>
			Start(file, args, null);

		/// <summary>
		/// Inicia un proceso de un programa y lo controla.
		/// </summary>
		/// <remarks>
		/// A diferencia de los otros metodos de la clase <see cref="Bash"/> este metodo no ejecuta un comando por medio del terminal sino que directamente ejecuta el programa por su archivo ejecutable esto permite manejar el error de archivo no encontrado directamente una vez llamado a este metodo, igual a diferencia de todos los otros metodo este si devuelve el proceso que crea por lo que permite un mayor control sobre lo que ocurre con el proceso.
		/// </remarks>
		/// <param name="file">Nombre del archivo de programa a ejecutar.</param>
		/// <param name="args">Argumentos opcionales para pasar al programa.</param>
		/// <param name="runin">Directorio en el que se ejecutara el proceso.</param>
		/// <returns>Devuelve un objeto de proceso que representa al programa invocado corriendo.</returns>
		public static Process Start(string file, string args, string runin)
		{
			var process = CreateProcess(file, args, runin, true);
			process.Start();
			return process;
		}

		#endregion
	}
}