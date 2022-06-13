using KYLib.Abstractions;
using KYLib.Utils;
#pragma warning disable CS1712
#pragma warning disable CS1573

namespace KYLib.Data;

/// <summary>
/// Provee metodos para la lectura y escritura de datos en formatos sencillos.
/// </summary>
public static class Files
{
	/// <inheritdoc cref="IDataFile.Load"/>
	/// <typeparam name="T">Tipo de <see cref="IDataFile"/> que se usara para la conversión.</typeparam>
	public static object? Load<T>(string path) where T : IDataFile, new() =>
		Load(path, new T());

	/// <inheritdoc cref="IDataFile.Load"/>
	/// <param name="dataFile">El <see cref="IDataFile"/> que se usara para cargar los datos.</param>
	public static object? Load(string path, IDataFile dataFile)
	{
		Ensure.NotNull(dataFile, nameof(dataFile));
		return dataFile.Load(path);
	}

	/// <inheritdoc cref="Load{T}(string)"/>
	/// <typeparam name="TResult">Tipo del objeto que sera leido.</typeparam>
	public static TResult? Load<T, TResult>(string path) where T : IDataFile, new() =>
		Load<TResult>(path, new T());
	
	/// <inheritdoc cref="Load"/>
	/// <typeparam name="T">Tipo de <see cref="IDataFile"/> que se usara para la conversión.</typeparam>
	public static T? Load<T>(string path, IDataFile dataFile)
	{
		Ensure.NotNull(dataFile, nameof(dataFile));
		return dataFile.Load<T>(path);
	}

	/// <inheritdoc cref="IDataFile.Save"/>
	/// <typeparam name="T">Tipo de <see cref="IDataFile"/> que se usara para la conversión.</typeparam>
	public static void Save<T>(object? source, string path) where T : IDataFile, new() =>
		Save(source, path, new T());

	/// <inheritdoc cref="IDataFile.Save"/>
	/// <param name="dataFile">El <see cref="IDataFile"/> que se usara para cargar los datos.</param>
	public static void Save(object? source, string path, IDataFile dataFile)
	{
		Ensure.NotNull(dataFile, nameof(dataFile));
		dataFile.Save(source, path);
	}

	/// <inheritdoc cref="IDataFile.Serialize"/>
	/// <typeparam name="T">Tipo de <see cref="IDataFile"/> que se usara para la conversión.</typeparam>
	public static string Serialize<T>(object? source) where T : IDataFile, new() =>
		Serialize(source, new T());
	
	/// <inheritdoc cref="IDataFile.Serialize"/>
	/// <param name="dataFile">El <see cref="IDataFile"/> que se usara para cargar los datos.</param>
	public static string Serialize(object? source, IDataFile dataFile)
	{
		Ensure.NotNull(dataFile, nameof(dataFile));
		return dataFile.Serialize(source);
	}

	/// <inheritdoc cref="IDataFile.Deserialize"/>
	/// <typeparam name="T">Tipo de <see cref="IDataFile"/> que se usara para la conversión.</typeparam>
	public static object? Deserialize<T>(string source) where T : IDataFile, new() =>
		Deserialize(source, new T());

	/// <inheritdoc cref="IDataFile.Deserialize"/>
	/// <param name="dataFile">El <see cref="IDataFile"/> que se usara para cargar los datos.</param>
	public static object? Deserialize(string source, IDataFile dataFile)
	{
		Ensure.NotNull(dataFile, nameof(dataFile));
		return dataFile.Deserialize(source);
	}

	/// <inheritdoc cref="Deserialize{T}(string)"/>
	/// <typeparam name="TResult">Tipo del objeto que sera leido.</typeparam>
	public static TResult? Deserialize<T, TResult>(string source) where T : IDataFile, new() =>
		Deserialize<TResult>(source, new T());

	/// <inheritdoc cref="Deserialize"/>
	/// <typeparam name="T">Tipo del objeto que sera leido.</typeparam>
	public static T? Deserialize<T>(string source, IDataFile dataFile)
	{
		Ensure.NotNull(dataFile, nameof(dataFile));
		return dataFile.Deserialize<T>(source);
	}
}