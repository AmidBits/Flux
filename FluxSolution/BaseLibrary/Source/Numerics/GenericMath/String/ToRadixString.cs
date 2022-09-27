#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Creates a string with the number converted using the specified radix (base).</summary>
    public static string ToRadixString<TSelf>(this TSelf number, byte radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GenericMath.ToString(number, Flux.Text.RuneSequences.Base62[..GenericMath.AssertRadix(radix)]).ToString();
  }
}
#endif
