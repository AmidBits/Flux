namespace Flux.Quantities
{
  public enum MagneticFluxStrengthUnit
  {
    /// <summary>This is the default unit for <see cref="MagneticFluxStrength"/>.</summary>
    AmperePerMeter
  }

  //public readonly record struct MagneticFluxStrengthEx
  //  //: IUnitValueQuantifiable<double, MagneticFluxStrengthUnit>
  //{
  //  private readonly ElectricCurrent m_electricCurrent;
  //  private readonly Length m_length;

  //  public MagneticFluxStrengthEx(ElectricCurrent electricCurrent, Length length)
  //  {
  //    m_electricCurrent = electricCurrent;
  //    m_length = length;
  //  }

  //  public MagneticFluxStrengthEx(double electricCurrentValue, ElectricCurrentUnit electricCurrentUnit, double lengthValue, LengthUnit lengthUnit)
  //  {
  //    m_electricCurrent = new(electricCurrentValue, electricCurrentUnit);
  //    m_length = new Length(lengthValue, lengthUnit);
  //  }

  //  #region Implemented interfaces

  //  //// IComparable
  //  //public int CompareTo(object? other) => other is not null && other is MagneticFluxStrength o ? CompareTo(o) : -1;

  //  //// IComparable<>
  //  //public int CompareTo(MagneticFluxStrength other) => m_value.CompareTo(other.m_value);

  //  //// IFormattable
  //  //public string ToString(string? format, System.IFormatProvider? formatProvider)
  //  //  => ToUnitValueString(MagneticFluxStrengthUnit.AmperePerMeter, format, formatProvider);

  //  // IQuantifiable<>
  //  /// <summary>
  //  /// <para>The unit of the <see cref="MagneticFluxStrength.Value"/> property is in <see cref="MagneticFluxStrengthUnit.AmperePerMeter"/>.</para>
  //  /// </summary>
  //  public double Value => m_electricCurrent.Value / m_length.Value;

  //  ////IUnitQuantifiable<>
  //  //public double GetMetricValue(MagneticFluxStrengthUnit unit)
  //  //  => unit switch
  //  //  {
  //  //    MagneticFluxStrengthUnit.AmperePerMeter => m_value,
  //  //    _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
  //  //  };

  //  //public string ToUnitValueString(MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
  //  //{
  //  //  var sb = new System.Text.StringBuilder();
  //  //  sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
  //  //  sb.Append(unitSpacing.ToSpacingString());
  //  //  sb.Append(unit.GetUnitString(useFullName));
  //  //  return sb.ToString();
  //  //}

  //  #endregion Implemented interfaces
  //}


  /// <summary>Magnetic flux strength (H), unit of ampere per meter.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Magnetic_field"/>
  public readonly record struct MagneticFluxStrength
    : System.IComparable, System.IComparable<MagneticFluxStrength>, System.IFormattable, IUnitValueQuantifiable<double, MagneticFluxStrengthUnit>
  {
    private readonly double m_value;

    public MagneticFluxStrength(double value, MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter)
      => m_value = unit switch
      {
        MagneticFluxStrengthUnit.AmperePerMeter => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    //public MetricMultiplicative ToMetricMultiplicative() => new(GetUnitValue(MagneticFluxStrengthUnit.AmperePerMeter), MetricMultiplicativePrefix.One);

    #region Overloaded operators

    public static bool operator <(MagneticFluxStrength a, MagneticFluxStrength b) => a.CompareTo(b) < 0;
    public static bool operator <=(MagneticFluxStrength a, MagneticFluxStrength b) => a.CompareTo(b) <= 0;
    public static bool operator >(MagneticFluxStrength a, MagneticFluxStrength b) => a.CompareTo(b) > 0;
    public static bool operator >=(MagneticFluxStrength a, MagneticFluxStrength b) => a.CompareTo(b) >= 0;

    public static MagneticFluxStrength operator -(MagneticFluxStrength v) => new(-v.m_value);
    public static MagneticFluxStrength operator +(MagneticFluxStrength a, double b) => new(a.m_value + b);
    public static MagneticFluxStrength operator +(MagneticFluxStrength a, MagneticFluxStrength b) => a + b.m_value;
    public static MagneticFluxStrength operator /(MagneticFluxStrength a, double b) => new(a.m_value / b);
    public static MagneticFluxStrength operator /(MagneticFluxStrength a, MagneticFluxStrength b) => a / b.m_value;
    public static MagneticFluxStrength operator *(MagneticFluxStrength a, double b) => new(a.m_value * b);
    public static MagneticFluxStrength operator *(MagneticFluxStrength a, MagneticFluxStrength b) => a * b.m_value;
    public static MagneticFluxStrength operator %(MagneticFluxStrength a, double b) => new(a.m_value % b);
    public static MagneticFluxStrength operator %(MagneticFluxStrength a, MagneticFluxStrength b) => a % b.m_value;
    public static MagneticFluxStrength operator -(MagneticFluxStrength a, double b) => new(a.m_value - b);
    public static MagneticFluxStrength operator -(MagneticFluxStrength a, MagneticFluxStrength b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is MagneticFluxStrength o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(MagneticFluxStrength other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(MagneticFluxStrengthUnit.AmperePerMeter, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="MagneticFluxStrength.Value"/> property is in <see cref="MagneticFluxStrengthUnit.AmperePerMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    //IUnitQuantifiable<>
    public string GetUnitName(MagneticFluxStrengthUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(MagneticFluxStrengthUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.MagneticFluxStrengthUnit.AmperePerMeter => "A/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(MagneticFluxStrengthUnit unit)
      => unit switch
      {
        MagneticFluxStrengthUnit.AmperePerMeter => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
