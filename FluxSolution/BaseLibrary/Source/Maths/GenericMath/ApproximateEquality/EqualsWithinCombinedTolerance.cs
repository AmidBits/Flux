namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Perform a comparison where a combined (absolute AND relative) tolerance.</summary>
    public static bool EqualsWithinCombinedTolerance<TValue, TRelativeTolerance>(this TValue a, TValue b, TValue absoluteTolerance, TRelativeTolerance relativeTolerance)
      where TValue : System.Numerics.INumber<TValue>
      where TRelativeTolerance : System.Numerics.IFloatingPoint<TRelativeTolerance>
      => EqualsWithinAbsoluteTolerance(a, b, absoluteTolerance) || EqualsWithinRelativeTolerance(a, b, relativeTolerance);
  }
}
