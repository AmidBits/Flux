#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the sign of <paramref name="x"/>.</summary>
    public static TSelf NumberSign<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.IsNegative(x) ? -TSelf.One : TSelf.IsPositive(x) ? TSelf.One : TSelf.Zero;
  }
}
#endif
