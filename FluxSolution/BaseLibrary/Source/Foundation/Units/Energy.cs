namespace Flux.Units
{
  /// <summary>Frequency is a mutable data type to accomodate changes across multiple consumers.</summary>
  public struct Energy
    : System.IComparable<Energy>, System.IEquatable<Energy>, System.IFormattable
  {
    private readonly double m_joule;

    public Energy(double joule)
      => m_joule = joule;

    public double Joule
      => m_joule;

    #region Static methods
    public static Energy Add(Energy left, Energy right)
      => new Energy(left.m_joule + right.m_joule);
    public static Energy Divide(Energy left, Energy right)
      => new Energy(left.m_joule / right.m_joule);
    public static Energy Multiply(Energy left, Energy right)
      => new Energy(left.m_joule * right.m_joule);
    public static Energy Negate(Energy value)
      => new Energy(-value.m_joule);
    public static Energy Remainder(Energy dividend, Energy divisor)
      => new Energy(dividend.m_joule % divisor.m_joule);
    public static Energy Subtract(Energy left, Energy right)
      => new Energy(left.m_joule - right.m_joule);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Energy v)
      => v.m_joule;
    public static implicit operator Energy(double v)
      => new Energy(v);

    public static bool operator <(Energy a, Energy b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Energy a, Energy b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Energy a, Energy b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Energy a, Energy b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Energy a, Energy b)
      => a.Equals(b);
    public static bool operator !=(Energy a, Energy b)
      => !a.Equals(b);

    public static Energy operator +(Energy a, Energy b)
      => Add(a, b);
    public static Energy operator /(Energy a, Energy b)
      => Divide(a, b);
    public static Energy operator *(Energy a, Energy b)
      => Multiply(a, b);
    public static Energy operator -(Energy v)
      => Negate(v);
    public static Energy operator %(Energy a, Energy b)
      => Remainder(a, b);
    public static Energy operator -(Energy a, Energy b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Energy other)
      => m_joule.CompareTo(other.m_joule);

    // IEquatable
    public bool Equals(Energy other)
      => m_joule == other.m_joule;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Energy)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Energy o && Equals(o);
    public override int GetHashCode()
      => m_joule.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
