namespace Flux.Quantities
{
  public enum MagneticFluxDensityUnit
  {
    /// <summary>This is the default unit for <see cref="MagneticFluxDensity"/>.</summary>
    Tesla,
    KilogramPerSquareSecond
  }

  /// <summary>Magnetic flux density (B), unit of tesla.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
  public readonly record struct MagneticFluxDensity
    : System.IComparable, System.IComparable<MagneticFluxDensity>, System.IFormattable, IUnitValueQuantifiable<double, MagneticFluxDensityUnit>
  {
    private readonly double m_value;

    public MagneticFluxDensity(double value, MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla)
      => m_value = unit switch
      {
        MagneticFluxDensityUnit.Tesla => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

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
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(MagneticFluxDensityUnit.Tesla, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="MagneticFluxDensity.Value"/> property is in <see cref="MagneticFluxDensityUnit.Tesla"/>.</para>
    /// </summary>
    public double Value => m_value;

    //IUnitQuantifiable<>
    public string GetUnitName(MagneticFluxDensityUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(MagneticFluxDensityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.MagneticFluxDensityUnit.Tesla => "T",
        Quantities.MagneticFluxDensityUnit.KilogramPerSquareSecond => "kg/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(MagneticFluxDensityUnit unit)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitName(unit, preferPlural));

    public string ToUnitValueSymbolString(MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitSymbol(unit, preferUnicode));

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
