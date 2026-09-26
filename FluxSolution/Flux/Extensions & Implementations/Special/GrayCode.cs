namespace Flux
{
  public static class GrayCode
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Converts a binary number to a reflected binary Gray code.</para>
      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      /// </summary>
      public static TInteger BinaryToGrayCode(TInteger value)
        => value ^ (value >>> 1);

      /// <summary>
      /// <para>Converts a reflected binary gray code to a binary number.</para>
      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      /// </summary>
      public static TInteger GrayCodeToBinary(TInteger value)
      {
        var mask = value;

        while (!TInteger.IsZero(mask))
        {
          mask >>>= 1;
          value ^= mask;
        }

        return value;
      }
    }
  }
}
