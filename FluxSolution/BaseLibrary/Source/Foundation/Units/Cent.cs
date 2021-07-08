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
      => ConvertCentToFrequencyRatio(m_value);

    #region Static methods
    /// <summary>Convert a specified interval ratio to cents.</summary>
    public static int ConvertFrequencyRatioToCent(double frequencyRatio)
      => (int)(System.Math.Log(frequencyRatio, 2.0) * 1200.0);
    /// <summary>Convert a specified cents to an interval ratio.</summary>
    public static double ConvertCentToFrequencyRatio(int cents)
      => System.Math.Pow(2.0, cents / 1200.0);
    /// <summary>Adjusts the pitch of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
    public static double ShiftPitch(double frequency, int cents)
      => frequency * ConvertCentToFrequencyRatio(cents);

    public static Cent FromFrequencyRatio(double frequencyRatio)
      => new Cent(ConvertFrequencyRatioToCent(frequencyRatio));
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator int(Cent v)
      => v.m_value;
    public static explicit operator Cent(int v)
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

    public static Cent operator -(Cent v)
      => new Cent(-v.Value);
    public static Cent operator +(Cent a, Cent b)
      => new Cent(a.Value + b.Value);
    public static Cent operator +(Cent a, int b)
      => new Cent(a.Value + b);
    public static Cent operator +(int a, Cent b)
      => new Cent(a + b.Value);
    public static Cent operator /(Cent a, Cent b)
      => new Cent(a.Value / b.Value);
    public static Cent operator /(Cent a, int b)
      => new Cent(a.Value / b);
    public static Cent operator /(int a, Cent b)
      => new Cent(a / b.Value);
    public static Cent operator *(Cent a, Cent b)
      => new Cent(a.Value * b.Value);
    public static Cent operator *(Cent a, int b)
      => new Cent(a.Value * b);
    public static Cent operator *(int a, Cent b)
      => new Cent(a * b.Value);
    public static Cent operator %(Cent a, Cent b)
      => new Cent(a.Value % b.Value);
    public static Cent operator %(Cent a, int b)
      => new Cent(a.Value % b);
    public static Cent operator %(int a, Cent b)
      => new Cent(a % b.Value);
    public static Cent operator -(Cent a, Cent b)
      => new Cent(a.Value - b.Value);
    public static Cent operator -(Cent a, int b)
      => new Cent(a.Value - b);
    public static Cent operator -(int a, Cent b)
      => new Cent(a - b.Value);
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
