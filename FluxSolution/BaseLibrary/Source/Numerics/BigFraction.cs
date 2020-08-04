namespace Flux.Numerics
{
  /// <summary></summary>
  /// <seealso cref="https://github.com/kiprobinson/BigFraction"/>
  /// <seealso cref="https://github.com/bazzilic/BigFraction"/>
  public struct BigFraction
    : System.IComparable<BigFraction>, System.IEquatable<BigFraction>
  {
    //public static BigFraction ONE_HALF = new BigFraction(BigInteger.ONE, BIGINT_TWO, Reduced.YES);
    //public static BigFraction ONE_TENTH = new BigFraction(BigInteger.ONE, BigInteger.TEN, Reduced.YES);
    //public static BigFraction Ten = new BigFraction(BigInteger.TEN, BigInteger.ONE, Reduced.YES);

    //public enum Reduced { Yes, No };
    public enum FareyMode { NEXT, PREV, CLOSEST };

    public System.Numerics.BigInteger Numerator { get; private set; }
    public System.Numerics.BigInteger Denominator { get; private set; }

    /// <summary>Returns whether the number is a whole integer, i.e. no fractional residue.</summary>
    public bool IsInteger
      => Denominator.IsOne;
    /// <summary>Returns whether the number is -1.</summary>
    public bool IsMinusOne
      => Numerator == System.Numerics.BigInteger.MinusOne && Denominator.IsOne;
    /// <summary>Returns whether the number is 1.</summary>
    public bool IsOne
      => Numerator.IsOne && Denominator.IsOne;
    /// <summary>Returns whether the number is 0.</summary>
    public bool IsZero
      => Numerator.IsZero && Denominator.IsOne;
    /// <summary>Returns the sign of the number, i.e. 1, 0 or -1.</summary>
    public int Sign
      => Numerator.Sign;

    private BigFraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, bool reduced)
    {
      if (denominator.IsZero) throw new System.DivideByZeroException();

      if (denominator.Sign < 0) // Only the numerator should be negative.
      {
        numerator = -numerator;
        denominator = -denominator;
      }

      if (!reduced)
      {
        if (numerator.IsZero) denominator = System.Numerics.BigInteger.One;
        else if (!denominator.IsOne && System.Numerics.BigInteger.GreatestCommonDivisor(numerator, denominator) is var gcd && !gcd.Equals(System.Numerics.BigInteger.One))
        {
          numerator /= gcd;
          denominator /= gcd;
        }
      }

      Numerator = numerator;
      Denominator = denominator;
    }

    public BigFraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
      : this(numerator, denominator, false)
    {
    }
    public BigFraction(System.Numerics.BigInteger value)
      : this(value, System.Numerics.BigInteger.One, false) { }
    public BigFraction(decimal value)
    {
      var countDecimalPlaces = System.BitConverter.GetBytes(decimal.GetBits(value)[3])[2];

      Numerator = new System.Numerics.BigInteger(value * (System.Decimal)Math.Pow(10, countDecimalPlaces));
      Denominator = new System.Numerics.BigInteger(Math.Pow(10, countDecimalPlaces));
    }
    public BigFraction(double value, double accuracy = 1E-15)
    {
      if (accuracy <= 0.0 || accuracy >= 1.0) throw new System.ArgumentOutOfRangeException(nameof(accuracy), @"Must be > 0 and < 1.");

      var sign = System.Math.Sign(value);

      if (sign == -1) value = System.Math.Abs(value);

      var epsilon = sign == 0 ? accuracy : value * accuracy; // Accuracy is the maximum relative error; convert to absolute maxError

      var n = new System.Numerics.BigInteger(value);
      value -= System.Math.Floor(value);

      if (value < epsilon)
      {
        Numerator = sign * n;
        Denominator = System.Numerics.BigInteger.One;
      }
      if (1 - epsilon < value)
      {
        Numerator = sign * (n + 1);
        Denominator = System.Numerics.BigInteger.One;
      }

      var lower = (numerator: 0, denominator: 1); // The lower fraction is 0/1
      var upper = (numerator: 1, denominator: 1); // The upper fraction is 1/1

      while (true)
      {
        var middle = (numerator: lower.numerator + upper.numerator, denominator: lower.denominator + upper.denominator); // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)

        if (middle.denominator * (value + epsilon) < middle.numerator) upper = middle; // real + error < middle : middle is our new upper.
        else if (middle.numerator < (value - epsilon) * middle.denominator) lower = middle; // middle < real - error : middle is our new lower.
        else // Middle is our best fraction.
        {
          Numerator = (n * middle.denominator + middle.numerator) * sign;
          Denominator = middle.denominator;
        }
      }
    }
    public BigFraction(long value)
      : this(value, System.Numerics.BigInteger.One, false) { }
    public BigFraction(int value)
      : this(value, System.Numerics.BigInteger.One, false) { }

    #region Operators
    public static explicit operator System.Numerics.BigInteger(BigFraction value)
      => value.Numerator / value.Denominator;
    public static explicit operator decimal(BigFraction value)
      => (decimal)value.Numerator / (decimal)value.Denominator;
    public static explicit operator double(BigFraction value)
      => (double)value.Numerator / (double)value.Denominator;
    public static explicit operator long(BigFraction value)
      => (long)(value.Numerator / value.Denominator);
    public static explicit operator int(BigFraction value)
      => (int)(value.Numerator / value.Denominator);

    public static implicit operator BigFraction(System.Numerics.BigInteger value)
      => new BigFraction(value);
    public static implicit operator BigFraction(decimal value)
      => new BigFraction(value);
    public static implicit operator BigFraction(double value)
      => new BigFraction(value);
    public static implicit operator BigFraction(long value)
      => new BigFraction(value);
    public static implicit operator BigFraction(int value)
      => new BigFraction(value);

    public static BigFraction operator -(BigFraction value)
      => Negate(value);

    public static bool operator ==(BigFraction a, BigFraction b)
      => a.Equals(b);
    public static bool operator !=(BigFraction a, BigFraction b)
      => !a.Equals(b);
    public static bool operator <=(BigFraction a, BigFraction b)
      => a.CompareTo(b) <= 0;
    public static bool operator >=(BigFraction a, BigFraction b)
      => a.CompareTo(b) >= 0;

    public static bool operator <(BigFraction a, BigFraction b)
      => a.CompareTo(b) < 0;
    public static bool operator >(BigFraction a, BigFraction b)
      => a.CompareTo(b) > 0;

    public static BigFraction operator -(BigFraction a, BigFraction b)
      => Subtract(a, b);
    public static BigFraction operator +(BigFraction a, BigFraction b)
      => Add(a, b);
    public static BigFraction operator *(BigFraction a, BigFraction b)
      => Multiply(a, b);
    public static BigFraction operator /(BigFraction a, BigFraction b)
      => Divide(a, b);
    #endregion Operators

    public void Simplify()
    {
      var quotient = System.Numerics.BigInteger.DivRem(Numerator, Denominator, out var remainder);

      var gcd = System.Numerics.BigInteger.GreatestCommonDivisor(remainder, Denominator);

      remainder = remainder / gcd;

      Denominator = Denominator / gcd;
      Numerator = (quotient * Denominator) + remainder;
    }

    public System.Numerics.BigInteger ToBigInteger()
      => Numerator / Denominator;
    public decimal ToDecimal()
    {
      return DecimalScale(this);

      static decimal DecimalScale(BigFraction bigFraction)
      {
        if (bigFraction.Numerator <= MaxDecimal && bigFraction.Numerator >= MinDecimal && bigFraction.Denominator <= MaxDecimal && bigFraction.Denominator >= MinDecimal)
          return (decimal)bigFraction.Numerator / (decimal)bigFraction.Denominator;

        if (bigFraction.Numerator / bigFraction.Denominator is var intPart && intPart != 0)
          return (decimal)intPart + DecimalScale(bigFraction - intPart);
        else
          return bigFraction.Numerator == 0 ? 0 : 1 / DecimalScale(1 / bigFraction);
      }
    }
    public double ToDouble()
  => (double)Numerator / (double)Denominator;
    public float ToFloat()
      => (float)Numerator / (float)Denominator;

    #region Static
    private static readonly System.Numerics.BigInteger MaxDecimal = new System.Numerics.BigInteger(decimal.MaxValue);
    private static readonly System.Numerics.BigInteger MinDecimal = new System.Numerics.BigInteger(decimal.MinValue);

    /// <summary>Represents a BigFraction value of 5.</summary>
    public static BigFraction Five = new BigFraction(5, System.Numerics.BigInteger.One, true);
    /// <summary>Represents a BigFraction value of -1.</summary>
    public static BigFraction MinusOne = new BigFraction(-1, System.Numerics.BigInteger.One, true);
    /// <summary>Represents a BigFraction value of 1.</summary>
    public static BigFraction One = new BigFraction(System.Numerics.BigInteger.One, System.Numerics.BigInteger.One, true);
    /// <summary>Represents a BigFraction value of 10.</summary>
    public static BigFraction Ten = new BigFraction(10, System.Numerics.BigInteger.One, true);
    /// <summary>Represents a BigFraction value of 2.</summary>
    public static BigFraction Two = new BigFraction(2, System.Numerics.BigInteger.One, true);
    /// <summary>Represents a BigFraction value of 0.</summary>
    public static BigFraction Zero = new BigFraction(0, System.Numerics.BigInteger.One, true);

    /// <summary>Returns the absolute value a value.</summary>
    public static BigFraction Abs(BigFraction value)
      => CopySign(value, One);
    /// <summary>Returns the sum of two values.</summary>
    public static BigFraction Add(BigFraction a, BigFraction b)
      => new BigFraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator, false);
    public static BigFraction CopySign(BigFraction value, BigFraction sign)
      => (sign.Sign == 0 || value.IsZero) ? Zero : ((value.Numerator.Sign < 0 && sign.Sign > 0) || (value.Numerator.Sign > 0 && sign.Sign < 0)) ? new BigFraction(-value.Numerator, value.Denominator, true) : value;
    /// <summary>Returns the quotient of two values.</summary>
    public static BigFraction Divide(BigFraction a, BigFraction b)
      => new BigFraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator, false);
    /// <summary>Convert from decimal to fraction.</summary>
    //public static BigFraction FromDouble(double value, double accuracy) // Accuracy is used to convert recurring decimals into fractions (eg. 0.166667 -> 1/6).
    //{
    //  if (accuracy <= 0.0 || accuracy >= 1.0) throw new System.ArgumentOutOfRangeException(nameof(accuracy), @"Must be > 0 and < 1.");

    //  var sign = System.Math.Sign(value);

    //  if (sign == -1) value = System.Math.Abs(value);

    //  var epsilon = sign == 0 ? accuracy : value * accuracy; // Accuracy is the maximum relative error; convert to absolute maxError

    //  var n = new System.Numerics.BigInteger(value);
    //  value -= System.Math.Floor(value);

    //  if (value < epsilon) return new BigFraction(sign * n, System.Numerics.BigInteger.One);
    //  if (1 - epsilon < value) return new BigFraction(sign * (n + 1), System.Numerics.BigInteger.One);

    //  // The lower fraction is 0/1
    //  var lower_n = 0;
    //  var lower_d = 1;

    //  // The upper fraction is 1/1
    //  var upper_n = 1;
    //  var upper_d = 1;

    //  while (true)
    //  {
    //    // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
    //    var middle_n = lower_n + upper_n;
    //    var middle_d = lower_d + upper_d;

    //    if (middle_d * (value + epsilon) < middle_n) // real + error < middle : middle is our new upper.
    //    {
    //      upper_n = middle_n;
    //      upper_d = middle_d;
    //    }
    //    else if (middle_n < (value - epsilon) * middle_d) // middle < real - error : middle is our new lower.
    //    {
    //      lower_n = middle_n;
    //      lower_d = middle_d;
    //    }
    //    else return new BigFraction((n * middle_d + middle_n) * sign, middle_d);  // Middle is our best fraction.
    //  }
    //}
    /// <summary>Returns the greatest common divisor (GCD) of two values.</summary>
    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: (gcd(a,c) * gcd(b,d)) / (|b*d|), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
    /// <example>gcd((a/b),(c/d)) = gcd(a,c) / lcm(b,d) = gcd(a,c) / (|b*d|/gcd(b,d))</example>
    public static BigFraction GreatestCommonDivisor(BigFraction a, BigFraction b)
    {
      return a.IsZero ? Abs(b) : b.IsZero ? Abs(a) : new BigFraction(
             System.Numerics.BigInteger.GreatestCommonDivisor(a.Numerator, b.Numerator),
             System.Numerics.BigInteger.Abs(a.Denominator * b.Denominator) / System.Numerics.BigInteger.GreatestCommonDivisor(a.Denominator, b.Denominator),
             true);
    }
    /// <summary>Returns the least common multiple (LCM) of two values.</summary>
    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: |a*c| / (gcd(a,c) * gcd(b,d)), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
    /// <example>lcm((a/b),(c/d)) = lcm(a,c) / gcd(b,d) = (|a*c| / gcd(a,c)) / gcd(b,d)</example>
    public static BigFraction LeastCommonMultiple(BigFraction a, BigFraction b)
      => a.IsZero || b.IsZero ? Zero : new BigFraction(
          System.Numerics.BigInteger.Abs(a.Numerator * b.Numerator) / System.Numerics.BigInteger.GreatestCommonDivisor(a.Numerator, b.Numerator),
          System.Numerics.BigInteger.GreatestCommonDivisor(a.Denominator, b.Denominator),
          true);
    /// <summary>Returns the maximum of two values.</summary>
    public static BigFraction Max(BigFraction a, BigFraction b)
      => a.CompareTo(b) >= 0 ? a : b;
    /// <summary>Returns the mediant of two values.</summary>
    public static BigFraction Mediant(BigFraction a, BigFraction b)
      => new BigFraction(a.Numerator + b.Numerator, a.Denominator + b.Denominator, false);
    /// <summary>Returns the minimum of two values.</summary>
    public static BigFraction Min(BigFraction a, BigFraction b)
      => a.CompareTo(b) <= 0 ? a : b;
    public static BigFraction Modulo(BigFraction a, System.Numerics.BigInteger b)
      => new BigFraction(a.Numerator % (a.Denominator * b), a.Denominator);
    /// <summary>Returns the product of two values.</summary>
    public static BigFraction Multiply(BigFraction a, BigFraction b)
      => new BigFraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator, false);
    /// <summary>Returns the value negated.</summary>
    public static BigFraction Negate(BigFraction v)
      => new BigFraction(-v.Numerator, v.Denominator, true);
    public static BigFraction NthRoot(BigFraction v, int n, BigFraction maxError)
    {
      if (n == 0) throw new System.DivideByZeroException("Zeroth root is not defined.");
      if (n == int.MinValue) throw new System.OverflowException("Value cannot be negated.");
      if (n < 0) return NthRoot(Reciprocal(v), -n, maxError);

      if (v.Sign < 0)
      {
        if ((n & 1) == 0) throw new System.ArithmeticException("Cannot compute even root of a negative number.");

        return Negate(NthRoot(Negate(v), n, maxError));
      }

      if (maxError == null || maxError.Sign <= 0) throw new System.ArgumentOutOfRangeException("Epsilon must be positive");

      if (v.IsZero) return Zero;
      if (n == 1) return v;
      if (v.IsOne) return v;

      //First, get the closest integer to the root of the numerator and the denominator for our first guess.

      var guessNumParts = NthRootInt(v.Numerator, n);
      var guessDenParts = NthRootInt(v.Denominator, n);

      var guess = new BigFraction(guessNumParts.value, guessDenParts.value);

      //If we got exact roots for numerator and denominator, then we know the guess is exact. Otherwise we must use 

      if (guessNumParts.isExact && guessDenParts.isExact) return guess;

      //implementation of nth-root algorithm: https://en.wikipedia.org/wiki/Nth_root_algorithm

      var x = guess;

      while (true)
      {
        var diff = (v / Pow(x, n - 1) - x) / n;

        x += diff;

        if (Abs(diff).CompareTo(maxError) < 0) break;
      }

      return x;

      static (System.Numerics.BigInteger value, bool isExact) NthRootInt(System.Numerics.BigInteger a, int n)
      {
        if (a.IsZero) return (System.Numerics.BigInteger.Zero, true);

        if (n == 1 || a.IsOne) return (a, true);

        // solve for x:  x^n = a
        // start by computing a lower/upper bound on x

        var lowerX = System.Numerics.BigInteger.One;
        var lowerPow = System.Numerics.BigInteger.One;

        var upperX = (System.Numerics.BigInteger)2;
        var upperPow = System.Numerics.BigInteger.Pow(upperX, n);

        while (upperPow.CompareTo(a) < 0)
        {
          lowerX = upperX;
          lowerPow = upperPow;
          upperX = lowerX * 2;
          upperPow = System.Numerics.BigInteger.Pow(upperX, n);
        }

        //if we happened to find the exact answer, just return it

        if (upperPow.Equals(a)) return (upperX, true);

        //now we know lowerX < x < upperX
        //next do binary search between lowerX and upperX

        while (true)
        {
          var testX = (lowerX + upperX) / 2;

          if (testX.Equals(lowerX) || testX.Equals(upperX))
            break;

          var testPow = System.Numerics.BigInteger.Pow(testX, n);

          if (testPow.Equals(a)) return (testX, true); // We found an exact answer.
          else if (testPow.CompareTo(a) > 0) // Still too high so set upper to the test value.
          {
            upperX = testX;
            upperPow = testPow;
          }
          else // Still too low.
          {
            lowerX = testX;
            lowerPow = testPow;
          }
        }

        //we didn't get an exact answer, but we know the two integers closest to the exact value.
        //now we just need to figure out which is closer and return that

        return (a - lowerPow).CompareTo(upperPow - a) < 0 ? (lowerX, false) : (upperX, false);
      }
    }
    public static BigFraction Parse(string s, int radix)
    {
      var index = s.IndexOf(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

      if (index <= -1) return new BigFraction(s.FromRadixString(radix), System.Numerics.BigInteger.One, true);

      var num = s.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, string.Empty).FromRadixString(radix);
      var den = System.Numerics.BigInteger.Pow(radix, s.Length - index - 1);

      return new BigFraction(num, den, false);
    }
    /// <summary>Returns this^exponent. Note: 0^0 will return 1/1.</summary>
    public static BigFraction Pow(BigFraction value, int exponent)
    {
      if (exponent < 0)
      {
        if (value.IsZero) throw new System.DivideByZeroException(@"Raising zero to negative exponent.");
        else if (exponent == int.MinValue) throw new System.OverflowException(@"Exponent cannot be negated."); // edge case: because we negate the exponent if it's negative, we would get into an infinite loop because -MIN_VALUE == MIN_VALUE
        else return new BigFraction(System.Numerics.BigInteger.Pow(value.Denominator, -exponent), System.Numerics.BigInteger.Pow(value.Numerator, -exponent), true);
      }

      if (exponent == 0) return One;
      else if (value.IsZero) return Zero;
      else if (exponent == 1) return value;
      else return new BigFraction(System.Numerics.BigInteger.Pow(value.Numerator, exponent), System.Numerics.BigInteger.Pow(value.Denominator, exponent), true);
    }
    /// <summary>Returns the reciprocal of a value.</summary>
    public static BigFraction Reciprocal(BigFraction value) => value.IsZero ? throw new System.DivideByZeroException(@"Reciprocal of zero.") : new BigFraction(value.Denominator, value.Numerator, true);
    /// <summary>Returns the difference of two values.</summary>
    public static BigFraction Subtract(BigFraction a, BigFraction b)
      => new BigFraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator, false);

    /// <summary>Create a new BigFraction from the numerator and denominator.</summary>
    public static BigFraction Create(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
    {
      if (denominator.IsZero) throw new System.DivideByZeroException();

      if (denominator.Sign < 0) // Only the numerator should be negative.
      {
        numerator = -numerator;
        denominator = -denominator;
      }

      if (numerator.IsZero) denominator = System.Numerics.BigInteger.One;
      else if (!denominator.IsOne && System.Numerics.BigInteger.GreatestCommonDivisor(numerator, denominator) is var gcd && !gcd.Equals(System.Numerics.BigInteger.One))
      {
        numerator /= gcd;
        denominator /= gcd;
      }

      return new BigFraction(numerator, denominator);
    }
    public static BigFraction Create(System.Numerics.BigInteger integer) => new BigFraction(integer, System.Numerics.BigInteger.One);
    /// <summary>Create a new BigFraction from the double .</summary>
    /// <param name="value">The value to convert to BigFraction.</param>
    /// <param name="accuracy">Used to convert recurring decimals into fractions (eg. 0.166667 -> 1/6).</param>
    public static BigFraction Create(double value, double accuracy)
    {
      if (accuracy <= 0.0 || accuracy >= 1.0) throw new System.ArgumentOutOfRangeException(nameof(accuracy), @"Must be > 0 and < 1.");

      var sign = System.Math.Sign(value);

      if (sign == -1) value = System.Math.Abs(value);

      var epsilon = sign == 0 ? accuracy : value * accuracy; // Accuracy is the maximum relative error; convert to absolute maxError

      var n = new System.Numerics.BigInteger(value);
      value -= System.Math.Floor(value);

      if (value < epsilon) return new BigFraction(sign * n, System.Numerics.BigInteger.One);
      if (1 - epsilon < value) return new BigFraction(sign * (n + 1), System.Numerics.BigInteger.One);

      var lower = (numerator: 0, denominator: 1); // The lower fraction is 0/1
      var upper = (numerator: 1, denominator: 1); // The upper fraction is 1/1

      while (true)
      {
        var middle = (numerator: lower.numerator + upper.numerator, denominator: lower.denominator + upper.denominator); // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)

        if (middle.denominator * (value + epsilon) < middle.numerator) upper = middle; // real + error < middle : middle is our new upper.
        else if (middle.numerator < (value - epsilon) * middle.denominator) lower = middle; // middle < real - error : middle is our new lower.
        else return new BigFraction((n * middle.denominator + middle.numerator) * sign, middle.denominator);  // Middle is our best fraction.
      }
    }
    /// <summary>Create a new BigFraction from the numerator and denominator.</summary>
    public static bool TryCreate(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out BigFraction result)
    {
      try
      {
        result = Create(numerator, denominator);
        return true;
      }
      catch { }

      result = default;
      return false;
    }
    #endregion Static

    #region Overrides
    public override bool Equals(object? obj)
      => obj is BigFraction ? Equals((BigFraction)obj) : false;

    public override int GetHashCode()
      => Numerator.GetHashCode() / Denominator.GetHashCode();

    public override string ToString()
      => $"{Numerator}/{Denominator}";
    #endregion Overrides

    // System.IComparable
    public int CompareTo(BigFraction other)
    {
      if (Sign != other.Sign) return Sign - other.Sign;

      if (Denominator.Equals(other.Denominator)) return Numerator.CompareTo(other.Numerator);

      return (Numerator * other.Denominator).CompareTo(Denominator * other.Numerator);
    }

    // System.IEquatable
    public bool Equals(BigFraction other)
      => Numerator.Equals(other.Numerator) && Denominator.Equals(other.Denominator);
  }
}
