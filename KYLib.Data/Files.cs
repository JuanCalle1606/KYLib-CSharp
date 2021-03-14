using System;
using KYLib.Interfaces;


namespace KYLib.Data
{
	/// <summary>
	/// Provee metodos para la lectura y escritura de datos en formatos sencillos.
	/// </summary>
	public static class Files
	{

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
		/// <typeparam name="T"></typeparam>
		public static void Save<T>(object source, string path, T serializer) where T : IDataFile =>
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
}