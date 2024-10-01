namespace Flux.Quantities
{
  public enum MagneticFluxDensityUnit
  {
    /// <summary>This is the default unit for <see cref="MagneticFluxDensity"/>.</summary>
    Tesla,
  }

  /// <summary>Magnetic flux density (B), unit of tesla.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
  public readonly record struct MagneticFluxDensity
    : System.IComparable, System.IComparable<MagneticFluxDensity>, System.IFormattable, ISiPrefixValueQuantifiable<double, MagneticFluxDensityUnit>
  {
    private readonly double m_value;

    public MagneticFluxDensity(double value, MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla) => m_value = ConvertFromUnit(unit, value);

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
    public static bool operator <=(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) > 0;
    public static bool operator >=(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) >= 0;

    public static MagneticFluxDensity operator -(MagneticFluxDensity v) => new(-v.m_value);
    public static MagneticFluxDensity operator +(MagneticFluxDensity a, double b) => new(a.m_value + b);
    public static MagneticFluxDensity operator +(MagneticFluxDensity a, MagneticFluxDensity b) => a + b.m_value;
    public static MagneticFluxDensity operator /(MagneticFluxDensity a, double b) => new(a.m_value / b);
    public static MagneticFluxDensity operator /(MagneticFluxDensity a, MagneticFluxDensity b) => a / b.m_value;
    public static MagneticFluxDensity operator *(MagneticFluxDensity a, double b) => new(a.m_value * b);
    public static MagneticFluxDensity operator *(MagneticFluxDensity a, MagneticFluxDensity b) => a * b.m_value;
    public static MagneticFluxDensity operator %(MagneticFluxDensity a, double b) => new(a.m_value % b);
    public static MagneticFluxDensity operator %(MagneticFluxDensity a, MagneticFluxDensity b) => a % b.m_value;
    public static MagneticFluxDensity operator -(MagneticFluxDensity a, double b) => new(a.m_value - b);
    public static MagneticFluxDensity operator -(MagneticFluxDensity a, MagneticFluxDensity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is MagneticFluxDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(MagneticFluxDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="MagneticFluxDensity.Value"/> property is in <see cref="MagneticFluxDensityUnit.Tesla"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiPrefixValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(MagneticFluxDensityUnit.Tesla, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(MagneticFluxDensityUnit.Tesla, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitQuantifiable<>

    private static double ConvertFromUnit(MagneticFluxDensityUnit unit, double value)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(MagneticFluxDensityUnit unit, double value)
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

    public string GetUnitName(MagneticFluxDensityUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(MagneticFluxDensityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.MagneticFluxDensityUnit.Tesla => "T",
        //Quantities.MagneticFluxDensityUnit.KilogramPerSquareSecond => "kg/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(MagneticFluxDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
