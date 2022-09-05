#if NET7_0_OR_GREATER
namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Computes the smallest power of 2 storage size, that is greater or equal to <paramref name="minimumStorageSize"/>, that would fit the value.</summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="minimumStorageSize"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf GetSmallestPowerOf2StorageSize<TSelf>(this TSelf value, TSelf minimumStorageSize)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var pow2MinimumStorageSize = RoundUpToPow2(minimumStorageSize);

      while (pow2MinimumStorageSize < value)
        pow2MinimumStorageSize <<= 1;

      return pow2MinimumStorageSize;
    }
  }
}
#endif
