namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitSymbol(this AmplitudeRatioUnit unit, bool useFullName = false, bool preferUnicode = false)
      => unit switch
      {
        AmplitudeRatioUnit.DecibelVolt => "dBV",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum AmplitudeRatioUnit
  {
    DecibelVolt,
  }

  /// <summary>Amplitude ratio unit of decibel volt, defined as twenty times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one volt RMS. A.k.a. logarithmic root-power ratio.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Decibel"/>
  public struct AmplitudeRatio
    : System.IComparable, System.IComparable<AmplitudeRatio>, System.IConvertible, System.IEquatable<AmplitudeRatio>, IUnitQuantifiable<double, AmplitudeRatioUnit>
  {
    public const AmplitudeRatioUnit DefaultUnit = AmplitudeRatioUnit.DecibelVolt;

    public const double ScalingFactor = 20;

    private readonly double m_value;

    public AmplitudeRatio(double value, AmplitudeRatioUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AmplitudeRatioUnit.DecibelVolt => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    [System.Diagnostics.Contracts.Pure]
    public double Value
      => m_value;

    [System.Diagnostics.Contracts.Pure]
    public PowerRatio ToPowerRatio()
      => new(System.Math.Pow(m_value, 2));

    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(AmplitudeRatioUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitSymbol()}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(AmplitudeRatioUnit unit = DefaultUnit)
      => unit switch
      {
        AmplitudeRatioUnit.DecibelVolt => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new AmplitudeRatio instance from the difference of the specified voltages (numerator and denominator).</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    public static AmplitudeRatio From(Voltage numerator, Voltage denominator)
      => new(ScalingFactor * System.Math.Log10(numerator.Value / denominator.Value));
    /// <summary>Creates a new AmplitudeRatio instance from the specified decibel change (i.e. a decibel interval).</summary>
    /// <param name="decibelChange"></param>
    public static AmplitudeRatio FromDecibelChange(double decibelChange)
      => new(System.Math.Pow(10, decibelChange / ScalingFactor)); // Inverse of Log10.
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(AmplitudeRatio v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator AmplitudeRatio(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(AmplitudeRatio a, AmplitudeRatio b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(AmplitudeRatio a, AmplitudeRatio b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static AmplitudeRatio operator -(AmplitudeRatio v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static AmplitudeRatio operator +(AmplitudeRatio a, double b) => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) + System.Math.Pow(10, b / ScalingFactor)));
    [System.Diagnostics.Contracts.Pure] public static AmplitudeRatio operator +(AmplitudeRatio a, AmplitudeRatio b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static AmplitudeRatio operator /(AmplitudeRatio a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static AmplitudeRatio operator /(AmplitudeRatio a, AmplitudeRatio b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static AmplitudeRatio operator *(AmplitudeRatio a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static AmplitudeRatio operator *(AmplitudeRatio a, AmplitudeRatio b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static AmplitudeRatio operator -(AmplitudeRatio a, double b) => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) - System.Math.Pow(10, b / ScalingFactor)));
    [System.Diagnostics.Contracts.Pure] public static AmplitudeRatio operator -(AmplitudeRatio a, AmplitudeRatio b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(AmplitudeRatio other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is AmplitudeRatio o ? CompareTo(o) : -1;

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
    [System.Diagnostics.Contracts.Pure] public bool Equals(AmplitudeRatio other) => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is AmplitudeRatio o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
