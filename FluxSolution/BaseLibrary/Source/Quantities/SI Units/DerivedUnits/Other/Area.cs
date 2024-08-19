namespace Flux.Quantities
{
  public enum AreaUnit
  {
    /// <summary>This is the default unit for <see cref="Area"/>.</summary>
    SquareMeter,
    Hectare,
  }

  /// <summary>
  /// <para>Area, unit of square meter. This is an SI derived quantity.</para>
  /// <see href="https://en.wikipedia.org/wiki/Area"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="Length"/>, <see cref="Area"/> and <see cref="Volume"/>.</remarks>
  public readonly record struct Area
    : System.IComparable, System.IComparable<Area>, System.IFormattable, IUnitValueQuantifiable<double, AreaUnit>
  {
    private readonly double m_value;

    public Area(double value, AreaUnit unit = AreaUnit.SquareMeter)
      => m_value = unit switch
      {
        AreaUnit.SquareMeter => value,
        AreaUnit.Hectare => ConvertHectareToSquareMeter(value),
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods

    #region Conversions

    public static double ConvertHectareToSquareMeter(double hectare) => hectare * 10000;

    public static double ConvertSquareMeterToHectare(double squareMeter) => squareMeter / 10000;

    #endregion // Conversions

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Area a, Area b) => a.CompareTo(b) < 0;
    public static bool operator <=(Area a, Area b) => a.CompareTo(b) <= 0;
    public static bool operator >(Area a, Area b) => a.CompareTo(b) > 0;
    public static bool operator >=(Area a, Area b) => a.CompareTo(b) >= 0;

    public static Area operator -(Area v) => new(-v.m_value);
    public static Area operator +(Area a, double b) => new(a.m_value + b);
    public static Area operator +(Area a, Area b) => a + b.m_value;
    public static Area operator /(Area a, double b) => new(a.m_value / b);
    public static Area operator /(Area a, Area b) => a / b.m_value;
    public static Area operator *(Area a, double b) => new(a.m_value * b);
    public static Area operator *(Area a, Area b) => a * b.m_value;
    public static Area operator %(Area a, double b) => new(a.m_value % b);
    public static Area operator %(Area a, Area b) => a % b.m_value;
    public static Area operator -(Area a, double b) => new(a.m_value - b);
    public static Area operator -(Area a, Area b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Area o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Area other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(AreaUnit.SquareMeter, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="Area.Value"/> property is in <see cref="AreaUnit.SquareMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(AreaUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(AreaUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.AreaUnit.SquareMeter => preferUnicode ? "\u33A1" : "m²",
        Quantities.AreaUnit.Hectare => preferUnicode ? "\u33CA" : "ha",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AreaUnit unit)
      => unit switch
      {
        AreaUnit.SquareMeter => m_value,
        AreaUnit.Hectare => ConvertSquareMeterToHectare(m_value),
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(AreaUnit unit = AreaUnit.SquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitName(unit, preferPlural));

    public string ToUnitValueSymbolString(AreaUnit unit = AreaUnit.SquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitSymbol(unit, preferUnicode));

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
