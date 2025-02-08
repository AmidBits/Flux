namespace Flux.Units
{
  /// <summary>
  /// <para>Turbidity, unit of NTU (nephelometric turbidity units).</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Turbidity"/></para>
  /// </summary>
  public readonly record struct Turbidity
    : System.IComparable, System.IComparable<Turbidity>, System.IFormattable, IUnitValueQuantifiable<double, TurbidityUnit>
  {
    private readonly double m_value;

    public Turbidity(double value, TurbidityUnit unit = TurbidityUnit.NephelometricTurbidityUnits) => m_value = ConvertFromUnit(unit, value);

    public Turbidity(MetricPrefix prefix, double nephelometricTurbidityUnits) => m_value = prefix.ConvertTo(nephelometricTurbidityUnits, MetricPrefix.Unprefixed);

    #region Static methods

    public static Turbidity From(Force force, Time time) => new(force.Value / time.Value);

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Turbidity a, Turbidity b) => a.CompareTo(b) < 0;
    public static bool operator <=(Turbidity a, Turbidity b) => a.CompareTo(b) <= 0;
    public static bool operator >(Turbidity a, Turbidity b) => a.CompareTo(b) > 0;
    public static bool operator >=(Turbidity a, Turbidity b) => a.CompareTo(b) >= 0;

    public static Turbidity operator -(Turbidity v) => new(-v.m_value);
    public static Turbidity operator +(Turbidity a, double b) => new(a.m_value + b);
    public static Turbidity operator +(Turbidity a, Turbidity b) => a + b.m_value;
    public static Turbidity operator /(Turbidity a, double b) => new(a.m_value / b);
    public static Turbidity operator /(Turbidity a, Turbidity b) => a / b.m_value;
    public static Turbidity operator *(Turbidity a, double b) => new(a.m_value * b);
    public static Turbidity operator *(Turbidity a, Turbidity b) => a * b.m_value;
    public static Turbidity operator %(Turbidity a, double b) => new(a.m_value % b);
    public static Turbidity operator %(Turbidity a, Turbidity b) => a % b.m_value;
    public static Turbidity operator -(Turbidity a, double b) => new(a.m_value - b);
    public static Turbidity operator -(Turbidity a, Turbidity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Turbidity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Turbidity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(TurbidityUnit.NephelometricTurbidityUnits, format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Turbidity.Value"/> property is in <see cref="TurbidityUnit.TurbidityUnit"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(TurbidityUnit unit, double value)
      => unit switch
      {
        TurbidityUnit.NephelometricTurbidityUnits => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(TurbidityUnit unit, double value)
      => unit switch
      {
        TurbidityUnit.NephelometricTurbidityUnits => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, TurbidityUnit from, TurbidityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(TurbidityUnit unit)
      => unit switch
      {
        TurbidityUnit.NephelometricTurbidityUnits => 1,

        TurbidityUnit.FormazinAttenuationUnit => 1,
        TurbidityUnit.FormazinNephelometricUnit => 1,
        TurbidityUnit.FormazinTurbidityUnit => 1,
        TurbidityUnit.AmericanSocietyOfBrewingChemists => 1 / 17.5,
        TurbidityUnit.EuropeanBreweryConvention => 1 / 0.25,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(TurbidityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(TurbidityUnit unit, bool preferUnicode)
      => unit switch
      {
        TurbidityUnit.NephelometricTurbidityUnits => "NTU",

        TurbidityUnit.FormazinAttenuationUnit => "FAU",
        TurbidityUnit.FormazinNephelometricUnit => "FNU",
        TurbidityUnit.FormazinTurbidityUnit => "FTU",
        TurbidityUnit.AmericanSocietyOfBrewingChemists => "ASBC",
        TurbidityUnit.EuropeanBreweryConvention => "EBC",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(TurbidityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(TurbidityUnit unit = TurbidityUnit.NephelometricTurbidityUnits, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
