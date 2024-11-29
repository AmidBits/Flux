namespace Flux.Quantities
{
  public enum SurfaceTensionUnit
  {
    /// <summary>This is the default unit for <see cref="SurfaceTension"/>.</summary>
    NewtonPerMeter,
  }

  /// <summary>Surface tension, unit of Newton per meter.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Surface_tension"/>
  public readonly record struct SurfaceTension
    : System.IComparable, System.IComparable<SurfaceTension>, System.IFormattable, ISiUnitValueQuantifiable<double, SurfaceTensionUnit>
  {
    private readonly double m_value;

    public SurfaceTension(double value, SurfaceTensionUnit unit = SurfaceTensionUnit.NewtonPerMeter) => m_value = ConvertToUnit(unit, value);

    public SurfaceTension(MetricPrefix prefix, double newtonPerMeter) => m_value = prefix.ConvertTo(newtonPerMeter, MetricPrefix.Unprefixed);

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

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(SurfaceTensionUnit.NewtonPerMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(SurfaceTensionUnit.NewtonPerMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(SurfaceTensionUnit unit, double value)
      => unit switch
      {
        SurfaceTensionUnit.NewtonPerMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(SurfaceTensionUnit unit, double value)
      => unit switch
      {
        SurfaceTensionUnit.NewtonPerMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, SurfaceTensionUnit from, SurfaceTensionUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(SurfaceTensionUnit unit)
      => unit switch
      {
        SurfaceTensionUnit.NewtonPerMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(SurfaceTensionUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural);

    public static string GetUnitSymbol(SurfaceTensionUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.SurfaceTensionUnit.NewtonPerMeter => "N/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(SurfaceTensionUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(SurfaceTensionUnit unit = SurfaceTensionUnit.NewtonPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

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
