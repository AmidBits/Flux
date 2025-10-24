namespace Flux
{
  public static partial class MachineEpsilon
  {
    /// <summary>Machine epsilon computed on the fly.</summary>
    public static TFloat Compute<TFloat>()
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      var epsilon = TFloat.One;

      while (epsilon / TFloat.CreateChecked(2) is var halfEpsilon && (TFloat.One + halfEpsilon) > TFloat.One)
        epsilon = halfEpsilon;

      return epsilon;
    }
  }
}
