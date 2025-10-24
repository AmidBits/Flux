namespace Flux
{
  public static partial class XtensionObject
  {
    private static readonly System.Reflection.MethodInfo m_typeConverterOfT = typeof(Fx).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Single(mi => mi.IsGenericMethod && mi.Name.Equals(nameof(TypeConverter), System.StringComparison.Ordinal) && mi.GetParameters().Length == 2);

    extension(System.Object source)
    {
      /// <summary>Complement the built-in TypeConverter system.</summary>
      /// <see href="https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.typeconverter"/>
      /// <remarks>This is the method used in the static property <see cref="m_typeConverterOfT"/>.</remarks>
      public T? TypeConverter<T>(System.Globalization.CultureInfo? culture = null)
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
      public bool TryTypeConverter<T>(out T? result, System.Globalization.CultureInfo? culture = null)
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
      public object TypeConverter(System.Globalization.CultureInfo? culture, params System.Type[] conversionSequence)
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
      public bool TryTypeConverter(out object result, System.Globalization.CultureInfo? culture, params System.Type[] conversionSequence)
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
    }
  }
}
