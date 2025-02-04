//namespace Flux
//{
//  public static partial class NumberSequence
//  {
//    // https://codeforces.com/blog/entry/22229
//    /// <summary>Generates an array of divisor counts of all numbers less than or equal to the specified <paramref name="number"/>. This is done as with the sum of divisors, only increase by 1 instead of by the divisor.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Divisor"/>
//    public static TSelf[] GenerateCountOfFactors<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      var counts = new TSelf[int.CreateChecked(number) + 1];

//      for (var i = TSelf.One; i <= number; i++)
//        for (var j = i; j <= number; j += i)
//          counts[int.CreateChecked(j)]++;

//      return counts;
//    }
//    /// <summary>Generates am array of Euler totient values for numbers up to the specified <paramref name="number"/>.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Divisor"/>
//    public static TSelf[] GenerateEulerTotient<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      var totient = new TSelf[int.CreateChecked(number) + 1];

//      for (var i = TSelf.One; i <= number; i++)
//        totient[int.CreateChecked(i)] = i;
//      for (var i = TSelf.One + TSelf.One; i <= number; i++)
//        if (totient[int.CreateChecked(i)] == i)
//          for (var j = i; j <= number; j += i)
//            totient[int.CreateChecked(j)] -= totient[int.CreateChecked(j)] / i;

//      return totient;
//    }

//    /// <summary>Generates am array of the largest prime factors for numbers up to the specified <paramref name="number"/>.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Divisor"/>
//    public static TSelf[] GenerateLargestPrimeFactor<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      var factor = new TSelf[int.CreateChecked(number) + 1];

//      System.Array.Fill(factor, TSelf.One);

//      for (var i = TSelf.One; i <= number; i++)
//        if (factor[int.CreateChecked(i)] == TSelf.One)
//          for (var j = i; j <= number; j += i)
//            factor[int.CreateChecked(j)] = i;

//      return factor;
//    }

//    /// <summary>Generates an array of divisor sums of all numbers less than or equal to the specified <paramref name="number"/>. This is done as the count of divisors, only we increase by the divisor instead of by 1.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Divisor"/>
//    public static TSelf[] GenerateSumOfFactors<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      var sums = new TSelf[int.CreateChecked(number) + 1];

//      for (var i = TSelf.One; i <= number; i++)
//        for (var j = i; j <= number; j += i)
//          sums[int.CreateChecked(j)] += i;

//      return sums;
//    }

//    /// <summary>Returns the count of divisors in the sequence for the specified <paramref name="number"/>.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Divisor"/>
//    public static TSelf GetCountOfDivisors<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => TSelf.CreateChecked(System.Linq.Enumerable.Count(GetDivisors(number)));

//    /// <summary>Returns the count of proper divisors in the sequence for the specified <paramref name="number"/> (divisors including 1 but not itself).</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Divisor"/>
//    public static TSelf GetCountOfProperDivisors<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => TSelf.CreateChecked(System.Linq.Enumerable.Count(GetProperDivisors(number)));

//    /// <summary>Results in a sequence of divisors for the specified <paramref name="number"/>.</summary>
//    /// <remarks>This implementaion does not order the result.</remarks>
//    /// <see href="https://en.wikipedia.org/wiki/Divisor"/>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetDivisors<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      if (number > TSelf.Zero)
//      {
//        var sqrt = number.IntegerSqrt();

//        for (var counter = TSelf.One; counter <= sqrt; counter++)
//          if (TSelf.IsZero(number % counter))
//          {
//            yield return counter;

//            if (number / counter is var quotient && quotient != counter)
//              yield return quotient;
//          }
//      }
//    }

//    /// <summary>Results in a sequence of proper divisors for the specified <paramref name="number"/> (divisors including 1 but not itself).</summary>
//    /// <remarks>This implementaion does not order the result.</remarks>
//    /// <see href="https://en.wikipedia.org/wiki/Divisor"/>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetProperDivisors<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => System.Linq.Enumerable.Where(GetDivisors(number), n => n != number);

//    /// <summary>Results in a sequence of divisors for the specified <paramref name="number"/>.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Divisor"/>
//    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
//    public static TSelf GetSumOfDivisors<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => Fx.Sum(GetDivisors(number));

//    /// <summary>Results in a sequence of proper divisors for the specified <paramref name="number"/> (divisors including 1 but not itself).</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Divisor"/>
//    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
//    public static TSelf GetSumOfProperDivisors<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => Fx.Sum(GetProperDivisors(number));

//    /// <summary>Determines whether the <paramref name="number"/> is a deficient number.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Deficient_number"/>
//    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
//    public static bool IsDeficientNumber<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => GetSumOfDivisors(number) - number < number;

//    /// <summary>Determines whether the <paramref name="number"/> is a perfect number.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Perfect_number"/>
//    public static bool IsPerfectNumber<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => GetSumOfDivisors(number) - number == number;
//  }
//}
