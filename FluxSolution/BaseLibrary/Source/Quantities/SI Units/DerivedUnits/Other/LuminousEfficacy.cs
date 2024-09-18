namespace Flux.Quantities
{
  public enum LuminousEfficacyUnit
  {
    /// <summary>This is the default unit for <see cref="LuminousEfficacy"/>.</summary>
    LumensPerWatt,
  }

  /// <summary>Torque unit of newton meter.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Torque"/>
  public readonly record struct LuminousEfficacy
    : System.IComparable, System.IComparable<LuminousEfficacy>, System.IFormattable, IUnitValueQuantifiable<double, LuminousEfficacyUnit>
  {
    public static readonly LuminousEfficacy LuminousEfficacyOf540THzRadiation = new(683);

    private readonly double m_value;

    public LuminousEfficacy(double value, LuminousEfficacyUnit unit = LuminousEfficacyUnit.LumensPerWatt)
      => m_value = unit switch
      {
        LuminousEfficacyUnit.LumensPerWatt => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public LuminousEfficacy(Energy energy, Angle angle) : this(energy.Value / angle.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) < 0;
    public static bool operator <=(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) <= 0;
    public static bool operator >(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) > 0;
    public static bool operator >=(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) >= 0;

    public static LuminousEfficacy operator -(LuminousEfficacy v) => new(-v.m_value);
    public static LuminousEfficacy operator +(LuminousEfficacy a, double b) => new(a.m_value + b);
    public static LuminousEfficacy operator +(LuminousEfficacy a, LuminousEfficacy b) => a + b.m_value;
    public static LuminousEfficacy operator /(LuminousEfficacy a, double b) => new(a.m_value / b);
    public static LuminousEfficacy operator /(LuminousEfficacy a, LuminousEfficacy b) => a / b.m_value;
    public static LuminousEfficacy operator *(LuminousEfficacy a, double b) => new(a.m_value * b);
    public static LuminousEfficacy operator *(LuminousEfficacy a, LuminousEfficacy b) => a * b.m_value;
    public static LuminousEfficacy operator %(LuminousEfficacy a, double b) => new(a.m_value % b);
    public static LuminousEfficacy operator %(LuminousEfficacy a, LuminousEfficacy b) => a % b.m_value;
    public static LuminousEfficacy operator -(LuminousEfficacy a, double b) => new(a.m_value - b);
    public static LuminousEfficacy operator -(LuminousEfficacy a, LuminousEfficacy b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is LuminousEfficacy o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(LuminousEfficacy other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(LuminousEfficacyUnit.LumensPerWatt, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="LuminousEfficacy.Value"/> property is in <see cref="LuminousEfficacyUnit.LumensPerWatt"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(LuminousEfficacyUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(LuminousEfficacyUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.LuminousEfficacyUnit.LumensPerWatt => "lm/W",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(LuminousEfficacyUnit unit)
      => unit switch
      {
        LuminousEfficacyUnit.LumensPerWatt => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(LuminousEfficacyUnit unit = LuminousEfficacyUnit.LumensPerWatt, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider)+ unitSpacing.ToSpacingString()+ GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(LuminousEfficacyUnit unit = LuminousEfficacyUnit.LumensPerWatt, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider)+ unitSpacing.ToSpacingString()+ GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
