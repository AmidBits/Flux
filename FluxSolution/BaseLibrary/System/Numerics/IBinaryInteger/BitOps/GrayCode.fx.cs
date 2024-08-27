namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Converts a binary number to a reflected binary Gray code.</para>
    /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    /// </summary>
    public static TSelf BinaryToGray<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value ^ (value >>> 1);

    /// <summary>
    /// <para>Converts a reflected binary gray code to a binary number.</para>
    /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    /// </summary>
    public static TSelf GrayToBinary<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var mask = value;

      while (!TSelf.IsZero(mask))
      {
        mask >>>= 1;
        value ^= mask;
      }

      return value;
    }
  }
}
