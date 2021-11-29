namespace Flux.Music
{
  /// <summary>Cent unit of itself.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cent_(music)"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
#if NET5_0
  public struct Cent
    : System.IComparable<Cent>, System.IEquatable<Cent>, Quantity.IValuedUnit<int>
#else
  public record struct Cent
    : System.IComparable<Cent>, Quantity.IValuedUnit<int>
#endif
  {
    public const double FrequencyRatio = 1.0005777895065548592967925757932;

    private readonly int m_value;

    public Cent(int cents)
      => m_value = cents;

    public int Cents
      => m_value;

    public int Value
      => m_value;

    /// <summary>Shifts the pitch of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
    public Quantity.Frequency ShiftPitch(Quantity.Frequency frequency)
      => new(PitchShift(frequency.Value, m_value));

    public double ToFrequencyRatio()
      => ConvertCentToFrequencyRatio(m_value);

    #region Static methods
    /// <summary>Convert a specified interval ratio to a number of cents.</summary>
    public static double ConvertFrequencyRatioToCent(double frequencyRatio)
      => System.Math.Log(frequencyRatio, 2) * 1200;
    /// <summary>Convert a specified number of cents to an interval ratio.</summary>
    public static double ConvertCentToFrequencyRatio(int cents)
      => System.Math.Pow(2, cents / 1200.0);

    /// <summary>Creates a new Cent instance from the specified frequency ratio.</summary>
    /// <param name="frequencyRatio"></param>
    public static Cent FromFrequencyRatio(double frequencyRatio)
      => new((int)ConvertFrequencyRatioToCent(frequencyRatio));

    /// <summary>Applies pitch shifting of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
    public static double PitchShift(double frequency, int cents)
      => frequency * ConvertCentToFrequencyRatio(cents);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator int(Cent v)
      => v.m_value;
    public static explicit operator Cent(int v)
      => new(v);

    public static bool operator <(Cent a, Cent b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Cent a, Cent b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Cent a, Cent b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Cent a, Cent b)
      => a.CompareTo(b) >= 0;

#if NET5_0
    public static bool operator ==(Cent a, Cent b)
      => a.Equals(b);
    public static bool operator !=(Cent a, Cent b)
      => !a.Equals(b);
#endif

    public static Cent operator -(Cent v)
      => new(-v.m_value);
    public static Cent operator +(Cent a, int b)
      => new(a.m_value + b);
    public static Cent operator +(Cent a, Cent b)
      => a + b.m_value;
    public static Cent operator /(Cent a, int b)
      => new(a.m_value / b);
    public static Cent operator /(Cent a, Cent b)
      => a / b.m_value;
    public static Cent operator *(Cent a, int b)
      => new(a.m_value * b);
    public static Cent operator *(Cent a, Cent b)
      => a * b.m_value;
    public static Cent operator %(Cent a, int b)
      => new(a.m_value % b);
    public static Cent operator %(Cent a, Cent b)
      => a % b.m_value;
    public static Cent operator -(Cent a, int b)
      => new(a.m_value - b);
    public static Cent operator -(Cent a, Cent b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Cent other)
      => m_value.CompareTo(other.m_value);

#if NET5_0
    // IEquatable
    public bool Equals(Cent other)
      => m_value == other.m_value;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is Cent o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} }}";
    #endregion Object overrides
  }
}
