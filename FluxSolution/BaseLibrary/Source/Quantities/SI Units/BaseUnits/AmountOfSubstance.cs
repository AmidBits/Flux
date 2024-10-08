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
    : System.IComparable, System.IComparable<AmountOfSubstance>, System.IFormattable, ISiUnitValueQuantifiable<double, AmountOfSubstanceUnit>
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
    public AmountOfSubstance(double moles, MetricPrefix prefix) => m_value = prefix.ConvertTo(moles, MetricPrefix.Unprefixed);

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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AmountOfSubstance.Value"/> property is in <see cref="AmountOfSubstanceUnit.Mole"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(AmountOfSubstanceUnit.Mole, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(AmountOfSubstanceUnit.Mole, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    private static double ConvertFromUnit(AmountOfSubstanceUnit unit, double value)
      => unit switch
      {
        AmountOfSubstanceUnit.Mole => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(AmountOfSubstanceUnit unit, double value)
      => unit switch
      {
        AmountOfSubstanceUnit.Mole => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, AmountOfSubstanceUnit from, AmountOfSubstanceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(AmountOfSubstanceUnit unit)
      => unit switch
      {
        AmountOfSubstanceUnit.Mole => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(AmountOfSubstanceUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

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

    public string ToUnitString(AmountOfSubstanceUnit unit = AmountOfSubstanceUnit.Mole, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
