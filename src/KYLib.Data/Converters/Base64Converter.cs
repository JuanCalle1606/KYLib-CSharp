using System;
using System.Text;
using Newtonsoft.Json;

namespace KYLib.Data.Converters
{
	/// <summary>
	/// Convierte cadenas de y a Base64.
	/// </summary>
	public class Base64Converter : JsonConverter<string>
	{
		/// <inheritdoc/>
		public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String)
				throw new NotSupportedException("Base64Converter solo funciona con tipos string.");
			var input = reader.Value as string;

			try
			{
				var strb = Convert.FromBase64String(input);
				return Encoding.UTF8.GetString(strb);
			}
			catch (Exception)
			{
				throw new ArgumentException("La cadena proporcionada no esta en formato base64");
			}

		}

		/// <inheritdoc/>
		public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
		{
			var str = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
			writer.WriteValue(str);
		}
	}
}