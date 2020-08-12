namespace Flux
{
  public static class Serialization
  {
    ///// <summary>Constructs a string representing a JSON array from IEnumerable.</summary>
    //public static string JsonArray(System.Collections.IEnumerable source)
    //{
    //  var sb = new System.Text.StringBuilder(@"[");

    //  foreach (var t in source)
    //  {
    //    sb.Append(JsonValue(t));
    //    sb.Append(',');
    //  }

    //  sb[sb.Length - 1] = ']';

    //  return sb.ToString();
    //}
    ///// <summary>Constructs a new string with any characters not in the 32-127 range (as well as " and \) escaped according to JSON.</summary>
    //public static string JsonEscape(object source)
    //{
    //  var sb = new System.Text.StringBuilder();

    //  foreach (var c in source?.ToString() ?? string.Empty)
    //  {
    //    switch (c)
    //    {
    //      case '\"':
    //        sb.Append(@"\""");
    //        break;
    //      case '\\':
    //        sb.Append(@"\\");
    //        break;
    //      case '/':
    //        sb.Append(@"\/");
    //        break;
    //      case '\b':
    //        sb.Append(@"\b");
    //        break;
    //      case '\f':
    //        sb.Append(@"\f");
    //        break;
    //      case '\n':
    //        sb.Append(@"\n");
    //        break;
    //      case '\r':
    //        sb.Append(@"\r");
    //        break;
    //      case '\t':
    //        sb.Append(@"\t");
    //        break;
    //      case char u when u < 32 || u > 127:
    //        sb.Append(u.ToUnicodeEscapeSequence());
    //        break;
    //      default:
    //        sb.Append(c);
    //        break;
    //    }
    //  }

    //  return sb.ToString();
    //}
    ///// <summary>Constructs a string representing a JSON object from IDictionary.</summary>
    //public static string JsonObject(System.Collections.IDictionary source)
    //{
    //  var sb = new System.Text.StringBuilder(@"{");

    //  foreach (var key in source.Keys)
    //  {
    //    sb.Append(JsonValue(key.ToString()));
    //    sb.Append(':');
    //    sb.Append(JsonValue(source[key]));
    //    sb.Append(',');
    //  }

    //  sb[sb.Length - 1] = '}';

    //  return sb.ToString();
    //}
    ///// <summary>Constructs a string representing a JSON value.</summary>
    //public static string JsonValue(object source)
    //{
    //  switch (source)
    //  {
    //    case null:
    //    case System.DBNull dbn:
    //      return @"null";
    //    case bool b:
    //      return b.ToString().ToLower();
    //    case char c:
    //      return JsonEscape(c.ToString()).Wrap('"', '"');
    //    case string s:
    //      return JsonEscape(s).Wrap('"', '"');
    //    case object n when n.GetType().IsPrimitive && !(n is System.IntPtr) && !(n is System.UIntPtr):
    //      return n.ToString();
    //    case System.Collections.DictionaryEntry de:
    //      return $"{JsonValue(de.Key)}:{JsonValue(de.Value)}";
    //    case System.Collections.IDictionary id:
    //      return JsonObject(id);
    //    case System.Collections.IEnumerable ie:
    //      return JsonArray(ie);
    //    default:
    //      return source.ToString();
    //  }
    //}

    /// <summary>Use the binary serializer to deep clone the source of type T.</summary>
    public static TTarget CloneBinary<TTarget>(object source) => FromBinary<TTarget>(ToBinary(source));
    /// <summary>Use the JSON serializer to deep clone the source of type TSource to a TTarget.</summary>
    /// <remarks>In contrast to the XML serializer, the JSON serializer does not expose the type name in its structure, so no need to rename anything.</remarks>
    public static TTarget CloneJson<TTarget>(object source) => FromJson<TTarget>(ToJson(source));
    /// <summary>Use the XML serializer to deep clone the source of type TSource to a TTarget.</summary>
    /// <remarks>Because the XML serializer uses the type name for the root element name, it needs to be renamed.</remarks>
    public static TTarget CloneXml<TTarget>(object source)
    {
      var xe = System.Xml.Linq.XElement.Parse(ToXml(source));
      xe.Name = typeof(TTarget).Name;
      return FromXml<TTarget>(xe.ToString(System.Xml.Linq.SaveOptions.DisableFormatting));
    }

    /// <summary>Deserialize the bytes to T.</summary>
    public static TTarget FromBinary<TTarget>(byte[] source)
    {
      using var ms = new System.IO.MemoryStream(source);

      return (TTarget)(new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Deserialize(ms));
    }
    /// <summary>Serialize the source from a JSON string to T.</summary>
#if NETCOREAPP3_1 || NETCOREAPP || NETSTANDARD2_1
    public static TTarget FromJson<TTarget>(string source) => System.Text.Json.JsonSerializer.Deserialize<TTarget>(source);
#else
    [System.Obsolete(@"This method is obsolete in favor of the NetFX optimized version.")]
    public static TTarget FromJson<TTarget>(string source)
    {
      //return Newtonsoft.Json.JsonConvert.DeserializeObject<TTarget>(source);
      using var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(source));
      return (TTarget)new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(TTarget)).ReadObject(ms);
    }
#endif
    /// <summary>Serialize the source from an XML string to T.</summary>
    public static TTarget FromXml<TTarget>(string source)
    {
      using var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(source));
      using var xr = System.Xml.XmlReader.Create(ms);
      return (TTarget)new System.Xml.Serialization.XmlSerializer(typeof(TTarget)).Deserialize(xr);
    }

    /// <summary>Serialize the source to bytes.</summary>
    public static byte[] ToBinary(object source)
    {
      using var ms = new System.IO.MemoryStream();
      new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize(ms, source);
      return ms.ToArray();
    }
    /// <summary>Serialize the source to a JSON string.</summary>
    public static string ToJson(object source) => System.Text.Json.JsonSerializer.Serialize(source);
    /// <summary>Serialize the source to an XML string.</summary>
    public static string ToXml(object source)
    {
      using var sw = new System.IO.StringWriter();
      using var xw = System.Xml.XmlWriter.Create(sw, new System.Xml.XmlWriterSettings() { OmitXmlDeclaration = true });
      var xsn = new System.Xml.Serialization.XmlSerializerNamespaces();
      xsn.Add(string.Empty, string.Empty);
      new System.Xml.Serialization.XmlSerializer(source.GetType()).Serialize(xw, source, xsn);
      return sw.ToString();
    }

    /// <summary>Use the binary serializer to deep clone the source of type T using the Try paradigm.</summary>
    public static bool TryCloneBinary<TTarget>(object source, out TTarget result)
    {
      try
      {
        result = CloneBinary<TTarget>(source);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      { }

      result = default!;
      return false;
    }
    /// <summary>Use the JSON serializer to deep clone the source of type TSource to a TTarget using the Try paradigm.</summary>
    public static bool TryCloneJson<TTarget>(object source, out TTarget result)
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
    public static bool TryCloneXml<TTarget>(object source, out TTarget result)
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

    /// <summary>Deserialize the bytes to T using the Try paradigm.</summary>
    public static bool TryFromBinary<TTarget>(byte[] source, out TTarget result)
    {
      try
      {
        result = (TTarget)FromBinary<TTarget>(source);
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
    public static bool TryFromJson<TTarget>(string source, out TTarget result)
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
    public static bool TryFromXml<TTarget>(string source, out TTarget result)
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

    /// <summary>Serialize the source to bytes using the Try paradigm.</summary>
    public static bool TryToBinary(object source, out byte[] result)
    {
      try
      {
        result = ToBinary(source);
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
