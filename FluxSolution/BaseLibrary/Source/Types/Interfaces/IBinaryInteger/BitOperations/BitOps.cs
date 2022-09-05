#if NET7_0_OR_GREATER
namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Computes the smallest power of 2 storage size, that is greater or equal to <paramref name="startingStorageSizeInPowerOf2"/>, that would fit the value.</summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="startingStorageSizeInPowerOf2"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf GetSmallestPowerOf2StorageSize<TSelf>(this TSelf value, TSelf startingStorageSizeInPowerOf2)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (!TSelf.IsPow2(startingStorageSizeInPowerOf2)) throw new System.ArgumentOutOfRangeException(nameof(startingStorageSizeInPowerOf2), "Must be a power of 2.");

      while (startingStorageSizeInPowerOf2 < value)
        startingStorageSizeInPowerOf2 <<= 1;

      return startingStorageSizeInPowerOf2;
    }
  }
}
#endif
