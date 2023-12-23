#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class NumberSequence
  {
    public static bool IsComposite<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var two = TSelf.CreateChecked(2);
      var three = TSelf.CreateChecked(3);
      var five = TSelf.CreateChecked(5);
      var six = TSelf.CreateChecked(6);

      if (number <= TSelf.CreateChecked(3))
        return false;

      if (TSelf.IsZero(number % two) || TSelf.IsZero(number % three))
        return true;

      var limit = number.IntegerSqrt();

      for (var k = five; k <= limit; k += six)
        if (TSelf.IsZero(number % k) || TSelf.IsZero(number % (k + two)))
          return true;

      return false;
    }

    public static System.Collections.Generic.IEnumerable<TSelf> GetCompositeNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var k = TSelf.One; ; k++)
        if (IsComposite(k))
          yield return k;
    }

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Highly_composite_number"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>> GetHighlyCompositeNumbers()
    {
      var largestCountOfDivisors = System.Numerics.BigInteger.Zero;
      for (var index = System.Numerics.BigInteger.One; ; index++)
        if (NumberSequence.GetCountOfDivisors(index) is var countOfDivisors && countOfDivisors > largestCountOfDivisors)
        {
          yield return new System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>(index, countOfDivisors);
          largestCountOfDivisors = countOfDivisors;
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
//    /// <see cref="https://en.wikipedia.org/wiki/Highly_composite_number"/>
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
