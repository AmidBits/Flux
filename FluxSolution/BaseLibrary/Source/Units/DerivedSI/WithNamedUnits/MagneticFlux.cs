namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.MagneticFluxUnit unit, QuantifiableValueStringOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.MagneticFluxUnit.Weber => options.PreferUnicode ? "\u33DD" : "Wb",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum MagneticFluxUnit
    {
      /// <summary>This is the default unit for <see cref="MagneticFlux"/>.</summary>
      Weber,
    }

    /// <summary>Magnetic flux unit of weber.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Magnetic_flux"/>
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
      public string ToValueString(QuantifiableValueStringOptions options = default) => ToUnitValueString(DefaultUnit, options);

      /// <summary>
      /// <para>The unit of the <see cref="MagneticFlux.Value"/> property is in <see cref="MagneticFluxUnit.Weber"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(MagneticFluxUnit unit)
        => unit switch
        {
          MagneticFluxUnit.Weber => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(MagneticFluxUnit unit, QuantifiableValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
