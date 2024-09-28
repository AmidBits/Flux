namespace Flux.Quantities
{
  public enum LuminousFluxUnit
  {
    /// <summary>This is the default unit for <see cref="LuminousFlux"/>.</summary>
    Lumen,
  }

  /// <summary>Luminous flux unit of lumen.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Luminous_flux"/>
  public readonly record struct LuminousFlux
    : System.IComparable, System.IComparable<LuminousFlux>, System.IFormattable, ISiPrefixValueQuantifiable<double, LuminousFluxUnit>
  {
    private readonly double m_value;

    public LuminousFlux(double value, LuminousFluxUnit unit = LuminousFluxUnit.Lumen)
      => m_value = unit switch
      {
        LuminousFluxUnit.Lumen => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators

    public static bool operator <(LuminousFlux a, LuminousFlux b) => a.CompareTo(b) < 0;
    public static bool operator <=(LuminousFlux a, LuminousFlux b) => a.CompareTo(b) <= 0;
    public static bool operator >(LuminousFlux a, LuminousFlux b) => a.CompareTo(b) > 0;
    public static bool operator >=(LuminousFlux a, LuminousFlux b) => a.CompareTo(b) >= 0;

    public static LuminousFlux operator -(LuminousFlux v) => new(-v.m_value);
    public static LuminousFlux operator +(LuminousFlux a, double b) => new(a.m_value + b);
    public static LuminousFlux operator +(LuminousFlux a, LuminousFlux b) => a + b.m_value;
    public static LuminousFlux operator /(LuminousFlux a, double b) => new(a.m_value / b);
    public static LuminousFlux operator /(LuminousFlux a, LuminousFlux b) => a / b.m_value;
    public static LuminousFlux operator *(LuminousFlux a, double b) => new(a.m_value * b);
    public static LuminousFlux operator *(LuminousFlux a, LuminousFlux b) => a * b.m_value;
    public static LuminousFlux operator %(LuminousFlux a, double b) => new(a.m_value % b);
    public static LuminousFlux operator %(LuminousFlux a, LuminousFlux b) => a % b.m_value;
    public static LuminousFlux operator -(LuminousFlux a, double b) => new(a.m_value - b);
    public static LuminousFlux operator -(LuminousFlux a, LuminousFlux b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is LuminousFlux o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(LuminousFlux other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixString(MetricPrefix.Unprefixed);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="LuminousFlux.Value"/> property is in <see cref="LuminousFluxUnit.Lumen"/>.</para>
    /// </summary>
    public double Value => m_value;

    // ISiPrefixValueQuantifiable<>
    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(LuminousFluxUnit.Lumen, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(LuminousFluxUnit.Lumen, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    //public string ToSiPrefixValueNameString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
    //  => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    //public string ToSiPrefixValueSymbolString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    // IUnitQuantifiable<>
    public string GetUnitName(LuminousFluxUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(LuminousFluxUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.LuminousFluxUnit.Lumen => preferUnicode ? "\u33D0" : "lm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(LuminousFluxUnit unit)
        => unit switch
        {
          LuminousFluxUnit.Lumen => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

    public string ToUnitString(LuminousFluxUnit unit = LuminousFluxUnit.Lumen, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    //public string ToUnitValueNameString(LuminousFluxUnit unit = LuminousFluxUnit.Lumen, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    //public string ToUnitValueSymbolString(LuminousFluxUnit unit = LuminousFluxUnit.Lumen, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
