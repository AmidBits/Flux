namespace Flux
{
  public static partial class Bits
  {
#if !NET7_0_OR_GREATER
    /// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
    public static int PopCount(this System.Numerics.BigInteger value)
    {
      if (value > 255 && value.ToByteArray() is var byteArray)
      {
        var count = 0;
        for (var index = byteArray.Length - 1; index >= 0; index--)
          count += System.Numerics.BitOperations.PopCount(byteArray[index]);
        return count;
      }
      else if (value >= 0)
        return System.Numerics.BitOperations.PopCount((uint)value);

      return -1;
    }
#endif
  }
}
