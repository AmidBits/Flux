namespace Flux.Units
{
  /// <summary>
  /// <para>Heat capacity, unit of Joule per Kelvin.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Heat_capacity"/></para>
  /// </summary>
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

    public HeatCapacity(MetricPrefix prefix, double joulePerKelvin) => m_value = prefix.ChangePrefix(joulePerKelvin, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + HeatCapacityUnit.JoulePerKelvin.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(HeatCapacityUnit unit, double value)
      => unit switch
      {
        HeatCapacityUnit.JoulePerKelvin => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(HeatCapacityUnit unit, double value)
      => unit switch
      {
        HeatCapacityUnit.JoulePerKelvin => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, HeatCapacityUnit from, HeatCapacityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(HeatCapacityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(HeatCapacityUnit unit = HeatCapacityUnit.JoulePerKelvin, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

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
