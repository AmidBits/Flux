namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    public static void GetIntegerLog2<TSelf, TResult>(this TSelf x, out TResult log2Ceiling, out TResult log2Floor)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      log2Ceiling = log2Floor = TResult.CreateChecked(TSelf.Log2(x));

      if (!TSelf.IsPow2(x))
        log2Ceiling++;
    }

    /// <summary>Computes the ceiling of the base 2 log of <paramref name="x"/>.</summary>
    public static int GetIntegerLog2Ceiling<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsPow2(x) ? GetIntegerLog2Floor(x) : GetIntegerLog2Floor(x) + 1;

    /// <summary>Computes the floor of the base 2 log of <paramref name="x"/>. This is the common log function.</summary>
    public static int GetIntegerLog2Floor<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => int.CreateChecked(TSelf.Log2(x));
  }
}
