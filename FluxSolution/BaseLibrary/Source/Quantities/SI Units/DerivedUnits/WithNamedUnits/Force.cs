namespace Flux.Quantities
{
  public enum ForceUnit
  {
    /// <summary>This is the default unit for <see cref="Force"/>.</summary>
    Newton,
    KilogramForce,
    PoundForce,
  }

  /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Force"/>
  public readonly record struct Force
    : System.IComparable, System.IComparable<Force>, System.IFormattable, ISiUnitValueQuantifiable<double, ForceUnit>
  {
    private readonly double m_value;

    public Force(double value, ForceUnit unit = ForceUnit.Newton) => m_value = ConvertFromUnit(unit, value);

    public Force(Mass mass, Acceleration acceleration) : this(mass.Value * acceleration.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Force a, Force b) => a.CompareTo(b) < 0;
    public static bool operator <=(Force a, Force b) => a.CompareTo(b) <= 0;
    public static bool operator >(Force a, Force b) => a.CompareTo(b) > 0;
    public static bool operator >=(Force a, Force b) => a.CompareTo(b) >= 0;

    public static Force operator -(Force v) => new(-v.m_value);
    public static Force operator +(Force a, double b) => new(a.m_value + b);
    public static Force operator +(Force a, Force b) => a + b.m_value;
    public static Force operator /(Force a, double b) => new(a.m_value / b);
    public static Force operator /(Force a, Force b) => a / b.m_value;
    public static Force operator *(Force a, double b) => new(a.m_value * b);
    public static Force operator *(Force a, Force b) => a * b.m_value;
    public static Force operator %(Force a, double b) => new(a.m_value % b);
    public static Force operator %(Force a, Force b) => a % b.m_value;
    public static Force operator -(Force a, double b) => new(a.m_value - b);
    public static Force operator -(Force a, Force b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Force o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Force other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Force.Value"/> property is in <see cref="ForceUnit.Newton"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(ForceUnit.Newton, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(ForceUnit.Newton, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    private static double ConvertFromUnit(ForceUnit unit, double value)
      => unit switch
      {
        ForceUnit.Newton => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(ForceUnit unit, double value)
      => unit switch
      {
        ForceUnit.Newton => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, ForceUnit from, ForceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(ForceUnit unit)
      => unit switch
      {
        ForceUnit.Newton => 1,
        ForceUnit.KilogramForce => 0.101971621,
        ForceUnit.PoundForce => 0.224808943,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(ForceUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(ForceUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.ForceUnit.Newton => "N",
        Quantities.ForceUnit.KilogramForce => "kgf",
        Quantities.ForceUnit.PoundForce => "lbf",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ForceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ForceUnit unit = ForceUnit.Newton, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
