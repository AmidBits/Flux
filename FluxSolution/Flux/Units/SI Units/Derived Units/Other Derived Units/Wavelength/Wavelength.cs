namespace Flux.Units
{
  /// <summary>
  /// <para>Wavelength, unit of meter per radian.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Wavelength"/></para>
  /// </summary>
  public readonly record struct Wavelength
    : System.IComparable, System.IComparable<Wavelength>, System.IFormattable, ISiUnitValueQuantifiable<double, WavelengthUnit>
  {
    private readonly double m_value;

    public Wavelength(double value, WavelengthUnit unit = WavelengthUnit.MeterPerRadian) => m_value = ConvertToUnit(unit, value);

    public Wavelength(MetricPrefix prefix, double meterPerRadian) => m_value = prefix.ConvertPrefix(meterPerRadian, MetricPrefix.Unprefixed);

    public Wavelength(Speed phaseSpeed, Frequency frequency) : this(phaseSpeed.Value / frequency.Value) { }

    public Wavelength(Speed phaseSpeed, AngularFrequency angularFrequency) : this(2 * double.Pi * phaseSpeed.Value / angularFrequency.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Wavelength a, Wavelength b) => a.CompareTo(b) < 0;
    public static bool operator >(Wavelength a, Wavelength b) => a.CompareTo(b) > 0;
    public static bool operator <=(Wavelength a, Wavelength b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Wavelength a, Wavelength b) => a.CompareTo(b) >= 0;

    public static Wavelength operator -(Wavelength v) => new(-v.m_value);
    public static Wavelength operator *(Wavelength a, Wavelength b) => new(a.m_value * b.m_value);
    public static Wavelength operator /(Wavelength a, Wavelength b) => new(a.m_value / b.m_value);
    public static Wavelength operator %(Wavelength a, Wavelength b) => new(a.m_value % b.m_value);
    public static Wavelength operator +(Wavelength a, Wavelength b) => new(a.m_value + b.m_value);
    public static Wavelength operator -(Wavelength a, Wavelength b) => new(a.m_value - b.m_value);
    public static Wavelength operator *(Wavelength a, double b) => new(a.m_value * b);
    public static Wavelength operator /(Wavelength a, double b) => new(a.m_value / b);
    public static Wavelength operator %(Wavelength a, double b) => new(a.m_value % b);
    public static Wavelength operator +(Wavelength a, double b) => new(a.m_value + b);
    public static Wavelength operator -(Wavelength a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Wavelength o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Wavelength other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(WavelengthUnit.MeterPerRadian, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + WavelengthUnit.MeterPerRadian.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(WavelengthUnit unit, double value)
      => unit switch
      {
        WavelengthUnit.MeterPerRadian => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(WavelengthUnit unit, double value)
      => unit switch
      {
        WavelengthUnit.MeterPerRadian => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, WavelengthUnit from, WavelengthUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(WavelengthUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(WavelengthUnit unit = WavelengthUnit.MeterPerRadian, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(INumber.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Wavelength.Value"/> property is in <see cref="WavelengthUnit.MeterPerRadian"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
