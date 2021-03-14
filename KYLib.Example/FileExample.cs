using System;
using KYLib.ConsoleUtils;
using KYLib.Data;
using KYLib.Data.Converters;
using KYLib.Data.DataFiles;
using Newtonsoft.Json;

namespace KYLib.Example
{
	public class FileExample : ConsoleMenu
	{
		[JsonProperty]
		ExampleObject exampleObject = new()
		{
			Name = "Juan",
			Age = 18,
			Length = 1.73f,
			IsMale = true
		};
		//{"Name": "Juan","Age": "18","Length": 1.73,"IsMale": true}
		public FileExample() : base(true)
		{
			Title = "Manejo de archivos";
			AddItem("Escribir Json", WriteJson);
			AddItem("Leer Json", ReadJson);
		}

		private void ReadJson()
		{
			Cons.Line = "Ingresa la cadena a deserializar";
			ExampleObject obj = Files.Deserialize<ExampleObject>(Cons.Line, new JsonFile());
			Cons.Line = $"Name: {obj.Name}";
			Cons.Line = $"Age: {obj.Age}";
			Cons.Line = $"Length: {obj.Length}";
			Cons.Line = $"IsMale: {obj.IsMale}";
		}

		void WriteJson()
		{
			Cons.Line = "Objeto serializado como json:";
			Cons.Line = Files.Serialize<JsonFile>(exampleObject);
		}

		void SaveJson()
		{
		}
	}
}