using KYLib.Abstractions;
using KYLib.Utils;
using System.IO;
using YamlDotNet.Serialization;

namespace KYLib.Data.DataFiles;

public class YamlFile : IDataFile
{
	/// <summary>
	/// Almacena una unica instancia de un <see cref="YamlFile"/>
	/// </summary>
	public static readonly IDataFile Default = new YamlFile();

	ISerializer serializer = new SerializerBuilder().Build();
	IDeserializer deserializer = new DeserializerBuilder().Build();

	#region IDataFile
	/// <inheritdoc/>
	public string Extension => ".yml";

	/// <inheritdoc/>
	public object? Deserialize(string source)
	{
		Ensure.NotNull(source, nameof(source));
		using var reader = new StringReader(source);
		return deserializer.Deserialize(reader);

	}

	/// <inheritdoc/>
	public T? Deserialize<T>(string source)
	{
		Ensure.NotNull(source, nameof(source));
		return deserializer.Deserialize<T>(source);
	}

	/// <inheritdoc/>
	public object? Load(string path)
	{
		Ensure.NotNull(path, nameof(path));
		using var reader = File.OpenText(path);
		return deserializer.Deserialize(reader);
	}

	/// <inheritdoc/>
	public T? Load<T>(string path)
	{
		Ensure.NotNull(path, nameof(path));
		using var reader = File.OpenText(path);
		var ret = deserializer.Deserialize<T>(reader);
		return ret;
	}

	/// <inheritdoc/>
	public void Save(object? source, string path)
	{
		Ensure.NotNull(path, nameof(path));
		if(source == null)
		{
			File.WriteAllText(path, string.Empty);
			return;
		}

		using var reader = File.CreateText(path);
		serializer.Serialize(reader, source);
	}
	
	/// <inheritdoc/>
	public string Serialize(object? source) => source == null ? string.Empty : serializer.Serialize(source);
	#endregion
}
