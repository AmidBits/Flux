namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ForceUnit unit, Units.TextOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.ForceUnit.Newton => "N",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum ForceUnit
    {
      /// <summary>This is the default unit for <see cref="Force"/>.</summary>
      Newton,
    }

    /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Force"/>
    public readonly record struct Force
      : System.IComparable, System.IComparable<Force>, System.IFormattable, IUnitValueQuantifiable<double, ForceUnit>
    {
      private readonly double m_value;

      public Force(double value, ForceUnit unit = ForceUnit.Newton)
        => m_value = unit switch
        {
          ForceUnit.Newton => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(Force v) => v.m_value;
      public static explicit operator Force(double v) => new(v);

      public static bool operator <(Force a, Force b) => a.CompareTo(b) < 0;
      public static bool operator <=(Force a, Force b) => a.CompareTo(b) <= 0;
      public static bool operator >(Force a, Force b) => a.CompareTo(b) > 0;
      public static bool operator >=(Force a, Force b) => a.CompareTo(b) >= 0;

      public static Force operator -(Force v) => new(-v.m_value);
      public static Force operator +(Force a, double b) => new(a.m_value + b);
      public static Force operator +(Force a, Force b) => a + b.m_value;
      public static Force operator /(Force a, double b) => new(a.m_value / b);
      public static Force operator /(Force a, Force b) => a / b.m_value;
      public static Force operator *(Force a, double b) => new(a.m_value * b);
      public static Force operator *(Force a, Force b) => a * b.m_value;
      public static Force operator %(Force a, double b) => new(a.m_value % b);
      public static Force operator %(Force a, Force b) => a % b.m_value;
      public static Force operator -(Force a, double b) => new(a.m_value - b);
      public static Force operator -(Force a, Force b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Force o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Force other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(TextOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(TextOptions options = default) => ToUnitValueString(ForceUnit.Newton, options);

      /// <summary>
      /// <para>The unit of the <see cref="Force.Value"/> property is in <see cref="ForceUnit.Newton"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(ForceUnit unit)
        => unit switch
        {
          ForceUnit.Newton => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ForceUnit unit, TextOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
