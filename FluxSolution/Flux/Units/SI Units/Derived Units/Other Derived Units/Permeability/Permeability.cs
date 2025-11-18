namespace Flux.Units
{
  /// <summary>
  /// <para>Permeability, unit of henry per meter. This is an SI derived quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Force"/></para>
  /// </summary>
  public readonly record struct Permeability
    : System.IComparable, System.IComparable<Permeability>, System.IFormattable, ISiUnitValueQuantifiable<double, PermeabilityUnit>
  {
    private readonly double m_value;

    public Permeability(double value, PermeabilityUnit unit = PermeabilityUnit.HenryPerMeter) => m_value = ConvertFromUnit(unit, value);

    public Permeability(MetricPrefix prefix, double henryPerMeter) => m_value = prefix.ConvertPrefix(henryPerMeter, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(Permeability a, Permeability b) => a.CompareTo(b) < 0;
    public static bool operator >(Permeability a, Permeability b) => a.CompareTo(b) > 0;
    public static bool operator <=(Permeability a, Permeability b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Permeability a, Permeability b) => a.CompareTo(b) >= 0;

    public static Permeability operator -(Permeability v) => new(-v.m_value);
    public static Permeability operator *(Permeability a, Permeability b) => new(a.m_value * b.m_value);
    public static Permeability operator /(Permeability a, Permeability b) => new(a.m_value / b.m_value);
    public static Permeability operator %(Permeability a, Permeability b) => new(a.m_value % b.m_value);
    public static Permeability operator +(Permeability a, Permeability b) => new(a.m_value + b.m_value);
    public static Permeability operator -(Permeability a, Permeability b) => new(a.m_value - b.m_value);
    public static Permeability operator *(Permeability a, double b) => new(a.m_value * b);
    public static Permeability operator /(Permeability a, double b) => new(a.m_value / b);
    public static Permeability operator %(Permeability a, double b) => new(a.m_value % b);
    public static Permeability operator +(Permeability a, double b) => new(a.m_value + b);
    public static Permeability operator -(Permeability a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Permeability o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Permeability other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(PermeabilityUnit.HenryPerMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + PermeabilityUnit.HenryPerMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(PermeabilityUnit unit, double value)
      => unit switch
      {
        PermeabilityUnit.HenryPerMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(PermeabilityUnit unit, double value)
      => unit switch
      {
        PermeabilityUnit.HenryPerMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, PermeabilityUnit from, PermeabilityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(PermeabilityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(PermeabilityUnit unit = PermeabilityUnit.HenryPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Permeability.Value"/> property is in <see cref="PermeabilityUnit.HenryPerMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
