namespace Flux.Units
{
  /// <summary>
  /// <para>Luminous flux, unit of lumen.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Luminous_flux"/></para>
  /// </summary>
  public readonly record struct LuminousFlux
    : System.IComparable, System.IComparable<LuminousFlux>, System.IFormattable, ISiUnitValueQuantifiable<double, LuminousFluxUnit>
  {
    private readonly double m_value;

    public LuminousFlux(double value, LuminousFluxUnit unit = LuminousFluxUnit.Lumen) => m_value = ConvertFromUnit(unit, value);

    public LuminousFlux(MetricPrefix prefix, double lumen) => m_value = prefix.ConvertPrefix(lumen, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(LuminousFlux a, LuminousFlux b) => a.CompareTo(b) < 0;
    public static bool operator >(LuminousFlux a, LuminousFlux b) => a.CompareTo(b) > 0;
    public static bool operator <=(LuminousFlux a, LuminousFlux b) => a.CompareTo(b) <= 0;
    public static bool operator >=(LuminousFlux a, LuminousFlux b) => a.CompareTo(b) >= 0;

    public static LuminousFlux operator -(LuminousFlux v) => new(-v.m_value);
    public static LuminousFlux operator *(LuminousFlux a, LuminousFlux b) => new(a.m_value * b.m_value);
    public static LuminousFlux operator /(LuminousFlux a, LuminousFlux b) => new(a.m_value / b.m_value);
    public static LuminousFlux operator %(LuminousFlux a, LuminousFlux b) => new(a.m_value % b.m_value);
    public static LuminousFlux operator +(LuminousFlux a, LuminousFlux b) => new(a.m_value + b.m_value);
    public static LuminousFlux operator -(LuminousFlux a, LuminousFlux b) => new(a.m_value - b.m_value);
    public static LuminousFlux operator *(LuminousFlux a, double b) => new(a.m_value * b);
    public static LuminousFlux operator /(LuminousFlux a, double b) => new(a.m_value / b);
    public static LuminousFlux operator %(LuminousFlux a, double b) => new(a.m_value % b);
    public static LuminousFlux operator +(LuminousFlux a, double b) => new(a.m_value + b);
    public static LuminousFlux operator -(LuminousFlux a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is LuminousFlux o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(LuminousFlux other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiPrefixValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + LuminousFluxUnit.Lumen.GetUnitSymbol();

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(LuminousFluxUnit unit, double value)
      => unit switch
      {
        LuminousFluxUnit.Lumen => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(LuminousFluxUnit unit, double value)
      => unit switch
      {
        LuminousFluxUnit.Lumen => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, LuminousFluxUnit from, LuminousFluxUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(LuminousFluxUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(LuminousFluxUnit unit = LuminousFluxUnit.Lumen, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(false) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="LuminousFlux.Value"/> property is in <see cref="LuminousFluxUnit.Lumen"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
