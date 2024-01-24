namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.IlluminanceUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.IlluminanceUnit.Lux => preferUnicode ? "\u33D3" : "lx",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum IlluminanceUnit
    {
      /// <summary>Lux.</summary>
      Lux,
    }

    /// <summary>Illuminance unit of lux.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Illuminance"/>
    public readonly record struct Illuminance
      : System.IComparable, System.IComparable<Illuminance>, IUnitValueQuantifiable<double, IlluminanceUnit>
    {
      public const IlluminanceUnit DefaultUnit = IlluminanceUnit.Lux;

      private readonly double m_value;

      public Illuminance(double value, IlluminanceUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          IlluminanceUnit.Lux => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };


      public MetricMultiplicative ToMetricMultiplicative()
        => new(m_value, MetricMultiplicativePrefix.One);

      #region Overloaded operators
      public static explicit operator double(Illuminance v) => v.m_value;
      public static explicit operator Illuminance(double v) => new(v);

      public static bool operator <(Illuminance a, Illuminance b) => a.CompareTo(b) < 0;
      public static bool operator <=(Illuminance a, Illuminance b) => a.CompareTo(b) <= 0;
      public static bool operator >(Illuminance a, Illuminance b) => a.CompareTo(b) > 0;
      public static bool operator >=(Illuminance a, Illuminance b) => a.CompareTo(b) >= 0;

      public static Illuminance operator -(Illuminance v) => new(-v.m_value);
      public static Illuminance operator +(Illuminance a, double b) => new(a.m_value + b);
      public static Illuminance operator +(Illuminance a, Illuminance b) => a + b.m_value;
      public static Illuminance operator /(Illuminance a, double b) => new(a.m_value / b);
      public static Illuminance operator /(Illuminance a, Illuminance b) => a / b.m_value;
      public static Illuminance operator *(Illuminance a, double b) => new(a.m_value * b);
      public static Illuminance operator *(Illuminance a, Illuminance b) => a * b.m_value;
      public static Illuminance operator %(Illuminance a, double b) => new(a.m_value % b);
      public static Illuminance operator %(Illuminance a, Illuminance b) => a % b.m_value;
      public static Illuminance operator -(Illuminance a, double b) => new(a.m_value - b);
      public static Illuminance operator -(Illuminance a, Illuminance b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Illuminance o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Illuminance other) => m_value.CompareTo(other.m_value);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(IlluminanceUnit unit)
        => unit switch
        {
          IlluminanceUnit.Lux => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(IlluminanceUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
