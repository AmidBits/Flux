#if NET7_0_OR_GREATER
namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class ExtensionMethods
  {
    // The fold 'left' (or up towards MSB) function, is the opposite of (<see cref="FoldRight"/>), sets all bits from LS1B and 'up' (or 'left'), to 1.
    /// <summary>PREVIEW! Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>All bits set from LSB up, or -1 if the value is less than zero.</returns>
    public static TSelf BitFoldLeft<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var tzc = GetTrailingZeroCount(value);

      return BitFoldRight(value << GetLeadingZeroCount(value)) >> tzc << tzc;
    }

    // The fold 'right' (or down towards LSB) function, is the opposite (<see cref="FoldLeft"/>), sets all bits from the MS1B bit 'down' (or 'right'), to 1.
    /// <summary>PREVIEW! "Folds" the upper bits into the lower bits, by taking the most significant 1 bit (MS1B) and OR it with (MS1B - 1). The process yields a bit vector with the same most significant 1 as the value, but all 1's below it.</summary>
    /// <returns>All bits set from MSB down, or -1 if the value is less than zero.</returns>
    public static TSelf BitFoldRight<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value < TSelf.Zero ? -TSelf.One
      : value > TSelf.Zero ? (((MostSignificant1Bit(value) - TSelf.One) << 1) | TSelf.One)
      : TSelf.Zero;
  }
}
#endif
