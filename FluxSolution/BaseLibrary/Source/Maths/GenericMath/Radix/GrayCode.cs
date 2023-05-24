namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Converts from reflected binary gray code number to a binary number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
    public static TSelf BinaryToGray<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value >= TSelf.Zero ? value ^ (value >> 1) : throw new System.ArgumentOutOfRangeException(nameof(value));

    /// <summary>Converts from reflected binary gray code number to a binary number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
    public static TSelf GrayToBinary<TSelf>(TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (value > TSelf.Zero)
      {
        var mask = value >> 1;

        while (!TSelf.IsZero(mask))
        {
          value ^= mask;
          mask >>= 1;
        }
      }

      return value;
    }

    /// <summary>Experimental adaption from wikipedia.</summary>
    public static void BinaryToGray<TSelf>(TSelf radix, TSelf value, TSelf[] gray)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (gray is null) throw new System.ArgumentNullException(nameof(gray));

      var baseN = new TSelf[gray.Length]; // Stores the ordinary base-N number, one digit per entry

      for (var index = 0; index < gray.Length; index++) // Put the normal baseN number into the baseN array. For base 10, 109 would be stored as [9,0,1]
      {
        baseN[index] = value % radix;

        value /= radix;
      }

      var shift = TSelf.Zero; // Convert the normal baseN number into the Gray code equivalent. Note that the loop starts at the most significant digit and goes down.

      for (var index = gray.Length - 1; index >= 0; index--) // The Gray digit gets shifted down by the sum of the higher digits.
      {
        gray[index] = (baseN[index] + shift) % radix;

        shift = shift + radix - gray[index]; // Subtract from base so shift is positive
      }
    }

#endif

    ///// <summary>Adaption from wikipedia.</summary>
    //[System.CLSCompliant(false)]
    //public static void BinaryToGray(uint radix, uint value, uint[] gray)
    //{
    //	if (gray is null) throw new System.ArgumentNullException(nameof(gray));

    //	var baseN = new uint[gray.Length]; // Stores the ordinary base-N number, one digit per entry

    //	for (var index = 0; index < gray.Length; index++) // Put the normal baseN number into the baseN array. For base 10, 109 would be stored as [9,0,1]
    //	{
    //		baseN[index] = value % radix;

    //		value /= radix;
    //	}

    //	var shift = 0U; // Convert the normal baseN number into the Gray code equivalent. Note that the loop starts at the most significant digit and goes down.

    //	for (var index = gray.Length - 1; index >= 0; index--) // The Gray digit gets shifted down by the sum of the higher digits.
    //	{
    //		gray[index] = (baseN[index] + shift) % radix;

    //		shift = shift + radix - gray[index]; // Subtract from base so shift is positive
    //	}
    //}
  }
}
