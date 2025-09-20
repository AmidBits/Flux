namespace Flux.Units
{
  /// <summary>
  /// <para>Mass. SI unit of kilogram. This is a base quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Mass"/></para>
  /// </summary>
  public readonly record struct Mass
    : System.IComparable, System.IComparable<Mass>, System.IEquatable<Mass>, System.IFormattable, ISiUnitValueQuantifiable<double, MassUnit>
  {
    /// <summary>
    /// <para>In particle physics, the electron mass (symbol: me) is the mass of a stationary electron, also known as the invariant mass of the electron. It is one of the fundamental constants of physics.</para>
    /// <para>This is one of the fundamental physical constants of physics.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Electron_mass"/></para>
    /// </summary>
    public const double ElectronMass = 9.109383701528e-31;

    /// <summary>
    /// <para></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Dalton_(unit)"/></para>
    /// </summary>
    public const double UnifiedAtomicMassUnit = 1.66053906892e-27;

    private readonly double m_value;

    /// <summary>
    /// <para>Creates a new instance from the specified <paramref name="value"/> of <paramref name="unit"/>. The default <paramref name="unit"/> is <see cref="MassUnit.Kilogram"/></para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unit"></param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public Mass(double value, MassUnit unit = MassUnit.Kilogram) => m_value = ConvertFromUnit(unit, value);

    /// <summary>
    /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="MassUnit.Gram"/>, e.g. <see cref="MetricPrefix.Kilo"/> for kilogram.</para>
    /// </summary>
    /// <remarks>Mass is the only </remarks>
    /// <param name="gram"></param>
    /// <param name="prefix"></param>
    public Mass(MetricPrefix prefix, double gram) => m_value = prefix.ChangePrefix(gram, MetricPrefix.Kilo);

    public Mass(Force force, Acceleration acceleration) => m_value = force.Value / acceleration.Value;

    #region Static methods
    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Mass a, Mass b) => a.CompareTo(b) < 0;
    public static bool operator >(Mass a, Mass b) => a.CompareTo(b) > 0;
    public static bool operator <=(Mass a, Mass b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Mass a, Mass b) => a.CompareTo(b) >= 0;

    public static Mass operator -(Mass v) => new(-v.m_value);
    public static Mass operator *(Mass a, Mass b) => new(a.m_value * b.m_value);
    public static Mass operator /(Mass a, Mass b) => new(a.m_value / b.m_value);
    public static Mass operator %(Mass a, Mass b) => new(a.m_value % b.m_value);
    public static Mass operator +(Mass a, Mass b) => new(a.m_value + b.m_value);
    public static Mass operator -(Mass a, Mass b) => new(a.m_value - b.m_value);
    public static Mass operator *(Mass a, double b) => new(a.m_value * b);
    public static Mass operator /(Mass a, double b) => new(a.m_value / b);
    public static Mass operator %(Mass a, double b) => new(a.m_value % b);
    public static Mass operator +(Mass a, double b) => new(a.m_value + b);
    public static Mass operator -(Mass a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Mass o ? CompareTo(o) : -1;

    // IComparable<T>
    public int CompareTo(Mass other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToSiUnitString(MetricPrefix.Kilo);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix)
      => MetricPrefix.Kilo.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + MassUnit.Gram.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(MassUnit unit, double value)
      => unit switch
      {
        MassUnit.Kilogram => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(MassUnit unit, double value)
      => unit switch
      {
        MassUnit.Kilogram => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, MassUnit from, MassUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(MassUnit unit)
      => ConvertToUnit(unit, m_value);

    public string ToUnitString(MassUnit unit = MassUnit.Kilogram, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Mass.Value"/> property is in <see cref="MassUnit.Kilogram"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
