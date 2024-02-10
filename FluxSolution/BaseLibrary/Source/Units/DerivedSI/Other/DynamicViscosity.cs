namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.DynamicViscosityUnit unit, Units.TextOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.DynamicViscosityUnit.PascalSecond => options.PreferUnicode ? "Pa\u22C5s" : "Pa·s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum DynamicViscosityUnit
    {
      /// <summary>This is the default unit for <see cref="DynamicViscosity"/>.</summary>
      PascalSecond,
    }

    /// <summary>Dynamic viscosity, unit of Pascal second.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Dynamic_viscosity"/>
    public readonly record struct DynamicViscosity
      : System.IComparable, System.IComparable<DynamicViscosity>, System.IFormattable, IUnitValueQuantifiable<double, DynamicViscosityUnit>
    {
      private readonly double m_value;

      public DynamicViscosity(double value, DynamicViscosityUnit unit = DynamicViscosityUnit.PascalSecond)
        => m_value = unit switch
        {
          DynamicViscosityUnit.PascalSecond => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static DynamicViscosity From(Pressure pressure, Time time)
        => new(pressure.Value * time.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(DynamicViscosity v) => v.m_value;
      public static explicit operator DynamicViscosity(double v) => new(v);

      public static bool operator <(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) < 0;
      public static bool operator <=(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) <= 0;
      public static bool operator >(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) > 0;
      public static bool operator >=(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) >= 0;

      public static DynamicViscosity operator -(DynamicViscosity v) => new(-v.m_value);
      public static DynamicViscosity operator +(DynamicViscosity a, double b) => new(a.m_value + b);
      public static DynamicViscosity operator +(DynamicViscosity a, DynamicViscosity b) => a + b.m_value;
      public static DynamicViscosity operator /(DynamicViscosity a, double b) => new(a.m_value / b);
      public static DynamicViscosity operator /(DynamicViscosity a, DynamicViscosity b) => a / b.m_value;
      public static DynamicViscosity operator *(DynamicViscosity a, double b) => new(a.m_value * b);
      public static DynamicViscosity operator *(DynamicViscosity a, DynamicViscosity b) => a * b.m_value;
      public static DynamicViscosity operator %(DynamicViscosity a, double b) => new(a.m_value % b);
      public static DynamicViscosity operator %(DynamicViscosity a, DynamicViscosity b) => a % b.m_value;
      public static DynamicViscosity operator -(DynamicViscosity a, double b) => new(a.m_value - b);
      public static DynamicViscosity operator -(DynamicViscosity a, DynamicViscosity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is DynamicViscosity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(DynamicViscosity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(TextOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(TextOptions options = default) => ToUnitValueString(DynamicViscosityUnit.PascalSecond, options);

      /// <summary>
      /// <para>The unit of the <see cref="DynamicViscosity.Value"/> property is in <see cref="DynamicViscosityUnit.PascalSecond"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(DynamicViscosityUnit unit)
        => unit switch
        {
          DynamicViscosityUnit.PascalSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(DynamicViscosityUnit unit, TextOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
