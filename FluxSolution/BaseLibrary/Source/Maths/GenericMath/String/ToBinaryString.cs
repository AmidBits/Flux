namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    public static string ToBinaryString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToRadixString(value, 2, value.GetBitCount());

#else

    public static string ToBinaryString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base2.NumberToText(value, value.GetBitCount());
    public static string ToBinaryString(this int value) => Text.PositionalNotation.Base2.NumberToText(value, value.GetBitCount());
    public static string ToBinaryString(this long value) => Text.PositionalNotation.Base2.NumberToText(value, value.GetBitCount());

#endif
  }
}
