namespace Flux.Quantities
{
  public enum DensityUnit
  {
    /// <summary>This is the default unit for <see cref="Density"/>.</summary>
    KilogramPerCubicMeter,
  }

  /// <summary>
  /// <para>Volumetric mass density, unit of kilograms per cubic meter.</para>
  /// <see href="https://en.wikipedia.org/wiki/Density"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="Density"/>.</remarks>
  public readonly record struct Density
    : System.IComparable, System.IComparable<Density>, System.IFormattable, IUnitValueQuantifiable<double, DensityUnit>
  {
    private readonly double m_value;

    public Density(double value, DensityUnit unit = DensityUnit.KilogramPerCubicMeter) => m_value = ConvertFromUnit(unit, value);

    public Density(Mass mass, Volume volume) : this(mass.Value / volume.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Density a, Density b) => a.CompareTo(b) < 0;
    public static bool operator <=(Density a, Density b) => a.CompareTo(b) <= 0;
    public static bool operator >(Density a, Density b) => a.CompareTo(b) > 0;
    public static bool operator >=(Density a, Density b) => a.CompareTo(b) >= 0;

    public static Density operator -(Density v) => new(-v.m_value);
    public static Density operator +(Density a, double b) => new(a.m_value + b);
    public static Density operator +(Density a, Density b) => a + b.m_value;
    public static Density operator /(Density a, double b) => new(a.m_value / b);
    public static Density operator /(Density a, Density b) => a / b.m_value;
    public static Density operator *(Density a, double b) => new(a.m_value * b);
    public static Density operator *(Density a, Density b) => a * b.m_value;
    public static Density operator %(Density a, double b) => new(a.m_value % b);
    public static Density operator %(Density a, Density b) => a % b.m_value;
    public static Density operator -(Density a, double b) => new(a.m_value - b);
    public static Density operator -(Density a, Density b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Density o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Density other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(DensityUnit.KilogramPerCubicMeter, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Density.Value"/> property is in <see cref="DensityUnit.KilogramPerCubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region IUnitQuantifiable<>

    public static double ConvertFromUnit(DensityUnit unit, double value)
      => unit switch
      {
        DensityUnit.KilogramPerCubicMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(DensityUnit unit, double value)
      => unit switch
      {
        DensityUnit.KilogramPerCubicMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double GetUnitFactor(DensityUnit unit)
      => unit switch
      {
        DensityUnit.KilogramPerCubicMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(DensityUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(DensityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.DensityUnit.KilogramPerCubicMeter => "kg/m³",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(DensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(DensityUnit unit = DensityUnit.KilogramPerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
