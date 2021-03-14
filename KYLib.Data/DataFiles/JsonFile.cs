using System;
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
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public T Load<T>(string path)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public void Save(object source, string path)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public string Serialize(object source) =>
			JsonConvert.SerializeObject(source, m_Settings);

		#endregion
	}
}