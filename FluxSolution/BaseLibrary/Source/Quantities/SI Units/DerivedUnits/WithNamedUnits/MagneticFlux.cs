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
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(MagneticFluxUnit.Weber, format, formatProvider);

    // ISiUnitValueQuantifiable<>
    public (MetricPrefix Prefix, MagneticFluxUnit Unit) GetSiPrefixUnit(MetricPrefix prefix) => (prefix, MagneticFluxUnit.Weber);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetUnitSymbol(preferUnicode) + GetUnitSymbol(GetSiPrefixUnit(prefix).Unit, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    {
      var sb = new System.Text.StringBuilder();
      sb.Append(GetSiPrefixValue(prefix).ToString(format, formatProvider));
      sb.Append(unitSpacing.ToSpacingString());
      sb.Append(GetSiPrefixSymbol(prefix, preferUnicode));
      return sb.ToString();
    }

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="MagneticFlux.Value"/> property is in <see cref="MagneticFluxUnit.Weber"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(MagneticFluxUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

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

    public string ToUnitValueNameString(MagneticFluxUnit unit = MagneticFluxUnit.Weber, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitName(unit, preferPlural));

    public string ToUnitValueSymbolString(MagneticFluxUnit unit = MagneticFluxUnit.Weber, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitSymbol(unit, preferUnicode));

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
