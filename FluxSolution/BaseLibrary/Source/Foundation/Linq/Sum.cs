namespace Flux
{
  public static partial class Liq
  {
    public static System.Numerics.BigInteger Sum(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source)
    {
      var sum = System.Numerics.BigInteger.Zero;
      foreach (var value in source)
        sum += value;
      return sum;
    }
  }
}
