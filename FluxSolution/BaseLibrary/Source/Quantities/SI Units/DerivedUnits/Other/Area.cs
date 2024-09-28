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

    public Area(double value, AreaUnit unit = AreaUnit.SquareMeter) => m_value = ConvertFromUnit(unit, value);
    //=> m_value = unit switch
    //{
    //  AreaUnit.SquareMeter => value,
    //  AreaUnit.Hectare => ConvertHectareToSquareMeter(value),
    //  _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
    //};

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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AreaUnit.SquareMeter, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Area.Value"/> property is in <see cref="AreaUnit.SquareMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => GetUnitName(AreaUnit.SquareMeter, preferPlural).Insert(6, prefix.GetPrefixName());

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(AreaUnit.SquareMeter, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix, 2);

    public string ToSiPrefixValueNameString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    public static double ConvertFromUnit(AreaUnit unit, double value)
      => unit switch
      {
        AreaUnit.SquareMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(AreaUnit unit, double value)
      => unit switch
      {
        AreaUnit.SquareMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double GetUnitFactor(AreaUnit unit)
      => unit switch
      {
        AreaUnit.SquareMeter => 1,

        AreaUnit.Hectare => 10000,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(AreaUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(AreaUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.AreaUnit.SquareMeter => preferUnicode ? "\u33A1" : "m²",
        Quantities.AreaUnit.Hectare => preferUnicode ? "\u33CA" : "ha",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AreaUnit unit) => ConvertToUnit(unit, m_value);
    //=> unit switch
    //{
    //  AreaUnit.SquareMeter => m_value,
    //  AreaUnit.Hectare => ConvertSquareMeterToHectare(m_value),
    //  _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
    //};

    public string ToUnitString(AreaUnit unit = AreaUnit.SquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    //public string ToUnitValueNameString(AreaUnit unit = AreaUnit.SquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    //public string ToUnitValueSymbolString(AreaUnit unit = AreaUnit.SquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //=> GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
