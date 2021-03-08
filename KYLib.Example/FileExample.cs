using KYLib.ConsoleUtils;
using Newtonsoft.Json;
using KYLib.Data;
using KYLib.Data.DataFiles;
using System;

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

		public FileExample() : base(true)
		{
			Title = "Manejo de archivos";
			AddItem("Escribir .json", WriteJson);
			AddItem("Guardar .json", SaveJson);
		}

		void WriteJson()
		{
			Cons.Line = "Objeto serializado como json:";
			Cons.Line = Files.Serialize<JsonFile>(this);
		}

		void SaveJson()
		{
			string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/kya/KYLib.example/json";

			Cons.Line =
			$"Saving in {path}.json";
			Cons.Line = Environment.UserName;
			Cons.Line = Environment.CurrentDirectory;
			Cons.Line = Environment.CommandLine;

			//Files.Save<JsonFile>(exampleObject, path);
		}
	}
}