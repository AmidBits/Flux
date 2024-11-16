namespace Flux.Quantities
{
  public enum MassUnit
  {
    /// <summary>This is the default unit for <see cref="Mass"/>.</summary>
    Kilogram,
    /// <summary>Even though gram is an unprefixed unit, it is not the base unit for mass.</summary>
    Gram,
    Ounce,
    Pound,
    /// <summary>The metric ton.</summary>
    Tonne,
  }

  /// <summary>
  /// <para>Mass. SI unit of kilogram. This is a base quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Mass"/></para>
  /// </summary>
  public readonly record struct Mass
    : System.IComparable, System.IComparable<Mass>, System.IFormattable, ISiUnitValueQuantifiable<double, MassUnit>
  {
    public static Mass ElectronMass { get; } = new(9.109383701528e-31);

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
    public Mass(MetricPrefix prefix, double gram) => m_value = prefix.ConvertTo(gram, MetricPrefix.Kilo);

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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Kilo);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(MassUnit.Gram, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(MassUnit.Gram, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Kilo.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(MassUnit unit, double value)
      => unit switch
      {
        MassUnit.Kilogram => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(MassUnit unit, double value)
      => unit switch
      {
        MassUnit.Kilogram => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, MassUnit from, MassUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(MassUnit unit)
      => unit switch
      {
        MassUnit.Gram => 0.001,
        MassUnit.Ounce => 0.028349523125,
        MassUnit.Pound => 0.45359237,
        MassUnit.Tonne => 1000,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(MassUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural);

    public static string GetUnitSymbol(MassUnit unit, bool preferUnicode)
      => unit switch
      {
        MassUnit.Gram => "g",
        MassUnit.Ounce => "oz",
        MassUnit.Pound => "lb",
        MassUnit.Kilogram => preferUnicode ? "\u338F" : "kg",
        MassUnit.Tonne => "t",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(MassUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MassUnit unit = MassUnit.Kilogram, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

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
