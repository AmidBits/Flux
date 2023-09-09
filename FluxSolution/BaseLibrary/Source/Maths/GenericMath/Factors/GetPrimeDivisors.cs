//namespace Flux
//{
//  public static partial class Maths
//  {
//#if NET7_0_OR_GREATER

//    /// <summary>Results in a sequence of divisors for the specified number.</summary>
//    /// <remarks>This implementaion does not order the result.</remarks>
//    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetPrimeDivisors<TSelf>(this TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      foreach (var prime in NumberSequences.PrimeNumber.GetAscendingPrimes(2))
//        while ((number % prime) == 0)
//        {
//          yield return prime;

//          number /= prime;

//          if (number < prime)
//            yield break;
//        }
//    }

//#else


//    /// <summary>Results in a sequence of divisors for the specified number.</summary>
//    /// <remarks>This implementaion does not order the result.</remarks>
//    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
//    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPrimeDivisors(System.Numerics.BigInteger number)
//    {
//      foreach (var prime in NumberSequences.PrimeNumber.GetAscendingPrimes(2))
//        while ((number % prime) == 0)
//        {
//          yield return prime;

//          number /= prime;

//          if (number < prime)
//            yield break;
//        }
//    }

//#endif
//  }
//}
