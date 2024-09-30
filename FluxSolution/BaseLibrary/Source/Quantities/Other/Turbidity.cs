namespace Flux.Quantities
{
  public enum TurbidityUnit
  {
    /// <summary>This is the default unit for <see cref="Turbidity"/>.</summary>
    NephelometricTurbidityUnits,
  }

  /// <summary>
  /// <para>Turbidity, unit of NTU (nephelometric turbidity units).</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Turbidity"/></para>
  /// </summary>
  public readonly record struct Turbidity
    : System.IComparable, System.IComparable<Turbidity>, System.IFormattable, IUnitValueQuantifiable<double, TurbidityUnit>
  {
    private readonly double m_value;

    public Turbidity(double value, TurbidityUnit unit = TurbidityUnit.NephelometricTurbidityUnits) => m_value = ConvertFromUnit(unit, value);

    #region Static methods

    public static Turbidity From(Force force, Time time)
      => new(force.Value / time.Value);
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

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Turbidity.Value"/> property is in <see cref="TurbidityUnit.TurbidityUnit"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region IUnitQuantifiable<>

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

    public static double GetUnitFactor(TurbidityUnit unit)
      => unit switch
      {
        TurbidityUnit.NephelometricTurbidityUnits => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(TurbidityUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(TurbidityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.TurbidityUnit.NephelometricTurbidityUnits => "NTU",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(TurbidityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(TurbidityUnit unit = TurbidityUnit.NephelometricTurbidityUnits, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
