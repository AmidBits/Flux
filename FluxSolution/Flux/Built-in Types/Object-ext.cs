namespace Flux
{
  public static class ObjectExtensions
  {
    #region TypeConverter (helper variable)

    private static readonly System.Reflection.MethodInfo m_typeConverterOfT = typeof(Fx).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Single(mi => mi.IsGenericMethod && mi.Name.Equals(nameof(TypeConverter), System.StringComparison.Ordinal) && mi.GetParameters().Length == 2);

    #endregion

    extension<T>(T source)
      where T : notnull
    {
      #region GetFieldInfos

      /// <summary>Enumerates all FieldInfo matching the specified binding flags. If the source is a System.Type, the fields are enumerated from the type, otherwise the instance is used.</summary>
      /// <param name="source">An instance object or a System.Type to enumerate fields on.</param>
      /// <param name="bindingFlags"></param>
      public System.Reflection.FieldInfo[] GetFieldInfos(System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
        => source is null
        ? throw new System.ArgumentNullException(nameof(source))
        : source is System.Type type
        ? type.GetFields(bindingFlags | System.Reflection.BindingFlags.Static)
        : source.GetType().GetFields(bindingFlags | System.Reflection.BindingFlags.Instance);

      #endregion

      #region GetPropertyInfos

      /// <summary>Enumerates all PropertyInfo matching the specified binding flags. If the source is a System.Type, the properties are enumerated from the type, otherwise the instance is used.</summary>
      /// <param name="source">An instance object or a System.Type to enumerate properties on.</param>
      /// <param name="bindingFlags"></param>
      public System.Reflection.PropertyInfo[] GetPropertyInfos(System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public)
        => source is null
        ? throw new System.ArgumentNullException(nameof(source))
        : source is System.Type type
        ? type.GetProperties(bindingFlags | System.Reflection.BindingFlags.Static) // Static property containers.
        : source.GetType().GetProperties(bindingFlags | System.Reflection.BindingFlags.Instance); // Instance property containers.

      #endregion

      #region Serialize..

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <returns></returns>
      public string SerializeToJson()
        => System.Text.Json.JsonSerializer.Serialize(source);

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public System.Xml.Linq.XDocument SerializeToXDocument()
      {
        var xd = new System.Xml.Linq.XDocument();

        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

        using (var writer = xd.CreateWriter())
          serializer.Serialize(writer, source);

        return xd;
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public System.Xml.XmlDocument SerializeToXmlDocument()
      {
        var xd = new System.Xml.XmlDocument();

        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

        if (xd.CreateNavigator() is System.Xml.XPath.XPathNavigator navigator)
          using (var writer = navigator.AppendChild())
            serializer.Serialize(writer, source);

        return xd;
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <returns></returns>
      public string SerializeToXml()
      {
        //var settings = new System.Xml.XmlWriterSettings() { OmitXmlDeclaration = true };
        //using var sw = new System.IO.StringWriter();
        //using var xw = System.Xml.XmlWriter.Create(sw, settings);
        //var xsn = new System.Xml.Serialization.XmlSerializerNamespaces();
        //xsn.Add(string.Empty, string.Empty);
        //new System.Xml.Serialization.XmlSerializer(source.GetType()).Serialize(xw, source, xsn);
        //return sw.ToString();

        var xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
        using var sw = new System.IO.StringWriter();
        xs.Serialize(sw, source);
        return sw.ToString();
      }

      #endregion
    }

    extension(System.Object)
    {
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
  }
}
