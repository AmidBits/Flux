namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Converts a binary number to a reflected binary Gray code.</para>
    /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber BinaryToGray<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value ^ (value >>> 1);

    /// <summary>
    /// <para>Convert a value to a Gray code with the given base and digits. Iterating through a sequence of values would result in a sequence of Gray codes in which only one digit changes at a time.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Gray_code"/></para>
    /// </summary>
    /// <remarks>Experimental adaption from wikipedia.</remarks>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static TNumber[] BinaryToGrayCode<TNumber>(this TNumber value, TNumber radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (value < TNumber.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

      var gray = new TNumber[int.CreateChecked(value.DigitCount(radix))];

      var baseN = new TNumber[gray.Length]; // Stores the ordinary base-N number, one digit per entry

      for (var index = 0; index < gray.Length; index++) // Put the normal baseN number into the baseN array. For base 10, 109 would be stored as [9,0,1]
      {
        baseN[index] = value % radix;

        value /= radix;
      }

      var shift = TNumber.Zero; // Convert the normal baseN number into the Gray code equivalent. Note that the loop starts at the most significant digit and goes down.

      for (var index = gray.Length - 1; index >= 0; index--) // The Gray digit gets shifted down by the sum of the higher digits.
      {
        gray[index] = (baseN[index] + shift) % radix;

        shift = shift + radix - gray[index]; // Subtract from base so shift is positive
      }

      return gray;
    }

    /// <summary>
    /// <para>Converts a reflected binary gray code to a binary number.</para>
    /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    /// </summary>
    public static TNumber GrayToBinary<TNumber>(this TNumber value)
        where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var mask = value;

      while (!TNumber.IsZero(mask))
      {
        mask >>>= 1;
        value ^= mask;
      }

      return value;
    }
  }
}
