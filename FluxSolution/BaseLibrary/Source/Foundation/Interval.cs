//namespace Flux
//{
//  /// <summary>Interval.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Interval_(mathematics)"/>
//  public struct Interval
//    : System.IEquatable<Interval>
//  {
//    public static Interval Empty;

//    private readonly bool m_isLeftOpen;
//    private readonly bool m_isRightOpen;

//    private readonly double m_valueOfLeft;
//    private readonly double m_valueOfRight;

//    public Interval(bool isLeftOpen, bool isRightOpen, double valueOfLeft, double valueOfRight)
//    {
//      m_isLeftOpen = isLeftOpen;
//      m_isRightOpen = isRightOpen;

//      m_valueOfLeft = valueOfLeft;
//      m_valueOfRight = valueOfRight;
//    }

//    public double Center
//      => (m_valueOfLeft + m_valueOfRight) / 2;

//    public double Diameter
//      => System.Math.Abs(m_valueOfLeft - m_valueOfRight);

//    public bool HasMaxValue
//      => !m_isRightOpen;
//    public bool HasMinValue
//      => !m_isLeftOpen;

//    public bool IsEmpty
//      => Equals(Empty);

//    public bool IsLeftOpen
//      => m_isLeftOpen;
//    public bool IsRightOpen
//      => m_isRightOpen;

//    public bool IsWithin(double value)
//      => IsWithinLeft(value) && IsWithinRight(value);
//    public bool IsWithinLeft(double value)
//      => m_isLeftOpen ? value > m_valueOfLeft : value >= m_valueOfLeft;
//    public bool IsWithinRight(double value)
//      => m_isRightOpen ? value < m_valueOfRight : value <= m_valueOfRight;

//    public double MaxValue
//      => HasMaxValue ? m_valueOfRight : throw new System.ArithmeticException();
//    public double MinValue
//      => HasMinValue ? m_valueOfLeft : throw new System.ArithmeticException();

//    public double Radius
//      => System.Math.Abs(m_valueOfLeft - m_valueOfRight) / 2;

//    public double ValueLeft
//      => m_valueOfLeft;
//    public double ValueRight
//      => m_valueOfLeft;

//    public double ClampLeft(double value)
//    {
//      if (m_isLeftOpen ? value <= m_valueOfLeft : value < m_valueOfLeft)
//        value = m_valueOfLeft - value;

//      return value;
//    }

//    #region Overloaded operators
//    public static bool operator ==(Interval a, Interval b)
//      => a.Equals(b);
//    public static bool operator !=(Interval a, Interval b)
//      => !a.Equals(b);
//    #endregion Overloaded operators

//    #region Implemented interfaces
//    // IEquatable
//    public bool Equals(Interval other)
//      => m_isLeftOpen == other.m_isLeftOpen && m_isRightOpen == other.m_isRightOpen && m_valueOfLeft == other.m_valueOfLeft && m_valueOfRight == other.m_valueOfRight;
//    #endregion Implemented interfaces

//    #region Object overrides
//    public override bool Equals(object? obj)
//      => obj is Interval o && Equals(o);
//    public override int GetHashCode()
//      => System.HashCode.Combine(m_isLeftOpen, m_isRightOpen, m_valueOfLeft, m_valueOfRight);
//    public override string ToString()
//      => $"{(m_isLeftOpen ? '(' : '[')}{m_valueOfLeft}, {m_valueOfRight}{(m_isRightOpen ? ')' : ']')}";
//    #endregion Object overrides
//  }
//}
