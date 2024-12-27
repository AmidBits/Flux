namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Complement the built-in TypeConverter system.</summary>
    /// <see href="https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.typeconverter"/>
    /// <remarks>This is the method used in the static property <see cref="m_typeConverterOfT"/>.</remarks>
    public static T? TypeConverter<T>(this object value, System.Globalization.CultureInfo? culture = null)
    {
      System.ArgumentNullException.ThrowIfNull(value);

      var exceptions = new System.Collections.Generic.List<System.Exception>();

      try
      {
        if (System.ComponentModel.TypeDescriptor.GetConverter(value) is var valueConverter && valueConverter != null && valueConverter.CanConvertTo(typeof(T)))
          return (T?)valueConverter.ConvertTo(null, culture, value, typeof(T));
      }
      catch (System.Exception ex) { exceptions.Add(ex); }

      try
      {
        if (System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)) is var typeConverter && typeConverter != null && typeConverter.CanConvertFrom(value.GetType()))
          return (T?)typeConverter.ConvertFrom(null, culture, value);
      }
      catch (System.Exception ex) { exceptions.Add(ex); }

      if (exceptions.Count > 0) throw new System.AggregateException(exceptions);

      throw new System.NotSupportedException(@"No type converter found.");
    }
    /// <summary>Complement the built-in TypeConverter system using the Try paradigm.</summary>
    public static bool TryTypeConverter<T>(this object value, out T? result, System.Globalization.CultureInfo? culture = null)
    {
      try
      {
        result = TypeConverter<T>(value, culture);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    private static readonly System.Reflection.MethodInfo m_typeConverterOfT = typeof(Fx).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Single(mi => mi.IsGenericMethod && mi.Name.Equals(nameof(TypeConverter), System.StringComparison.Ordinal) && mi.GetParameters().Length == 2);

    /// <summary>Complement the built-in TypeConverter system.</summary>
    public static object TypeConverter(this object value, System.Globalization.CultureInfo? culture, params System.Type[] conversionSequence)
    {
      var parameters = new object[] { value ?? throw new System.ArgumentNullException(nameof(value)), culture! };

      var result = default(object);

      foreach (var type in conversionSequence)
      {
        result = m_typeConverterOfT.MakeGenericMethod(type).Invoke(null, parameters);

        parameters[0] = result!;
      }

      return result!;
    }
    /// <summary>Complement the built-in TypeConverterfunctionality with a sequential conversion chain using the Try paradigm.</summary>
    public static bool TryTypeConverter(this object value, out object result, System.Globalization.CultureInfo? culture, params System.Type[] conversionSequence)
    {
      try
      {
        result = TypeConverter(value, culture, conversionSequence);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
  }
}
