using System.Linq;

namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Returns a char with the numeric subscript.</summary>
    public static string ToSubscriptString(System.Numerics.BigInteger number, int radix)
      => radix >= 2 && radix <= 10 ? string.Concat(Maths.GetDigits(number, radix).Select(d => Text.Sequences.Subscript0Through9[(int)d][0])) : throw new System.ArgumentOutOfRangeException(nameof(number));
    /// <summary>Returns a string with the numeric subscript.</summary>
    public static string ToSubscriptString(int number, int radix)
      => radix >= 2 && radix <= 10 ? string.Concat(Maths.GetDigits(number, radix).Select(d => Text.Sequences.Subscript0Through9[d][0])) : throw new System.ArgumentOutOfRangeException(nameof(number));
    /// <summary>Returns a string with the numeric subscript.</summary>
    public static string ToSubscriptString(long number, int radix)
      => radix >= 2 && radix <= 10 ? string.Concat(Maths.GetDigits(number, radix).Select(d => Text.Sequences.Subscript0Through9[(int)d][0])) : throw new System.ArgumentOutOfRangeException(nameof(number));
  }
}
