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
		/// 
		/// </summary>
		/// <param name="task"></param>
		/// <param name="delay"></param>
		/// <param name="repeats"></param>
		public static async void Every(Action task, TimeSpan delay, int repeats)
		{
			await Task.Run(async () =>
			{
				int exes = 0;
				while (true && exes++ < repeats)
				{
					task?.Invoke();
					await Task.Delay(delay);
				}
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="task"></param>
		/// <param name="delay"></param>
		/// <param name="token"></param>
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
		/// <param name="task"></param>
		/// <param name="delay">Función a ejecutar</param>
		/// <param name="predicate"></param>
		public static async void Every(Action task, TimeSpan delay, Func<bool> predicate)
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
		public static void WaitWhile(Func<bool> predicate) =>
			WaitWhile(predicate, TimeSpan.FromMilliseconds(25));

		/// <summary>
		/// Detiene el ciclo actual y hasta que <paramref name="predicate"/> no evalue <c>false</c> no devuelve el control.
		/// </summary>
		/// <param name="predicate">Predicado que indica si se debe seguir esperando, si el predicado retorna <c>false</c> entonces se devuelve el control al llamador.</param>
		/// <param name="interval">Intervalo de tiempo que indica cada cuando hay que evaluar <paramref name="predicate"/>.</param>
		public static void WaitWhile(Func<bool> predicate, TimeSpan interval) =>
			WaitWhile(predicate, interval, TimeSpan.Zero);

		/// <summary>
		/// Detiene el ciclo actual y hasta que <paramref name="predicate"/> no evalue <c>false</c> no devuelve el control.
		/// </summary>
		/// <param name="predicate">Predicado que indica si se debe seguir esperando, si el predicado retorna <c>false</c> entonces se devuelve el control al llamador.</param>
		/// <param name="interval">Intervalo de tiempo que indica cada cuando hay que evaluar <paramref name="predicate"/>.</param>
		/// <param name="timeout">Maximo tiempo de espera, cuando se supera este tiempo entonces se devolvera el control. Si este valor es <see cref="TimeSpan.Zero"/> entonces se esperara indefinidamente.</param>
		public static void WaitWhile(Func<bool> predicate, TimeSpan interval, TimeSpan timeout)
		{
			using (var token = timeout == TimeSpan.Zero ?
			new CancellationTokenSource() :
			new CancellationTokenSource(timeout))
			{
				Task.Run(() =>
				{
					while ((predicate?.Invoke()).GetValueOrDefault(false) && !token.IsCancellationRequested)
						Task.Delay(interval).Wait();
				}).Wait();
			}
		}

		#endregion

	}
}