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

    public LuminousFlux(double value, LuminousFluxUnit unit = LuminousFluxUnit.Lumen) => m_value = ConvertFromUnit(unit, value);

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

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="LuminousFlux.Value"/> property is in <see cref="LuminousFluxUnit.Lumen"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiPrefixValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(LuminousFluxUnit.Lumen, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(LuminousFluxUnit.Lumen, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitQuantifiable<>

    public static double ConvertFromUnit(LuminousFluxUnit unit, double value)
      => unit switch
      {
        LuminousFluxUnit.Lumen => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(LuminousFluxUnit unit, double value)
      => unit switch
      {
        LuminousFluxUnit.Lumen => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double GetUnitFactor(LuminousFluxUnit unit)
      => unit switch
      {
        LuminousFluxUnit.Lumen => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(LuminousFluxUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(LuminousFluxUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.LuminousFluxUnit.Lumen => preferUnicode ? "\u33D0" : "lm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(LuminousFluxUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(LuminousFluxUnit unit = LuminousFluxUnit.Lumen, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
