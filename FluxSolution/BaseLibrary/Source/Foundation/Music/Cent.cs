namespace Flux
{
  /// <summary>Cent, unit of itself. Musical interval equal to one hundredth of one semitone.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cent_(music)"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
  public struct Cent
    : System.IComparable<Cent>, System.IConvertible, System.IEquatable<Cent>, IQuantifiable<int>
  {
    public const double FrequencyRatio = 1.0005777895065548592967925757932;

    private readonly int m_value;

    public Cent(int cents)
      => m_value = cents;

    [System.Diagnostics.Contracts.Pure]
    public int Value
      => m_value;

    /// <summary>Shifts the pitch of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Frequency ShiftPitch(Frequency frequency)
      => new(PitchShift(frequency.Value, m_value));

    [System.Diagnostics.Contracts.Pure]
    public double ToFrequencyRatio()
      => ConvertCentToFrequencyRatio(m_value);

    #region Static methods
    /// <summary>Convert a specified interval ratio to a number of cents.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertFrequencyRatioToCent(double frequencyRatio) => System.Math.Log(frequencyRatio, 2) * 1200;
    /// <summary>Convert a specified number of cents to an interval ratio.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertCentToFrequencyRatio(int cents) => System.Math.Pow(2, cents / 1200.0);

    /// <summary>Creates a new Cent instance from the specified frequency ratio.</summary>
    /// <param name="frequencyRatio"></param>
    [System.Diagnostics.Contracts.Pure] public static Cent FromFrequencyRatio(double frequencyRatio) => new((int)ConvertFrequencyRatioToCent(frequencyRatio));

    /// <summary>Applies pitch shifting of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
    [System.Diagnostics.Contracts.Pure] public static double PitchShift(double frequency, int cents) => frequency * ConvertCentToFrequencyRatio(cents);
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator int(Cent v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator Cent(int v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Cent a, Cent b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Cent a, Cent b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Cent a, Cent b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Cent a, Cent b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Cent a, Cent b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Cent a, Cent b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static Cent operator -(Cent v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static Cent operator +(Cent a, int b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static Cent operator +(Cent a, Cent b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Cent operator /(Cent a, int b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static Cent operator /(Cent a, Cent b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Cent operator *(Cent a, int b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static Cent operator *(Cent a, Cent b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Cent operator %(Cent a, int b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static Cent operator %(Cent a, Cent b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Cent operator -(Cent a, int b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static Cent operator -(Cent a, Cent b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Cent other) => m_value.CompareTo(other.m_value);

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

    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(Cent other) => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Cent o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {m_value} }}";
    #endregion Object overrides
  }
}
