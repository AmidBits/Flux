namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.MagneticFluxUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.MagneticFluxUnit.Weber => preferUnicode ? "\u33DD" : "Wb",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum MagneticFluxUnit
    {
      /// <summary>Weber.</summary>
      Weber,
    }

    /// <summary>Magnetic flux unit of weber.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux"/>
    public readonly record struct MagneticFlux
      : System.IComparable, System.IComparable<MagneticFlux>, System.IFormattable, IUnitValueQuantifiable<double, MagneticFluxUnit>
    {
      public const MagneticFluxUnit DefaultUnit = MagneticFluxUnit.Weber;

      private readonly double m_value;

      public MagneticFlux(double value, MagneticFluxUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          MagneticFluxUnit.Weber => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(MagneticFlux v) => v.m_value;
      public static explicit operator MagneticFlux(double v) => new(v);

      public static bool operator <(MagneticFlux a, MagneticFlux b) => a.CompareTo(b) < 0;
      public static bool operator <=(MagneticFlux a, MagneticFlux b) => a.CompareTo(b) <= 0;
      public static bool operator >(MagneticFlux a, MagneticFlux b) => a.CompareTo(b) > 0;
      public static bool operator >=(MagneticFlux a, MagneticFlux b) => a.CompareTo(b) >= 0;

      public static MagneticFlux operator -(MagneticFlux v) => new(-v.m_value);
      public static MagneticFlux operator +(MagneticFlux a, double b) => new(a.m_value + b);
      public static MagneticFlux operator +(MagneticFlux a, MagneticFlux b) => a + b.m_value;
      public static MagneticFlux operator /(MagneticFlux a, double b) => new(a.m_value / b);
      public static MagneticFlux operator /(MagneticFlux a, MagneticFlux b) => a / b.m_value;
      public static MagneticFlux operator *(MagneticFlux a, double b) => new(a.m_value * b);
      public static MagneticFlux operator *(MagneticFlux a, MagneticFlux b) => a * b.m_value;
      public static MagneticFlux operator %(MagneticFlux a, double b) => new(a.m_value % b);
      public static MagneticFlux operator %(MagneticFlux a, MagneticFlux b) => a % b.m_value;
      public static MagneticFlux operator -(MagneticFlux a, double b) => new(a.m_value - b);
      public static MagneticFlux operator -(MagneticFlux a, MagneticFlux b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is MagneticFlux o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(MagneticFlux other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(MagneticFluxUnit unit)
        => unit switch
        {
          MagneticFluxUnit.Weber => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(MagneticFluxUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
