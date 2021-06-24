namespace Flux.Units
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Semitone"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
  public struct Semitone
    : System.IComparable<Semitone>, System.IEquatable<Semitone>
  {
    public const double FrequencyRatio = 1.0594630943592952645618252949463;

    private readonly int m_value;

    public Semitone(int semitones)
      => m_value = semitones;

    public int Value
      => m_value;

    public int ToCent()
      => ConvertToCent(m_value);
    public double ToFrequencyRatio()
      => ConvertToFrequencyRatio(m_value);

    /// <summary>Adjusts the pitch of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
    public static double ShiftPitch(double frequency, int semitones)
      => System.Math.Pow(FrequencyRatio, semitones) * frequency;

    #region Static methods
    public static Semitone Add(Semitone left, Semitone right)
      => new Semitone(left.m_value + right.m_value);
    /// <summary>Convert a specified interval ratio to cents.</summary>
    public static int ConvertFrequencyRatioToSemitone(double frequencyRatio)
      => (int)(System.Math.Log(frequencyRatio, 2.0) * 12.0);
    /// <summary>Convert a specified semitone to cents.</summary>
    public static int ConvertToCent(int semitones)
      => semitones * 100;
    /// <summary>Convert a specified semitone to an interval ratio.</summary>
    public static double ConvertToFrequencyRatio(int semitones)
      => System.Math.Pow(2.0, semitones / 12.0);
    public static Semitone Divide(Semitone left, Semitone right)
      => new Semitone(left.m_value / right.m_value);
    public static Semitone FromFrequencyRatio(double frequencyRatio)
      => ConvertFrequencyRatioToSemitone(frequencyRatio);
    public static Semitone Multiply(Semitone left, Semitone right)
      => new Semitone(left.m_value * right.m_value);
    public static Semitone Negate(Semitone value)
      => new Semitone(-value.m_value);
    public static Semitone Remainder(Semitone dividend, Semitone divisor)
      => new Semitone(dividend.m_value % divisor.m_value);
    public static Semitone Subtract(Semitone left, Semitone right)
      => new Semitone(left.m_value - right.m_value);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator int(Semitone v)
      => v.m_value;
    public static implicit operator Semitone(int v)
      => new Semitone(v);

    public static bool operator <(Semitone a, Semitone b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Semitone a, Semitone b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Semitone a, Semitone b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Semitone a, Semitone b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Semitone a, Semitone b)
      => a.Equals(b);
    public static bool operator !=(Semitone a, Semitone b)
      => !a.Equals(b);

    public static Semitone operator +(Semitone a, Semitone b)
      => Add(a, b);
    public static Semitone operator /(Semitone a, Semitone b)
      => Divide(a, b);
    public static Semitone operator *(Semitone a, Semitone b)
      => Multiply(a, b);
    public static Semitone operator -(Semitone v)
      => Negate(v);
    public static Semitone operator %(Semitone a, Semitone b)
      => Remainder(a, b);
    public static Semitone operator -(Semitone a, Semitone b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Semitone other)
      => m_value.CompareTo(other.m_value);

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
      => $"<{GetType().Name}: {m_value}>";
    #endregion Object overrides
  }
}
