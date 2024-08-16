namespace Flux.Quantities
{
  public enum LinearDensityUnit
  {
    /// <summary>This is the default unit for <see cref="LinearDensity"/>.</summary>
    KilogramPerMeter,
  }

  /// <summary>
  /// <para>Linear mass density, unit of kilograms per meter.</para>
  /// <see href="https://en.wikipedia.org/wiki/Linear_density"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="Density"/>.</remarks>
  public readonly record struct LinearDensity
    : System.IComparable, System.IComparable<LinearDensity>, System.IFormattable, IUnitValueQuantifiable<double, LinearDensityUnit>
  {
    private readonly double m_value;

    public LinearDensity(double value, LinearDensityUnit unit = LinearDensityUnit.KilogramPerMeter)
      => m_value = unit switch
      {
        LinearDensityUnit.KilogramPerMeter => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public LinearDensity(Mass mass, Volume volume) : this(mass.Value / volume.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(LinearDensity a, LinearDensity b) => a.CompareTo(b) < 0;
    public static bool operator <=(LinearDensity a, LinearDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >(LinearDensity a, LinearDensity b) => a.CompareTo(b) > 0;
    public static bool operator >=(LinearDensity a, LinearDensity b) => a.CompareTo(b) >= 0;

    public static LinearDensity operator -(LinearDensity v) => new(-v.m_value);
    public static LinearDensity operator +(LinearDensity a, double b) => new(a.m_value + b);
    public static LinearDensity operator +(LinearDensity a, LinearDensity b) => a + b.m_value;
    public static LinearDensity operator /(LinearDensity a, double b) => new(a.m_value / b);
    public static LinearDensity operator /(LinearDensity a, LinearDensity b) => a / b.m_value;
    public static LinearDensity operator *(LinearDensity a, double b) => new(a.m_value * b);
    public static LinearDensity operator *(LinearDensity a, LinearDensity b) => a * b.m_value;
    public static LinearDensity operator %(LinearDensity a, double b) => new(a.m_value % b);
    public static LinearDensity operator %(LinearDensity a, LinearDensity b) => a % b.m_value;
    public static LinearDensity operator -(LinearDensity a, double b) => new(a.m_value - b);
    public static LinearDensity operator -(LinearDensity a, LinearDensity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is LinearDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(LinearDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(LinearDensityUnit.KilogramPerMeter, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="LinearDensity.Value"/> property is in <see cref="LinearDensityUnit.KilogramPerCubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(LinearDensityUnit unit, bool preferPlural)
      => unit.ToString() + GetUnitValue(unit).PluralStringSuffix();

    public string GetUnitSymbol(LinearDensityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.LinearDensityUnit.KilogramPerMeter => "kg/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(LinearDensityUnit unit)
      => unit switch
      {
        LinearDensityUnit.KilogramPerMeter => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(LinearDensityUnit unit = LinearDensityUnit.KilogramPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitName(unit, preferPlural));

    public string ToUnitValueSymbolString(LinearDensityUnit unit = LinearDensityUnit.KilogramPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitSymbol(unit, preferUnicode));

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
