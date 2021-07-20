namespace Flux.Units
{
  /// <summary>Energy unit of Joule.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Energy"/>
  public struct Energy
    : System.IComparable<Energy>, System.IEquatable<Energy>, IStandardizedScalar
  {
    private readonly double m_value;

    public Energy(double joule)
      => m_value = joule;

    public double Value
      => m_value;

    #region Overloaded operators
    public static explicit operator double(Energy v)
      => v.m_value;
    public static explicit operator Energy(double v)
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

    public static Energy operator -(Energy v)
      => new Energy(-v.m_value);
    public static Energy operator +(Energy a, Energy b)
      => new Energy(a.m_value + b.m_value);
    public static Energy operator /(Energy a, Energy b)
      => new Energy(a.m_value / b.m_value);
    public static Energy operator *(Energy a, Energy b)
      => new Energy(a.m_value * b.m_value);
    public static Energy operator %(Energy a, Energy b)
      => new Energy(a.m_value % b.m_value);
    public static Energy operator -(Energy a, Energy b)
      => new Energy(a.m_value - b.m_value);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Energy other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Energy other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Energy o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} J>";
    #endregion Object overrides
  }
}
