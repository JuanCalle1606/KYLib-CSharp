using System.Dynamic;
namespace KYLib.Utils;

/// <summary>
/// Encapsula un objeto para proveer metodos privados y otras utilidades.
/// </summary>
public class ObjectWrapper : DynamicObject, IDynamicMetaObjectProvider
{
	/// <summary>
	/// Obtiene el objeto almacenado.
	/// </summary>
	public object Value { get; }

	/// <summary>
	/// Crea un nuevo wrapper a un objeto dado.
	/// </summary>
	/// <param name="object">objeto a almacenar.</param>
	public ObjectWrapper(object @object)
	{
		Ensure.NotNull(@object, nameof(@object));
		Value = @object;
	}

	/// <summary>
	/// Convierte <see cref="Value"/> en un tipo especifico.
	/// </summary>
	/// <typeparam name="T">Tipo al cual se va a convertir.</typeparam>
	/// <returns>Objeto convertido o <c>null</c> si no se puede convertir.</returns>
	// ReSharper disable once ReturnTypeCanBeNotNullable
	public T? As<T>() => (T)Value;

	/// <summary>
	/// Indica si <see cref="Value"/> es de un tipo.
	/// </summary>
	/// <typeparam name="T">El tipo a validar.</typeparam>
	/// <returns><c>true</c> si <see cref="Value"/> es una instancia de <typeparamref name="T"/>, de lo contrario <c>false</c>.</returns>
	public bool Is<T>() => Value is T;

	/// <summary>
	/// Indica si <see cref="Value"/> es de un tipo.
	/// </summary>
	/// <param name="type">El tipo a validar.</param>
	/// <returns><c>true</c> si <see cref="Value"/> es una instancia de <paramref name="type"/>, de lo contrario <c>false</c>.</returns>
	public bool Is(Type type) => type.IsInstanceOfType(Value);

}
