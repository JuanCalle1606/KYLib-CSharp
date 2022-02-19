using KYLib.Interfaces;


namespace KYLib.Data;

/// <summary>
/// Provee metodos para la lectura y escritura de datos en formatos sencillos.
/// </summary>
public static class Files
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="path"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static object Load<T>(string path) where T : IDataFile, new() =>
		Load(path, new T());

	/// <summary>
	/// 
	/// </summary>
	/// <param name="path"></param>
	/// <param name="serializer"></param>
	/// <returns></returns>
	public static object Load(string path, IDataFile serializer) =>
		serializer.Load(path);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="path"></param>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <returns></returns>
	public static TResult Load<T, TResult>(string path) where T : IDataFile, new() =>
		Load<TResult>(path, new T());

	/// <summary>
	/// 
	/// </summary>
	/// <param name="path"></param>
	/// <param name="deserializer"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T Load<T>(string path, IDataFile deserializer) =>
		deserializer.Load<T>(path);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <param name="path"></param>
	/// <typeparam name="T"></typeparam>
	public static void Save<T>(object source, string path) where T : IDataFile, new() =>
		Save(source, path, new T());

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <param name="path"></param>
	/// <param name="serializer"></param>
	public static void Save(object source, string path, IDataFile serializer) =>
		serializer.Save(source, path);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static string Serialize<T>(object source) where T : IDataFile, new() =>
		Serialize(source, new T());

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <param name="serializer"></param>
	/// <returns></returns>
	public static string Serialize(object source, IDataFile serializer) =>
		serializer.Serialize(source);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static object Deserialize<T>(string source) where T : IDataFile, new() =>
		new T().Deserialize(source);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <param name="deserializer"></param>
	/// <returns></returns>
	public static object Deserialize(string source, IDataFile deserializer) =>
		deserializer.Deserialize(source);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <returns></returns>
	public static TResult Deserialize<T, TResult>(string source) where T : IDataFile, new() =>
		new T().Deserialize<TResult>(source);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <param name="deserializer"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T Deserialize<T>(string source, IDataFile deserializer) =>
		deserializer.Deserialize<T>(source);
}