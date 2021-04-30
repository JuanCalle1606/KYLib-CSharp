using System;
using System.Threading;
using System.Threading.Tasks;

namespace KYLib.Utils
{
	/// <summary>
	/// Provee metodos para la ejecución de tareas y esperas.
	/// </summary>
	public static class Runner
	{
		#region Every

		/// <summary>
		/// Ejecuta <paramref name="task"/> cada cierto tiempo.
		/// </summary>
		/// <param name="task">Tarea a ejecutar repetidamente.</param>
		/// <param name="delay">Tiempo de cada cuanto se eejcutara la función.</param>
		/// <param name="repeats">Numero de veces que ejecutaremos <paramref name="task"/>.</param>
		public static async void Every(Action task, TimeSpan delay, int repeats)
		{
			await Task.Run(async () =>
			{
				int exes = 0;
				while (exes++ < repeats)
				{
					task?.Invoke();
					await Task.Delay(delay);
				}
			});
		}

		/// <summary>
		/// Ejecuta <paramref name="task"/> cada cierto tiempo.
		/// </summary>
		/// <param name="task">Tarea a ejecutar repetidamente.</param>
		/// <param name="delay">Tiempo de cada cuanto se ejecutara la función.</param>
		/// <param name="token">Token de cancelación para cancelar la ejecución de la tarea.</param>
		public static async void Every(Action task, TimeSpan delay, CancellationToken token)
		{
			await Task.Run(async () =>
			{
				while (!token.IsCancellationRequested)
				{
					task?.Invoke();
					await Task.Delay(delay);
				}
			});
		}

		/// <summary>
		/// Ejecuta <paramref name="task"/> cada cierto tiempo.
		/// </summary>
		/// <param name="task">Tarea a ejecutar repetidamente.</param>
		/// <param name="delay">Tiempo de cada cuanto se eejcutara la función.</param>
		/// <param name="predicate">Función que se evalua para saber cuando detener la ejecución. Si evalua <c>false</c> se detiene la ejecución.</param>
		public static async void Every(Action task, TimeSpan delay, Predicate predicate)
		{
			await Task.Run(async () =>
			{
				while ((predicate?.Invoke()).GetValueOrDefault(false))
				{
					task?.Invoke();
					await Task.Delay(delay);
				}
			});
		}

		#endregion Every

		#region WaitWhile

		/// <summary>
		/// Detiene el ciclo actual y hasta que <paramref name="predicate"/> no evalue <c>false</c> no devuelve el control.
		/// </summary>
		/// <param name="predicate">Predicado que indica si se debe seguir esperando, si el predicado retorna <c>false</c> entonces se devuelve el control al llamador.</param>
		public static void WaitWhile(Predicate predicate) =>
			WaitWhile(predicate, TimeSpan.FromMilliseconds(25));

		/// <summary>
		/// Detiene el ciclo actual y hasta que <paramref name="predicate"/> no evalue <c>false</c> no devuelve el control.
		/// </summary>
		/// <param name="predicate">Predicado que indica si se debe seguir esperando, si el predicado retorna <c>false</c> entonces se devuelve el control al llamador.</param>
		/// <param name="interval">Intervalo de tiempo que indica cada cuando hay que evaluar <paramref name="predicate"/>.</param>
		public static void WaitWhile(Predicate predicate, TimeSpan interval) =>
			WaitWhile(predicate, interval, TimeSpan.Zero);

		/// <summary>
		/// Detiene el ciclo actual y hasta que <paramref name="predicate"/> no evalue <c>false</c> no devuelve el control.
		/// </summary>
		/// <param name="predicate">Predicado que indica si se debe seguir esperando, si el predicado retorna <c>false</c> entonces se devuelve el control al llamador.</param>
		/// <param name="interval">Intervalo de tiempo que indica cada cuando hay que evaluar <paramref name="predicate"/>.</param>
		/// <param name="timeout">Maximo tiempo de espera, cuando se supera este tiempo entonces se devolvera el control. Si este valor es <see cref="TimeSpan.Zero"/> entonces se esperara indefinidamente.</param>
		public static void WaitWhile(Predicate predicate, TimeSpan interval, TimeSpan timeout)
		{
			using var token = timeout == TimeSpan.Zero ?
			new CancellationTokenSource() :
			new CancellationTokenSource(timeout);
			Task.Run(() =>
			{
				while ((predicate?.Invoke()).GetValueOrDefault(false) && !token.IsCancellationRequested)
					Task.Delay(interval).Wait();
			}).Wait();
		}

		#endregion

	}

	/// <summary>
	/// Representa un metodo que se usa como bandera para saber si continuar o no con una tarea.
	/// </summary>
	/// <returns>Devuelve un valor que indica si continuar o no con la tarea.</returns>
	public delegate bool Predicate();
}