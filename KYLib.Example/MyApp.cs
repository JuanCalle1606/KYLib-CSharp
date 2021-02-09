using KYLib.ConsoleUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYLib.Example
{
	public class MyApp : ConsoleApp
	{
		public MyApp() : base(nameof(MyApp))
		{
			AddItem("Saludar", Saludar);

			AfterRender = ShowTicks;
		}

		void ShowTicks() => Cons.Trace($"Ticks: {DateTime.Now.Ticks}\n");

		void Saludar() => Cons.Trace("Hola mundo!");
	}
}
