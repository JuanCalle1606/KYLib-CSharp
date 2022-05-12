using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace KYLib.Abstractions;

/// <summary>
/// Representa un archivo que se usa para guardar y cargar datos.
/// </summary>
public interface IDataFile
{ 
	/// <summary>
	/// Convierte un objeto en su representación de tipo de archivo.
	/// </summary>
	/// <param name="source">Objeto a serializar.</param>
	/// <returns>Devuelve el contenido con el objeto serializado.</returns>
	string Serialize(object? source);

	/// <summary>
	/// Guarda un objeto despues de serializarlo.
	/// </summary>
	/// <param name="source">Objeto a serializar.</param>
	/// <param name="path">Ruta en la que sera guardado el archivo.</param>
	void Save(object? source, string path);

	/// <summary>
	/// Crea un objeto a partir de una cadena en formato de archivo.
	/// </summary>
	/// <param name="source">Cadena que contiene los datos.</param>
	/// <returns>Un nuevo objeto con los datos deserializados.</returns>
	object? Deserialize(string source);

	/// <summary>
	/// Crea un objeto de tipo <typeparamref name="T"/> a partir de una cadena en formato de archivo.
	/// </summary>
	/// <param name="source">Cadena que contiene los datos.</param>
	/// <typeparam name="T">Cualquier tipo instanciable.</typeparam>
	/// <returns>Un nuevo objeto con los datos deserializados.</returns>
	T? Deserialize<T>(string source);

	/// <summary>
	/// Carga desde un archivo con formato un objeto nuevo.
	/// </summary>
	/// <param name="path">Ubicación desde la que se cargara el archivo. El archivo debe contener la cadena <see cref="Extension"/> al final del la ruta pero no debe estar incluida en <paramref name="path"/>.</param>
	/// <returns>Un nuevo objeto con los datos deserializados.</returns>
	object? Load(string path);

	/// <summary>
	/// Carga desde un archivo con formato un objeto nuevo.
	/// </summary>
	/// <param name="path">Ubicación desde la que se cargara el archivo. El archivo debe contener la cadena <see cref="Extension"/> al final del la ruta pero no debe estar incluida en <paramref name="path"/>.</param>
	/// <typeparam name="T">Cualquier tipo instanciable.</typeparam>
	/// <returns>Un nuevo objeto con los datos deserializados.</returns>
	T? Load<T>(string path);

	/// <summary>
	/// Obtiene la extensión que se usa para cargar y guardar el archivo.
	/// </summary>
	string Extension { get; }
}