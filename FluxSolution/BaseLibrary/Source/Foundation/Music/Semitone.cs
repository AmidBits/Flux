namespace Flux
{
  /// <summary>Semitone, unit of itself. A musical interval equal to one hundred cents.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Semitone"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
  public struct Semitone
    : System.IComparable<Semitone>, System.IConvertible, System.IEquatable<Semitone>, IQuantifiable<int>
  {
    public const double FrequencyRatio = 1.0594630943592952645618252949463;

    private readonly int m_value;

    public Semitone(int semitones)
      => m_value = semitones;

    public int Value
      => m_value;

    /// <summary>Shifts the pitch of the specified frequency, up or down, using a pitch interval specified in semitones.</summary>
    public Frequency ShiftPitch(Frequency frequency)
      => new(PitchShift(frequency.Value, m_value));

    public Cent ToCent()
      => new(ConvertSemitoneToCent(m_value));
    public double ToFrequencyRatio()
      => ConvertSemitoneToFrequencyRatio(m_value);

    #region Static methods
    /// <summary>Convert a specified interval ratio to a number of semitones.</summary>
    public static double ConvertFrequencyRatioToSemitone(double frequencyRatio)
      => System.Math.Log(frequencyRatio, 2) * 12;
    /// <summary>Convert a specified number of semitones to cents.</summary>
    public static int ConvertSemitoneToCent(int semitones)
      => semitones * 100;
    /// <summary>Convert a specified number of semitones to an interval ratio.</summary>
    public static double ConvertSemitoneToFrequencyRatio(int semitones)
      => System.Math.Pow(2, semitones / 12.0);

    /// <summary>Creates a new Semitone instance from the specified frequency ratio.</summary>
    /// <param name="frequencyRatio"></param>
    public static Semitone FromFrequencyRatio(double frequencyRatio)
      => new((int)ConvertFrequencyRatioToSemitone(frequencyRatio));

    /// <summary>Applies pitch shifting of the specified frequency, up or down, using a pitch interval specified in semitones.</summary>
    public static double PitchShift(double frequency, int semitones)
      => frequency * ConvertSemitoneToFrequencyRatio(semitones);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator int(Semitone v)
      => v.m_value;
    public static explicit operator Semitone(int v)
      => new(v);

    public static bool operator <(Semitone a, Semitone b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Semitone a, Semitone b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Semitone a, Semitone b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Semitone a, Semitone b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Semitone a, Semitone b)
      => a.Equals(b);
    public static bool operator !=(Semitone a, Semitone b)
      => !a.Equals(b);

    public static Semitone operator -(Semitone v)
      => new(-v.m_value);
    public static Semitone operator +(Semitone a, int b)
      => new(a.m_value + b);
    public static Semitone operator +(Semitone a, Semitone b)
      => a + b.m_value;
    public static Semitone operator /(Semitone a, int b)
      => new(a.m_value / b);
    public static Semitone operator /(Semitone a, Semitone b)
      => a / b.m_value;
    public static Semitone operator *(Semitone a, int b)
      => new(a.m_value * b);
    public static Semitone operator *(Semitone a, Semitone b)
      => a * b.m_value;
    public static Semitone operator %(Semitone a, int b)
      => new(a.m_value % b);
    public static Semitone operator %(Semitone a, Semitone b)
      => a % b.m_value;
    public static Semitone operator -(Semitone a, int b)
      => new(a.m_value - b);
    public static Semitone operator -(Semitone a, Semitone b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Semitone other)
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
    public bool Equals(Semitone other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Semitone o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} }}";
    #endregion Object overrides
  }
}
