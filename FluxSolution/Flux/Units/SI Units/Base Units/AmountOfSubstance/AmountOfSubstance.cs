namespace Flux.Units
{
  /// <summary>
  /// <para>Amount of substance. SI unit of mole. This is a base quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Amount_of_substance"/></para>
  /// </summary>
  public readonly record struct AmountOfSubstance
    : System.IComparable, System.IComparable<AmountOfSubstance>, System.IEquatable<AmountOfSubstance>, System.IFormattable, ISiUnitValueQuantifiable<double, AmountOfSubstanceUnit>
  {
    /// <summary>
    /// <para>The exact number of elementary entities in one mole.</para>
    /// <para>The Avogadro number, sometimes denoted N0, is the numeric value of the Avogadro constant (i.e., without a unit), namely the dimensionless number 6.02214076e23; the value chosen based on the number of atoms in 12 grams of carbon-12 in alignment with the historical definition of a mole.</para>
    ///// <para><see href="https://en.wikipedia.org/wiki/Avogadro_constant"/></para>
    /// </summary>
    public const double AvogadrosNumber = 6.02214076e23;

    private readonly double m_mole;

    public AmountOfSubstance(double value, AmountOfSubstanceUnit unit = AmountOfSubstanceUnit.Mole)
      => m_mole = unit switch
      {
        AmountOfSubstanceUnit.Mole => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    /// <summary>
    /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="AmountOfSubstanceUnit.Mole"/>, e.g. <see cref="MetricPrefix.Yocto"/> for yoctomoles.</para>
    /// </summary>
    /// <param name="mole"></param>
    /// <param name="prefix"></param>
    public AmountOfSubstance(MetricPrefix prefix, double mole) => m_mole = prefix.ConvertPrefix(mole, MetricPrefix.Unprefixed);

    public double NumberOfParticles => m_mole * AvogadrosNumber;

    #region Static methods
    #endregion // Static methods

    #region Overloaded operators

    public static explicit operator AmountOfSubstance(double value) => new(value);
    public static implicit operator double(AmountOfSubstance value) => value.m_mole;

    public static bool operator <(AmountOfSubstance a, AmountOfSubstance b) => a.CompareTo(b) < 0;
    public static bool operator >(AmountOfSubstance a, AmountOfSubstance b) => a.CompareTo(b) > 0;
    public static bool operator <=(AmountOfSubstance a, AmountOfSubstance b) => a.CompareTo(b) <= 0;
    public static bool operator >=(AmountOfSubstance a, AmountOfSubstance b) => a.CompareTo(b) >= 0;

    public static AmountOfSubstance operator -(AmountOfSubstance v) => new(-v.m_mole);
    public static AmountOfSubstance operator *(AmountOfSubstance a, AmountOfSubstance b) => new(a.m_mole * b.m_mole);
    public static AmountOfSubstance operator /(AmountOfSubstance a, AmountOfSubstance b) => new(a.m_mole / b.m_mole);
    public static AmountOfSubstance operator %(AmountOfSubstance a, AmountOfSubstance b) => new(a.m_mole % b.m_mole);
    public static AmountOfSubstance operator +(AmountOfSubstance a, AmountOfSubstance b) => new(a.m_mole + b.m_mole);
    public static AmountOfSubstance operator -(AmountOfSubstance a, AmountOfSubstance b) => new(a.m_mole - b.m_mole);
    public static AmountOfSubstance operator *(AmountOfSubstance a, double b) => new(a.m_mole * b);
    public static AmountOfSubstance operator /(AmountOfSubstance a, double b) => new(a.m_mole / b);
    public static AmountOfSubstance operator %(AmountOfSubstance a, double b) => new(a.m_mole % b);
    public static AmountOfSubstance operator +(AmountOfSubstance a, double b) => new(a.m_mole + b);
    public static AmountOfSubstance operator -(AmountOfSubstance a, double b) => new(a.m_mole - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is AmountOfSubstance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(AmountOfSubstance other) => m_mole.CompareTo(other.m_mole);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_mole, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + AmountOfSubstanceUnit.Mole.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(AmountOfSubstanceUnit unit, double value)
      => unit switch
      {
        AmountOfSubstanceUnit.Mole => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(AmountOfSubstanceUnit unit, double value)
      => unit switch
      {
        AmountOfSubstanceUnit.Mole => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, AmountOfSubstanceUnit from, AmountOfSubstanceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(AmountOfSubstanceUnit unit)
      => unit switch
      {
        AmountOfSubstanceUnit.Mole => m_mole,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitString(AmountOfSubstanceUnit unit = AmountOfSubstanceUnit.Mole, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(Number.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AmountOfSubstance.Value"/> property is in <see cref="AmountOfSubstanceUnit.Mole"/>.</para>
    /// </summary>
    public double Value => m_mole;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
