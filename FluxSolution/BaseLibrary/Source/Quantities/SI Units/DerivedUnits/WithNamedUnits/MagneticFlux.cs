namespace Flux.Quantities
{
  public enum MagneticFluxUnit
  {
    /// <summary>This is the default unit for <see cref="MagneticFlux"/>.</summary>
    Weber,
  }

  /// <summary>Magnetic flux unit of weber.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Magnetic_flux"/>
  public readonly record struct MagneticFlux
    : System.IComparable, System.IComparable<MagneticFlux>, System.IFormattable, ISiPrefixValueQuantifiable<double, MagneticFluxUnit>
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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixString(MetricPrefix.Unprefixed);

    // ISiUnitValueQuantifiable<>
    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(MagneticFluxUnit.Weber, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(MagneticFluxUnit.Weber, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    //public string ToSiPrefixValueNameString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
    //  => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    //public string ToSiPrefixValueSymbolString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="MagneticFlux.Value"/> property is in <see cref="MagneticFluxUnit.Weber"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(MagneticFluxUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(MagneticFluxUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.MagneticFluxUnit.Weber => preferUnicode ? "\u33DD" : "Wb",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(MagneticFluxUnit unit)
        => unit switch
        {
          MagneticFluxUnit.Weber => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

    public string ToUnitString(MagneticFluxUnit unit = MagneticFluxUnit.Weber, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    //public string ToUnitValueNameString(MagneticFluxUnit unit = MagneticFluxUnit.Weber, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    //public string ToUnitValueSymbolString(MagneticFluxUnit unit = MagneticFluxUnit.Weber, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
