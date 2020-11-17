

// https://stackoverflow.com/questions/32664/is-there-a-constraint-that-restricts-my-generic-method-to-numeric-types?noredirect=1&lq=1

using System.Linq;

namespace Flux
{
  public static partial class Maths
  {

    
    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    public static System.Numerics.BigInteger ClampX(System.Numerics.BigInteger value, System.Numerics.BigInteger minimum, System.Numerics.BigInteger maximum)
      => value < minimum ? minimum : value > maximum ? maximum : value;

    
    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    public static System.Decimal ClampX(System.Decimal value, System.Decimal minimum, System.Decimal maximum)
      => value < minimum ? minimum : value > maximum ? maximum : value;

    
    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    public static System.Double ClampX(System.Double value, System.Double minimum, System.Double maximum)
      => value < minimum ? minimum : value > maximum ? maximum : value;

    
    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    public static System.Single ClampX(System.Single value, System.Single minimum, System.Single maximum)
      => value < minimum ? minimum : value > maximum ? maximum : value;

    
    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    public static System.Int32 ClampX(System.Int32 value, System.Int32 minimum, System.Int32 maximum)
      => value < minimum ? minimum : value > maximum ? maximum : value;

    
    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    public static System.Int64 ClampX(System.Int64 value, System.Int64 minimum, System.Int64 maximum)
      => value < minimum ? minimum : value > maximum ? maximum : value;

    
    
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Numerics.BigInteger CopySignX(System.Numerics.BigInteger value, System.Numerics.BigInteger sign)
        => System.Numerics.BigInteger.Abs(value) * sign.Sign;
    
    
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Decimal CopySignX(System.Decimal value, System.Decimal sign)
        => System.Math.Abs(value) * System.Math.Sign(sign);
    
    
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Double CopySignX(System.Double value, System.Double sign)
        => System.Math.Abs(value) * System.Math.Sign(sign);
    
    
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Single CopySignX(System.Single value, System.Single sign)
        => System.Math.Abs(value) * System.Math.Sign(sign);
    
    
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Int32 CopySignX(System.Int32 value, System.Int32 sign)
        => System.Math.Abs(value) * System.Math.Sign(sign);
    
    
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Int64 CopySignX(System.Int64 value, System.Int64 sign)
        => System.Math.Abs(value) * System.Math.Sign(sign);
    
    
    
    /// <summary>Compute the integral that would include the fractional part, if any. It works like truncate but instead of discarding the fractional part, it picks the "next" integral, if needed.</summary>
    public static System.Decimal EnvelopX(System.Decimal value)
        => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value);
    
    
    /// <summary>Compute the integral that would include the fractional part, if any. It works like truncate but instead of discarding the fractional part, it picks the "next" integral, if needed.</summary>
    public static System.Double EnvelopX(System.Double value)
        => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value);
    
    
    /// <summary>Compute the integral that would include the fractional part, if any. It works like truncate but instead of discarding the fractional part, it picks the "next" integral, if needed.</summary>
    public static System.Single EnvelopX(System.Single value)
        => System.MathF.Sign(value) < 0 ? System.MathF.Floor(value) : System.MathF.Ceiling(value);
    
    
    
    /// <summary>Folds out-of-bound values over across the range, back and forth (between minimum and maximum), until the value is in range.</summary>
    public static System.Numerics.BigInteger FoldX(System.Numerics.BigInteger value, System.Numerics.BigInteger minimum, System.Numerics.BigInteger maximum)
    {
      System.Numerics.BigInteger magnitude, range;

      if (value > maximum)
      {
        magnitude = value - maximum;
        range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? maximum - (magnitude % range) : minimum + (magnitude % range);
      }
      else if (value < minimum)
      {
        magnitude = minimum - value;
        range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? minimum + (magnitude % range) : maximum - (magnitude % range);
      }

      return value;
    }

    
    /// <summary>Folds out-of-bound values over across the range, back and forth (between minimum and maximum), until the value is in range.</summary>
    public static System.Decimal FoldX(System.Decimal value, System.Decimal minimum, System.Decimal maximum)
    {
      System.Decimal magnitude, range;

      if (value > maximum)
      {
        magnitude = value - maximum;
        range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? maximum - (magnitude % range) : minimum + (magnitude % range);
      }
      else if (value < minimum)
      {
        magnitude = minimum - value;
        range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? minimum + (magnitude % range) : maximum - (magnitude % range);
      }

      return value;
    }

    
    /// <summary>Folds out-of-bound values over across the range, back and forth (between minimum and maximum), until the value is in range.</summary>
    public static System.Double FoldX(System.Double value, System.Double minimum, System.Double maximum)
    {
      System.Double magnitude, range;

      if (value > maximum)
      {
        magnitude = value - maximum;
        range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? maximum - (magnitude % range) : minimum + (magnitude % range);
      }
      else if (value < minimum)
      {
        magnitude = minimum - value;
        range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? minimum + (magnitude % range) : maximum - (magnitude % range);
      }

      return value;
    }

    
    /// <summary>Folds out-of-bound values over across the range, back and forth (between minimum and maximum), until the value is in range.</summary>
    public static System.Single FoldX(System.Single value, System.Single minimum, System.Single maximum)
    {
      System.Single magnitude, range;

      if (value > maximum)
      {
        magnitude = value - maximum;
        range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? maximum - (magnitude % range) : minimum + (magnitude % range);
      }
      else if (value < minimum)
      {
        magnitude = minimum - value;
        range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? minimum + (magnitude % range) : maximum - (magnitude % range);
      }

      return value;
    }

    
    /// <summary>Folds out-of-bound values over across the range, back and forth (between minimum and maximum), until the value is in range.</summary>
    public static System.Int32 FoldX(System.Int32 value, System.Int32 minimum, System.Int32 maximum)
    {
      System.Int32 magnitude, range;

      if (value > maximum)
      {
        magnitude = value - maximum;
        range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? maximum - (magnitude % range) : minimum + (magnitude % range);
      }
      else if (value < minimum)
      {
        magnitude = minimum - value;
        range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? minimum + (magnitude % range) : maximum - (magnitude % range);
      }

      return value;
    }

    
    /// <summary>Folds out-of-bound values over across the range, back and forth (between minimum and maximum), until the value is in range.</summary>
    public static System.Int64 FoldX(System.Int64 value, System.Int64 minimum, System.Int64 maximum)
    {
      System.Int64 magnitude, range;

      if (value > maximum)
      {
        magnitude = value - maximum;
        range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? maximum - (magnitude % range) : minimum + (magnitude % range);
      }
      else if (value < minimum)
      {
        magnitude = minimum - value;
        range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? minimum + (magnitude % range) : maximum - (magnitude % range);
      }

      return value;
    }

    
        /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Numerics.BigInteger GetDivisorCountX(System.Numerics.BigInteger number)
    {
      System.Numerics.BigInteger count = 0;
      var sqrt = number.Sqrt();
      for (System.Numerics.BigInteger counter = 1; counter <= sqrt; counter++)
        if (number % counter == 0)
          count += (number / counter == counter ? 1 : 2);
      return count;
    }

        /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Int32 GetDivisorCountX(System.Int32 number)
    {
      System.Int32 count = 0;
      var sqrt = System.Math.Sqrt(number);
      for (System.Int32 counter = 1; counter <= sqrt; counter++)
        if (number % counter == 0)
          count += (number / counter == counter ? 1 : 2);
      return count;
    }

        /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Int64 GetDivisorCountX(System.Int64 number)
    {
      System.Int64 count = 0;
      var sqrt = System.Math.Sqrt(number);
      for (System.Int64 counter = 1; counter <= sqrt; counter++)
        if (number % counter == 0)
          count += (number / counter == counter ? 1 : 2);
      return count;
    }

    
    
    /// <summary>Results in a sequence of divisors for the specified number, with the option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <remarks>This implementaion does not order the result, and the first element contains the number itself, so if only proper divsors are needed, skip the first element.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDivisorsX(System.Numerics.BigInteger number)
    {
      var sqrt = number.Sqrt();
      for (int counter = 1; counter <= sqrt; counter++)
        if (number % counter == 0)
        {
          yield return counter;
          if (number / counter is var quotient && quotient != counter) yield return quotient;
        }
    }

    
    /// <summary>Results in a sequence of divisors for the specified number, with the option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <remarks>This implementaion does not order the result, and the first element contains the number itself, so if only proper divsors are needed, skip the first element.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<System.Int32> GetDivisorsX(System.Int32 number)
    {
      var sqrt = System.Math.Sqrt(number);
      for (int counter = 1; counter <= sqrt; counter++)
        if (number % counter == 0)
        {
          yield return counter;
          if (number / counter is var quotient && quotient != counter) yield return quotient;
        }
    }

    
    /// <summary>Results in a sequence of divisors for the specified number, with the option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <remarks>This implementaion does not order the result, and the first element contains the number itself, so if only proper divsors are needed, skip the first element.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<System.Int64> GetDivisorsX(System.Int64 number)
    {
      var sqrt = System.Math.Sqrt(number);
      for (int counter = 1; counter <= sqrt; counter++)
        if (number % counter == 0)
        {
          yield return counter;
          if (number / counter is var quotient && quotient != counter) yield return quotient;
        }
    }

    
    
    /// <summary>Results in a sequence of divisors for the specified number, with option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static System.Numerics.BigInteger GetSumOfDivisorsX(System.Numerics.BigInteger number)
    {
      System.Numerics.BigInteger sum = 0;
      var sqrt = number.Sqrt();
      for (var counter = 1; counter <= sqrt; counter++)
        if (number % counter == 0)
        {
          sum += counter;
          if (number / counter is var quotient && quotient != counter) sum += quotient;
        }
      return sum;
    }

    
    /// <summary>Results in a sequence of divisors for the specified number, with option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static System.Int32 GetSumOfDivisorsX(System.Int32 number)
    {
      System.Int32 sum = 0;
      var sqrt = System.Math.Sqrt(number);
      for (var counter = 1; counter <= sqrt; counter++)
        if (number % counter == 0)
        {
          sum += counter;
          if (number / counter is var quotient && quotient != counter) sum += quotient;
        }
      return sum;
    }

    
    /// <summary>Results in a sequence of divisors for the specified number, with option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static System.Int64 GetSumOfDivisorsX(System.Int64 number)
    {
      System.Int64 sum = 0;
      var sqrt = System.Math.Sqrt(number);
      for (var counter = 1; counter <= sqrt; counter++)
        if (number % counter == 0)
        {
          sum += counter;
          if (number / counter is var quotient && quotient != counter) sum += quotient;
        }
      return sum;
    }

    
    
    /// <summary>Returns the unit (or Heaviside) step of the specified value, i.e. 0.0 when less than zero (negative), 1.0 when greater than or equal to zero (positive).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static double HeavisideX(System.Numerics.BigInteger value, double whenZero = 0.5)
      => value < 0 ? 0 : value > 0 ? 1 : value == 0 ? whenZero : throw new System.ArithmeticException();

    
    /// <summary>Returns the unit (or Heaviside) step of the specified value, i.e. 0.0 when less than zero (negative), 1.0 when greater than or equal to zero (positive).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static double HeavisideX(System.Decimal value, double whenZero = 0.5)
      => value < 0 ? 0 : value > 0 ? 1 : value == 0 ? whenZero : throw new System.ArithmeticException();

    
    /// <summary>Returns the unit (or Heaviside) step of the specified value, i.e. 0.0 when less than zero (negative), 1.0 when greater than or equal to zero (positive).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static double HeavisideX(System.Double value, double whenZero = 0.5)
      => value < 0 ? 0 : value > 0 ? 1 : value == 0 ? whenZero : throw new System.ArithmeticException();

    
    /// <summary>Returns the unit (or Heaviside) step of the specified value, i.e. 0.0 when less than zero (negative), 1.0 when greater than or equal to zero (positive).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static double HeavisideX(System.Single value, double whenZero = 0.5)
      => value < 0 ? 0 : value > 0 ? 1 : value == 0 ? whenZero : throw new System.ArithmeticException();

    
    /// <summary>Returns the unit (or Heaviside) step of the specified value, i.e. 0.0 when less than zero (negative), 1.0 when greater than or equal to zero (positive).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static double HeavisideX(System.Int32 value, double whenZero = 0.5)
      => value < 0 ? 0 : value > 0 ? 1 : value == 0 ? whenZero : throw new System.ArithmeticException();

    
    /// <summary>Returns the unit (or Heaviside) step of the specified value, i.e. 0.0 when less than zero (negative), 1.0 when greater than or equal to zero (positive).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    public static double HeavisideX(System.Int64 value, double whenZero = 0.5)
      => value < 0 ? 0 : value > 0 ? 1 : value == 0 ? whenZero : throw new System.ArithmeticException();

    
    
    /// <summary>Calculates the power of the specified value and exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double x, and if pow has a 1 bit at that position, we multiply/accumulate that into the return value.</summary>
    /// <see cref="https://tkramesh.wordpress.com/2011/04/17/numerical-computations-in-c-exponentiation-by-repeated-squaring/"/>
    public static System.Numerics.BigInteger PowX(System.Numerics.BigInteger value, System.Numerics.BigInteger exponent)
    {
      if (exponent >= 1)
      {
        checked
        {
          System.Numerics.BigInteger result = 1;

          while (exponent > 0)
          {
            if ((exponent & 1) == 1)
              result *= value;
            value *= value;
            exponent >>= 1;
          }

          return result;
        }
      }
      else if (exponent == 0) return 1;

      throw new System.ArithmeticException(@"Invalid operands, value must be greater or equal to zero and exponent must be greater or equal to one.");
    }

    
    /// <summary>Calculates the power of the specified value and exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double x, and if pow has a 1 bit at that position, we multiply/accumulate that into the return value.</summary>
    /// <see cref="https://tkramesh.wordpress.com/2011/04/17/numerical-computations-in-c-exponentiation-by-repeated-squaring/"/>
    public static System.Int32 PowX(System.Int32 value, System.Int32 exponent)
    {
      if (exponent >= 1)
      {
        checked
        {
          System.Int32 result = 1;

          while (exponent > 0)
          {
            if ((exponent & 1) == 1)
              result *= value;
            value *= value;
            exponent >>= 1;
          }

          return result;
        }
      }
      else if (exponent == 0) return 1;

      throw new System.ArithmeticException(@"Invalid operands, value must be greater or equal to zero and exponent must be greater or equal to one.");
    }

    
    /// <summary>Calculates the power of the specified value and exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double x, and if pow has a 1 bit at that position, we multiply/accumulate that into the return value.</summary>
    /// <see cref="https://tkramesh.wordpress.com/2011/04/17/numerical-computations-in-c-exponentiation-by-repeated-squaring/"/>
    public static System.Int64 PowX(System.Int64 value, System.Int64 exponent)
    {
      if (exponent >= 1)
      {
        checked
        {
          System.Int64 result = 1;

          while (exponent > 0)
          {
            if ((exponent & 1) == 1)
              result *= value;
            value *= value;
            exponent >>= 1;
          }

          return result;
        }
      }
      else if (exponent == 0) return 1;

      throw new System.ArithmeticException(@"Invalid operands, value must be greater or equal to zero and exponent must be greater or equal to one.");
    }

    
        /// <summary>Returns the probability that specified event count in a group of total event count are all different (or unique). It's complementary (1 - ProbabilityOfNoDuplicates) yields the probability that at least 2 events are the equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfNoDuplicatesX(System.Numerics.BigInteger whenCount, System.Numerics.BigInteger ofTotalCount)
    {
      var accumulation = 1d;
      for (var index = ofTotalCount - whenCount + 1; index < ofTotalCount; index++)
        accumulation *= (double)index / (double)ofTotalCount;
      return accumulation;
    }
    /// <summary>Returns the probability that at least 2 events are equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfDuplicatesX(System.Numerics.BigInteger whenCount, System.Numerics.BigInteger ofTotalCount)
      => 1 - ProbabilityOfNoDuplicatesX(whenCount, ofTotalCount);
        /// <summary>Returns the probability that specified event count in a group of total event count are all different (or unique). It's complementary (1 - ProbabilityOfNoDuplicates) yields the probability that at least 2 events are the equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfNoDuplicatesX(System.Decimal whenCount, System.Decimal ofTotalCount)
    {
      var accumulation = 1d;
      for (var index = ofTotalCount - whenCount + 1; index < ofTotalCount; index++)
        accumulation *= (double)index / (double)ofTotalCount;
      return accumulation;
    }
    /// <summary>Returns the probability that at least 2 events are equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfDuplicatesX(System.Decimal whenCount, System.Decimal ofTotalCount)
      => 1 - ProbabilityOfNoDuplicatesX(whenCount, ofTotalCount);
        /// <summary>Returns the probability that specified event count in a group of total event count are all different (or unique). It's complementary (1 - ProbabilityOfNoDuplicates) yields the probability that at least 2 events are the equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfNoDuplicatesX(System.Double whenCount, System.Double ofTotalCount)
    {
      var accumulation = 1d;
      for (var index = ofTotalCount - whenCount + 1; index < ofTotalCount; index++)
        accumulation *= (double)index / (double)ofTotalCount;
      return accumulation;
    }
    /// <summary>Returns the probability that at least 2 events are equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfDuplicatesX(System.Double whenCount, System.Double ofTotalCount)
      => 1 - ProbabilityOfNoDuplicatesX(whenCount, ofTotalCount);
        /// <summary>Returns the probability that specified event count in a group of total event count are all different (or unique). It's complementary (1 - ProbabilityOfNoDuplicates) yields the probability that at least 2 events are the equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfNoDuplicatesX(System.Single whenCount, System.Single ofTotalCount)
    {
      var accumulation = 1d;
      for (var index = ofTotalCount - whenCount + 1; index < ofTotalCount; index++)
        accumulation *= (double)index / (double)ofTotalCount;
      return accumulation;
    }
    /// <summary>Returns the probability that at least 2 events are equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfDuplicatesX(System.Single whenCount, System.Single ofTotalCount)
      => 1 - ProbabilityOfNoDuplicatesX(whenCount, ofTotalCount);
        /// <summary>Returns the probability that specified event count in a group of total event count are all different (or unique). It's complementary (1 - ProbabilityOfNoDuplicates) yields the probability that at least 2 events are the equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfNoDuplicatesX(System.Int32 whenCount, System.Int32 ofTotalCount)
    {
      var accumulation = 1d;
      for (var index = ofTotalCount - whenCount + 1; index < ofTotalCount; index++)
        accumulation *= (double)index / (double)ofTotalCount;
      return accumulation;
    }
    /// <summary>Returns the probability that at least 2 events are equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfDuplicatesX(System.Int32 whenCount, System.Int32 ofTotalCount)
      => 1 - ProbabilityOfNoDuplicatesX(whenCount, ofTotalCount);
        /// <summary>Returns the probability that specified event count in a group of total event count are all different (or unique). It's complementary (1 - ProbabilityOfNoDuplicates) yields the probability that at least 2 events are the equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfNoDuplicatesX(System.Int64 whenCount, System.Int64 ofTotalCount)
    {
      var accumulation = 1d;
      for (var index = ofTotalCount - whenCount + 1; index < ofTotalCount; index++)
        accumulation *= (double)index / (double)ofTotalCount;
      return accumulation;
    }
    /// <summary>Returns the probability that at least 2 events are equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfDuplicatesX(System.Int64 whenCount, System.Int64 ofTotalCount)
      => 1 - ProbabilityOfNoDuplicatesX(whenCount, ofTotalCount);
    
    
    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    public static System.Numerics.BigInteger RescaleX(System.Numerics.BigInteger value, System.Numerics.BigInteger sourceMinimum, System.Numerics.BigInteger sourceMaximum, System.Numerics.BigInteger targetMinimum, System.Numerics.BigInteger targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    
    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    public static System.Decimal RescaleX(System.Decimal value, System.Decimal sourceMinimum, System.Decimal sourceMaximum, System.Decimal targetMinimum, System.Decimal targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    
    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    public static System.Double RescaleX(System.Double value, System.Double sourceMinimum, System.Double sourceMaximum, System.Double targetMinimum, System.Double targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    
    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    public static System.Single RescaleX(System.Single value, System.Single sourceMinimum, System.Single sourceMaximum, System.Single targetMinimum, System.Single targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    
    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    public static System.Int32 RescaleX(System.Int32 value, System.Int32 sourceMinimum, System.Int32 sourceMaximum, System.Int32 targetMinimum, System.Int32 targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    
    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    public static System.Int64 RescaleX(System.Int64 value, System.Int64 sourceMinimum, System.Int64 sourceMaximum, System.Int64 targetMinimum, System.Int64 targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    
    
    /// <summary>In difference from the System.Math.Sign(), this returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    public static System.Numerics.BigInteger SignX(System.Numerics.BigInteger value)
      => value < 0 ? -1 : 1;

    
    /// <summary>In difference from the System.Math.Sign(), this returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    public static System.Decimal SignX(System.Decimal value)
      => value < 0 ? -1 : 1;

    
    /// <summary>In difference from the System.Math.Sign(), this returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    public static System.Double SignX(System.Double value)
      => value < 0 ? -1 : 1;

    
    /// <summary>In difference from the System.Math.Sign(), this returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    public static System.Single SignX(System.Single value)
      => value < 0 ? -1 : 1;

    
    /// <summary>In difference from the System.Math.Sign(), this returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    public static System.Int32 SignX(System.Int32 value)
      => value < 0 ? -1 : 1;

    
    /// <summary>In difference from the System.Math.Sign(), this returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    public static System.Int64 SignX(System.Int64 value)
      => value < 0 ? -1 : 1;

    
    
    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0.0 when not zero, and 1.0 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Numerics.BigInteger UnitImpulseX(System.Numerics.BigInteger value)
      => value != 0 ? 0 : value == 0 ? 1 : throw new System.ArithmeticException();

    
    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0.0 when not zero, and 1.0 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Decimal UnitImpulseX(System.Decimal value)
      => value != 0 ? 0 : value == 0 ? 1 : throw new System.ArithmeticException();

    
    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0.0 when not zero, and 1.0 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Double UnitImpulseX(System.Double value)
      => value != 0 ? 0 : value == 0 ? 1 : throw new System.ArithmeticException();

    
    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0.0 when not zero, and 1.0 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Single UnitImpulseX(System.Single value)
      => value != 0 ? 0 : value == 0 ? 1 : throw new System.ArithmeticException();

    
    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0.0 when not zero, and 1.0 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Int32 UnitImpulseX(System.Int32 value)
      => value != 0 ? 0 : value == 0 ? 1 : throw new System.ArithmeticException();

    
    /// <summary>Returns the discrete-time unit impulse of the specified value, i.e. 0.0 when not zero, and 1.0 when zero.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Int64 UnitImpulseX(System.Int64 value)
      => value != 0 ? 0 : value == 0 ? 1 : throw new System.ArithmeticException();

    
    
    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Numerics.BigInteger UnitStepX(System.Numerics.BigInteger value)
      => value < 0 ? 0 : 1;

    
    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Decimal UnitStepX(System.Decimal value)
      => value < 0 ? 0 : 1;

    
    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Double UnitStepX(System.Double value)
      => value < 0 ? 0 : 1;

    
    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Single UnitStepX(System.Single value)
      => value < 0 ? 0 : 1;

    
    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Int32 UnitStepX(System.Int32 value)
      => value < 0 ? 0 : 1;

    
    /// <summary>Returns the discrete unit step of the specified value, i.e. 0 when less than zero (negative), and 1 when greater than or equal to zero.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Heaviside_step_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Dirac_delta_function"/>
    public static System.Int64 UnitStepX(System.Int64 value)
      => value < 0 ? 0 : 1;

    
    
    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    public static System.Numerics.BigInteger WrapX(System.Numerics.BigInteger value, System.Numerics.BigInteger minimum, System.Numerics.BigInteger maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;

    
    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    public static System.Decimal WrapX(System.Decimal value, System.Decimal minimum, System.Decimal maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;

    
    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    public static System.Double WrapX(System.Double value, System.Double minimum, System.Double maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;

    
    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    public static System.Single WrapX(System.Single value, System.Single minimum, System.Single maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;

    
    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    public static System.Int32 WrapX(System.Int32 value, System.Int32 minimum, System.Int32 maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;

    
    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    public static System.Int64 WrapX(System.Int64 value, System.Int64 minimum, System.Int64 maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;

    
  }
}
