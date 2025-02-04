namespace Flux.Quantities
{
  public enum HeatCapacityUnit
  {
    /// <summary>This is the default unit for <see cref="HeatCapacity"/>.</summary>
    JoulePerKelvin,
  }

  /// <summary>Heat capacity, unit of Joule per Kelvin.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Heat_capacity"/>
  public readonly record struct HeatCapacity
    : System.IComparable, System.IComparable<HeatCapacity>, System.IFormattable, ISiUnitValueQuantifiable<double, HeatCapacityUnit>
  {
    /// <summary>
    /// <para>The Boltzmann constant (kB or k) is the proportionality factor that relates the average relative thermal energy of particles in a gas with the thermodynamic temperature of the gas.</para>
    /// <para>This is one of the fundamental physical constants of physics.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Boltzmann_constant"/></para>
    /// </summary>
    public const double BoltzmannConstant = 1.380649e-23;

    private readonly double m_value;

    public HeatCapacity(double value, HeatCapacityUnit unit = HeatCapacityUnit.JoulePerKelvin) => m_value = ConvertToUnit(unit, value);

    public HeatCapacity(MetricPrefix prefix, double joulePerKelvin) => m_value = prefix.ConvertTo(joulePerKelvin, MetricPrefix.Unprefixed);

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(HeatCapacity a, HeatCapacity b) => a.CompareTo(b) < 0;
    public static bool operator >(HeatCapacity a, HeatCapacity b) => a.CompareTo(b) > 0;
    public static bool operator <=(HeatCapacity a, HeatCapacity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(HeatCapacity a, HeatCapacity b) => a.CompareTo(b) >= 0;

    public static HeatCapacity operator -(HeatCapacity v) => new(-v.m_value);
    public static HeatCapacity operator *(HeatCapacity a, HeatCapacity b) => new(a.m_value * b.m_value);
    public static HeatCapacity operator /(HeatCapacity a, HeatCapacity b) => new(a.m_value / b.m_value);
    public static HeatCapacity operator %(HeatCapacity a, HeatCapacity b) => new(a.m_value % b.m_value);
    public static HeatCapacity operator +(HeatCapacity a, HeatCapacity b) => new(a.m_value + b.m_value);
    public static HeatCapacity operator -(HeatCapacity a, HeatCapacity b) => new(a.m_value - b.m_value);
    public static HeatCapacity operator *(HeatCapacity a, double b) => new(a.m_value * b);
    public static HeatCapacity operator /(HeatCapacity a, double b) => new(a.m_value / b);
    public static HeatCapacity operator %(HeatCapacity a, double b) => new(a.m_value % b);
    public static HeatCapacity operator +(HeatCapacity a, double b) => new(a.m_value + b);
    public static HeatCapacity operator -(HeatCapacity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is HeatCapacity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(HeatCapacity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(HeatCapacityUnit.JoulePerKelvin, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(HeatCapacityUnit.JoulePerKelvin, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(HeatCapacityUnit.JoulePerKelvin, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(HeatCapacityUnit unit, double value)
      => unit switch
      {
        HeatCapacityUnit.JoulePerKelvin => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(HeatCapacityUnit unit, double value)
      => unit switch
      {
        HeatCapacityUnit.JoulePerKelvin => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, HeatCapacityUnit from, HeatCapacityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(HeatCapacityUnit unit)
      => unit switch
      {
        HeatCapacityUnit.JoulePerKelvin => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(HeatCapacityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(HeatCapacityUnit unit, bool preferUnicode)
      => unit switch
      {
        HeatCapacityUnit.JoulePerKelvin => "J/K",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(HeatCapacityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(HeatCapacityUnit unit = HeatCapacityUnit.JoulePerKelvin, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="HeatCapacity.Value"/> property is in <see cref="HeatCapacityUnit.JoulePerKelvin"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
