#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static TBound RoundTo<TSelf, TBound>(this TSelf value, TBound boundTowardsZero, TBound boundAwayFromZero, FullRounding mode)
      where TSelf : System.Numerics.INumber<TSelf>
      where TBound : System.Numerics.INumber<TBound>
    {
      return mode switch
      {
        FullRounding.AwayFromZero => boundAwayFromZero,
        FullRounding.Ceiling => value < TSelf.Zero ? boundTowardsZero : boundAwayFromZero,
        FullRounding.Floor => value < TSelf.Zero ? boundAwayFromZero : boundTowardsZero,
        FullRounding.TowardZero => boundTowardsZero,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }
  }
}
#endif
