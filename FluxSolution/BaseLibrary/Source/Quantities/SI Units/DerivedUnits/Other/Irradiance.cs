namespace Flux.Quantities
{
  public enum IrradianceUnit
  {
    /// <summary>This is the default unit for <see cref="Irradiance"/>.</summary>
    WattPerSquareMeter,
  }

  /// <summary>
  /// <para>Irradiance, unit of watt per square meter.</para>
  /// <see href="https://en.wikipedia.org/wiki/Irradiance"/>
  /// </summary>
  public readonly record struct Irradiance
    : System.IComparable, System.IComparable<Irradiance>, System.IFormattable, IUnitValueQuantifiable<double, IrradianceUnit>
  {
    private readonly double m_value;

    public Irradiance(double value, IrradianceUnit unit = IrradianceUnit.WattPerSquareMeter)
      => m_value = unit switch
      {
        IrradianceUnit.WattPerSquareMeter => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public Irradiance(Power power, Area area) : this(power.Value / area.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Irradiance a, Irradiance b) => a.CompareTo(b) < 0;
    public static bool operator <=(Irradiance a, Irradiance b) => a.CompareTo(b) <= 0;
    public static bool operator >(Irradiance a, Irradiance b) => a.CompareTo(b) > 0;
    public static bool operator >=(Irradiance a, Irradiance b) => a.CompareTo(b) >= 0;

    public static Irradiance operator -(Irradiance v) => new(-v.m_value);
    public static Irradiance operator +(Irradiance a, double b) => new(a.m_value + b);
    public static Irradiance operator +(Irradiance a, Irradiance b) => a + b.m_value;
    public static Irradiance operator /(Irradiance a, double b) => new(a.m_value / b);
    public static Irradiance operator /(Irradiance a, Irradiance b) => a / b.m_value;
    public static Irradiance operator *(Irradiance a, double b) => new(a.m_value * b);
    public static Irradiance operator *(Irradiance a, Irradiance b) => a * b.m_value;
    public static Irradiance operator %(Irradiance a, double b) => new(a.m_value % b);
    public static Irradiance operator %(Irradiance a, Irradiance b) => a % b.m_value;
    public static Irradiance operator -(Irradiance a, double b) => new(a.m_value - b);
    public static Irradiance operator -(Irradiance a, Irradiance b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Irradiance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Irradiance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(IrradianceUnit.WattPerSquareMeter, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="Irradiance.Value"/> property is in <see cref="IrradianceUnit.WattPerSquareMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(IrradianceUnit unit, bool preferPlural)
      => unit.ToString() + GetUnitValue(unit).PluralStringSuffix();

    public string GetUnitSymbol(IrradianceUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.IrradianceUnit.WattPerSquareMeter => "W/m²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(IrradianceUnit unit)
      => unit switch
      {
        IrradianceUnit.WattPerSquareMeter => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(IrradianceUnit unit = IrradianceUnit.WattPerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitName(unit, preferPlural));

    public string ToUnitValueSymbolString(IrradianceUnit unit = IrradianceUnit.WattPerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitSymbol(unit, preferUnicode));

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
