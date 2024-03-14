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
      /// <summary>This is the default unit for <see cref="MagneticFlux"/>.</summary>
      Weber,
    }

    /// <summary>Magnetic flux unit of weber.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Magnetic_flux"/>
    public readonly record struct MagneticFlux
      : System.IComparable, System.IComparable<MagneticFlux>, System.IFormattable, IUnitValueQuantifiable<double, MagneticFluxUnit>
    {
      private readonly double m_value;

      public MagneticFlux(double value, MagneticFluxUnit unit = MagneticFluxUnit.Weber)
        => m_value = unit switch
        {
          MagneticFluxUnit.Weber => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

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
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(MagneticFluxUnit.Weber, format, formatProvider);

      // IQuantifiable<>
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

      public string ToUnitValueString(MagneticFluxUnit unit = MagneticFluxUnit.Weber, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
