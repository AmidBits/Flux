namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.AmplitudeRatioUnit unit, bool preferUnicode, bool useFullName = false)
      => unit switch
      {
        Quantities.AmplitudeRatioUnit.DecibelVolt => "dBV",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum AmplitudeRatioUnit
    {
      DecibelVolt,
    }

    /// <summary>Amplitude ratio unit of decibel volt, defined as twenty times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one volt RMS. A.k.a. logarithmic root-power ratio.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Decibel"/>
    public readonly record struct AmplitudeRatio
      : System.IComparable, System.IComparable<AmplitudeRatio>, System.IConvertible, System.IFormattable, IUnitQuantifiable<double, AmplitudeRatioUnit>
    {
      public static readonly AmplitudeRatio Zero;

      public const AmplitudeRatioUnit DefaultUnit = AmplitudeRatioUnit.DecibelVolt;

      public const double ScalingFactor = 20;

      private readonly double m_value;

      public AmplitudeRatio(double value, AmplitudeRatioUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          AmplitudeRatioUnit.DecibelVolt => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };


      public PowerRatio ToPowerRatio()
        => new(System.Math.Pow(m_value, 2));

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
      public static explicit operator double(AmplitudeRatio v) => v.m_value;
      public static explicit operator AmplitudeRatio(double v) => new(v);

      public static bool operator <(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) < 0;
      public static bool operator <=(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) <= 0;
      public static bool operator >(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) > 0;
      public static bool operator >=(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) >= 0;

      public static AmplitudeRatio operator -(AmplitudeRatio v) => new(-v.m_value);
      public static AmplitudeRatio operator +(AmplitudeRatio a, double b) => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) + System.Math.Pow(10, b / ScalingFactor)));
      public static AmplitudeRatio operator +(AmplitudeRatio a, AmplitudeRatio b) => a + b.m_value;
      public static AmplitudeRatio operator /(AmplitudeRatio a, double b) => new(a.m_value - b);
      public static AmplitudeRatio operator /(AmplitudeRatio a, AmplitudeRatio b) => a / b.m_value;
      public static AmplitudeRatio operator *(AmplitudeRatio a, double b) => new(a.m_value + b);
      public static AmplitudeRatio operator *(AmplitudeRatio a, AmplitudeRatio b) => a * b.m_value;
      public static AmplitudeRatio operator -(AmplitudeRatio a, double b) => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) - System.Math.Pow(10, b / ScalingFactor)));
      public static AmplitudeRatio operator -(AmplitudeRatio a, AmplitudeRatio b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(AmplitudeRatio other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is AmplitudeRatio o ? CompareTo(o) : -1;

      #region IConvertible
      public System.TypeCode GetTypeCode() => System.TypeCode.Object;
      public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
      public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
      public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
      public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
      public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
      public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
      public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
      public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
      public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
      [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
      public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
      public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
      public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
      [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
      [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
      [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
      #endregion IConvertible

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);

      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>

      public string ToUnitString(AmplitudeRatioUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      public double ToUnitValue(AmplitudeRatioUnit unit = DefaultUnit)
        => unit switch
        {
          AmplitudeRatioUnit.DecibelVolt => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
