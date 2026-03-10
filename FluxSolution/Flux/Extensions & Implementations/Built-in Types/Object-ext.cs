namespace Flux
{
  public static class ObjectExtensions
  {
    #region TypeConverter (helper variable)

    private static readonly System.Reflection.MethodInfo m_typeConverterOfT = typeof(Fx).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Single(mi => mi.IsGenericMethod && mi.Name.Equals(nameof(TypeConverter), System.StringComparison.Ordinal) && mi.GetParameters().Length == 2);

    #endregion

    extension(object)
    {
      #region FitSmallestIntegerType (potentially useful for delegates... private for now)

      //private static object FitSmallestIntegerType(object value)
      //{
      //  var bi = value switch
      //  {
      //    System.Numerics.BigInteger sbi => sbi,
      //    System.IConvertible sc => new System.Numerics.BigInteger(System.Convert.ToDecimal(sc)),
      //    _ => throw new System.ArgumentException("Unsupported numeric type")
      //  };

      //  return System.Numerics.BigInteger.FitSmallestIntegerType(bi);
      //}

      #endregion

      #region SerializeToXDocument

      /// <summary>
      /// <para>Creates a new <see cref="System.Xml.Linq.XDocument"/> with the specified object serialized into it.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public static System.Xml.Linq.XDocument SerializeToXDocument(object source)
      {
        var xd = new System.Xml.Linq.XDocument();

        var xs = new System.Xml.Serialization.XmlSerializer(source.GetType());

        using (var xw = xd.CreateWriter())
          xs.Serialize(xw, source);

        return xd;
      }

      #endregion

      #region SerializeToXmlDocument

      /// <summary>
      /// <para>Creates a new <see cref="System.Xml.XmlDocument"/> with the specified object serialized into it.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public static System.Xml.XmlDocument SerializeToXmlDocument(object source)
      {
        var xd = new System.Xml.XmlDocument();

        var xs = new System.Xml.Serialization.XmlSerializer(source.GetType());

        if (xd.CreateNavigator() is System.Xml.XPath.XPathNavigator xpn)
          using (var xw = xpn.AppendChild())
            xs.Serialize(xw, source);

        return xd;
      }

      #endregion

      #region ..TypeConverter

      /// <summary>Complement the built-in TypeConverter system.</summary>
      /// <see href="https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.typeconverter"/>
      /// <remarks>This is the method used in the static property <see cref="m_typeConverterOfT"/>.</remarks>
      public static T? TypeConverter<T>(object source, System.Globalization.CultureInfo? culture = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var exceptions = new System.Collections.Generic.List<System.Exception>();

        try
        {
          if (System.ComponentModel.TypeDescriptor.GetConverter(source) is var valueConverter && valueConverter is not null && valueConverter.CanConvertTo(typeof(T)))
            return (T?)valueConverter.ConvertTo(null, culture, source, typeof(T));
        }
        catch (System.Exception ex) { exceptions.Add(ex); }

        try
        {
          if (System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)) is var typeConverter && typeConverter is not null && typeConverter.CanConvertFrom(source.GetType()))
            return (T?)typeConverter.ConvertFrom(null, culture, source);
        }
        catch (System.Exception ex) { exceptions.Add(ex); }

        if (exceptions.Count > 0) throw new System.AggregateException(exceptions);

        throw new System.NotSupportedException(@"No type converter found.");
      }

      /// <summary>Complement the built-in TypeConverter system using the Try paradigm.</summary>
      public static bool TryTypeConverter<T>(object source, out T? result, System.Globalization.CultureInfo? culture = null)
      {
        try
        {
          result = TypeConverter<T>(source, culture);
          return true;
        }
        catch { }

        result = default!;
        return false;
      }

      /// <summary>Complement the built-in TypeConverter system.</summary>
      public static object TypeConverter(object source, System.Globalization.CultureInfo? culture, params System.Type[] conversionSequence)
      {
        var parameters = new object[] { source ?? throw new System.ArgumentNullException(nameof(source)), culture! };

        var result = default(object);

        foreach (var type in conversionSequence)
        {
          result = m_typeConverterOfT.MakeGenericMethod(type).Invoke(null, parameters);

          parameters[0] = result!;
        }

        return result!;
      }

      /// <summary>Complement the built-in TypeConverterfunctionality with a sequential conversion chain using the Try paradigm.</summary>
      public static bool TryTypeConverter(object source, out object result, System.Globalization.CultureInfo? culture, params System.Type[] conversionSequence)
      {
        try
        {
          result = TypeConverter(source, culture, conversionSequence);
          return true;
        }
        catch { }

        result = default!;
        return false;
      }

      #endregion
    }

    extension(object source)
    {
      #region SerializeToJson

      /// <summary>
      /// <para>Returns a JSON string that represents the current object.</para>
      /// </summary>
      /// <returns></returns>
      public string SerializeToJson()
        => System.Text.Json.JsonSerializer.Serialize(source);

      #endregion

      #region SerializeToXml

      /// <summary>
      /// <para>Returns an XML string that represents the current object.</para>
      /// </summary>
      /// <returns></returns>
      public string SerializeToXml()
        => SerializeToXDocument(source).ToString();

      //public string SerializeToXml()
      //{
      //  var xs = new System.Xml.Serialization.XmlSerializer(source.GetType());
      //  using var sw = new System.IO.StringWriter();
      //  xs.Serialize(sw, source);
      //  return sw.ToString();
      //}

      //public string SerializeToXml(System.Xml.XmlWriterSettings? xmlWriterSettings = null)
      //{
      //  xmlWriterSettings ??= new System.Xml.XmlWriterSettings();

      //  using var sw = new System.IO.StringWriter();
      //  using var xw = System.Xml.XmlWriter.Create(sw, xmlWriterSettings);
      //  //var xsn = new System.Xml.Serialization.XmlSerializerNamespaces();
      //  //xsn.Add(string.Empty, string.Empty);
      //  var xs = new System.Xml.Serialization.XmlSerializer(source.GetType());
      //  //xs.Serialize(xw, source, xsn);
      //  xs.Serialize(xw, source);
      //  return sw.ToString();
      //}

      #endregion
    }
  }
}
