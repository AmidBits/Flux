namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Convert a value to a Gray code with the given base and digits. Iterating through a sequence of values would result in a sequence of Gray codes in which only one digit changes at a time.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Gray_code"/></para>
    /// </summary>
    /// <remarks>Experimental adaption from wikipedia.</remarks>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static TSelf[] BinaryToGrayCode<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (number < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(number));

      var gray = new TSelf[int.CreateChecked(number.DigitCount(radix))];

      var baseN = new TSelf[gray.Length]; // Stores the ordinary base-N number, one digit per entry

      for (var index = 0; index < gray.Length; index++) // Put the normal baseN number into the baseN array. For base 10, 109 would be stored as [9,0,1]
      {
        baseN[index] = number % radix;

        number /= radix;
      }

      var shift = TSelf.Zero; // Convert the normal baseN number into the Gray code equivalent. Note that the loop starts at the most significant digit and goes down.

      for (var index = gray.Length - 1; index >= 0; index--) // The Gray digit gets shifted down by the sum of the higher digits.
      {
        gray[index] = (baseN[index] + shift) % radix;

        shift = shift + radix - gray[index]; // Subtract from base so shift is positive
      }

      return gray;
    }
  }
}