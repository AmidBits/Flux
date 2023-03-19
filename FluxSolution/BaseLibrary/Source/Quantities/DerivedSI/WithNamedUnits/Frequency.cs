namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.FrequencyUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.FrequencyUnit.Hertz => preferUnicode ? "\u3390" : "Hz",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum FrequencyUnit
    {
      /// <summary>Hertz.</summary>
      Hertz,
    }

    /// <summary>Temporal frequency, unit of Hertz. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Frequency"/>
    public readonly record struct Frequency
      : System.IComparable, System.IComparable<Frequency>, System.IConvertible, IUnitQuantifiable<double, FrequencyUnit>
    {
      public static readonly Frequency Zero;

      public const FrequencyUnit DefaultUnit = FrequencyUnit.Hertz;

      public static Frequency HyperfineTransitionFrequencyOfCs => new(9192631770);

      private readonly double m_hertz;

      public Frequency(double value, FrequencyUnit unit = DefaultUnit)
        => m_hertz = unit switch
        {
          FrequencyUnit.Hertz => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      /// <summary>Returns the angular velocity from the (rotational) frequency.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>

      public AngularVelocity ToAngularVelocity()
        => new(double.Tau * m_hertz);

      /// <summary>Creates a new Time instance representing the time it takes to complete one cycle at the frequency.</summary>

      public Time ToPeriod()
        => new(1.0 / m_hertz);

      #region Static methods
      /// <remarks>Revolutions Per Minute (RPM) is officially a frequency and as such measured in Hertz (which is 'per second'). Conversion is a straight forward by a factor of 60 (i.e. seconds per minute)</remarks>
      /// <see cref="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>

      public static double ConvertFrequencyToRpm(double frequency)
        => frequency * 60;

      /// <remarks>Revolutions Per Minute (RPM) is officially a frequency and as such measured in Hertz (which is 'per second'). Conversion is a straight forward by a factor of 60 (i.e. seconds per minute)</remarks>
      /// <see cref="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>

      public static double ConvertRpmToFrequency(double revolutionPerMinute)
        => revolutionPerMinute / 60;

      /// <summary>Computes the normalized frequency (a.k.a. cycles/sample) of the specified frequency and sample rate. The normalized frequency represents a fractional part of the cycle, per sample.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Normalized_frequency_(unit)"/>

      public static double NormalizedFrequency(double frequency, double sampleRate)
        => frequency / sampleRate;

      /// <summary>Creates a new Frequency instance from the specified frequency shifted in pitch (positive or negative) by the interval specified in cents.</summary>
      /// <param name="frequency"></param>
      /// <param name="cent"></param>

      public static Frequency PitchShift(Frequency frequency, Music.Cent cent)
        => new(frequency.Value * Music.Cent.ConvertCentToFrequencyRatio(cent.Value));

      /// <summary>Creates a new Frequency instance from the specified frequency shifted in pitch (positive or negative) by the interval specified in semitones.</summary>
      /// <param name="frequency"></param>
      /// <param name="semitone"></param>

      public static Frequency PitchShift(Frequency frequency, Music.Semitone semitone)
        => new(frequency.Value * Music.Semitone.ConvertSemitoneToFrequencyRatio(semitone.Value));

      /// <summary>Computes the number of samples per cycle at the specified frequency and sample rate.</summary>

      public static double SamplesPerCycle(double frequency, double sampleRate)
        => sampleRate / frequency;

      /// <summary>Creates a new Frequency instance from the specified acoustic properties of sound velocity and wavelength.</summary>
      /// <param name="soundVelocity"></param>
      /// <param name="wavelength"></param>

      public static Frequency From(LinearVelocity soundVelocity, Length wavelength)
        => new(soundVelocity.Value / wavelength.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator Frequency(double value) => new(value);
      public static explicit operator double(Frequency value) => value.m_hertz;

      public static bool operator <(Frequency a, Frequency b) => a.CompareTo(b) < 0;
      public static bool operator <=(Frequency a, Frequency b) => a.CompareTo(b) <= 0;
      public static bool operator >(Frequency a, Frequency b) => a.CompareTo(b) > 0;
      public static bool operator >=(Frequency a, Frequency b) => a.CompareTo(b) >= 0;

      public static Frequency operator -(Frequency v) => new(-v.m_hertz);
      public static Frequency operator +(Frequency a, double b) => new(a.m_hertz + b);
      public static Frequency operator +(Frequency a, Frequency b) => a + b.m_hertz;
      public static Frequency operator /(Frequency a, double b) => new(a.m_hertz / b);
      public static Frequency operator /(Frequency a, Frequency b) => a / b.m_hertz;
      public static Frequency operator *(Frequency a, double b) => new(a.m_hertz * b);
      public static Frequency operator *(Frequency a, Frequency b) => a * b.m_hertz;
      public static Frequency operator %(Frequency a, double b) => new(a.m_hertz % b);
      public static Frequency operator %(Frequency a, Frequency b) => a % b.m_hertz;
      public static Frequency operator -(Frequency a, double b) => new(a.m_hertz - b);
      public static Frequency operator -(Frequency a, Frequency b) => a - b.m_hertz;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Frequency o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Frequency other) => m_hertz.CompareTo(other.m_hertz);

      #region IConvertible
      public System.TypeCode GetTypeCode() => System.TypeCode.Object;
      public bool ToBoolean(System.IFormatProvider? provider) => m_hertz != 0;
      public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_hertz);
      public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_hertz);
      public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_hertz);
      public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_hertz);
      public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_hertz);
      public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_hertz);
      public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_hertz);
      public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_hertz);
      [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_hertz);
      public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_hertz);
      public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_hertz);
      public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_hertz, conversionType, provider);
      [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_hertz);
      [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_hertz);
      [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_hertz);
      #endregion IConvertible

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_hertz; init => m_hertz = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(FrequencyUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(FrequencyUnit unit = DefaultUnit)
        => unit switch
        {
          FrequencyUnit.Hertz => m_hertz,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
