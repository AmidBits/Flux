#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Yields a sequence of all sphenic numbers less than <paramref name="limitNumber"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="limitNumber"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TSelf> GetSphenicNumbers<TSelf>(TSelf limitNumber)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (TSelf i = TSelf.CreateChecked(30); i < limitNumber; i++)
      {
        var count = 0;

        var k = i;

        for (var j = TSelf.CreateChecked(2); k > TSelf.One && count <= 2; j++)
        {
          if (TSelf.IsZero(k % j))
          {
            k /= j;

            if (TSelf.IsZero(k % j))
              break;

            count++;
          }

          if (count == 0 && j > limitNumber / (j * j))
            break;

          if (count == 1 && j > (k / j))
            break;
        }

        if (count == 3 && k == TSelf.One)
          yield return i;
      }
    }
  }
}
#endif

//#if NET7_0_OR_GREATER
//namespace Flux.NumberSequences
//{
//  public sealed class CompositeNumber
//    : INumericSequence<System.Numerics.BigInteger>
//  {
//    #region Static methods

//    public static bool IsComposite<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      var two = TSelf.CreateChecked(2);
//      var three = TSelf.CreateChecked(3);
//      var five = TSelf.CreateChecked(5);
//      var six = TSelf.CreateChecked(6);

//      if (number <= TSelf.CreateChecked(3))
//        return false;

//      if (TSelf.IsZero(number % two) || TSelf.IsZero(number % three))
//        return true;

//      var limit = number.IntegerSqrt();

//      for (var k = five; k <= limit; k += six)
//        if (TSelf.IsZero(number % k) || TSelf.IsZero(number % (k + two)))
//          return true;

//      return false;
//    }

//    public static System.Collections.Generic.IEnumerable<TSelf> GetCompositeNumbers<TSelf>()
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      for (var k = TSelf.One; ; k++)
//        if (IsComposite(k))
//          yield return k;
//    }

//    /// <summary></summary>
//    /// <see href="https://en.wikipedia.org/wiki/Highly_composite_number"/>
//    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>> GetHighlyCompositeNumbers()
//    {
//      var largestCountOfDivisors = System.Numerics.BigInteger.Zero;
//      for (var index = System.Numerics.BigInteger.One; ; index++)
//        if (Factors.GetCountOfDivisors(index) is var countOfDivisors && countOfDivisors > largestCountOfDivisors)
//        {
//          yield return new System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>(index, countOfDivisors);
//          largestCountOfDivisors = countOfDivisors;
//        }
//    }

//    #endregion Static methods

//    #region Implemented interfaces
//    // INumberSequence
//    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
//      => GetCompositeNumbers<System.Numerics.BigInteger>();


//    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
//      => GetSequence().GetEnumerator();

//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//      => GetEnumerator();
//    #endregion Implemented interfaces
//  }
//}
//#endif
