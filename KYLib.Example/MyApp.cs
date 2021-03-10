using System;
using KYLib.ConsoleUtils;
using KYLib.Extensions;
using KYLib.MathFn;
using KYLib.System;
using static KYLib.ConsoleUtils.Cons;
using Sys = System;

namespace KYLib.Example
{
	public class MyApp : ConsoleApp
	{

		public MyApp() : base(nameof(MyApp))
		{
			AddItem("Saludar", Saludar);
			AddItem("Mostar Carpetas", ShowFolders);
			AddItem("Ejecutar comando", RunCommand);
			AddItem("Información del sistema", SysInfo);
			AddMenu(new FileExample());
			AddItem("Probar numeros", TestNumber);
			AfterRender = ShowTicks;
		}

		private void TestNumber()
		{
			Int[] ints = { 10, 5, 15, 37 };
			Trace(Mathf.Mean<Int>(ints));
		}

		private void SysInfo()
		{
			var os = Environment.OSVersion;
			Cons.Trace($"Current User: {Environment.UserName}");
			Cons.Trace("Current OS Information:\n");
			Cons.Trace($"Platform: {os.Platform}");
			Cons.Trace($"Version String: {os.VersionString}");
			Cons.Trace("Version Information:");
			Cons.Trace($"   Major: {os.Version.Major}");
			Cons.Trace($"   Minor: {os.Version.Minor}");
			Cons.Trace($"Service Pack: '{os.ServicePack}'");
			Cons.Trace($"Detected OS: {Info.CurrentSystem}");
			Cons.Trace($"Is Linux?: {Info.CurrentSystem.IsUnix()}");
			Cons.Trace($"Is Windows?: {Info.CurrentSystem.IsWindows()}");
		}

		private void RunCommand()
		{
			Cons.Line = "Ingresa el comando a ejecutar";
			string comando = Cons.Line;
			Bash.RunCommand(comando);
		}

		void ShowTicks() => Cons.Trace($"Ticks: {DateTime.Now.Ticks}\n");

		void Saludar() => Cons.Trace("Hola mundo!");

		void ShowFolders()
		{
			var t = Enum.GetValues<Environment.SpecialFolder>();

			Array.ForEach(t, (F) =>
			{
				Cons.Line = $"{F}: {Environment.GetFolderPath(F, Environment.SpecialFolderOption.DoNotVerify)}";
			});
		}
	}
}
