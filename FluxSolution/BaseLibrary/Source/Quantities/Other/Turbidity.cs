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

    public Turbidity(double value, TurbidityUnit unit = TurbidityUnit.NephelometricTurbidityUnits)
      => m_value = unit switch
      {
        TurbidityUnit.NephelometricTurbidityUnits => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

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
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(TurbidityUnit.NephelometricTurbidityUnits, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="Turbidity.Value"/> property is in <see cref="TurbidityUnit.TurbidityUnit"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(TurbidityUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(TurbidityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.TurbidityUnit.NephelometricTurbidityUnits => "NTU",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(TurbidityUnit unit)
      => unit switch
      {
        TurbidityUnit.NephelometricTurbidityUnits => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(TurbidityUnit unit = TurbidityUnit.NephelometricTurbidityUnits, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(TurbidityUnit unit = TurbidityUnit.NephelometricTurbidityUnits, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
