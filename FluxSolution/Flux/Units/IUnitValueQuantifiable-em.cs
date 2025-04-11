using System.Reflection;

namespace Flux
{
  public static partial class Em
  {
    public static T GetDefaultValue<T>()
      where T : struct, System.Enum
    {
      if (typeof(T).GetCustomAttribute<System.ComponentModel.DefaultValueAttribute>(inherit: false) is var a && a is not null)
        return (T)(a.Value ?? default(T));

      return default;
    }

    public static System.Collections.Generic.Dictionary<TUnit, TValue> GetUnitValueAll<TValue, TUnit>(this Units.IUnitValueQuantifiable<TValue, TUnit> source)
      where TValue : System.IEquatable<TValue>
      where TUnit : System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<TUnit, TValue>();
      foreach (TUnit unit in System.Enum.GetValues(typeof(TUnit)))
        d.Add(unit, source.GetUnitValue(unit));
      return d;
    }

    public static string ToPluralUnitName(this string source, bool preferPlural)
    {
      if (!preferPlural) return source; // Do not want plural.

      if (source.IndexOf("Per", StringComparison.InvariantCulture) is var index && index > 0)
        return ConvertUnitNameToPlural(source[..index]) + source[index..]; // Replace the (singular assumed) unit before the "Per" with a plural version.

      return ConvertUnitNameToPlural(source);

      static string ConvertUnitNameToPlural(string unitName)
      {
        if (
          unitName.Equals("Celsius")
          || unitName.Equals("Fahrenheit")
          || unitName.Equals("Siemens")
        )
          return unitName; // Skip changes to the ones above.

        if (
          unitName.Equals("Foot")
        )
          return "Feet";

        if (unitName.EndsWith('x')
          || unitName.Equals("Inch")
        )
          return unitName + "es";

        if (
          unitName.EndsWith('y')
        )
          return unitName + "ies";

        if (!unitName.EndsWith('s'))
          return unitName + "s";

        return unitName;
      }
    }

    public static System.Collections.Generic.Dictionary<TUnit, string> ToUnitStringAll<TValue, TUnit>(this Units.IUnitValueQuantifiable<TValue, TUnit> source, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, Unicode.UnicodeSpacing unitSpacing = Unicode.UnicodeSpacing.Space, bool useFullName = false)
      where TValue : System.IEquatable<TValue>
      where TUnit : System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<TUnit, string>();
      foreach (TUnit unit in System.Enum.GetValues(typeof(TUnit)))
        d.Add(unit, source.ToUnitString(unit, format, formatProvider, useFullName));
      return d;
    }
  }
}
