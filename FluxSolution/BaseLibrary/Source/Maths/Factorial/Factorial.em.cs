using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers. They are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public static System.Numerics.BigInteger Factorial(System.Numerics.BigInteger value)
      => value < 0
      ? -Factorial(-value)
      : value <= 1
      ? 1
      : Flux.LinqX.Range(2, value - 1, 1).AsParallel().Aggregate(System.Numerics.BigInteger.One, (a, b) => a * b);
    // => ParallelSplitFactorial.Default.ComputeProduct(value);

    ///// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
    ///// <remarks>These implementations have been extended to accomodate negative integers. They are simply computed with the sign removed and then re-applied.</remarks>
    ///// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    //public static int Factorial(int value)
    //  => value < 0
    //  ? -Factorial(-value)
    //  : unchecked((int)Factorial((uint)value));
    ///// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
    ///// <remarks>These implementations have been extended to accomodate negative integers. They are simply computed with the sign removed and then re-applied.</remarks>
    ///// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    //public static long Factorial(long value)
    //  => value < 0
    //  ? -Factorial(-value)
    //  : unchecked((long)Factorial((ulong)value));

    ///// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
    ///// <remarks>These implementations have been extended to accomodate negative integers. They are simply computed with the sign removed and then re-applied.</remarks>
    ///// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    //[System.CLSCompliant(false)]
    //public static uint Factorial(uint value)
    //  => value <= 1
    //  ? 1
    //  : value <= 12
    //  ? Flux.LinqX.Range(2, value - 1, 1).AsParallel().Aggregate(1U, (a, b) => a * b)
    //  : throw new System.ArithmeticException();
    ///// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
    ///// <remarks>These implementations have been extended to accomodate negative integers. They are simply computed with the sign removed and then re-applied.</remarks>
    ///// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    //[System.CLSCompliant(false)]
    //public static ulong Factorial(ulong value)
    //  => value <= 1
    //  ? 1
    //  : value <= 20
    //  ? Flux.LinqX.Range(2, value - 1, 1).AsParallel().Aggregate(1UL, (a, b) => a * b)
    //  : throw new System.ArithmeticException();
  }
}
