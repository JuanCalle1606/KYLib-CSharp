using System;
using System.Diagnostics.CodeAnalysis;
using KYLib.Interfaces;
using Newtonsoft.Json;

namespace KYLib.Data.Converters
{
	/// <summary>
	/// Converter usado para serializar y deserializar los tipos INumber de la libreria.
	/// Si se definen tipos INumber personalizados se deberia heredar esta clase o crear una propia que establezca la propiedad <see cref="CanWrite"/> en <c>true</c>.
	/// </summary>
	public class NumberConverter : JsonConverter<INumber>
	{
		/// <inheritdoc/>
		public override bool CanWrite => false;

		/// <inheritdoc/>
		public override INumber ReadJson(JsonReader reader, Type objectType, [AllowNull] INumber existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			// Se obtiene el valor actual que por defecto es 0 en los tipos definidos INumber en las librerias
			INumber ins = existingValue;
			//Actualizamos el valor del numero actual al valor leido por el Serializer. En caso de ser un valor no valido esta linea producira un error que debe ser controlado.
			ins.UpdateValue(reader.Value);
			// Devolvemos la instancia con el valor dado, al manejar INumber se nos da la posibilidad de que el valor escrito sea en formato de cadena parseable o en cualquier formato numerico ya que es convertido.
			return ins;
		}

		/// <summary>
		/// Por defecto esta clase no serializa los <see cref="INumber"/> ya que la serialización por defecto de <see cref="JsonConvert"/> ya serializa como los tipos bases. Sin embargo en caso de crearse un tipo numerico que no es serializado por defecto entonces se debe heredar esta clase he implementar este metodo, si el tipo tipo numerico puede ser convertido  una cadena simple se puede usar <code>base.WriteJson([parametros])</code> en el tipo heredado.
		/// </summary>
		public override void WriteJson(JsonWriter writer, [AllowNull] INumber value, JsonSerializer serializer) =>
			writer.WriteValue(value.ToString());
	}
}