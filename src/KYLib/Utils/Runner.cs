using System;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace KYLib.Utils;

/// <summary>
/// Representa un metodo que se usa como bandera para saber si continuar o no con una tarea.
/// </summary>
/// <returns>Devuelve un valor que indica si continuar o no con la tarea.</returns>
public delegate bool Predicate();

/// <summary>
/// Provee metodos para la ejecución de tareas y esperas.
/// </summary>
// ReSharper disable UnusedType.Global
public static class Runner {

	#region Utils

	static Func<Task> Wrap(Action task) => () => {
		task();
		return Task.CompletedTask;
	};

	#endregion

	#region Every

	/// <summary>
	/// Ejecuta <paramref name="task"/> cada cierto tiempo.
	/// </summary>
	/// <param name="task">Tarea a ejecutar repetidamente.</param>
	/// <param name="delay">Tiempo de cada cuanto se ejecutara la función.</param>
	/// <param name="repeats">Numero de veces que ejecutaremos <paramref name="task"/>.</param>
	public static async Task Every(Action task, TimeSpan delay, kint repeats) =>
		await Every(Wrap(task), delay, repeats);

	/// <summary>
	/// Ejecuta <paramref name="task"/> cada cierto tiempo.
	/// </summary>
	/// <param name="task">Tarea a ejecutar repetidamente.</param>
	/// <param name="delay">Tiempo de cada cuanto se ejecutara la función.</param>
	/// <param name="repeats">Numero de veces que ejecutaremos <paramref name="task"/>.</param>
	public static async Task Every(Func<Task> task, TimeSpan delay, kint repeats)
	{
		kint exes = 0;
		while (exes++ < repeats)
		{
			await task();
			await Task.Delay(delay);
		}
	}

	/// <summary>
	/// Ejecuta <paramref name="task"/> cada cierto tiempo.
	/// </summary>
	/// <param name="task">Tarea a ejecutar repetidamente.</param>
	/// <param name="delay">Tiempo de cada cuanto se ejecutara la función.</param>
	/// <param name="token">Token de cancelación para cancelar la ejecución de la tarea.</param>
	public static async Task Every(Action task, TimeSpan delay, CancellationToken token = default) =>
		await Every(Wrap(task), delay, token);

	/// <summary>
	/// Ejecuta <paramref name="task"/> cada cierto tiempo.
	/// </summary>
	/// <param name="task">Tarea a ejecutar repetidamente.</param>
	/// <param name="delay">Tiempo de cada cuanto se ejecutara la función.</param>
	/// <param name="token">Token de cancelación para cancelar la ejecución de la tarea.</param>
	/// <param name="throwOnCancel">Indica si se debe producir una excepción cuando se cancela la tarea.</param>
	/// <exception cref="TaskCanceledException">La tarea a sido cancelada.</exception>
	public static async Task Every(Func<Task> task, TimeSpan delay, CancellationToken token = default, bool throwOnCancel = false)
	{
		Ensure.NotNull(task, nameof(task));

		if (throwOnCancel)
			await EveryThrown(task, delay, token);
		else
			await EveryNoThrown(task, delay, token);
	}
	
	static async Task EveryNoThrown(Func<Task> task, TimeSpan delay, CancellationToken token)
	{
		try
		{
			while (!token.IsCancellationRequested)
			{
				await task();
				await Task.Delay(delay, token);
			}
		}
		catch (TaskCanceledException)
		{
			// ignored
		}
	}

	static async Task EveryThrown(Func<Task> task, TimeSpan delay, CancellationToken token)
	{
		while (!token.IsCancellationRequested)
		{
			await task();
			await Task.Delay(delay, token);
		}
		throw new TaskCanceledException("Se ha cancelado la tarea");
	}

	/// <summary>
	/// Ejecuta <paramref name="task"/> cada cierto tiempo.
	/// </summary>
	/// <param name="task">Tarea a ejecutar repetidamente.</param>
	/// <param name="delay">Tiempo de cada cuanto se eejcutara la función.</param>
	/// <param name="predicate">Función que se evalua para saber cuando detener la ejecución. Si evalua <c>false</c> se detiene la ejecución.</param>
	public static async Task Every(Action task, TimeSpan delay, Predicate predicate) =>
		await Every(Wrap(task), delay, predicate);

	/// <summary>
	/// Ejecuta <paramref name="task"/> cada cierto tiempo.
	/// </summary>
	/// <param name="task">Tarea a ejecutar repetidamente.</param>
	/// <param name="delay">Tiempo de cada cuanto se eejcutara la función.</param>
	/// <param name="predicate">Función que se evalua para saber cuando detener la ejecución. Si evalua <c>false</c> se detiene la ejecución.</param>
	public static async Task Every(Func<Task> task, TimeSpan delay, Predicate predicate)
	{
		Ensure.NotNull(task, nameof(task));
		Ensure.NotNull(predicate, nameof(predicate));
		while (predicate())
		{
			await task();
			await Task.Delay(delay);
		}
	}

	#endregion Every

	#region WaitWhile

	/// <summary>
	/// Define el tiempo en ms por defecto de espera entre interacciones del metodo <see cref="WaitWhile(Predicate)"/>.
	/// </summary>
	// ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
	public static kint DefaulWaitWhileInterval { get; set; } = 25;

	/// <summary>
	/// Detiene el ciclo actual y hasta que <paramref name="predicate"/> no evalue <c>false</c> no devuelve el control.
	/// </summary>
	/// <param name="predicate">Predicado que indica si se debe seguir esperando, si el predicado retorna <c>false</c> entonces se devuelve el control al llamador.</param>
	public static void WaitWhile(Predicate predicate) =>
		WaitWhile(predicate, TimeSpan.FromMilliseconds(DefaulWaitWhileInterval));

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
		Ensure.NotNull(predicate, nameof(predicate));
		var waittask = Delay(interval);
		if (timeout == TimeSpan.Zero)
		{
			while (predicate())
				waittask();
		}
		else
		{
			try
			{

				using var token = new CancellationTokenSource(timeout);
				while (predicate() && !token.IsCancellationRequested)
					Task.Delay(interval, token.Token).Wait(token.Token);
			}
			catch (OperationCanceledException) { /* ignored */ }
		}
	}

	static Action Delay(TimeSpan time)
	{
		if (time == TimeSpan.Zero)
			return () => Thread.Sleep(0);
		return ()=> Task.Delay(time).Wait();
	}

	#endregion

}
