namespace Flux
{
	public static class Serialize
	{
		/// <summary>Use the JSON serializer to deep clone the source of type TSource to a TTarget.</summary>
		/// <remarks>In contrast to the XML serializer, the JSON serializer does not expose the type name in its structure, so no need to rename anything.</remarks>
		public static TTarget? CloneJson<TTarget>(object source)
			=> FromJson<TTarget>(ToJson(source));
		/// <summary>Use the XML serializer to deep clone the source of type TSource to a TTarget.</summary>
		/// <remarks>Because the XML serializer uses the type name for the root element name, it needs to be renamed.</remarks>
		public static TTarget? CloneXml<TTarget>(object source)
		{
			var xe = System.Xml.Linq.XElement.Parse(ToXml(source));
			xe.Name = typeof(TTarget).Name;
			return FromXml<TTarget>(xe.ToString(System.Xml.Linq.SaveOptions.DisableFormatting));
		}

		/// <summary>Serialize the source from a JSON string to T, using the specified options.</summary>
		public static TTarget? FromJson<TTarget>(string source, System.Text.Json.JsonSerializerOptions? options = null)
			=> System.Text.Json.JsonSerializer.Deserialize<TTarget>(source, options);
		/// <summary>Serialize the source from a JSON string to T.</summary>
		public static TTarget? FromJson<TTarget>(string source)
			=> System.Text.Json.JsonSerializer.Deserialize<TTarget>(source);
		/// <summary>Serialize the source from an XML string to T.</summary>
		public static TTarget? FromXml<TTarget>(string source)
		{
			using var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(source));
			using var xr = System.Xml.XmlReader.Create(ms);
			return (TTarget)new System.Xml.Serialization.XmlSerializer(typeof(TTarget)).Deserialize(xr);
		}

		/// <summary>Serialize the source to a JSON string, using the specified options.</summary>
		public static string ToJson(object source, System.Text.Json.JsonSerializerOptions? options = null)
			=> System.Text.Json.JsonSerializer.Serialize(source, options);
		/// <summary>Serialize the source to a JSON string.</summary>
		public static string ToJson(object source)
			=> System.Text.Json.JsonSerializer.Serialize(source);
		/// <summary>Serialize the source to an XML string.</summary>
		public static string ToXml(object source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			using var sw = new System.IO.StringWriter();
			using var xw = System.Xml.XmlWriter.Create(sw, new System.Xml.XmlWriterSettings() { OmitXmlDeclaration = true });
			var xsn = new System.Xml.Serialization.XmlSerializerNamespaces();
			xsn.Add(string.Empty, string.Empty);
			new System.Xml.Serialization.XmlSerializer(source.GetType()).Serialize(xw, source, xsn);
			return sw.ToString();
		}

		/// <summary>Use the JSON serializer to deep clone the source of type TSource to a TTarget using the Try paradigm.</summary>
		public static bool TryCloneJson<TTarget>(object source, out TTarget? result)
		{
			try
			{
				result = CloneJson<TTarget>(source);
				return true;
			}
#pragma warning disable CA1031 // Do not catch general exception types.
			catch
#pragma warning restore CA1031 // Do not catch general exception types.
			{ }

			result = default!;
			return false;
		}
		/// <summary>Use the XML serializer to deep clone the source of type TSource to a TTarget using the Try paradigm.</summary>
		public static bool TryCloneXml<TTarget>(object source, out TTarget? result)
		{
			try
			{
				result = CloneXml<TTarget>(source);
				return true;
			}
#pragma warning disable CA1031 // Do not catch general exception types.
			catch
#pragma warning restore CA1031 // Do not catch general exception types.
			{ }

			result = default!;
			return false;
		}

		/// <summary>Tries to serialize the source from a JSON string to T.</summary>
		public static bool TryFromJson<TTarget>(string source, out TTarget? result)
		{
			try
			{
				result = FromJson<TTarget>(source);
				return true;
			}
#pragma warning disable CA1031 // Do not catch general exception types.
			catch
#pragma warning restore CA1031 // Do not catch general exception types.
			{ }

			result = default!;
			return false;
		}
		/// <summary>Tries to serialize the source from an XML string to T.</summary>
		public static bool TryFromXml<TTarget>(string source, out TTarget? result)
		{
			try
			{
				result = FromXml<TTarget>(source);
				return true;
			}
#pragma warning disable CA1031 // Do not catch general exception types.
			catch
#pragma warning restore CA1031 // Do not catch general exception types.
			{ }

			result = default!;
			return false;
		}

		/// <summary>Tries to serialize the source to a JSON string.</summary>
		public static bool TryToJson(object source, out string result)
		{
			try
			{
				result = ToJson(source);
				return true;
			}
#pragma warning disable CA1031 // Do not catch general exception types.
			catch
#pragma warning restore CA1031 // Do not catch general exception types.
			{ }

			result = default!;
			return false;
		}
		/// <summary>Tries to serialize the source to an XML string.</summary>
		public static bool TryToXml(object source, out string result)
		{
			try
			{
				result = ToXml(source);
				return true;
			}
#pragma warning disable CA1031 // Do not catch general exception types.
			catch
#pragma warning restore CA1031 // Do not catch general exception types.
			{ }

			result = default!;
			return false;
		}
	}
}
