namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Generates a new sequence of composite numbers.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TSelf> GetCompositeNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var k = TSelf.One; ; k++)
        if (IsComposite(k))
          yield return k;
    }

    /// <summary>
    /// <para>Creates a new sequence of highly composite numbers.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Highly_composite_number"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TSelf, TSelf>> GetHighlyCompositeNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var largestCountOfDivisors = TSelf.Zero;
      for (var index = TSelf.One; ; index++)
        if (TSelf.CreateChecked(index.Factors(false).Count) is var countOfDivisors && countOfDivisors > largestCountOfDivisors)
        {
          yield return new System.Collections.Generic.KeyValuePair<TSelf, TSelf>(index, countOfDivisors);
          largestCountOfDivisors = countOfDivisors;
        }
    }

    /// <summary>
    /// <para>Determines if the <paramref name="number"/> is a composite number.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsComposite<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var three = TSelf.CreateChecked(3); // Used twice.

      if (number <= three)
        return false;

      var two = TSelf.CreateChecked(2); // Used twice.

      if (TSelf.IsZero(number % two) || TSelf.IsZero(number % three))
        return true;

      var limit = number.IntegerSqrt();

      var six = TSelf.CreateChecked(6); // Used in the for loop below.

      for (var k = TSelf.CreateChecked(5); k <= limit; k += six)
        if (TSelf.IsZero(number % k) || TSelf.IsZero(number % (k + two)))
          return true;

      return false;
    }
  }
}
