namespace Flux
{
  public static partial class Maths
  {
    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Highly_composite_number"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>> GetHighlyCompositeNumbers()
    {
      var largestCountOfDivisors = System.Numerics.BigInteger.Zero;

      for (var index = System.Numerics.BigInteger.One; ; index++)
      {
        if (GetCountOfDivisors(index) is var countOfDivisors && countOfDivisors > largestCountOfDivisors)
        {
          yield return new System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>(index, countOfDivisors);

          largestCountOfDivisors = countOfDivisors;
        }
      }
    }
  }
}
