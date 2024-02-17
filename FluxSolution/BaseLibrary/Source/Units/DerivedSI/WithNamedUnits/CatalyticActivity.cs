namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.CatalyticActivityUnit unit, Units.UnitValueStringOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.CatalyticActivityUnit.Katal => options.PreferUnicode ? "\u33CF" : "kat",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum CatalyticActivityUnit
    {
      /// <summary>This is the default unit for <see cref="CatalyticActivity"/>. Katal = (mol/s).</summary>
      Katal,
    }

    /// <summary>Catalytic activity unit of Katal.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Catalysis"/>
    public readonly record struct CatalyticActivity
      : System.IComparable, System.IComparable<CatalyticActivity>, System.IFormattable, IUnitValueQuantifiable<double, CatalyticActivityUnit>
    {
      private readonly double m_value;

      public CatalyticActivity(double value, CatalyticActivityUnit unit = CatalyticActivityUnit.Katal)
        => m_value = unit switch
        {
          CatalyticActivityUnit.Katal => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(CatalyticActivity v) => v.m_value;
      public static explicit operator CatalyticActivity(double v) => new(v);

      public static bool operator <(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) < 0;
      public static bool operator <=(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) <= 0;
      public static bool operator >(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) > 0;
      public static bool operator >=(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) >= 0;

      public static CatalyticActivity operator -(CatalyticActivity v) => new(-v.m_value);
      public static CatalyticActivity operator +(CatalyticActivity a, double b) => new(a.m_value + b);
      public static CatalyticActivity operator +(CatalyticActivity a, CatalyticActivity b) => a + b.m_value;
      public static CatalyticActivity operator /(CatalyticActivity a, double b) => new(a.m_value / b);
      public static CatalyticActivity operator /(CatalyticActivity a, CatalyticActivity b) => a / b.m_value;
      public static CatalyticActivity operator *(CatalyticActivity a, double b) => new(a.m_value * b);
      public static CatalyticActivity operator *(CatalyticActivity a, CatalyticActivity b) => a * b.m_value;
      public static CatalyticActivity operator %(CatalyticActivity a, double b) => new(a.m_value % b);
      public static CatalyticActivity operator %(CatalyticActivity a, CatalyticActivity b) => a % b.m_value;
      public static CatalyticActivity operator -(CatalyticActivity a, double b) => new(a.m_value - b);
      public static CatalyticActivity operator -(CatalyticActivity a, CatalyticActivity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is CatalyticActivity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(CatalyticActivity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(CatalyticActivityUnit.Katal, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="CatalyticActivity.Value"/> property is in <see cref="CatalyticActivityUnit.Katal"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(CatalyticActivityUnit unit)
        => unit switch
        {
          CatalyticActivityUnit.Katal => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(CatalyticActivityUnit unit, UnitValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces
    }
  }
}
