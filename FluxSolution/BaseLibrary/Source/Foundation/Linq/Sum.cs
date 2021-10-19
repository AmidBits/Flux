namespace Flux.Linq
{
  public static partial class Enumerable
  {
    public static System.Numerics.BigInteger Sum(System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source)
    {
      var sum = System.Numerics.BigInteger.Zero;
      foreach (var value in source)
        sum += value;
      return sum;
    }
  }
}
