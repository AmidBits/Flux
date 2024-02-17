namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.IlluminanceUnit unit, Units.UnitValueStringOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.IlluminanceUnit.Lux => options.PreferUnicode ? "\u33D3" : "lx",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum IlluminanceUnit
    {
      /// <summary>This is the default unit for <see cref="Illuminance"/>.</summary>
      Lux,
    }

    /// <summary>Illuminance unit of lux.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Illuminance"/>
    public readonly record struct Illuminance
      : System.IComparable, System.IComparable<Illuminance>, System.IFormattable, IUnitValueQuantifiable<double, IlluminanceUnit>
    {
      private readonly double m_value;

      public Illuminance(double value, IlluminanceUnit unit = IlluminanceUnit.Lux)
        => m_value = unit switch
        {
          IlluminanceUnit.Lux => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      //public MetricMultiplicative ToMetricMultiplicative() => new(m_value, MetricMultiplicativePrefix.One);

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

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(IlluminanceUnit.Lux, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Illuminance.Value"/> property is in <see cref="IlluminanceUnit.Lux"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(IlluminanceUnit unit)
        => unit switch
        {
          IlluminanceUnit.Lux => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(IlluminanceUnit unit, UnitValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces
    }
  }
}
