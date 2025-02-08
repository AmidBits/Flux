namespace Flux.Units
{
  /// <summary>
  /// <para>Magnetic flux density (B), unit of tesla.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Magnetic_flux_density"/></para>
  /// </summary>
  public readonly record struct MagneticFluxDensity
    : System.IComparable, System.IComparable<MagneticFluxDensity>, System.IFormattable, ISiUnitValueQuantifiable<double, MagneticFluxDensityUnit>
  {
    private readonly double m_value;

    public MagneticFluxDensity(double value, MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla) => m_value = ConvertFromUnit(unit, value);

    public MagneticFluxDensity(MetricPrefix prefix, double tesla) => m_value = prefix.ConvertTo(tesla, MetricPrefix.Unprefixed);

    /// <summary>
    /// <para>Creates a new magnetic flux density from the length (magnitude) of <paramref name="vector"/> and <paramref name="unit"/>.</para>
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="unit"></param>
    public MagneticFluxDensity(System.Numerics.Vector2 vector, MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla) : this(vector.Length(), unit) { }

    /// <summary>
    /// <para>Creates a new magnetic flux density from the length (magnitude) of <paramref name="vector"/> and <paramref name="unit"/>.</para>
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="unit"></param>
    public MagneticFluxDensity(System.Numerics.Vector3 vector, MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla) : this(vector.Length(), unit) { }

    #region Overloaded operators

    public static bool operator <(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) < 0;
    public static bool operator >(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) > 0;
    public static bool operator <=(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) >= 0;

    public static MagneticFluxDensity operator -(MagneticFluxDensity v) => new(-v.m_value);
    public static MagneticFluxDensity operator *(MagneticFluxDensity a, MagneticFluxDensity b) => new(a.m_value * b.m_value);
    public static MagneticFluxDensity operator /(MagneticFluxDensity a, MagneticFluxDensity b) => new(a.m_value / b.m_value);
    public static MagneticFluxDensity operator %(MagneticFluxDensity a, MagneticFluxDensity b) => new(a.m_value % b.m_value);
    public static MagneticFluxDensity operator +(MagneticFluxDensity a, MagneticFluxDensity b) => new(a.m_value + b.m_value);
    public static MagneticFluxDensity operator -(MagneticFluxDensity a, MagneticFluxDensity b) => new(a.m_value - b.m_value);
    public static MagneticFluxDensity operator *(MagneticFluxDensity a, double b) => new(a.m_value * b);
    public static MagneticFluxDensity operator /(MagneticFluxDensity a, double b) => new(a.m_value / b);
    public static MagneticFluxDensity operator %(MagneticFluxDensity a, double b) => new(a.m_value % b);
    public static MagneticFluxDensity operator +(MagneticFluxDensity a, double b) => new(a.m_value + b);
    public static MagneticFluxDensity operator -(MagneticFluxDensity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is MagneticFluxDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(MagneticFluxDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiPrefixValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(MagneticFluxDensityUnit.Tesla, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(MagneticFluxDensityUnit.Tesla, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(MagneticFluxDensityUnit unit, double value)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(MagneticFluxDensityUnit unit, double value)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, MagneticFluxDensityUnit from, MagneticFluxDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(MagneticFluxDensityUnit unit)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(MagneticFluxDensityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(MagneticFluxDensityUnit unit, bool preferUnicode)
      => unit switch
      {
        Units.MagneticFluxDensityUnit.Tesla => "T",

        //Quantities.MagneticFluxDensityUnit.KilogramPerSquareSecond => "kg/s²",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(MagneticFluxDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="MagneticFluxDensity.Value"/> property is in <see cref="MagneticFluxDensityUnit.Tesla"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
