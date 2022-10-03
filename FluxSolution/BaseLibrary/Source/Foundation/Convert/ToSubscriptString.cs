using Flux.Text;

namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Returns a string with the numeric subscript.</summary>
    public static string ToSubscriptString(System.Numerics.BigInteger number, int radix)
    {
      if (radix < 2 || radix > 10) throw new System.ArgumentOutOfRangeException(nameof(radix));

      var span = Maths.GetDigits(number, radix);
      var chars = new char[span.Length];
      for (var index = span.Length - 1; index >= 0; index--)
        chars[index] = RuneSequences.Subscript0Through9[index].ToString()[0];
      return new string(chars);
    }

    /// <summary>Returns a string with the numeric subscript.</summary>
    public static string ToSubscriptString(int number, int radix)
    {
      if (radix < 2 || radix > 10) throw new System.ArgumentOutOfRangeException(nameof(radix));

      var span = Maths.GetDigits(number, radix);
      var chars = new char[span.Length];
      for (var index = span.Length - 1; index >= 0; index--)
        chars[index] = RuneSequences.Subscript0Through9[span[index]].ToString()[0];
      return new string(chars);
    }
    /// <summary>Returns a string with the numeric subscript.</summary>
    public static string ToSubscriptString(long number, int radix)
    {
      if (radix < 2 || radix > 10) throw new System.ArgumentOutOfRangeException(nameof(radix));

      var span = Maths.GetDigits(number, radix);
      var chars = new char[span.Length];
      for (var index = span.Length - 1; index >= 0; index--)
        chars[index] = RuneSequences.Subscript0Through9[(int)span[index]].ToString()[0];
      return new string(chars);
    }
  }
}
