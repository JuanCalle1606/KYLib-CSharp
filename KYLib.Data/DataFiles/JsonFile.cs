using System;
using System.IO;
using KYLib.Data.Converters;
using KYLib.Interfaces;
using Newtonsoft.Json;

namespace KYLib.Data.DataFiles
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class JsonFile : IDataFile
	{

		/// <summary>
		/// Configuraciones que usa este formato de archivo
		/// </summary>
		private JsonSerializerSettings m_Settings = new JsonSerializerSettings();

		/// <summary>
		/// Crea un nuevo <see cref="JsonFile"/> para la serialización y deserialización en formato Json.
		/// </summary>
		public JsonFile()
		{
			DefaultConverter = new NumberConverter();
			m_Settings.Converters.Add(DefaultConverter);
			m_Settings.Formatting = Formatting.Indented;
			m_Settings.NullValueHandling = NullValueHandling.Ignore;
		}

		#region IDataFile

		/// <inheritdoc/>
		public JsonConverter DefaultConverter { get; private set; }

		/// <inheritdoc/>
		public string Extension => ".json";

		/// <inheritdoc/>
		public object Deserialize(string source) =>
			JsonConvert.DeserializeObject(source, m_Settings);

		/// <inheritdoc/>
		public T Deserialize<T>(string source) =>
			JsonConvert.DeserializeObject<T>(source, m_Settings);

		/// <inheritdoc/>
		public object Load(string path)
		{
			string realpath = ValidatePath(path);
			string content = File.ReadAllText(realpath);
			return JsonConvert.DeserializeObject(content, m_Settings);
		}

		/// <inheritdoc/>
		public T Load<T>(string path)
		{
			string realpath = ValidatePath(path);
			string content = File.ReadAllText(realpath);
			return JsonConvert.DeserializeObject<T>(content, m_Settings);
		}

		/// <summary>
		/// Usamos esta función para ver si existe el archivo solicitado.
		/// </summary>
		private string ValidatePath(string path)
		{
			string realpath = null;
			//primero vemos si el archivo especificado existe, esto se hace por si el nombre pasado no tiene la extension del archivo
			if (File.Exists(path))
				realpath = path;
			// ahora vemos si existe el archivo pero con extension especifica
			else if (File.Exists(path + Extension))
				realpath = path + Extension;
			//si no existe se genera error
			else
				throw new FileNotFoundException("El archivo especificado no existe por lo que no puede ser cargado", path);
			return realpath;
		}

		/// <inheritdoc/>
		public void Save(object source, string path)
		{
			using (StreamWriter file = File.CreateText(path))
			{
				JsonSerializer serializer = JsonSerializer.CreateDefault(m_Settings);
				serializer.Serialize(file, source);
			}
		}

		/// <inheritdoc/>
		public string Serialize(object source) =>
			JsonConvert.SerializeObject(source, m_Settings);

		#endregion
	}
}