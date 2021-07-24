using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KYLib.System;

[assembly: AutoLoad]
[module: AutoLoad]
namespace KYLib.Example
{
	[AutoLoad(Async = true)]
	class AutoLoaded
	{
		
		AutoLoaded()
		{
			Console.WriteLine("AutoLoaded!");
		}

		[AutoLoad]
		void Method()
		{
			Console.WriteLine("Method AutoLoaded!");
		}

		static void StaticMethod()
		{
			Console.WriteLine("Static Method AutoLoaded!");
		}

		static AutoLoaded()
		{
			Console.WriteLine("Static AutoLoaded!");
		}
	}
}
