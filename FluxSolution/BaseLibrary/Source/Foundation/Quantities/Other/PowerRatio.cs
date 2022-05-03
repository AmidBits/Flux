namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this PowerRatioUnit unit, bool useFullName = false, bool preferUnicode = false)
      => unit switch
      {
        PowerRatioUnit.DecibelWatt => "dBW",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum PowerRatioUnit
  {
    DecibelWatt,
  }

  /// <summary>Power ratio unit of decibel watts, defined as ten times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one watt. A.k.a. logarithmic power ratio.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Decibel"/>
  public struct PowerRatio
    : System.IComparable, System.IComparable<PowerRatio>, System.IConvertible, System.IEquatable<PowerRatio>, System.IFormattable, IMetricOneQuantifiable, IUnitQuantifiable<double, PowerRatioUnit>
  {
    public const PowerRatioUnit DefaultUnit = PowerRatioUnit.DecibelWatt;

    public const double ScalingFactor = 10;

    private readonly double m_value;

    public PowerRatio(double value, PowerRatioUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        PowerRatioUnit.DecibelWatt => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    [System.Diagnostics.Contracts.Pure]
    public AmplitudeRatio ToAmplitudeRatio()
      => new(System.Math.Sqrt(m_value));

    #region Static methods
    /// <summary>Creates a new PowerRatio instance from the difference of the specified voltages (numerator and denominator).</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    public static PowerRatio From(Power numerator, Power denominator)
      => new(ScalingFactor * System.Math.Log10(numerator.Value / denominator.Value));
    /// <summary>Creates a new PowerRatio instance from the specified decibel change (i.e. a decibel interval).</summary>
    /// <param name="decibelChange"></param>
    public static PowerRatio FromDecibelChange(double decibelChange)
      => new(System.Math.Pow(10, decibelChange / ScalingFactor)); // Inverse of Log10.
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(PowerRatio v)
      => v.m_value;
    public static explicit operator PowerRatio(double v)
      => new(v);

    public static bool operator <(PowerRatio a, PowerRatio b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(PowerRatio a, PowerRatio b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(PowerRatio a, PowerRatio b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(PowerRatio a, PowerRatio b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(PowerRatio a, PowerRatio b)
      => a.Equals(b);
    public static bool operator !=(PowerRatio a, PowerRatio b)
      => !a.Equals(b);

    public static PowerRatio operator -(PowerRatio v)
      => new(-v.m_value);
    public static PowerRatio operator +(PowerRatio a, double b)
      => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) + System.Math.Pow(10, b / ScalingFactor)));
    public static PowerRatio operator +(PowerRatio a, PowerRatio b)
      => a + b.m_value;
    public static PowerRatio operator /(PowerRatio a, double b)
      => new(a.m_value - b);
    public static PowerRatio operator /(PowerRatio a, PowerRatio b)
      => a / b.m_value;
    public static PowerRatio operator *(PowerRatio a, double b)
      => new(a.m_value + b);
    public static PowerRatio operator *(PowerRatio a, PowerRatio b)
      => a * b.m_value;
    public static PowerRatio operator -(PowerRatio a, double b)
      => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) - System.Math.Pow(10, b / ScalingFactor)));
    public static PowerRatio operator -(PowerRatio a, PowerRatio b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(PowerRatio other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is PowerRatio o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(PowerRatio other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IMetricOneQuantifiable
    [System.Diagnostics.Contracts.Pure]
    public string ToMetricOneString(MetricMultiplicativePrefix prefix, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{new MetricMultiplicative(m_value, MetricMultiplicativePrefix.One).ToUnitString(prefix, format, useFullName, preferUnicode)}{DefaultUnit.GetUnitString(useFullName, preferUnicode)}";

    // IUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public double Value
      => m_value;
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(PowerRatioUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(PowerRatioUnit unit = DefaultUnit)
      => unit switch
      {
        PowerRatioUnit.DecibelWatt => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is PowerRatio o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
