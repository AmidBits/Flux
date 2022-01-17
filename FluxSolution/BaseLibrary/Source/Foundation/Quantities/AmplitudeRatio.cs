namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static AmplitudeRatio Create(this AmplitudeRatioUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this AmplitudeRatioUnit unit)
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
    : System.IComparable<AmplitudeRatio>, System.IConvertible, System.IEquatable<AmplitudeRatio>, IValueGeneralizedUnit<double>
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

    public double Value
      => m_value;

    public PowerRatio ToPowerRatio()
      => new(System.Math.Pow(m_value, 2));

    public string ToUnitString(AmplitudeRatioUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitSymbol()}";
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
    public static explicit operator double(AmplitudeRatio v)
      => v.m_value;
    public static explicit operator AmplitudeRatio(double v)
      => new(v);

    public static bool operator <(AmplitudeRatio a, AmplitudeRatio b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AmplitudeRatio a, AmplitudeRatio b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AmplitudeRatio a, AmplitudeRatio b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(AmplitudeRatio a, AmplitudeRatio b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(AmplitudeRatio a, AmplitudeRatio b)
      => a.Equals(b);
    public static bool operator !=(AmplitudeRatio a, AmplitudeRatio b)
      => !a.Equals(b);

    public static AmplitudeRatio operator -(AmplitudeRatio v)
      => new(-v.m_value);
    public static AmplitudeRatio operator +(AmplitudeRatio a, double b)
      => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) + System.Math.Pow(10, b / ScalingFactor)));
    public static AmplitudeRatio operator +(AmplitudeRatio a, AmplitudeRatio b)
      => a + b.m_value;
    public static AmplitudeRatio operator /(AmplitudeRatio a, double b)
      => new(a.m_value - b);
    public static AmplitudeRatio operator /(AmplitudeRatio a, AmplitudeRatio b)
      => a / b.m_value;
    public static AmplitudeRatio operator *(AmplitudeRatio a, double b)
      => new(a.m_value + b);
    public static AmplitudeRatio operator *(AmplitudeRatio a, AmplitudeRatio b)
      => a * b.m_value;
    public static AmplitudeRatio operator -(AmplitudeRatio a, double b)
      => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) - System.Math.Pow(10, b / ScalingFactor)));
    public static AmplitudeRatio operator -(AmplitudeRatio a, AmplitudeRatio b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AmplitudeRatio other)
      => m_value.CompareTo(other.m_value);

    #region IConvertible
    public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IEquatable
    public bool Equals(AmplitudeRatio other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AmplitudeRatio o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
