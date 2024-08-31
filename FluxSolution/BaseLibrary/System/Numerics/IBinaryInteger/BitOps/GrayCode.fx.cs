namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Converts a binary number to a reflected binary Gray code.</para>
    /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    /// </summary>
    public static TValue BinaryToGray<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value ^ (value >>> 1);

    /// <summary>
    /// <para>Converts a reflected binary gray code to a binary number.</para>
    /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    /// </summary>
    public static TValue GrayToBinary<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      var mask = value;

      while (!TValue.IsZero(mask))
      {
        mask >>>= 1;
        value ^= mask;
      }

      return value;
    }
  }
}
