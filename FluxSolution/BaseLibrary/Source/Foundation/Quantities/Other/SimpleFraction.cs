//#if NET7_0_OR_GREATER
//using Flux.AmbOps;
//using Flux.Hashing;
//using System.Globalization;
//using System.Numerics;

//namespace Flux
//{
//  /// <summary>Simple fraction, unit of rational number, i.e. in the form of numerator and denominator.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Fraction#Simple,_common,_or_vulgar_fractions"/>
//  public readonly struct SimpleFraction
//    : System.IComparable, System.IComparable<SimpleFraction>, System.IConvertible, System.IEquatable<SimpleFraction>, IQuantifiable<double>
//    , System.Numerics.IComparisonOperators<SimpleFraction, SimpleFraction, bool>
//    , System.Numerics.IModulusOperators<SimpleFraction, System.Numerics.BigInteger, SimpleFraction>
//    , System.Numerics.INumber<SimpleFraction>
//    , System.Numerics.INumberBase<SimpleFraction>
//    , System.Numerics.ISignedNumber<SimpleFraction>
//  {
//    public static readonly SimpleFraction EpsilonLikeSingle = new(1, 1000000);

//    public static readonly SimpleFraction EpsilonLikeDouble = new(1, 1000000000000000);

//    /// <summary>Represents a BigFraction value of the Golden Ratio.</summary>
//    public static readonly SimpleFraction GoldenRatio = new(7540113804746346429L, 4660046610375530309L, true);

//    /// <summary>Represents a BigFraction value of PI.</summary>
//    public static readonly SimpleFraction PI = new(2646693125139304345L, 842468587426513207L, true);

//    private readonly System.Numerics.BigInteger m_numerator;
//    private readonly System.Numerics.BigInteger m_denominator;

//    /// <summary>Creates a new simple fraction from the specified numerator and denominator. Optionally the fraction can be reduced, if possible.</summary>
//    /// <param name="numerator"></param>
//    /// <param name="denominator"></param>
//    /// <param name="reduceIfPossible">If true, reduce if possible, and if false, do not attempt to reduce.</param>
//    public SimpleFraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, bool reduceIfPossible)
//    {
//      if (denominator.IsZero) throw new System.DivideByZeroException();

//      if (denominator.Sign < 0)
//      {
//        numerator = -numerator;
//        denominator = -denominator;
//      }

//      if (reduceIfPossible && IsReducible(numerator, denominator, out var gcd))
//      {
//        m_numerator = numerator / gcd;
//        m_denominator = denominator / gcd;
//      }
//      else
//      {
//        m_numerator = numerator;
//        m_denominator = denominator;
//      }
//    }
//    public SimpleFraction(System.Numerics.BigInteger whole, System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
//      : this(whole * denominator + numerator, denominator, true)
//    { }
//    /// <summary>Creates a new simple fraction from the specified numerator and denominator. If the fraction can be reduced, it will be.</summary>
//    /// <param name="numerator"></param>
//    /// <param name="denominator"></param>
//    public SimpleFraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
//      : this(numerator, denominator, true)
//    { }
//    public SimpleFraction(System.Numerics.BigInteger value)
//      : this(value, System.Numerics.BigInteger.One, false)
//    { }

//    public System.Numerics.BigInteger Numerator
//      => m_numerator;
//    public System.Numerics.BigInteger Denominator
//      => m_denominator;

//    /// <summary>Indicates whether the number is a whole integer, i.e. no fractional residue.</summary>
//    //public bool IsInteger
//    //  => m_denominator.IsOne;

//    /// <summary>Indicates whether the number is -1.</summary>
//    public bool IsMinusOne
//      => m_numerator == System.Numerics.BigInteger.MinusOne && m_denominator.IsOne;

//    /// <summary>Indicates whether the number is 1.</summary>
//    public bool IsOne
//      => m_numerator.IsOne && m_denominator.IsOne;

//    /// <summary>A fraction is proper if its absolute value is strictly less than 1, i.e. if it is greater than -1 and less than 1.</summary>
//    public bool IsProper
//      => m_numerator < m_denominator;

//    /// <summary>A fraction is a unit fraction if its numerator is equal to 1.</summary>
//    public bool IsUnitFraction
//      => m_numerator.IsOne;

//    /// <summary>Indicates whether the number is 0.</summary>
//    //public bool IsZero
//    //  => m_numerator.IsZero && m_denominator.IsOne;

//    /// <summary>Indicates the sign of the number, i.e. 1, 0 or -1.</summary>
//    public int Sign
//      => m_numerator.Sign is var ns && ns == 0
//      ? 0
//      : m_denominator.Sign is var ds && (ns < 0 && ds > 0) || (ds < 0 && ns >= 0)
//      ? -1
//      : 1;

//    /// <summary>Returns the integer quotient and an out variable containing the remainder.</summary>
//    public System.Numerics.BigInteger ToDivRem(out System.Numerics.BigInteger remainder)
//      => System.Numerics.BigInteger.DivRem(m_numerator, m_denominator, out remainder);

//    /// <summary>Yields a string with the fraction in improper (if applicable) fractional notation.</summary>
//    public string ToImproperString()
//    {
//      var sb = new System.Text.StringBuilder();

//      if (IsProper)
//        return ToProperString();
//      else
//      {
//        sb.Append(ToDivRem(out var remainder));

//        if (remainder > 0)
//        {
//          sb.Append(' ');
//          sb.Append(remainder);
//          sb.Append('\u2044');
//          sb.Append(m_denominator);
//        }
//      }

//      return sb.ToString();
//    }

//    /// <summary>Yields a string with the fraction in proper fractional notation.</summary>
//    public string ToProperString()
//      => $"{m_numerator}\u2044{m_denominator}";

//    /// <summary>Returns the quotient result from division of numerator / denominator.</summary>
//    public double ToQuotient()
//      => (double)m_numerator / (double)m_denominator;

//    #region Static methods

//    ///// <summary>Returns the absolute value a value.</summary>
//    //public static SimpleFraction Abs(SimpleFraction value)
//    //  => CopySign(value, One);

//    /// <summary>Returns the value with the sign of the second argument.</summary>
//    public static SimpleFraction CopySign(SimpleFraction value, SimpleFraction sign)
//      => (sign.Sign == 0 || value.IsZero)
//      ? Zero
//      : (value.m_numerator.Sign < 0 && sign.Sign > 0) || (value.m_numerator.Sign > 0 && sign.Sign < 0)
//      ? new(-value.m_numerator, value.m_denominator, true)
//      : value;

//    /// <summary>Returns the greatest common divisor (GCD) of two values.</summary>
//    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: (gcd(a,c) * gcd(b,d)) / (|b*d|), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
//    /// <example>gcd((a/b),(c/d)) = gcd(a,c) / lcm(b,d) = gcd(a,c) / (|b*d|/gcd(b,d))</example>
//    public static SimpleFraction GreatestCommonDivisor(SimpleFraction a, SimpleFraction b)
//      => a.IsZero
//      ? Abs(b)
//      : b.IsZero
//      ? Abs(a)
//      : new(
//          System.Numerics.BigInteger.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
//          System.Numerics.BigInteger.Abs(a.m_denominator * b.m_denominator) / System.Numerics.BigInteger.GreatestCommonDivisor(a.m_denominator, b.m_denominator),
//          true
//        );

//    /// <summary>Returns whether the specified numerator and denominator (fraction) is reducible.</summary>
//    public static bool IsReducible(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out System.Numerics.BigInteger gcd)
//      => (gcd = System.Numerics.BigInteger.GreatestCommonDivisor(numerator, denominator)) > 1;

//    /// <summary>Returns the least common multiple (LCM) of two values.</summary>
//    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: |a*c| / (gcd(a,c) * gcd(b,d)), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
//    /// <example>lcm((a/b),(c/d)) = lcm(a,c) / gcd(b,d) = (|a*c| / gcd(a,c)) / gcd(b,d)</example>
//    public static SimpleFraction LeastCommonMultiple(SimpleFraction a, SimpleFraction b)
//      => (a.IsZero || b.IsZero)
//      ? Zero
//      : new(
//          System.Numerics.BigInteger.Abs(a.m_numerator * b.m_numerator) / System.Numerics.BigInteger.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
//          System.Numerics.BigInteger.GreatestCommonDivisor(a.m_denominator, b.m_denominator),
//          true
//        );

//    /// <summary>Returns the greater of two values.</summary>
//    public static SimpleFraction Max(SimpleFraction a, SimpleFraction b)
//      => a.CompareTo(b) >= 0 ? a : b;

//    /// <summary>Returns the mediant of two values.</summary>
//    public static SimpleFraction Mediant(SimpleFraction a, SimpleFraction b)
//      => new(a.m_numerator + b.m_numerator, a.m_denominator + b.m_denominator, false);

//    /// <summary>Returns the lesser of two values.</summary>
//    public static SimpleFraction Min(SimpleFraction a, SimpleFraction b)
//      => a.CompareTo(b) <= 0 ? a : b;

//    /// <summary>Returns the nth root of the value.</summary>
//    private static SimpleFraction NthRoot(SimpleFraction value, int n, SimpleFraction maxError)
//    {
//      if (n < 0)
//        return NthRoot(Reciprocal(value), -n, maxError);

//      if (n == 0) throw new System.DivideByZeroException("Zeroth root is not defined.");
//      if (n == int.MinValue) throw new System.OverflowException("Value cannot be negated.");


//      if (value.Sign < 0)
//      {
//        if ((n & 1) == 0) throw new System.ArithmeticException("Cannot compute even root of a negative number.");

//        return -NthRoot(-value, n, maxError);
//      }

//      if (maxError.Sign <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxError), "Epsilon must be positive");

//      if (value == Zero)
//        return Zero;
//      if (n == 1)
//        return value;
//      if (value == One)
//        return value;

//      // First, get the closest integer to the root of the numerator and the denominator as an initial guess.

//      var guessNumerator = IntegerNthRoot(value.Numerator, n);
//      var guessDenominator = IntegerNthRoot(value.Denominator, n);

//      var initialGuess = new SimpleFraction(guessNumerator.value, guessDenominator.value);

//      // If we got exact roots for numerator and denominator, then we know the guess is exact.

//      if (guessNumerator.isExact && guessDenominator.isExact)
//        return initialGuess;

//      // Otherwise we use the implementation of nth-root algorithm: https://en.wikipedia.org/wiki/Nth_root_algorithm

//      var x = initialGuess;

//      while (true)
//      {
//        var diff = (value / Pow(x, n - 1) - x) / n;

//        x += diff;

//        if (Abs(diff).CompareTo(maxError) < 0)
//          break;
//      }

//      return x;

//      static (System.Numerics.BigInteger value, bool isExact) IntegerNthRoot(System.Numerics.BigInteger a, int n)
//      {
//        if (a.IsZero)
//          return (System.Numerics.BigInteger.Zero, true);

//        if (n == 1 || a.IsOne)
//          return (a, true);

//        // Solve for x:  x^n = a
//        // Start by computing a lower/upper bound on x.

//        var lowerX = System.Numerics.BigInteger.One;
//        var lowerPow = System.Numerics.BigInteger.One;

//        var upperX = (System.Numerics.BigInteger)2;
//        var upperPow = System.Numerics.BigInteger.Pow(upperX, n);

//        while (upperPow.CompareTo(a) < 0)
//        {
//          lowerX = upperX;
//          lowerPow = upperPow;
//          upperX = lowerX * 2;
//          upperPow = System.Numerics.BigInteger.Pow(upperX, n);
//        }

//        if (upperPow.Equals(a))
//          return (upperX, true); // If it's the exact answer, return it.

//        // Now we know lowerX < x < upperX.
//        // Next do binary search between lowerX and upperX.

//        while (true)
//        {
//          var testX = (lowerX + upperX) / 2;

//          if (testX.Equals(lowerX) || testX.Equals(upperX))
//            break;

//          var testPow = System.Numerics.BigInteger.Pow(testX, n);

//          if (testPow.Equals(a))
//            return (testX, true); // We found an exact answer.
//          else if (testPow.CompareTo(a) > 0) // Still too high so set upper to the test value.
//          {
//            upperX = testX;
//            upperPow = testPow;
//          }
//          else // Still too low.
//          {
//            lowerX = testX;
//            lowerPow = testPow;
//          }
//        }

//        //we didn't get an exact answer, but we know the two integers closest to the exact value.
//        //now we just need to figure out which is closer and return that

//        return (a - lowerPow).CompareTo(upperPow - a) < 0 ? (lowerX, false) : (upperX, false);
//      }
//    }

//    /// <summary>Returns this^exponent. Note: 0^0 will return 1/1.</summary>
//    public static SimpleFraction Pow(SimpleFraction value, int exponent)
//    {
//      if (exponent < 0)
//      {
//        if (IsZero(value)) throw new System.DivideByZeroException(@"Raising zero to negative exponent.");
//        else if (exponent == int.MinValue) throw new System.OverflowException(@"Exponent cannot be negated."); // edge case: because we negate the exponent if it's negative, we would get into an infinite loop because -MIN_VALUE == MIN_VALUE
//        else return new(System.Numerics.BigInteger.Pow(value.Denominator, -exponent), System.Numerics.BigInteger.Pow(value.Numerator, -exponent), true);
//      }

//      if (exponent == 0) return One;
//      else if (IsZero(value)) return Zero;
//      else if (exponent == 1) return value;
//      else return new(System.Numerics.BigInteger.Pow(value.Numerator, exponent), System.Numerics.BigInteger.Pow(value.Denominator, exponent), true);
//    }

//    /// <summary>Returns the reciprocal of a value.</summary>
//    public static SimpleFraction Reciprocal(SimpleFraction value)
//      => value.IsZero
//      ? throw new System.DivideByZeroException(@"Reciprocal of zero.")
//      : new(value.m_denominator, value.m_numerator, true);

//    /// <summary>Compute the square root of the specified value.</summary>
//    public static SimpleFraction Sqrt(SimpleFraction value)
//      => NthRoot(value, 2, EpsilonLikeDouble);

//    #endregion Static methods

//    #region Overloaded operators
//    [System.Diagnostics.Contracts.Pure] public static explicit operator double(SimpleFraction v) => v.Value;
//    #endregion Overloaded operators

//    #region Implemented interfaces
//    // IComparable<>
//    [System.Diagnostics.Contracts.Pure]
//    public int CompareTo(SimpleFraction other)
//      => (Sign != other.Sign)
//      ? (Sign - other.Sign)
//      : (m_denominator.Equals(other.m_denominator))
//      ? m_numerator.CompareTo(other.m_numerator)
//      : (m_numerator * other.m_denominator).CompareTo(m_denominator * other.m_numerator);
//    // IComparable
//    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is SimpleFraction o ? CompareTo(o) : -1;

//    #region IConvertible
//    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
//    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
//    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
//    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
//    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
//    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
//    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
//    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
//    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
//    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
//    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
//    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
//    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
//    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
//    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
//    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
//    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
//    #endregion IConvertible

//    // IEquatable<>
//    [System.Diagnostics.Contracts.Pure] public bool Equals(SimpleFraction other) => m_numerator == other.m_numerator && m_denominator == other.m_denominator;

//    // IQuantifiable<>
//    [System.Diagnostics.Contracts.Pure] public double Value => ToQuotient();

//    // IComparisonOperators<>
//    public static bool operator <(SimpleFraction a, SimpleFraction b) => a.CompareTo(b) < 0;
//    public static bool operator <=(SimpleFraction a, SimpleFraction b) => a.CompareTo(b) <= 0;
//    public static bool operator >(SimpleFraction a, SimpleFraction b) => a.CompareTo(b) > 0;
//    public static bool operator >=(SimpleFraction a, SimpleFraction b) => a.CompareTo(b) >= 0;

//    // INumberBase<>
//    public static SimpleFraction One => new(1, 1, false);
//    public static int Radix => 2;
//    public static SimpleFraction Two => new(2, 1, false);
//    public static SimpleFraction Zero => new(0, 1, false);

//    public static bool IsCanonical(SimpleFraction f) => false;
//    public static bool IsComplexNumber(SimpleFraction f) => false;
//    public static bool IsEvenInteger(SimpleFraction f) => IsInteger(f) && f.m_numerator.IsEven && !f.m_denominator.IsEven;
//    public static bool IsFinite(SimpleFraction f) => true;
//    public static bool IsImaginaryNumber(SimpleFraction f) => false;
//    public static bool IsInfinity(SimpleFraction f) => false;
//    public static bool IsInteger(SimpleFraction f) => f.m_denominator.IsOne;
//    public static bool IsNaN(SimpleFraction f) => false;
//    public static bool IsNegative(SimpleFraction f) => f.m_numerator.Sign is var ns && f.m_denominator.Sign is var ds && (ns < 0 && ds > 0) || (ds < 0 && ns >= 0);
//    public static bool IsNegativeInfinity(SimpleFraction f) => false;
//    public static bool IsNormal(SimpleFraction f) => false;
//    public static bool IsOddInteger(SimpleFraction f) => IsInteger(f) && !f.m_numerator.IsEven && !f.m_denominator.IsEven;
//    public static bool IsPositive(SimpleFraction f) => !IsNegative(f) && !IsZero(f);
//    public static bool IsPositiveInfinity(SimpleFraction f) => false;
//    public static bool IsRealNumber(SimpleFraction f) => false;
//    public static bool IsSubnormal(SimpleFraction f) => false;
//    public static bool IsZero(SimpleFraction f) => f.m_numerator.IsZero && f.m_denominator.IsOne;
//    public static SimpleFraction MaxMagnitude(SimpleFraction a, SimpleFraction b) => a > b ? a : b;
//    public static SimpleFraction MaxMagnitudeNumber(SimpleFraction a, SimpleFraction b) => MaxMagnitude(a, b);
//    public static SimpleFraction MinMagnitude(SimpleFraction a, SimpleFraction b) => a < b ? a : b;
//    public static SimpleFraction MinMagnitudeNumber(SimpleFraction a, SimpleFraction b) => MinMagnitude(a, b);

//    public static SimpleFraction AdditiveIdentity => Zero;
//    public static SimpleFraction MultiplicativeIdentity => One;

//    public static SimpleFraction operator +(SimpleFraction a, SimpleFraction b)
//    {
//      var lcm = GenericMath.LeastCommonMultiple(a.m_denominator, b.m_denominator);

//      var an = lcm / a.m_denominator * a.m_numerator;
//      var bn = lcm / b.m_denominator * b.m_numerator;

//      return new SimpleFraction(an + bn, lcm);
//    }
//    public static SimpleFraction operator +(SimpleFraction a, System.Numerics.BigInteger b) => a + new SimpleFraction(b);
//    public static SimpleFraction operator --(SimpleFraction f) => f - One;
//    public static SimpleFraction operator /(SimpleFraction a, SimpleFraction b) => new(a.m_numerator * b.m_denominator, a.m_denominator * b.m_numerator);
//    public static SimpleFraction operator /(SimpleFraction a, System.Numerics.BigInteger b) => a / new SimpleFraction(b);
//    public static bool operator ==(SimpleFraction a, SimpleFraction b) => a.Equals(b);
//    public static bool operator !=(SimpleFraction a, SimpleFraction b) => !a.Equals(b);
//    public static SimpleFraction operator ++(SimpleFraction f) => f + One;
//    public static SimpleFraction operator %(SimpleFraction a, System.Numerics.BigInteger b) => new(a.m_numerator % (a.m_denominator * b), a.m_denominator);
//    public static SimpleFraction operator *(SimpleFraction a, SimpleFraction b) => new(a.m_numerator * b.m_numerator, a.m_denominator * b.m_denominator);
//    public static SimpleFraction operator *(SimpleFraction a, System.Numerics.BigInteger b) => a * new SimpleFraction(b);
//    public static SimpleFraction operator -(SimpleFraction a, SimpleFraction b)
//    {
//      var lcm = GenericMath.LeastCommonMultiple(a.m_denominator, b.m_denominator);

//      var an = lcm / a.m_denominator * a.m_numerator;
//      var bn = lcm / b.m_denominator * b.m_numerator;

//      return new SimpleFraction(an - bn, lcm);
//    }
//    public static SimpleFraction operator -(SimpleFraction a, System.Numerics.BigInteger b) => a - new SimpleFraction(b);
//    public static SimpleFraction operator -(SimpleFraction f) => new(f.m_numerator, -f.m_denominator, false);
//    public static SimpleFraction operator +(SimpleFraction f) => new(f.m_numerator, f.m_denominator, false);

//    public static SimpleFraction Abs(SimpleFraction f) => CopySign(f, One);
//    static SimpleFraction INumberBase<SimpleFraction>.Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider) => throw new NotImplementedException();
//    static SimpleFraction INumberBase<SimpleFraction>.Parse(string s, NumberStyles style, IFormatProvider? provider) => throw new NotImplementedException();
//    static bool INumberBase<SimpleFraction>.TryConvertFromChecked<TOther>(TOther value, out SimpleFraction result) => throw new NotImplementedException();
//    static bool INumberBase<SimpleFraction>.TryConvertFromSaturating<TOther>(TOther value, out SimpleFraction result) => throw new NotImplementedException();
//    static bool INumberBase<SimpleFraction>.TryConvertFromTruncating<TOther>(TOther value, out SimpleFraction result) => throw new NotImplementedException();
//    static bool INumberBase<SimpleFraction>.TryConvertToChecked<TOther>(SimpleFraction value, out TOther result) => throw new NotImplementedException();
//    static bool INumberBase<SimpleFraction>.TryConvertToSaturating<TOther>(SimpleFraction value, out TOther result) => throw new NotImplementedException();
//    static bool INumberBase<SimpleFraction>.TryConvertToTruncating<TOther>(SimpleFraction value, out TOther result) => throw new NotImplementedException();
//    static bool INumberBase<SimpleFraction>.TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out SimpleFraction result) => throw new NotImplementedException();
//    static bool INumberBase<SimpleFraction>.TryParse(string? s, NumberStyles style, IFormatProvider? provider, out SimpleFraction result) => throw new NotImplementedException();
//    bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider) => throw new NotImplementedException();
//    string IFormattable.ToString(string? format, IFormatProvider? formatProvider) => throw new NotImplementedException();
//    static SimpleFraction ISpanParsable<SimpleFraction>.Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => throw new NotImplementedException();
//    static bool ISpanParsable<SimpleFraction>.TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out SimpleFraction result) => throw new NotImplementedException();
//    static SimpleFraction IParsable<SimpleFraction>.Parse(string s, IFormatProvider? provider) => throw new NotImplementedException();
//    static bool IParsable<SimpleFraction>.TryParse(string? s, IFormatProvider? provider, out SimpleFraction result) => throw new NotImplementedException();

//    // ISignedNumber<>
//    public static SimpleFraction NegativeOne => new(-1, 1, false);
//    public static SimpleFraction NegativeTwo => new(-2, 1, false);

//    #endregion Implemented interfaces

//    #region Object overrides
//    [System.Diagnostics.Contracts.Pure]
//    public override bool Equals(object? obj)
//      => obj is SimpleFraction o && Equals(o);
//    [System.Diagnostics.Contracts.Pure]
//    public override int GetHashCode()
//      => System.HashCode.Combine(m_numerator, m_denominator);
//    [System.Diagnostics.Contracts.Pure]
//    public override string ToString()
//    {
//      var sb = new System.Text.StringBuilder();

//      sb.Append(GetType().Name);
//      sb.Append(" { ");

//      sb.Append(ToProperString());

//      var mixedString = ToImproperString();
//      if (!sb.EndsWith(mixedString))
//      {
//        sb.Append(" = ");
//        sb.Append(mixedString);
//      }

//      sb.Append(" = ");
//      sb.Append(ToQuotient());
//      sb.Append(" } ");

//      return sb.ToString();
//    }
//    #endregion Object overrides
//  }
//}
//#endif
