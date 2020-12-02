namespace Flux
{
  public static partial class BigIntegerEm
  {
    /// <summary>Returns either the built-in byte array, or if a zero byte padding is present, a byte array excluding the zero byte is returned.</summary>
    public static byte[] ToByteArrayEx(this System.Numerics.BigInteger source, out int length)
    {
      var byteArray = source.ToByteArray();

      length = byteArray.Length - 1;

      if (length > 0 && byteArray[length] == 0)
        System.Array.Resize(ref byteArray, length);
      else
        length++;

      return byteArray;
    }

    /// <summary>This is essentially the same as the native ToByteArray (which is even called from this extension method) with the addition of the most significant byte index and its value as out parameters.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static byte[] ToByteArrayEx(this System.Numerics.BigInteger source, out int msbIndex, out byte msbValue)
    {
      var byteArray = source.ToByteArray();

      msbIndex = byteArray.Length - 1;
      msbValue = byteArray[msbIndex];

      if (msbIndex > 0 && msbValue == 0)
        msbValue = byteArray[--msbIndex];

      return byteArray;
    }
  }
}
