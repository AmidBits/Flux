namespace Flux.Quantity
{
  /// <summary>Simple fraction.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Fraction#Simple,_common,_or_vulgar_fractions"/>
#if NET5_0
  public struct SimpleFraction
    : System.IComparable<SimpleFraction>, System.IEquatable<SimpleFraction>, IValuedUnit<double>
#else
  public record struct SimpleFraction
    : System.IComparable<SimpleFraction>, IValuedUnit<double>
#endif
  {
    /// <summary>Represents a SimpleFraction value of -1.</summary>
    public static SimpleFraction MinusOne
      => new(-1, 1, false);
    /// <summary>Represents a SimpleFraction value of -1.</summary>
    public static SimpleFraction MinusTwo
      => new(-2, 1, false);
    /// <summary>Represents a SimpleFraction value of 1.</summary>
    public static SimpleFraction One
      => new(1, 1, false);
    /// <summary>Represents a SimpleFraction value of 2.</summary>
    public static SimpleFraction Two
      => new(2, 1, false);
    /// <summary>Represents a SimpleFraction value of 0.</summary>
    public static SimpleFraction Zero
      => new(0, 1, false);

    private readonly System.Numerics.BigInteger m_numerator;
    private readonly System.Numerics.BigInteger m_denominator;

    /// <summary>Creates a new simple fraction from the specified numerator and denominator. Optionally the fraction can be reduced, if possible.</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="reduceIfPossible">If true, reduce if possible, and if false, do not attempt to reduce.</param>
    public SimpleFraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, bool reduceIfPossible)
    {
      if (reduceIfPossible && IsReducible(numerator, denominator, out var gcd))
      {
        m_numerator = numerator / gcd;
        m_denominator = denominator / gcd;
      }
      else
      {
        m_numerator = numerator;
        m_denominator = denominator;
      }

      if (m_denominator == 0) throw new System.ArithmeticException(@"The denominator cannot be zero.");
    }
    /// <summary>Creates a new simple fraction from the specified numerator and denominator. If the fraction can be reduced, it will be.</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    public SimpleFraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
      : this(numerator, denominator, true)
    { }

    public System.Numerics.BigInteger Numerator
      => m_numerator;
    public System.Numerics.BigInteger Denominator
      => m_denominator;

    /// <summary>Is this a unit fraction, e.g. a probability.</summary>
    public bool IsUnitFraction
      => m_numerator == 1;

    /// <summary>Indicates the sign of the number, i.e. 1, 0 or -1.</summary>
    public int Sign
      => m_numerator.Sign is var ns && ns == 0
      ? 0
      : m_denominator.Sign is var ds && (ns < 0 && ds >= 0) || (ds < 0 && ns >= 0)
      ? -1
      : 1;

    /// <summary>Returns the decimal representation (as a double, floating point value) of the fraction.</summary>
    public double Value
      => (double)m_numerator / (double)m_denominator;

    #region Static methods
    /// <summary>Returns the absolute value a value.</summary>
    public static SimpleFraction Abs(SimpleFraction value)
      => CopySign(value, One);
    /// <summary>Returns the value with the sign of the second argument.</summary>
    public static SimpleFraction CopySign(SimpleFraction value, SimpleFraction sign)
      => (sign.Sign == 0 || value == Zero)
      ? Zero
      : (value.m_numerator.Sign < 0 && sign.Sign > 0) || (value.m_numerator.Sign > 0 && sign.Sign < 0)
      ? new SimpleFraction(-value.m_numerator, value.m_denominator, true)
      : value;
    /// <summary>Returns the greatest common divisor (GCD) of two values.</summary>
    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: (gcd(a,c) * gcd(b,d)) / (|b*d|), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
    /// <example>gcd((a/b),(c/d)) = gcd(a,c) / lcm(b,d) = gcd(a,c) / (|b*d|/gcd(b,d))</example>
    public static SimpleFraction GreatestCommonDivisor(SimpleFraction a, SimpleFraction b)
      => (a == Zero)
      ? Abs(b)
      : (b == Zero)
      ? Abs(a)
      : new SimpleFraction(
          System.Numerics.BigInteger.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
          System.Numerics.BigInteger.Abs(a.m_denominator * b.m_denominator) / System.Numerics.BigInteger.GreatestCommonDivisor(a.m_denominator, b.m_denominator),
          true
        );
    /// <summary>Returns whether the specified numerator and denominator (fraction) is reducible.</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="gcd"></param>
    /// <returns></returns>
    public static bool IsReducible(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out System.Numerics.BigInteger gcd)
      => (gcd = System.Numerics.BigInteger.GreatestCommonDivisor(numerator, denominator)) > 1;
    /// <summary>Returns the least common multiple (LCM) of two values.</summary>
    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: |a*c| / (gcd(a,c) * gcd(b,d)), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
    /// <example>lcm((a/b),(c/d)) = lcm(a,c) / gcd(b,d) = (|a*c| / gcd(a,c)) / gcd(b,d)</example>
    public static SimpleFraction LeastCommonMultiple(SimpleFraction a, SimpleFraction b)
      => (a == Zero || b == Zero)
      ? Zero
      : new SimpleFraction(
          System.Numerics.BigInteger.Abs(a.m_numerator * b.m_numerator) / System.Numerics.BigInteger.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
          System.Numerics.BigInteger.GreatestCommonDivisor(a.m_denominator, b.m_denominator),
          true
        );
    /// <summary>Returns the mediant of two values.</summary>
    public static SimpleFraction Mediant(SimpleFraction a, SimpleFraction b)
      => new(a.m_numerator + b.m_numerator, a.m_denominator + b.m_denominator, false);
    /// <summary>Returns the reciprocal of a value.</summary>
    public static SimpleFraction Reciprocal(SimpleFraction value)
      => value == Zero
      ? throw new System.DivideByZeroException(@"Reciprocal of zero.")
      : new SimpleFraction(value.m_denominator, value.m_numerator, true);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(SimpleFraction v)
      => v.Value;

    public static bool operator <(SimpleFraction a, SimpleFraction b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(SimpleFraction a, SimpleFraction b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(SimpleFraction a, SimpleFraction b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(SimpleFraction a, SimpleFraction b)
      => a.CompareTo(b) >= 0;

#if NET5_0
    public static bool operator ==(SimpleFraction a, SimpleFraction b)
      => a.Equals(b);
    public static bool operator !=(SimpleFraction a, SimpleFraction b)
      => !a.Equals(b);
#endif

    public static SimpleFraction operator -(SimpleFraction v)
      => new(-v.m_numerator, -v.m_denominator, false);
    public static SimpleFraction operator +(SimpleFraction a, SimpleFraction b)
    {
      var lcm = Maths.LeastCommonMultiple(a.m_denominator, b.m_denominator);

      var an = lcm / a.m_denominator * a.m_numerator;
      var bn = lcm / b.m_denominator * b.m_numerator;

      return new SimpleFraction(an + bn, lcm);
    }
    public static SimpleFraction operator /(SimpleFraction a, SimpleFraction b)
      => new(a.m_numerator * b.m_denominator, a.m_denominator * b.m_numerator);
    public static SimpleFraction operator *(SimpleFraction a, SimpleFraction b)
      => new(a.m_numerator * b.m_numerator, a.m_denominator * b.m_denominator);
    public static SimpleFraction operator %(SimpleFraction a, System.Numerics.BigInteger b)
      => new(a.m_numerator % (a.m_denominator * b), a.m_denominator);
    public static SimpleFraction operator -(SimpleFraction a, SimpleFraction b)
    {
      var lcm = Maths.LeastCommonMultiple(a.m_denominator, b.m_denominator);

      var an = lcm / a.m_denominator * a.m_numerator;
      var bn = lcm / b.m_denominator * b.m_numerator;

      return new SimpleFraction(an - bn, lcm);
    }
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(SimpleFraction other)
    {
      if (Sign is var sign && other.Sign is var otherSign && sign != otherSign)
        return sign - otherSign;

      if (m_denominator.Equals(other.m_denominator))
        return m_numerator.CompareTo(other.m_numerator);

      return (m_numerator * other.m_denominator).CompareTo(m_denominator * other.m_numerator);
    }

#if NET5_0
    // IEquatable
    public bool Equals(SimpleFraction other)
      => m_numerator == other.m_numerator && m_denominator == other.m_denominator;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is SimpleFraction o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_numerator, m_denominator);
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Numerator = {m_numerator}, Denominator = {m_denominator} ({Value}) }}";
    #endregion Object overrides
  }
}
