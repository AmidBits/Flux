namespace Flux.Units
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cent_(music)"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
  public struct Cent
    : System.IComparable<Cent>, System.IEquatable<Cent>
  {
    public const double FrequencyRatio = 1.0005777895065548592967925757932;

    private readonly int m_value;

    public Cent(int cents)
      => m_value = cents;

    public int Value
      => m_value;

    public double ToFrequencyRatio()
      => ConvertToFrequencyRatio(m_value);

    /// <summary>Adjusts the pitch of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
    public static double ShiftPitch(double frequency, int cents)
      => System.Math.Pow(FrequencyRatio, cents) * frequency;

    #region Static methods
    public static Cent Add(Cent left, Cent right)
      => new Cent(left.m_value + right.m_value);
    /// <summary>Convert a specified interval ratio to cents.</summary>
    public static int ConvertFrequencyRatioToCent(double frequencyRatio)
      => (int)(System.Math.Log(frequencyRatio, 2.0) * 1200.0);
    /// <summary>Convert a specified cents to an interval ratio.</summary>
    public static double ConvertToFrequencyRatio(int cents)
      => System.Math.Pow(2.0, cents / 1200.0);
    public static Cent Divide(Cent left, Cent right)
      => new Cent(left.m_value / right.m_value);
    public static Cent FromFrequencyRatio(double frequencyRatio)
      => ConvertFrequencyRatioToCent(frequencyRatio);
    public static Cent Multiply(Cent left, Cent right)
      => new Cent(left.m_value * right.m_value);
    public static Cent Negate(Cent value)
      => new Cent(-value.m_value);
    public static Cent Remainder(Cent dividend, Cent divisor)
      => new Cent(dividend.m_value % divisor.m_value);
    public static Cent Subtract(Cent left, Cent right)
      => new Cent(left.m_value - right.m_value);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator int(Cent v)
      => v.m_value;
    public static implicit operator Cent(int v)
      => new Cent(v);

    public static bool operator <(Cent a, Cent b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Cent a, Cent b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Cent a, Cent b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Cent a, Cent b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Cent a, Cent b)
      => a.Equals(b);
    public static bool operator !=(Cent a, Cent b)
      => !a.Equals(b);

    public static Cent operator +(Cent a, Cent b)
      => Add(a, b);
    public static Cent operator /(Cent a, Cent b)
      => Divide(a, b);
    public static Cent operator *(Cent a, Cent b)
      => Multiply(a, b);
    public static Cent operator -(Cent v)
      => Negate(v);
    public static Cent operator %(Cent a, Cent b)
      => Remainder(a, b);
    public static Cent operator -(Cent a, Cent b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Cent other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Cent other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Cent o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value}>";
    #endregion Object overrides
  }
}