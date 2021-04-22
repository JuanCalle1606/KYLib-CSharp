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
		/// 
		/// </summary>
		/// <param name="task"></param>
		/// <param name="delay"></param>
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
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		public static void WaitWhile(Func<bool> predicate) =>
			WaitWhile(predicate, TimeSpan.FromMilliseconds(25));

		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="interval"></param>
		public static void WaitWhile(Func<bool> predicate, TimeSpan interval) =>
			WaitWhile(predicate, interval, TimeSpan.Zero);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="interval"></param>
		/// <param name="timeout"></param>
		public static void WaitWhile(Func<bool> predicate, TimeSpan interval, TimeSpan timeout)
		{
			using (var token = timeout == TimeSpan.Zero ?
			new CancellationTokenSource() :
			new CancellationTokenSource(timeout))
			{
				var task = Task.Run(() =>
				{
					while ((predicate?.Invoke()).GetValueOrDefault(false) && !token.IsCancellationRequested)
						Task.Delay(interval).Wait();
				});

				task.Wait();
			}
		}

		#endregion

	}
}