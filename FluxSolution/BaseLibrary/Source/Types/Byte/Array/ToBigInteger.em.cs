namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Converts the specified number of bytes to a number, starting at the specified index.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this byte[] source, int startIndex, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var number = System.Numerics.BigInteger.Zero;

      for (int i = startIndex, maxIndex = startIndex + count; i < maxIndex; i++)
        number = number * 256 + source[i];

      return number;
    }
  }
}
