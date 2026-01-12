namespace Flux.Units
{
  /// <summary>
  /// <para>Surface tension, unit of Newton per meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Surface_tension"/></para>
  /// </summary>
  public readonly record struct SurfaceTension
    : System.IComparable, System.IComparable<SurfaceTension>, System.IFormattable, ISiUnitValueQuantifiable<double, SurfaceTensionUnit>
  {
    private readonly double m_value;

    public SurfaceTension(double value, SurfaceTensionUnit unit = SurfaceTensionUnit.NewtonPerMeter) => m_value = ConvertToUnit(unit, value);

    public SurfaceTension(MetricPrefix prefix, double newtonPerMeter) => m_value = prefix.ConvertPrefix(newtonPerMeter, MetricPrefix.Unprefixed);

    public SurfaceTension(Force force, Length length) : this(force.Value / length.Value) { }

    public SurfaceTension(Energy energy, Area area) : this(energy.Value / area.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) < 0;
    public static bool operator >(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) > 0;
    public static bool operator <=(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) <= 0;
    public static bool operator >=(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) >= 0;

    public static SurfaceTension operator -(SurfaceTension v) => new(-v.m_value);
    public static SurfaceTension operator *(SurfaceTension a, SurfaceTension b) => new(a.m_value * b.m_value);
    public static SurfaceTension operator /(SurfaceTension a, SurfaceTension b) => new(a.m_value / b.m_value);
    public static SurfaceTension operator %(SurfaceTension a, SurfaceTension b) => new(a.m_value % b.m_value);
    public static SurfaceTension operator +(SurfaceTension a, SurfaceTension b) => new(a.m_value + b.m_value);
    public static SurfaceTension operator -(SurfaceTension a, SurfaceTension b) => new(a.m_value - b.m_value);
    public static SurfaceTension operator *(SurfaceTension a, double b) => new(a.m_value * b);
    public static SurfaceTension operator /(SurfaceTension a, double b) => new(a.m_value / b);
    public static SurfaceTension operator %(SurfaceTension a, double b) => new(a.m_value % b);
    public static SurfaceTension operator +(SurfaceTension a, double b) => new(a.m_value + b);
    public static SurfaceTension operator -(SurfaceTension a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is SurfaceTension o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(SurfaceTension other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(SurfaceTensionUnit.NewtonPerMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + SurfaceTensionUnit.NewtonPerMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(SurfaceTensionUnit unit, double value)
      => unit switch
      {
        SurfaceTensionUnit.NewtonPerMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(SurfaceTensionUnit unit, double value)
      => unit switch
      {
        SurfaceTensionUnit.NewtonPerMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, SurfaceTensionUnit from, SurfaceTensionUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(SurfaceTensionUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(SurfaceTensionUnit unit = SurfaceTensionUnit.NewtonPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(Numbers.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="SurfaceTension.Value"/> property is in <see cref="SurfaceTensionUnit.NewtonPerMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
