using System;
using System.Diagnostics.CodeAnalysis;
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
		public override string ReadJson(JsonReader reader, Type objectType, [AllowNull] string existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var strb = Convert.FromBase64String(reader.ReadAsString());
			return Encoding.UTF8.GetString(strb);
		}

		/// <inheritdoc/>
		public override void WriteJson(JsonWriter writer, [AllowNull] string value, JsonSerializer serializer)
		{
			var str = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
			writer.WriteValue(str);
		}
	}
}