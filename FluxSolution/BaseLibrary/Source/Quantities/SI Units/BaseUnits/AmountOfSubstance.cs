namespace Flux.Quantities
{
  public enum AmountOfSubstanceUnit
  {
    Mole,
  }

  /// <summary>
  /// <para>Amount of substance. SI unit of mole. This is a base quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Amount_of_substance"/></para>
  /// </summary>
  public readonly record struct AmountOfSubstance
    : System.IComparable, System.IComparable<AmountOfSubstance>, System.IFormattable, ISiPrefixValueQuantifiable<double, AmountOfSubstanceUnit>
  {
    /// <summary>The exact number of elementary entities in one mole.</summary>
    public const double AvogadroNumber = 6.02214076e23;

    /// <summary>The dimension of the Avagadro constant is the reciprocal of amount of substance.</summary>
    public const double AvogadroConstant = 1 / AvogadroNumber;

    private readonly double m_value;

    public AmountOfSubstance(double value, AmountOfSubstanceUnit unit = AmountOfSubstanceUnit.Mole)
      => m_value = unit switch
      {
        AmountOfSubstanceUnit.Mole => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    /// <summary>
    /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="AmountOfSubstanceUnit.Mole"/>, e.g. <see cref="MetricPrefix.Yocto"/> for yoctomoles.</para>
    /// </summary>
    /// <param name="moles"></param>
    /// <param name="prefix"></param>
    public AmountOfSubstance(double moles, MetricPrefix prefix) => m_value = prefix.Convert(moles, MetricPrefix.NoPrefix);

    public double NumberOfParticles => m_value * AvogadroNumber;

    #region Static methods
    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(AmountOfSubstance a, AmountOfSubstance b) => a.CompareTo(b) < 0;
    public static bool operator <=(AmountOfSubstance a, AmountOfSubstance b) => a.CompareTo(b) <= 0;
    public static bool operator >(AmountOfSubstance a, AmountOfSubstance b) => a.CompareTo(b) > 0;
    public static bool operator >=(AmountOfSubstance a, AmountOfSubstance b) => a.CompareTo(b) >= 0;

    public static AmountOfSubstance operator -(AmountOfSubstance v) => new(-v.Value);
    public static AmountOfSubstance operator +(AmountOfSubstance a, double b) => new(a.m_value + b);
    public static AmountOfSubstance operator +(AmountOfSubstance a, AmountOfSubstance b) => a + b.m_value;
    public static AmountOfSubstance operator /(AmountOfSubstance a, double b) => new(a.m_value / b);
    public static AmountOfSubstance operator /(AmountOfSubstance a, AmountOfSubstance b) => a / b.m_value;
    public static AmountOfSubstance operator *(AmountOfSubstance a, double b) => new(a.m_value * b);
    public static AmountOfSubstance operator *(AmountOfSubstance a, AmountOfSubstance b) => a * b.m_value;
    public static AmountOfSubstance operator %(AmountOfSubstance a, double b) => new(a.m_value % b);
    public static AmountOfSubstance operator %(AmountOfSubstance a, AmountOfSubstance b) => a % b.m_value;
    public static AmountOfSubstance operator -(AmountOfSubstance a, double b) => new(a.m_value - b);
    public static AmountOfSubstance operator -(AmountOfSubstance a, AmountOfSubstance b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is AmountOfSubstance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(AmountOfSubstance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixValueSymbolString(MetricPrefix.NoPrefix, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AmountOfSubstance.Value"/> property is in <see cref="AmountOfSubstanceUnit.Mole"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetUnitName() + GetUnitName(AmountOfSubstanceUnit.Mole, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetUnitSymbol(preferUnicode) + GetUnitSymbol(AmountOfSubstanceUnit.Mole, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

    public string ToSiPrefixValueNameString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    public string GetUnitName(AmountOfSubstanceUnit unit, bool preferPlural) => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(AmountOfSubstanceUnit unit, bool preferUnicode)
      => unit switch
      {
        AmountOfSubstanceUnit.Mole => preferUnicode ? "\u33D6" : "mol",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AmountOfSubstanceUnit unit)
      => unit switch
      {
        AmountOfSubstanceUnit.Mole => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(AmountOfSubstanceUnit unit = AmountOfSubstanceUnit.Mole, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(AmountOfSubstanceUnit unit = AmountOfSubstanceUnit.Mole, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
