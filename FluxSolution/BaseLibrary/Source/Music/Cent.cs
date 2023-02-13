namespace Flux.Music
{
  /// <summary>Cent, unit of itself. Musical interval equal to one hundredth of one semitone.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cent_(music)"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
  public readonly record struct Cent
    : System.IComparable<Cent>, System.IConvertible, Quantities.IQuantifiable<int>
  {
    public const double FrequencyRatio = 1.0005777895065548592967925757932;

    private readonly int m_value;

    public Cent(int cents)
      => m_value = cents;

    /// <summary>Shifts the pitch of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
    public Quantities.Frequency ShiftPitch(Quantities.Frequency frequency) => new(PitchShift(frequency.Value, m_value));

    public double ToFrequencyRatio() => ConvertCentToFrequencyRatio(m_value);

    #region Static methods
    /// <summary>Convert a specified interval ratio to a number of cents.</summary>
    public static double ConvertFrequencyRatioToCent(double frequencyRatio) => System.Math.Log(frequencyRatio, 2) * 1200;
    /// <summary>Convert a specified number of cents to an interval ratio.</summary>
    public static double ConvertCentToFrequencyRatio(int cents) => System.Math.Pow(2, cents / 1200.0);

    /// <summary>Creates a new Cent instance from the specified frequency ratio.</summary>
    /// <param name="frequencyRatio"></param>
    public static Cent FromFrequencyRatio(double frequencyRatio) => new((int)ConvertFrequencyRatioToCent(frequencyRatio));

    /// <summary>Applies pitch shifting of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
    public static double PitchShift(double frequency, int cents) => frequency * ConvertCentToFrequencyRatio(cents);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator int(Cent v) => v.m_value;
    public static explicit operator Cent(int v) => new(v);

    public static bool operator <(Cent a, Cent b) => a.CompareTo(b) < 0;
    public static bool operator <=(Cent a, Cent b) => a.CompareTo(b) <= 0;
    public static bool operator >(Cent a, Cent b) => a.CompareTo(b) > 0;
    public static bool operator >=(Cent a, Cent b) => a.CompareTo(b) >= 0;

    public static Cent operator -(Cent v) => new(-v.m_value);
    public static Cent operator +(Cent a, int b) => new(a.m_value + b);
    public static Cent operator +(Cent a, Cent b) => a + b.m_value;
    public static Cent operator /(Cent a, int b) => new(a.m_value / b);
    public static Cent operator /(Cent a, Cent b) => a / b.m_value;
    public static Cent operator *(Cent a, int b) => new(a.m_value * b);
    public static Cent operator *(Cent a, Cent b) => a * b.m_value;
    public static Cent operator %(Cent a, int b) => new(a.m_value % b);
    public static Cent operator %(Cent a, Cent b) => a % b.m_value;
    public static Cent operator -(Cent a, int b) => new(a.m_value - b);
    public static Cent operator -(Cent a, Cent b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    public int CompareTo(Cent other) => m_value.CompareTo(other.m_value);
    // IComparable
    public int CompareTo(object? other) => other is not null && other is Cent o ? CompareTo(o) : -1;

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

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
      => $"{m_value} cent{(m_value == 1 ? string.Empty : 's'.ToString())}";

    public int Value
      => m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override string ToString() => $"{GetType().Name} {{ {ToQuantityString()} }}";
    #endregion Object overrides
  }
}
