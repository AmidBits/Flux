using System.Linq;

namespace Flux
{
  public static partial class Convert
  {
    private static System.Reflection.MethodInfo m_typeConverterOfT => typeof(Convert).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Single(mi => mi.IsGenericMethod && mi.Name.Equals(nameof(TypeConverter), System.StringComparison.Ordinal) && mi.GetParameters().Length == 2);

    /// <summary>Complement the built-in TypeConverterfunctionality with a sequential conversion chain using the Try paradigm.</summary>
    public static bool TryTypeConverter(object value, out object result, System.Globalization.CultureInfo? culture, params System.Type[] type)
    {
      try
      {
        result = TypeConverter(value, culture, type);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types
      catch { }
#pragma warning restore CA1031 // Do not catch general exception types

      result = default!;
      return false;
    }
    /// <summary>Complement the built-in TypeConverter system using the Try paradigm.</summary>
    public static bool TryTypeConverter<T>(object value, out T result, System.Globalization.CultureInfo? culture = null)
    {
      try
      {
        result = TypeConverter<T>(value, culture);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types
      catch { }
#pragma warning restore CA1031 // Do not catch general exception types

      result = default!;
      return false;
    }

    /// <summary>Complement the built-in TypeConverter system.</summary>
    public static object TypeConverter(object value, System.Globalization.CultureInfo? culture, params System.Type[] types)
    {
      var parameters = new object[] { value ?? throw new System.ArgumentNullException(nameof(value)), culture! };

      var result = default(object);

      foreach (var type in types)
      {
        result = m_typeConverterOfT.MakeGenericMethod(type).Invoke(null, parameters);

        parameters[0] = result!;
      }

      return result!;
    }
    /// <summary>Complement the built-in TypeConverter system.</summary>
    /// <see cref="https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.typeconverter"/>
    public static T TypeConverter<T>(object value, System.Globalization.CultureInfo? culture = null)
    {
      var exceptions = new System.Collections.Generic.List<System.Exception>();

      try
      {
        if (System.ComponentModel.TypeDescriptor.GetConverter(value) is var valueConverter && valueConverter != null && valueConverter.CanConvertTo(typeof(T)))
        {
          return (T)valueConverter.ConvertTo(null, culture, value, typeof(T));
        }
      }
      catch (System.Exception ex) { exceptions.Add(ex); }

      try
      {
        if (System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)) is var typeConverter && typeConverter != null && typeConverter.CanConvertFrom(value.GetType()))
        {
          return (T)typeConverter.ConvertFrom(null, culture, value);
        }
      }
      catch (System.Exception ex) { exceptions.Add(ex); }

      if (exceptions.Count > 0) throw new System.AggregateException(exceptions);

      throw new System.NotSupportedException(@"No type converter found.");
    }
  }
}
