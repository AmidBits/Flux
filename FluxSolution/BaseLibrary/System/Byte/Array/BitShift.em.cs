namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Performs an in-place bit shift <paramref name="count"/> left on all bytes in the <paramref name="source"/> array. Returns the overflow byte with all bits as it would look if rotated with the array.</summary>
    public static byte BitShiftLeft(this byte[] source, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (count < 0 || count > 8) throw new System.ArgumentOutOfRangeException(nameof(count));

      var maxIndex = source.Length - 1;

      var reverseShift = 8 - count;

      var carryBits = (byte)(source[maxIndex] >> reverseShift);

      var msbMap = ((1 << count) - 1) << reverseShift;

      for (var index = maxIndex; index > 0; index--)
        source[index] = (byte)(source[index] << count | ((source[index - 1] & msbMap) >> reverseShift));

      source[0] <<= count;

      return carryBits;
    }

    /// <summary>Performs an in-place bit shift <paramref name="count"/> right on all bytes in the <paramref name="source"/> array. Returns the overflow byte with all bits as it would look if rotated with the array.</summary>
    public static byte BitShiftRight(this byte[] source, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (count < 0 || count > 8) throw new System.ArgumentOutOfRangeException(nameof(count));

      var maxIndex = source.Length - 1;

      var reverseShift = 8 - count;

      var carryBits = (byte)(source[0] << reverseShift);

      var lsbMap = ((1 << count) - 1);

      for (var index = 0; index < maxIndex; index++)
        source[index] = (byte)(((source[index + 1] & lsbMap) << reverseShift) | source[index] >> count);

      source[maxIndex] >>= count;

      return carryBits;
    }
  }
}
