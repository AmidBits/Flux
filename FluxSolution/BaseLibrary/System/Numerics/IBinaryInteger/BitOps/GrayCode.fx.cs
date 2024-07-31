namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Converts an binary number to reflected binary Gray code.</para>
    /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    /// </summary>
    public static TSelf BinaryToGray<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value ^ (value >>> 1);

    /// <summary>
    /// <para>Converts a reflected binary gray code number to a binary number.</para>
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

    ///// <summary>
    ///// <para>Converts a reflected binary Gray code to a binary number.</para>
    ///// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    ///// </summary>
    //[System.CLSCompliant(false)]
    //public static System.UInt32 GrayToBinary(System.UInt32 value)
    //{
    //  value ^= (value >> 16);
    //  value ^= (value >> 8);
    //  value ^= (value >> 4);
    //  value ^= (value >> 2);
    //  value ^= (value >> 1);

    //  return value;
    //}

    ///// <summary>
    ///// <para>Converts a reflected binary Gray code to a binary number.</para>
    ///// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    ///// </summary>
    //[System.CLSCompliant(false)]
    //public static System.UInt64 GrayToBinary(System.UInt64 value)
    //{
    //  value ^= (value >> 32);
    //  value ^= (value >> 16);
    //  value ^= (value >> 8);
    //  value ^= (value >> 4);
    //  value ^= (value >> 2);
    //  value ^= (value >> 1);

    //  return value;
    //}

    ///// <summary>
    ///// <para>Converts from reflected binary gray code number to a binary number.</para>
    ///// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    ///// </summary>
    //[System.CLSCompliant(false)]
    //public static System.UInt128 GrayToBinary(System.UInt128 value)
    //{
    //  value ^= (value >> 64);
    //  value ^= (value >> 32);
    //  value ^= (value >> 16);
    //  value ^= (value >> 8);
    //  value ^= (value >> 4);
    //  value ^= (value >> 2);
    //  value ^= (value >> 1);

    //  return value;
    //}
  }
}
