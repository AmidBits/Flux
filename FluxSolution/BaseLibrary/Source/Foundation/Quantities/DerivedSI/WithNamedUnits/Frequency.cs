namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this FrequencyUnit unit, bool useNameInsteadOfSymbol = false, bool useUnicodeIfAvailable = false)
      => useNameInsteadOfSymbol ? unit.ToString() : unit switch
      {
        FrequencyUnit.Hertz => useUnicodeIfAvailable ? "\u3390" : "Hz",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum FrequencyUnit
  {
    /// <summary>Hertz.</summary>
    Hertz,
  }

  /// <summary>Temporal frequency, unit of Hertz. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Frequency"/>
  public struct Frequency
    : System.IComparable, System.IComparable<Frequency>, System.IConvertible, System.IEquatable<Frequency>, System.IFormattable, ISiDerivedUnitQuantifiable<double, FrequencyUnit>
  {
    public const FrequencyUnit DefaultUnit = FrequencyUnit.Hertz;

    [System.Diagnostics.Contracts.Pure] public static Frequency HyperfineTransitionFrequencyOfCs133 => new(9192631770);

    private readonly double m_value;

    public Frequency(double value, FrequencyUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        FrequencyUnit.Hertz => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    /// <summary>Creates a new Time instance representing the time it takes to complete one cycle at the frequency.</summary>
    [System.Diagnostics.Contracts.Pure] public Time ToPeriod() => new(1.0 / m_value);

    [System.Diagnostics.Contracts.Pure]
    public MetricMultiplicative ToMetricMultiplicative()
      => new(ToUnitValue(DefaultUnit), MetricMultiplicativePrefix.One);

    #region Static methods
    /// <summary>Computes the normalized frequency (a.k.a. cycles/sample) of the specified frequency and sample rate. The normalized frequency represents a fractional part of the cycle, per sample.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Normalized_frequency_(unit)"/>
    [System.Diagnostics.Contracts.Pure] public static double NormalizedFrequency(double frequency, double sampleRate) => frequency / sampleRate;
    /// <summary>Creates a new Frequency instance from the specified frequency shifted in pitch (positive or negative) by the interval specified in cents.</summary>
    /// <param name="frequency"></param>
    /// <param name="cent"></param>
    [System.Diagnostics.Contracts.Pure] public static Frequency PitchShift(Frequency frequency, Cent cent) => new(frequency.Value * Cent.ConvertCentToFrequencyRatio(cent.Value));
    /// <summary>Creates a new Frequency instance from the specified frequency shifted in pitch (positive or negative) by the interval specified in semitones.</summary>
    /// <param name="frequency"></param>
    /// <param name="semitone"></param>
    [System.Diagnostics.Contracts.Pure] public static Frequency PitchShift(Frequency frequency, Semitone semitone) => new(frequency.Value * Semitone.ConvertSemitoneToFrequencyRatio(semitone.Value));
    /// <summary>Computes the number of samples per cycle at the specified frequency and sample rate.</summary>
    [System.Diagnostics.Contracts.Pure] public static double SamplesPerCycle(double frequency, double sampleRate) => sampleRate / frequency;

    /// <summary>Creates a new Frequency instance from the specified acoustic properties of sound velocity and wavelength.</summary>
    /// <param name="soundVelocity"></param>
    /// <param name="wavelength"></param>
    [System.Diagnostics.Contracts.Pure] public static Frequency From(Speed soundVelocity, Length wavelength) => new(soundVelocity.Value / wavelength.Value);
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator Frequency(double value) => new(value);
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Frequency value) => value.m_value;

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Frequency a, Frequency b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Frequency a, Frequency b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Frequency a, Frequency b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Frequency a, Frequency b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Frequency a, Frequency b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Frequency a, Frequency b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static Frequency operator -(Frequency v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static Frequency operator +(Frequency a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static Frequency operator +(Frequency a, Frequency b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Frequency operator /(Frequency a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static Frequency operator /(Frequency a, Frequency b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Frequency operator *(Frequency a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static Frequency operator *(Frequency a, Frequency b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Frequency operator %(Frequency a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static Frequency operator %(Frequency a, Frequency b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Frequency operator -(Frequency a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static Frequency operator -(Frequency a, Frequency b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Frequency other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is Frequency o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(Frequency other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IMetricOneQuantifiable
    [System.Diagnostics.Contracts.Pure]
    public string ToMetricOneString(MetricMultiplicativePrefix prefix, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{ToMetricMultiplicative().ToUnitString(prefix, format, useFullName, preferUnicode)}{DefaultUnit.GetUnitString(useFullName, preferUnicode)}";

    // ISiDerivedUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public double Value
      => m_value;
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(FrequencyUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(FrequencyUnit unit = DefaultUnit)
      => unit switch
      {
        FrequencyUnit.Hertz => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Frequency o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
