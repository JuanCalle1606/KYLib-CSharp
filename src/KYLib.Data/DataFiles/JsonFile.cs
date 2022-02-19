using System.IO;
using KYLib.Data.Converters;
using KYLib.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KYLib.Data.DataFiles;

/// <summary>
/// 
/// </summary>
public sealed class JsonFile : IDataFile
{
	/// <summary>
	/// Almacena una unica instancia de un <see cref="JsonFile"/>
	/// </summary>
	public static readonly JsonFile Default = new();

	/// <inheritdoc/>
	public JsonSerializerSettings Settings { get; set; } = new JsonSerializerSettings();

	/// <summary>
	/// Crea un nuevo <see cref="JsonFile"/> para la serialización y deserialización en formato Json.
	/// </summary>
	public JsonFile()
	{
		Settings.Converters.Add(new NumberConverter());
		Settings.Converters.Add(new StringEnumConverter());
		Settings.Formatting = Formatting.Indented;
		Settings.NullValueHandling = NullValueHandling.Ignore;
	}

	#region IDataFile

	/// <inheritdoc/>
	public string Extension => ".json";

	/// <inheritdoc/>
	public object Deserialize(string source) =>
		JsonConvert.DeserializeObject(source, Settings);

	/// <inheritdoc/>
	public T Deserialize<T>(string source) =>
		JsonConvert.DeserializeObject<T>(source, Settings);

	/// <inheritdoc/>
	public object Load(string path)
	{
		var realpath = ValidatePath(path);
		var content = File.ReadAllText(realpath);
		return JsonConvert.DeserializeObject(content, Settings);
	}

	/// <inheritdoc/>
	public T Load<T>(string path)
	{
		var realpath = ValidatePath(path);
		var content = File.ReadAllText(realpath);
		return JsonConvert.DeserializeObject<T>(content, Settings);
	}

	/// <summary>
	/// Usamos esta función para ver si existe el archivo solicitado.
	/// </summary>
	string ValidatePath(string path)
	{
		string realpath;
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
		using var file = File.CreateText(path);
		var serializer = JsonSerializer.CreateDefault(Settings);
		serializer.Serialize(file, source);
	}

	/// <inheritdoc/>
	public string Serialize(object source) =>
		JsonConvert.SerializeObject(source, Settings);

	#endregion
}