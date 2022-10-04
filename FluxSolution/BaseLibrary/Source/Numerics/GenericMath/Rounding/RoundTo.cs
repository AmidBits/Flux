#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Rounds <paramref name="x"/> to the nearest boundaries. The <paramref name="mode"/> specifies how to round.</summary>
    public static TBound RoundTo<TSelf, TBound>(this TSelf x, TBound boundaryTowardsZero, TBound boundaryAwayFromZero, FullRounding mode)
      where TSelf : System.Numerics.INumber<TSelf>
      where TBound : System.Numerics.INumber<TBound>
    {
      return mode switch
      {
        FullRounding.AwayFromZero => boundaryAwayFromZero,
        FullRounding.Ceiling => x < TSelf.Zero ? boundaryTowardsZero : boundaryAwayFromZero,
        FullRounding.Floor => x < TSelf.Zero ? boundaryAwayFromZero : boundaryTowardsZero,
        FullRounding.TowardZero => boundaryTowardsZero,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }
  }
}
#endif
